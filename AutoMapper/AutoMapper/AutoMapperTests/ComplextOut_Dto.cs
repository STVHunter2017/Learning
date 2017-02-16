namespace AutoMapperTests
{
    internal class ComplextOut_Dto
    {
        public string FirstName { get; set; }
        public int NumberOfFish { get; set; }
        public float BarrelSize { get; set; }
        public bool AreMicePresent { get; set; }
        public bool MiceArePresent { get; set; }
        public object FieldToBeIgnored { get; set; }
        public int DoubleOutput { get; set; }
    }
}