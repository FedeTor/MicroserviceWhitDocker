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
    public class UpdateTests
    {
        private Mock<IRepository> _productRepositoryMock;
        private Mock<ILogger<Update>> _loggerMock;
        private Update _updateService;

        [SetUp]
        public void SetUp()
        {
            _productRepositoryMock = new Mock<IRepository>();
            _loggerMock = new Mock<ILogger<Update>>();
            _updateService = new Update(_productRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task UpdateAsync_ProductExists_ReturnsSuccessResult()
        {
            // Arrange
            var productId = 1;
            var productDto = new ProductDto { Id = productId, Name = "Updated Product", Price = 200 };
            var product = Product.Create("Original Product", 100);

            _productRepositoryMock.Setup(repo => repo.GetById(productId)).ReturnsAsync(product);
            _productRepositoryMock.Setup(repo => repo.Update(product)).Returns(Task.CompletedTask);

            // Act
            var result = await _updateService.UpdateAsync(productDto);

            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Message, Is.EqualTo("Operación exitosa."));
            Assert.That(product.Name, Is.EqualTo(productDto.Name));
            Assert.That(product.Price, Is.EqualTo(productDto.Price));
        }

        [Test]
        public async Task UpdateAsync_ProductDoesNotExist_ReturnsErrorResult()
        {
            // Arrange
            var productId = 1;
            var productDto = new ProductDto { Id = productId, Name = "Updated Product", Price = 200 };

            _productRepositoryMock.Setup(repo => repo.GetById(productId)).ReturnsAsync((Product)null);

            // Act
            var result = await _updateService.UpdateAsync(productDto);

            // Assert
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Producto no encontrado."));
        }

        [Test]
        public async Task UpdateAsync_ExceptionThrown_ReturnsErrorResult()
        {
            // Arrange
            var productId = 1;
            var productDto = new ProductDto { Id = productId, Name = "Updated Product", Price = 200 };

            _productRepositoryMock.Setup(repo => repo.GetById(productId)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _updateService.UpdateAsync(productDto);

            // Assert
            Assert.That(result.Success, Is.False);
            Assert.That(result.Message, Is.EqualTo("Error al actualizar el producto: Database error"));
        }
    }
}
