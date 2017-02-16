using System;
using System.Collections.Specialized;
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

        internal static ComplextOut_Dto Map(ComplexIn_Dto inDto)
        {
            var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<ComplexIn_Dto, ComplextOut_Dto>()
                            .ForMember(dest=>dest.FirstName, opts=>opts.MapFrom(src=>src.A))
                            .ForMember(dest => dest.NumberOfFish, opts => opts.MapFrom(src => src.B))
                            .ForMember(dest => dest.BarrelSize, opts => opts.MapFrom(src => src.C))
                            .ForMember(dest => dest.AreMicePresent, opts => opts.MapFrom(src => src.D))
                            .ForMember(dest => dest.MiceArePresent, opts => opts.MapFrom(src => src.E))
                            .ForMember(dest => dest.FieldToBeIgnored, opts => opts.Ignore())
                            .ForMember(dest => dest.DoubleOutput, opts => opts.MapFrom(src => src.G))
                            ;
                });

            var mapper = config.CreateMapper();

            ComplextOut_Dto dto = mapper.Map<ComplextOut_Dto>(inDto);

            return dto;
        }
    }
}