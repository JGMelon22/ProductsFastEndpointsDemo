using NSubstitute;
using ProductsFastEndpointsDemo.Exceptions;
using ProductsFastEndpointsDemo.Infrastructure.Interfaces;
using ProductsFastEndpointsDemo.Infrastructure.Services;
using ProductsFastEndpointsDemo.Products.DTOs;
using ProductsFastEndpointsDemo.Products.Entities;
using ProductsFastEndpointsDemo.Products.Mappings;

namespace ProductsFastEndpointsDemo.Infrastructure.UnitTests.Services;

[TestFixture]
public class ProductServiceTests
{
    private IProductRepository _repository;
    private IProductService _service;

    [SetUp]
    public void Setup()
    {
        _repository = Substitute.For<IProductRepository>();

        _service = new ProductService(_repository);
    }

    [Test]
    public async Task Should_ThrowProductAvailabilityException_When_QuantityAndAvailibilityConflict()
    {
        // Arrange
        ProductRequest request = new("PlayStation 4", 200.00M, 10, false);

        // Act & Assert
        Assert.ThrowsAsync<ProductAvailabilityException>(async () => await _service.AddAsync(request)
        );

        // Verify
        await _repository.DidNotReceive().AddAsync(Arg.Any<Product>());
    }

    [Test]
    public async Task Should_ReturnProductResponse_When_ValidProduct()
    {
        // Arrange
        ProductRequest request = new("Xbox Series X", 500.00M, 30, true);
        Product product = request.ToDomain();
        ProductResponse response = product.ToResponse();

        _repository.AddAsync(Arg.Any<Product>())
            .Returns(product);

        // Act
        var result = await _service.AddAsync(request);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(product.Name));
        Assert.That(result.Price, Is.EqualTo(product.Price));
        Assert.That(result.Quantity, Is.EqualTo(product.Quantity));
        Assert.That(result.IsAvailable, Is.EqualTo(product.IsAvailable));

        // Verify
        await _repository.Received(1).AddAsync(Arg.Any<Product>());
    }
}