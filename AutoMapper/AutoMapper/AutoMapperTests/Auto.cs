using System;
using AutoMapper;

namespace AutoMapperTests
{
    internal class Auto
    {
        public static Out_Dto Map(In_Dto inDto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<In_Dto, Out_Dto>());
            //var mapper = new Mapper( );
            var mapper = config.CreateMapper();

            Out_Dto dto = mapper.Map<Out_Dto>(inDto);

            return dto;
        }
    }
}