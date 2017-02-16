using System;

namespace AutoMapperTests
{
    internal class Brute
    {
        public static Out_Dto Map(In_Dto inDto)
        {
            return  new Out_Dto()
            {
                FirstName = inDto.FirstName,
                NumberOfFish = inDto.NumberOfFish,
                BarrelSize = inDto.BarrelSize,
                AreMicePresent = inDto.MiceArePresnt,
                MiceArePresent = inDto.MiceArePresnt

            };
        }

        internal static ComplextOut_Dto Map(ComplexIn_Dto inDto)
        {
            return new ComplextOut_Dto()
            {
                FirstName = inDto.A,
                NumberOfFish = inDto.B,
                BarrelSize = inDto.C,
                AreMicePresent = inDto.D,
                MiceArePresent = inDto.E,
                FieldToBeIgnored = null,
                DoubleOutput = inDto.G *2
            };
        }
    }
}