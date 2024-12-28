using Application.UseCase;
using Domain.Domain.Interfaces.IRepositorySqlServer;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Application.Tests
{
    [TestFixture]
    public class CreateTests
    {
        private Mock<IRepository> _productRepositoryMock;
        private Mock<ILogger<Create>> _loggerMock;
        private Create _createCommand;

        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IRepository>();
            _loggerMock = new Mock<ILogger<Create>>();
            _createCommand = new Create(_productRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task CreateAsync_ProductCreatedSuccessfully_ReturnsSuccessResult()
        {
            // Arrange
            var productDto = new ProductDto { Name = "NewProduct", Price = 100 };

            var product = Product.Create(productDto.Name, productDto.Price);

            _productRepositoryMock.Setup(repo => repo.Add(It.IsAny<Product>()))
                .Callback<Product>(p =>
                {
                    var assignedId = 1;
                    p.GetType().GetProperty("Id")?.SetValue(p, assignedId);
                })
                .Returns(Task.CompletedTask);

            // Act
            var result = await _createCommand.CreateAsync(productDto);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Producto creado exitosamente."));
            Assert.That(result.Data.Id, Is.EqualTo(1));
        }
    }
}
