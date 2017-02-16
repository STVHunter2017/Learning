using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoMapperTests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestWithoutAutoMapper()
        {
            var inDto = CreateTest();
            var output = Mapper.Map (inDto, false);

            Assert.AreEqual(inDto.A, output.FirstName);
            Assert.AreEqual(inDto.B, output.NumberOfFish);
            Assert.AreEqual(inDto.C, output.BarrelSize);
            Assert.AreEqual(inDto.D, output.AreMicePresent);
            Assert.AreEqual(inDto.E, output.MiceArePresent);
            Assert.AreEqual(null, output.FieldToBeIgnored);
            Assert.AreEqual(inDto.G * 2, output.DoubleOutput);
        }

        private static ComplexIn_Dto CreateTest()
        {
            return new ComplexIn_Dto()
            {
                A = "Steve",
                B = 30,
                C = (float)100,
                D = true,
                E = true,
                G= 20
            };
        }

        [TestMethod]
        public void TestWithAutoMapper()
        {
            var inDto = CreateTest();
            var output = Mapper.Map(inDto, true);

            Assert.AreEqual(inDto.A, output.FirstName);
            Assert.AreEqual(inDto.B, output.NumberOfFish);
            Assert.AreEqual(inDto.C, output.BarrelSize);
            Assert.AreEqual(inDto.D, output.AreMicePresent);
            Assert.AreEqual(inDto.E, output.AreMicePresent);
            Assert.AreEqual(null, output.FieldToBeIgnored);
            Assert.AreEqual(inDto.G, output.DoubleOutput);
        }
    }
}
