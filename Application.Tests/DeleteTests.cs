using Application.UseCase;
using Domain.Domain.Interfaces.IRepositorySqlServer;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Application.Tests
{
    [TestFixture]
    public class DeleteTests
    {
        private Mock<IRepository> _productRepositoryMock;
        private Mock<ILogger<Delete>> _loggerMock;
        private Delete _deleteService;

        [SetUp]
        public void SetUp()
        {
            _productRepositoryMock = new Mock<IRepository>();
            _loggerMock = new Mock<ILogger<Delete>>();
            _deleteService = new Delete(_productRepositoryMock.Object, _loggerMock.Object);
        }


        [Test]
        public async Task DeleteAsync_ProductExists_ReturnsSuccessResult()
        {
            // Arrange
            var productId = 1;
            var product = Product.Create("Test Product", 100);

            _productRepositoryMock.Setup(repo => repo.GetById(productId))
                .ReturnsAsync(product);

            _productRepositoryMock.Setup(repo => repo.Delete(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _deleteService.DeleteAsync(productId);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Producto eliminado exitosamente."));
            Assert.That(result.Data.Id, Is.EqualTo(productId));
        }

        [Test]
        public async Task DeleteAsync_ProductDoesNotExist_ReturnsErrorResult()
        {
            // Arrange
            var productId = 1;

            _productRepositoryMock.Setup(repo => repo.GetById(productId)).ReturnsAsync((Product)null);

            // Act
            var result = await _deleteService.DeleteAsync(productId);

            // Assert
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Producto no encontrado."));
            Assert.That(result.Data, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_ExceptionThrown_ReturnsErrorResult()
        {
            // Arrange
            var productId = 1;

            _productRepositoryMock.Setup(repo => repo.GetById(productId)).ThrowsAsync(new System.Exception("Database error"));

            // Act
            var result = await _deleteService.DeleteAsync(productId);

            // Assert
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Error al eliminar el producto: Database error"));
            Assert.That(result.Data, Is.Null);
        }
    }
}
