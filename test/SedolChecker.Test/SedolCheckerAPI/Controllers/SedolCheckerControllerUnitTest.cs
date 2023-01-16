
using Moq;
using SedolChecker.Core;
using SedolChecker.Core.Interfaces;
using SedolCheckerAPI.Controllers;
using Xunit;

namespace SedolChecker.Test.SedolCheckerAPI.Controllers
{
    public class SedolCheckerControllerUnitTest
    {
        private readonly Mock<ISedolValidator> _mockSedolValidator;
        private readonly SedolCheckerController _sut;
        public SedolCheckerControllerUnitTest()
        {
            _mockSedolValidator= new Mock<ISedolValidator>();
            _sut=new SedolCheckerController(_mockSedolValidator.Object);
        }

        [Fact]
        public void Verify_CheckSedol_InputStringLength()
        {
            var mockInput = "123456789";
            var mockResponse = new SedolValidationResult(mockInput, false, false, "Input string was not 7-characters long");
            _mockSedolValidator.Setup(x => x.ValidateSedol(mockInput)).Returns(mockResponse);
            var actualResponse=_sut.CheckSedol(mockInput);
            _mockSedolValidator.Verify(x=>x.ValidateSedol(mockInput),Times.Once);
            Assert.Equal(mockResponse, ((Microsoft.AspNetCore.Mvc.ObjectResult)actualResponse).Value);
        }

        [Fact]
        public void Verify_CheckSedol_Invalid_Checksum_Non_User_Defined_SEDOL()
        {
            var mockInput = "1234567";
            var mockResponse = new SedolValidationResult(mockInput, false, false, "Checksum digit does not agree with the rest of the input");
            _mockSedolValidator.Setup(x => x.ValidateSedol(mockInput)).Returns(mockResponse);
            var actualResponse = _sut.CheckSedol(mockInput);
            _mockSedolValidator.Verify(x => x.ValidateSedol(mockInput), Times.Once);
            Assert.Equal(mockResponse, ((Microsoft.AspNetCore.Mvc.ObjectResult)actualResponse).Value);
        }

        [Fact]
        public void Verify_CheckSedol_Valid_Checksum_Non_User_Defined_SEDOL()
        {
            var mockInput = "B0YBKJ7";
            var mockResponse = new SedolValidationResult(mockInput, true, false, "");
            _mockSedolValidator.Setup(x => x.ValidateSedol(mockInput)).Returns(mockResponse);
            var actualResponse = _sut.CheckSedol(mockInput);
            _mockSedolValidator.Verify(x => x.ValidateSedol(mockInput), Times.Once);
            Assert.Equal(mockResponse, ((Microsoft.AspNetCore.Mvc.ObjectResult)actualResponse).Value);
        }

        [Fact]
        public void Verify_CheckSedol_Invalid_Checksum_User_Defined_SEDOL()
        {
            var mockInput = "9123451";
            var mockResponse = new SedolValidationResult(mockInput, false, true, "Checksum digit does not agree with the rest of the input");
            _mockSedolValidator.Setup(x => x.ValidateSedol(mockInput)).Returns(mockResponse);
            var actualResponse = _sut.CheckSedol(mockInput);
            _mockSedolValidator.Verify(x => x.ValidateSedol(mockInput), Times.Once);
            Assert.Equal(mockResponse, ((Microsoft.AspNetCore.Mvc.ObjectResult)actualResponse).Value);
        }

        [Fact]
        public void Verify_CheckSedol_Invalid_Characters_Found()
        {
            var mockInput = "9123_51";
            var mockResponse = new SedolValidationResult(mockInput, false, false, "SEDOL contains invalid characters");
            _mockSedolValidator.Setup(x => x.ValidateSedol(mockInput)).Returns(mockResponse);
            var actualResponse = _sut.CheckSedol(mockInput);
            _mockSedolValidator.Verify(x => x.ValidateSedol(mockInput), Times.Once);
            Assert.Equal(mockResponse, ((Microsoft.AspNetCore.Mvc.ObjectResult)actualResponse).Value);
        }

        [Fact]
        public void Verify_CheckSedol_Valid_User_Defined_SEDOL()
        {
            var mockInput = "9123458";
            var mockResponse = new SedolValidationResult(mockInput, true, true, "");
            _mockSedolValidator.Setup(x => x.ValidateSedol(mockInput)).Returns(mockResponse);
            var actualResponse = _sut.CheckSedol(mockInput);
            _mockSedolValidator.Verify(x => x.ValidateSedol(mockInput), Times.Once);
            Assert.Equal(mockResponse, ((Microsoft.AspNetCore.Mvc.ObjectResult)actualResponse).Value);
        }
    }
}
