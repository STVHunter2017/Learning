using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoMapperTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestWithoutAutoMapper()
        {
            var inDto = CreateTest();
            var output = Mapper.Map (inDto, false);

            Assert.AreEqual(inDto.FirstName, output.FirstName);
            Assert.AreEqual(inDto.NumberOfFish, output.NumberOfFish);
            Assert.AreEqual(inDto.BarrelSize, output.BarrelSize);
            Assert.AreEqual(inDto.AreMicePresent, output.AreMicePresent);
            Assert.AreEqual(inDto.MiceArePresnt, output.MiceArePresent);
        }

        private static In_Dto CreateTest()
        {
            return new In_Dto()
            {
                FirstName = "Steve",
                NumberOfFish = 30,
                BarrelSize = (float)100,
                AreMicePresent = true,
                MiceArePresnt = true
            };
        }

        [TestMethod]
        public void TestWithAutoMapper()
        {
            var inDto = CreateTest();
            var output = Mapper.Map(inDto, true);

            Assert.AreEqual(inDto.FirstName, output.FirstName);
            Assert.AreEqual(inDto.NumberOfFish, output.NumberOfFish);
            Assert.AreEqual(inDto.BarrelSize, output.BarrelSize);
            Assert.AreEqual(inDto.AreMicePresent, output.AreMicePresent);
        }
    }
}
