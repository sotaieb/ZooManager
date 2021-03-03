using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using NUnit.Framework;

namespace St.Zoo.Core.Tests
{
    public class FileServiceTests
    {   
        [Test]
        public static void EnsureConstructorArgumentsNotNull()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var assertion = new GuardClauseAssertion(fixture);

            // Act, Assert
            assertion.Verify(typeof(FileService).GetConstructors());
        }

        [Test]
        public void ReadLines_when_Null_Path_Throws_Exception()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var assertion = new GuardClauseAssertion(fixture);

            // Act, Assert
            assertion.Verify(typeof(FileService).GetMethod(nameof(FileService.ReadLines)));
        }
    }
}