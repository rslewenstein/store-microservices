using AutoMapper;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using WebApi.Products.Application;
using WebApi.Products.Domain;
using WebApi.Products.Infrastructure.Repository.Interfaces;

namespace WebApi.Products.Unit.Tests;

public class ApplicationTests
{
    private readonly ProductService _sut;
    private readonly IProductRepository _mockRepository = Substitute.For<IProductRepository>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();

    public ApplicationTests()
    {
        _sut = new ProductService(_mockRepository, _mapper);
    }


    [Fact]
    public async Task ListAll_ShouldReturnProductList_WhenProductsExists()
    {
        //Arrange 
        await _mockRepository.GetAllAsync();

        //Act
        var result = await _sut.ListAll();

        //Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task ListById_ShouldReturnProduct_WhenProductExists()
    {
        //Arrange 
        int productId = 1;
        string productName = "Camisa";
        string productType = "tamanho M";
        int quantity = 10;
        double price = 45.69;

        var product = new Product{
            ProductId = productId,
            ProductName = productName,
            ProductType = productType,
            Quantity = quantity,
            Price = price
        };
        
        _mockRepository.GetByIdAsync(productId).Returns(product);

        //Act
        var result = await _sut.ListById(productId);

        //Assert
        Assert.Equal(productId, result.ProductId);
        Assert.Equal(productName, result.ProductName);
    }

     [Fact]
    public async Task ListById_ShouldReturnNull_WhenProductDoesNotExists()
    {
        //Arrange
        _mockRepository.GetByIdAsync(Arg.Any<int>()).ReturnsNull();

        //Act
        var result = await _sut.ListById(new int());

        //Assert
        Assert.Null(result.ProductName);
    }
}