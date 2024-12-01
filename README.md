# Softuni WebProject : Gamers pub

Welcome to the PCBuilder - ASP.NET web application 

## Features 

- **User-Friendly Interface:** An intuitive and user-friendly design ensures seamless navigation and functionality for all users.

- **Browsing and Ordering parts and computers:**Users can explore a rich catalog of PC components, including CPUs, GPUs, RAM, and more. The app also features pre-configured computer builds for those looking for convenience.

- **Responsive Design:**

- **Shopping Cart and Checkout:** A streamlined shopping cart and checkout process allow users to save items, review their selections, and place orders effortlessly.

## Purpose

The Gamers Pub serves as a learning resource for aspiring developers interested in ASP.NET Core development.
By examining its structure and features, developers can gain practical insights into building dynamic, scalable, and feature-rich web applications.

## How to Start the Project

1. Clone the Repository Or download form the <Code> button and unzip the file

2. Open the Solution

- Open PCBuilder.Web/Program.cs 
- Open application.json and change the "DefaultConnection" string
- Connection string: Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;
- Do the same for PCBuilder.WebApi

3. Update the Database

- Use the Package Manager Console and enter the command "Update-Database"

4. Run the application

- Use "dotnet run" in Bash or PowerShell
- Or use Ctrl + F5
- !!Optional!! You can set the start up project to both PCBuilder.Web and PCBuilder.WebApi if you want  to use Swagger

**Admin Profile:**
- Email: admin@gmail.com
- Password: admin

**Common User Profile:**
- Email: john.doe@example.com
- Password: Password123!
  
## Notes

Feel free to test, expand and change the app.

The project is developed and maintained by Borislav Sapundjiev. 