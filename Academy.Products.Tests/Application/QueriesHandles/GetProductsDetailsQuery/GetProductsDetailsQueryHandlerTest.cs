using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.Products.Tests.Application.QueriesHandles.GetProductsDetailsQuery
{
    public class GetProductsDetailsQueryHandlerTest
    {
        /*Tipos de pruebas unitarias 
        [Fact]
        
        [InlineData()] // Parametrizadas -> aqui van los datos para múltiples pruebas
        Example: [InlineData(1, "Product A", 100.0)]
        */

        // Pruebas Happy path
        [Fact]
        public async Task Handler_Should_Return_Product_Details()
        {
            // Arrange

            // Act

            // Assert
            // si se usa InlineData, se realiza lógica para cada conjunto de datos
        }

        [Fact]
        public async Task Handler_Should_Return_OutOfStock_Product()
        {
            // Arrange

            // Act

            // Assert
        }

        public async Task Handler_Should_Return_Complete_Product_Details()
        {
            // Arrange

            // Act

            // Assert
        }


        // Error test 
        [Fact]
        public async Task Handler_Should_Return_Empty_When_No_Products()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task Handler_Should_Throw_Exception_When_Invalid_Id()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task Handler_Should_Return_Default_Image_When_No_Image()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task Handler_Should_Return_NotFound_When_Product_Does_Not_Exist()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task Handler_Should_Handle_DatabaseConnection_Error()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task Handler_Should_Throw_InternalServerError_On_Failure()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}