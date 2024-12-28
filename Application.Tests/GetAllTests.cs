using Application.UseCase;
using Domain.Domain.Interfaces.IRepositorySqlServer;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Application.Tests
{
    [TestFixture]
    public class GetAllTests
    {
        private Mock<IRepository> _productRepositoryMock;
        private Mock<ILogger<GetAll>> _loggerMock;
        private GetAll _getAllService;

        [SetUp]
        public void SetUp()
        {
            _productRepositoryMock = new Mock<IRepository>();
            _loggerMock = new Mock<ILogger<GetAll>>();
            _getAllService = new GetAll(_productRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task GetAllAsync_ProductsExist_ReturnsSuccessResult()
        {
            // Arrange
            var products = new List<Product>
            {
                Product.Create("Product 1", 10),
                Product.Create("Product 2", 20)
            };
            _productRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(products);

            // Act
            var result = await _getAllService.GetAllAsync();

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Productos encontrados."));
            Assert.That(result.Data, Is.EquivalentTo(products));
        }

        [Test]
        public async Task GetAllAsync_ExceptionThrown_ReturnsErrorResult()
        {
            // Arrange
            _productRepositoryMock.Setup(repo => repo.GetAll()).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _getAllService.GetAllAsync();

            // Assert
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Error al obtener los productos: Database error"));
            Assert.That(result.Data, Is.Null);
        }
    }
}
