namespace AutoMapperTests
{
    public class Mapper
    {

        internal static Out_Dto Map(In_Dto inDto, bool useAutoMap)
        {
            if (!useAutoMap)
            {
                return Brute.Map(inDto);
            }
            else
            {
                return Auto.Map(inDto);
            }
        }

        internal static ComplextOut_Dto Map(ComplexIn_Dto inDto, bool useAutoMap)
        {
            if (!useAutoMap)
            {
                return Brute.Map(inDto);
            }
            else
            {
                return Auto.Map(inDto);
            }
        }
    }
}