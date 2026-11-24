using NSubstitute;
using ProductsFastEndpointsDemo.Infrastructure.Interfaces;
using ProductsFastEndpointsDemo.Infrastructure.Services;
using ProductsFastEndpointsDemo.Products.DTOs;
using ProductsFastEndpointsDemo.Products.Entities;
using ProductsFastEndpointsDemo.Products.Mappings;
using ProductsFastEndpointsDemo.Shared;

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
    public async Task Should_ReturnInvalidQuantityError_When_QuantityAndAvailabilityConflict()
    {
        // Arrange
        ProductRequest request = new("PlayStation 4", 200.00M, 10, false);

        // Act 
        var result = await _service.AddAsync(request);

        // Assert
        Assert.That(result.Value, Is.InstanceOf<Error>());
        Assert.That(result.Value, Is.EqualTo(ProductErrors.InvalidQuantity));

        // Verify
        await _repository.DidNotReceive().AddAsync(Arg.Any<Product>());
    }

    [Test]
    public async Task Should_ReturnProductResponse_When_ValidProduct()
    {
        // Arrange
        ProductRequest request = new("Xbox Series X", 500.00M, 30, true);
        Product product = request.ToDomain();
        ProductResponse expected = product.ToResponse();

        _repository.AddAsync(Arg.Any<Product>())
            .Returns(Task.FromResult(product));

        // Act
        var result = await _service.AddAsync(request);

        // Assert
        Assert.That(result.Value, Is.InstanceOf<ProductResponse>());
        var actual = (ProductResponse)result.Value;

        Assert.That(actual.Name, Is.EqualTo(expected.Name));
        Assert.That(actual.Price, Is.EqualTo(expected.Price));
        Assert.That(actual.Quantity, Is.EqualTo(expected.Quantity));
        Assert.That(actual.IsAvailable, Is.EqualTo(expected.IsAvailable));

        // Verify
        await _repository.Received(1).AddAsync(Arg.Any<Product>());
    }

    [Test]
    public async Task Should_ReturnInvalidQuantityError_When_UpdatingWithQuantityAndAvailabilityConflict()
    {
        // Arrange
        ProductRequest request = new("PlayStation 4", 200.00M, 10, false);

        // Act 
        var result = await _service.UpdateAsync(Guid.NewGuid(), request);

        // Assert
        Assert.That(result.Value, Is.EqualTo(ProductErrors.InvalidQuantity));

        // Verify
        await _repository.DidNotReceive().UpdateAsync(Arg.Any<Guid>(), Arg.Any<Product>());
    }

    [Test]
    public async Task Should_ReturnProductResponse_When_UpdatingValidProduct()
    {
        // Arrange
        ProductRequest request = new("Xbox Series X", 500.00M, 30, true);
        Product product = request.ToDomain();
        ProductResponse expected = product.ToResponse();

        _repository.UpdateAsync(Arg.Any<Guid>(), Arg.Any<Product>())
            .Returns(product);

        // Act
        var result = await _service.UpdateAsync(Guid.NewGuid(), request);

        // Assert
        Assert.That(result.Value, Is.InstanceOf<ProductResponse>());
        var actual = (ProductResponse)result.Value;

        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.Name, Is.EqualTo(expected.Name));
        Assert.That(actual.Price, Is.EqualTo(expected.Price));
        Assert.That(actual.Quantity, Is.EqualTo(expected.Quantity));
        Assert.That(actual.IsAvailable, Is.EqualTo(expected.IsAvailable));

        // Verify
        await _repository.Received(1).UpdateAsync(Arg.Any<Guid>(), Arg.Any<Product>());
    }
}