using Application.UseCase;
using Domain.Domain.Interfaces.IRepositorySqlServer;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Application.Tests
{
    [TestFixture]
    public class GetByIdTests
    {
        private Mock<IRepository> _productRepositoryMock;
        private Mock<ILogger<GetById>> _loggerMock;
        private GetById _getByIdService;

        [SetUp]
        public void SetUp()
        {
            _productRepositoryMock = new Mock<IRepository>();
            _loggerMock = new Mock<ILogger<GetById>>();
            _getByIdService = new GetById(_productRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task GetByIdAsync_ProductExists_ReturnsSuccessResult()
        {
            // Arrange
            int productId = 1;
            var product = Product.Create("Product Test", 100);
            _productRepositoryMock.Setup(repo => repo.GetById(productId)).ReturnsAsync(product);

            // Act
            var result = await _getByIdService.GetByIdAsync(productId);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Producto encontrado."));
            Assert.That(result.Data, Is.EqualTo(product));
        }

        [Test]
        public async Task GetByIdAsync_ProductDoesNotExist_ReturnsNullDataInErrorResult()
        {
            // Arrange
            int productId = 1;

            _productRepositoryMock.Setup(repo => repo.GetById(productId)).ReturnsAsync((Product)null);

            // Act
            var result = await _getByIdService.GetByIdAsync(productId);

            // Assert
            Assert.That(result.Success, Is.False, "El resultado no indica error como se esperaba.");
            Assert.That(result.Message, Is.EqualTo("Producto no encontrado."), "El mensaje no coincide con el esperado.");
            Assert.That(result.Data, Is.Null, "El Data no debería contener datos cuando el producto no existe.");
        }

        [Test]
        public async Task GetByIdAsync_ExceptionThrown_ReturnsErrorResult()
        {
            // Arrange
            int productId = 1;
            _productRepositoryMock.Setup(repo => repo.GetById(productId)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _getByIdService.GetByIdAsync(productId);

            // Assert
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Error al obtener los productos: Database error"));
            Assert.That(result.Data, Is.Null);
        }
    }
}
