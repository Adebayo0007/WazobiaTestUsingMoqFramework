using AgroExpressAPI.Entities;
using AgroExpressAPI.Repositories.Interfaces;
using AgroExpressAPI.Services.Implementations;
using AgroExpressAPI.Services.Interfaces;
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
        private Admin _admin { get; set; }
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
                Id = Guid.NewGuid().ToString(),
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
                Id = Guid.NewGuid().ToString().Substring(0, 36),
                UserId = _user.Id,
                User = _user
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
        public void DeleteAsync_WhenIsActiveIsFalse_ChangeIsActiveToTrue()
        {
            //Arrange 
            _user.IsActive = false;
            _iUserRepository.Setup(u => u.GetByIdAsync(_admin.UserId)).Returns(_user);
            //Act
            _iAdminService.DeleteAsync(_admin.UserId);
            //Assert
            _iUserRepository.Verify(u => u.Delete(_user));
            Assert.IsTrue(_user.IsActive);
        }
    }
}
