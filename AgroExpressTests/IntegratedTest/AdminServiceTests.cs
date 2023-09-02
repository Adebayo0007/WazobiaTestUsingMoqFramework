/*using AgroExpressAPI.Entities;
using sib_api_v3_sdk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AgroExpressTests.IntegratedTest
{
    public class AdminServiceTests : BaseIntegrationTest
    {
        public AdminServiceTests(IntegrationTestWebAppFactory factory)
        : base(factory)
        {
        }
        [Fact]
        public async Task Create_ShouldCreateAdmin()
        {
            // Arrange
            var address = new Address
            {
                Id = Guid.NewGuid().ToString(),
                FullAddress = "Command,Ipaja,Lagos",
                LocalGovernment = "Alimosho",
                State = "Lagos"
            };
            var user = new User()
            {
                Id = "d2672100-f5bb-4594-ae53-4542a733fb76",
                UserName = "Charles",
                ProfilePicture = "profilePix.png",
                Name = "Charles Wilson",
                PhoneNumber = "09087986576",
                AddressId = address.Id,
                Address = address,
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
            var command = new Admin
            {
                Id = "d2672100-f5bb-4594-ae53-4542a733fb78",
                UserId = user.Id,
                User =user
            };
           

            // Act
            var adminId = await Sender.Send(command);

            // Assert
            var product = DbContext.Admins.FirstOrDefault(a => a.Id == adminId);

            Assert.NotNull(product);
         
        }

        [Fact]
        public async Task Get_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var productId = await CreateProduct();
            var query = new GetProduct.Query { Id = productId };

            // Act
            var productResponse = await Sender.Send(query);

            // Assert
            Assert.NotNull(productResponse);
        }

        [Fact]
        public async Task Get_ShouldThrow_WhenProductIsNull()
        {
            // Arrange
            var query = new GetProduct.Query { Id = Guid.NewGuid() };

            // Act
            Task Action() => Sender.Send(query);

            // Assert
            await Assert.ThrowsAsync<ApplicationException>(Action);
        }

        [Fact]
        public async Task Update_ShouldUpdateProduct_WhenProductExists()
        {
            // Arrange
            var productId = await CreateProduct();
            var command = new UpdateProduct.Command
            {
                Id = productId,
                Name = "Test",
                Category = "Test category",
                Price = 100.0m
            };

            // Act
            await Sender.Send(command);

            // Assert
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenProductIsNull()
        {
            // Arrange
            var command = new UpdateProduct.Command
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Category = "Test category",
                Price = 100.0m
            };

            // Act
            Task Action() => Sender.Send(command);

            // Assert
            await Assert.ThrowsAsync<ApplicationException>(Action);
        }

        [Fact]
        public async Task Delete_ShouldDeleteProduct_WhenProductExists()
        {
            // Arrange
            var productId = await CreateProduct();
            var command = new DeleteProduct.Command { Id = productId };

            // Act
            await Sender.Send(command);

            // Assert
            var product = DbContext.Products.FirstOrDefault(p => p.Id == productId);

            Assert.Null(product);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenProductIsNull()
        {
            // Arrange
            var command = new DeleteProduct.Command { Id = Guid.NewGuid() };

            // Act
            Task Action() => Sender.Send(command);

            // Assert
            await Assert.ThrowsAsync<ApplicationException>(Action);
        }

        private async Task<Guid> CreateProduct()
        {
            var command = new CreateProduct.Command
            {
                Name = "Test",
                Category = "Test category",
                Price = 100.0m
            };

            var productId = await Sender.Send(command);

            return productId;
        }
    }
}
*/