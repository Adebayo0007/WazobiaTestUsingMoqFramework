using AgroExpressAPI.Dtos;
using AgroExpressAPI.Dtos.Admin;
using AgroExpressAPI.Dtos.Buyer;
using AgroExpressAPI.Dtos.User;
using AgroExpressAPI.Entities;
using AgroExpressAPI.Repositories.Interfaces;
using AgroExpressAPI.Services.Implementations;
using AgroExpressAPI.Services.Interfaces;
//using Google.Protobuf.WellKnownTypes;
using Moq;

namespace AgroExpressTests.ServicesTests
{
    [TestFixture]
    internal class AdminServiceTests
    {
        private Mock<IAdminRepository> _iAdminRepository { get; set; }
        private Mock<IUserRepository> _iUserRepository { get; set; }
        private Mock<IUserService> _iUserService { get; set; }
        private IAdminService _iAdminService { get; set; }
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

            _iAdminRepository = new Mock<IAdminRepository>();
            _iUserRepository = new Mock<IUserRepository>();
            _iUserService = new Mock<IUserService>();
            _iAdminService = new AdminService(_iAdminRepository.Object, _iUserService.Object, _iUserRepository.Object);
        }
        [TearDown]
        public void TearDown()
        {
            
           /* _iAdminRepository = new Mock<IAdminRepository>();
            _iUserRepository = new Mock<IUserRepository>();
            _iUserService = new Mock<IUserService>();
            _iAdminService = new AdminService(_iAdminRepository.Object, _iUserService.Object, _iUserRepository.Object);*/
        }

        [Test]
        public  void DeleteAsync_WhenIsActiveIsTrue_ChangeIsActiveToFalse()
        {
            //Arrange
           _iUserRepository.Setup(u => u.GetByIdAsync(_admin.UserId)).Returns(_user);
            //Act
            _iAdminService.DeleteAsync(_admin.UserId);
            //Assert
            _iUserRepository.Verify(u => u.Delete(_user));
            Assert.IsFalse(_user.IsActive);
        }

        [Test]
        public async Task DeleteAsync_WhenIsActiveIsFalse_ChangeIsActiveToTrue()
        {
            //Arrange 
            _user.IsActive = false;
            _iUserRepository.Setup(u => u.GetByIdAsync(_admin.UserId)).Returns(_user);
            //Act
            await _iAdminService.DeleteAsync(_admin.UserId);
            //Assert
            _iUserRepository.Verify(u => u.Delete(_user));
            Assert.IsTrue(_user.IsActive);
        }

        [Test]
        public async Task GetAllAsync_WhenCalled_ReturnsAllAdmins()
        {  
            //Act
            var result =  await _iAdminService.GetAllAsync();
            //Assert
            Assert.IsNotNull(result);
            Assert.GreaterOrEqual(result.Data.Count(), 0);
        }
        [Test]
        public async Task GetByEmailAsync_WhenInputIsNull_ReturnBaseResponseHavingMessageOfInvalidInput()
        {
            //Arrange 
            _iAdminRepository.Setup(u => u.GetByEmailAsync(null)).Returns(_admin);
            //Act
            var result = await _iAdminService.GetByEmailAsync(null);
            //Assert
            Assert.AreEqual(result.Message, "Invalid Input");
            Assert.AreEqual(result.IsSuccess, false);
            Assert.AreEqual(result.Data, null);

        }

        [Test]
        public async Task GetByEmailAsync_WhenInputIsEmptyString_ReturnBaseResponseHavingMessageOfInvalidInput()
        {
            //Arrange 
            _iAdminRepository.Setup(u => u.GetByEmailAsync("")).Returns(_admin);
            //Act
            var result = await _iAdminService.GetByEmailAsync("");
            //Assert
            Assert.AreEqual(result.Message, "Invalid Input");
            Assert.AreEqual(result.IsSuccess, false);
            Assert.AreEqual(result.Data, null);

        }
        [Test]
        public async Task GetByEmailAsync_WhenInputIsWhiteSpace_ReturnBaseResponseHavingMessageOfInvalidInput()
        {
            //Arrange 
            _iAdminRepository.Setup(u => u.GetByEmailAsync(" ")).Returns(_admin);
            //Act
            var result = await _iAdminService.GetByEmailAsync(" ");
            //Assert
            Assert.AreEqual(result.Message, "Invalid Input");
            Assert.AreEqual(result.IsSuccess, false);
            Assert.AreEqual(result.Data, null);

        }

        [Test]
        public async Task GetByEmailAsync_WhenAdminDoesNotExist_ReturnBaseResponseHavingMessageOfAdminNotFound()
        {
            //Arrange 
            _iAdminRepository.Setup(u => u.GetByEmailAsync("charles@gmail.com")).Returns(_admin);
            //Act
            var result = await _iAdminService.GetByEmailAsync("charles123@gmail.com");
            //Assert
            Assert.AreEqual(result.Message, "Admin not Found 🙄");
            Assert.AreEqual(result.IsSuccess, false);
            Assert.AreEqual(result.Data, null);

        }
        [Test]
        public async Task GetByEmailAsync_WhenAdminExist_ReturnBaseResponseOfAdminDto()
        {
            //Arrange 
            _iAdminRepository.Setup(u => u.GetByEmailAsync("charles123@gmail.com")).Returns(_admin);
            _baseResponseOfAdminDto.Data = _adminDto;
            //Act
            var result = await _iAdminService.GetByEmailAsync("charles123@gmail.com");
            //Assert
            Assert.AreEqual(result.Message, "Admin Found successfully 😎");
            Assert.AreEqual(result.IsSuccess, true);
            Assert.That(result.Data, Is.InstanceOf<AdminDto>());
            Assert.That(result, Is.InstanceOf<BaseResponse<AdminDto>>());

        }
        [Test]
        public async Task GetByIdAsync_WhenInputIsNull_ReturnBaseResponseHavingMessageOfInvalidInput()
        {
            //Arrange 
            _iAdminRepository.Setup(u => u.GetByIdAsync(null)).Returns(_admin);
            //Act
            var result = await _iAdminService.GetByIdAsync(null);
            //Assert
            Assert.AreEqual(result.Message, "Invalid Input");
            Assert.AreEqual(result.IsSuccess, false);
            Assert.AreEqual(result.Data, null);

        }

        [Test]
        public async Task GetByIdAsync_WhenInputIsEmptyString_ReturnBaseResponseHavingMessageOfInvalidInput()
        {
            //Arrange 
            _iAdminRepository.Setup(u => u.GetByIdAsync(" ")).Returns(_admin);
            //Act
            var result = await _iAdminService.GetByIdAsync(" ");
            //Assert
            Assert.AreEqual(result.Message, "Invalid Input");
            Assert.AreEqual(result.IsSuccess, false);
            Assert.AreEqual(result.Data, null);

        }
        [Test]
        public async Task GetByIdAsync_WhenInputIsWhiteSpace_ReturnBaseResponseHavingMessageOfInvalidInput()
        {
            //Arrange 
            _iAdminRepository.Setup(u => u.GetByIdAsync(" ")).Returns(_admin);
            //Act
            var result = await _iAdminService.GetByIdAsync(" ");
            //Assert
            Assert.AreEqual(result.Message, "Invalid Input");
            Assert.AreEqual(result.IsSuccess, false);
            Assert.AreEqual(result.Data, null);

        }
        [Test]
        public async Task GetByIdAsync_WhenAdminExist_ReturnBaseResponseHavingSuccessfulMessage()
        {
            //Arrange 
            var id = "d2672100-f5bb-4594-ae53-4542a733fb78";
            _iAdminRepository.Setup(u => u.GetByIdAsync(id)).Returns(_admin);
            //Act
            var result = await _iAdminService.GetByIdAsync(id);
            //Assert
            _iAdminRepository.Verify(u => u.GetByIdAsync(id));
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(result.Message, "Admin Found successfully 😎");
            Assert.That(result.Data, Is.InstanceOf<AdminDto>());
            Assert.That(result, Is.InstanceOf<BaseResponse<AdminDto>>());
        }

        [Test]
        public async Task GetByIdAsync_WhenAdminDoesNotExist_ConfirmingItIsTheSameIdGoingToTheContextClass()
        {
            //Arrange 
            var id = "d2672100-f5bb-4594-ae53-4542a733fb12";
            _iAdminRepository.Setup(u => u.GetByIdAsync(id)).Returns(_admin);
            //Act
            var result = await _iAdminService.GetByIdAsync(id);
            //Assert
            _iAdminRepository.Verify(u => u.GetByIdAsync(id));

        }
        [Test]
        public async Task UpdateAsync_WhenModelAndIdIsNull_ReturningMessageOfInvalidInput()
        {
            //Arrange
            var updateAdminRequestModel = new UpdateAdminRequestModel();
            string id = null;
            updateAdminRequestModel = null;
            _iUserRepository.Setup(u => u.Update(_user)).Returns(_user);
            //Act
            var result = await _iAdminService.UpdateAsync(updateAdminRequestModel, id);
            //Assert
            Assert.That(result, Is.InstanceOf<BaseResponse<AdminDto>>());
            Assert.AreEqual(result.IsSuccess, false);
            Assert.AreEqual(result.Message, "Invalid Input");
            Assert.AreEqual(result.Data, null);
        }
        [Test]
        public async Task UpdateAsync_WhenIdIsEmptyString_ReturningMessageOfInvalidInput()
        {
            //Arrange
            var updateAdminRequestModel = new UpdateAdminRequestModel();
            string id = "";
            _iUserRepository.Setup(u => u.Update(_user)).Returns(_user);
            //Act
            var result = await _iAdminService.UpdateAsync(updateAdminRequestModel, id);
            //Assert
            Assert.That(result, Is.InstanceOf<BaseResponse<AdminDto>>());
            Assert.AreEqual(result.IsSuccess, false);
            Assert.AreEqual(result.Message, "Invalid Input");
            Assert.AreEqual(result.Data, null);
        }
        [Test]
        public async Task UpdateAsync_WhenIdIsWhiteSpace_ReturningMessageOfInvalidInput()
        {
            //Arrange
            var updateAdminRequestModel = new UpdateAdminRequestModel();
            string id = " ";
            _iUserRepository.Setup(u => u.Update(_user)).Returns(_user);
            //Act
            var result = await _iAdminService.UpdateAsync(updateAdminRequestModel, id);
            //Assert
            Assert.That(result, Is.InstanceOf<BaseResponse<AdminDto>>());
            Assert.AreEqual(result.IsSuccess, false);
            Assert.AreEqual(result.Message, "Invalid Input");
            Assert.AreEqual(result.Data, null);
        }


    }
}
