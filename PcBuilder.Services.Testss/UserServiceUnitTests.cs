using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Identity;
using PcBuilder.Data.Models;
using PcBuilder.Services.Data;
using PcBuilder.Web.ViewModels.User;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

[TestFixture]
public class UserServiceUnitTests
{
    private Mock<UserManager<ApplicationUser>> _userManagerMock;
    private Mock<SignInManager<ApplicationUser>> _signInManagerMock;
    private Mock<RoleManager<IdentityRole<Guid>>> _roleManagerMock;
    private UserService _userService;

    [SetUp]
    public void SetUp()
    {
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

        _signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
            _userManagerMock.Object,
            Mock.Of<IHttpContextAccessor>(),
            Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
            null, null, null, null);

        _roleManagerMock = new Mock<RoleManager<IdentityRole<Guid>>>(
            Mock.Of<IRoleStore<IdentityRole<Guid>>>(), null, null, null, null);

        _userService = new UserService(_userManagerMock.Object, _signInManagerMock.Object, _roleManagerMock.Object);
    }

    [Test]
    public async Task RegisterUserAsync_ShouldCreateUserAndAssignRole()
    {
        var registerModel = new RegisterFormModel
        {
            Email = "testuser@example.com",
            Password = "Test@1234",
            FirstName = "Test",
            LastName = "User"
        };

        var identityResult = IdentityResult.Success;

        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), registerModel.Password))
            .ReturnsAsync(identityResult);

        _roleManagerMock.Setup(x => x.RoleExistsAsync("USER"))
            .ReturnsAsync(false);

        _roleManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityRole<Guid>>()))
            .ReturnsAsync(IdentityResult.Success);

        _userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), "USER"))
            .ReturnsAsync(IdentityResult.Success);

        var result = await _userService.RegisterUserAsync(registerModel);

        Assert.IsTrue(result.Succeeded);
        _userManagerMock.Verify(x => x.CreateAsync(It.IsAny<ApplicationUser>(), registerModel.Password), Times.Once);
        _roleManagerMock.Verify(x => x.RoleExistsAsync("USER"), Times.Once);
        _roleManagerMock.Verify(x => x.CreateAsync(It.IsAny<IdentityRole<Guid>>()), Times.Once);
        _userManagerMock.Verify(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), "USER"), Times.Once);
    }

    [Test]
    public async Task RegisterUserAsync_ShouldFailWhenUserCreationFails()
    {
        var registerModel = new RegisterFormModel
        {
            Email = "testuser@example.com",
            Password = "Test@1234",
            FirstName = "Test",
            LastName = "User"
        };

        var identityResult = IdentityResult.Failed(new IdentityError { Description = "Error creating user" });

        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), registerModel.Password))
            .ReturnsAsync(identityResult);

        var result = await _userService.RegisterUserAsync(registerModel);

        Assert.IsFalse(result.Succeeded);
        _userManagerMock.Verify(x => x.CreateAsync(It.IsAny<ApplicationUser>(), registerModel.Password), Times.Once);
        _roleManagerMock.Verify(x => x.RoleExistsAsync("USER"), Times.Never);
    }

    [Test]
    public async Task LoginUserAsync_ShouldReturnTrueWhenLoginSucceeds()
    {
        var loginModel = new LoginFormModel
        {
            Email = "testuser@example.com",
            Password = "Test@1234",
            RememberMe = true
        };

        var signInResult = SignInResult.Success;

        _signInManagerMock.Setup(x => x.PasswordSignInAsync(
            loginModel.Email, loginModel.Password, loginModel.RememberMe, false))
            .ReturnsAsync(signInResult);

        var result = await _userService.LoginUserAsync(loginModel);

        Assert.IsTrue(result);
        _signInManagerMock.Verify(x => x.PasswordSignInAsync(
            loginModel.Email, loginModel.Password, loginModel.RememberMe, false), Times.Once);
    }

    [Test]
    public async Task LoginUserAsync_ShouldReturnFalseWhenLoginFails()
    {
    
        var loginModel = new LoginFormModel
        {
            Email = "testuser@example.com",
            Password = "WrongPassword",
            RememberMe = false
        };

        var signInResult = SignInResult.Failed;

        _signInManagerMock.Setup(x => x.PasswordSignInAsync(
            loginModel.Email, loginModel.Password, loginModel.RememberMe, false))
            .ReturnsAsync(signInResult);

        var result = await _userService.LoginUserAsync(loginModel);

        Assert.IsFalse(result);
        _signInManagerMock.Verify(x => x.PasswordSignInAsync(
            loginModel.Email, loginModel.Password, loginModel.RememberMe, false), Times.Once);
    }

    [Test]
    public async Task LogoutUserAsync_ShouldSignOutUser()
    {
        await _userService.LogoutUserAsync();
        _signInManagerMock.Verify(x => x.SignOutAsync(), Times.Once);
    }
}
