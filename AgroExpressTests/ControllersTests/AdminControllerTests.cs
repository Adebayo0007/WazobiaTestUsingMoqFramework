using AgroExpressAPI.Controllers;
using AgroExpressAPI.Dtos;
using AgroExpressAPI.Dtos.Admin;
using AgroExpressAPI.Entities;
using AgroExpressAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AgroExpressTests.ControllersTests
{
   [TestFixture]
    internal class AdminControllerTests
    {
        private Mock<IAdminService> _iAdminService { get; set; }
        private AdminController _iAdminController { get; set; }
        private User _user { get; set; }
        private Address _address { get; set; }
        private AdminDto _adminDto { get; set; }
        private Admin _admin { get; set; }
        private BaseResponse<AdminDto> _baseResponseOfAdminDto { get; set; }
        [SetUp]
        public void SetUp()
        {
            _address = new Address
            {
                Id = Guid.NewGuid().ToString(),
                FullAddress = "Command,Ipaja,Lagos",
                LocalGovernment = "Alimosho",
                State = "Lagos"
            };
            _user = new User()
            {
                Id = "d2672100-f5bb-4594-ae53-4542a733fb76",
                UserName = "Charles",
                ProfilePicture = "profilePix.png",
                Name = "Charles Wilson",
                PhoneNumber = "09087986576",
                AddressId = _address.Id,
                Address = _address,
                Gender = "Male",
                Email = "charles123@gmail.com",
                Password = "Charles123",
                Role = "Admin",
                IsActive = true,
                IsRegistered = true,
                Haspaid = true,
                Due = true,
                DateCreated = DateTime.Now,
                DateModified = null,

            };
            _admin = new Admin
            {
                Id = "d2672100-f5bb-4594-ae53-4542a733fb78",
                UserId = _user.Id,
                User = _user
            };
            _adminDto = new AdminDto
            {
                UserName = _user.UserName,
                Name = _user.Name,
                PhoneNumber = _user.PhoneNumber,
                FullAddress = _user.Address.FullAddress,
                LocalGovernment = _user.Address.LocalGovernment,
                State = _user.Address.State,
                Gender = _user.Gender,
                Email = _user.Email,
                Password = _user.Password,
                Role = _user.Role,
                IsActive = _user.IsActive,
                DateCreated = _user.DateCreated,
                DateModified = _user.DateModified
            };
            _baseResponseOfAdminDto = new BaseResponse<AdminDto>
            {
                Message = null,
                IsSuccess = false,
                Data = _adminDto
            };
            _iAdminService = new Mock<IAdminService>();
            _iAdminController = new AdminController(_iAdminService.Object);
        }
        [Test]
        [Ignore("This is tested independently but ignoring it because it has been handled with some Claims !")]
        public async Task AdminProfile_WhenInputIsNull_ReturnBadResponse()
        {
            //Arrange
            var result = await _iAdminController.AdminProfile(null);
            //Assert
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        [Ignore("This is tested independently but ignoring it because it has been handled with some Claims !")]
        public async Task AdminProfile_WhenInputIsWhiteSpace_ReturnBadResponse()
        {
            //Arrange
            var result = await _iAdminController.AdminProfile(" ");
            //Assert
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }
    }
}
