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
    }
}