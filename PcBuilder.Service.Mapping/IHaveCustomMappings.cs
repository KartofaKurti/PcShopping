using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcBuilder.Service.Mapping
{
	using AutoMapper;

	public interface IHaveCustomMappings
	{
		void CreateMappings(IProfileExpression configuration);
	}
}
