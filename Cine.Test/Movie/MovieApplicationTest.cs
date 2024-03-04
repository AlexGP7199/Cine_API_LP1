using Cine.Application.Dtos.Request.Movie;
using Cine.Application.Interfaces;
using Cine.Utilities.Static;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cine.Test.Movie
{
    [TestClass]
    public class MovieApplicationTest
    {

        // Pendiente de Fix
        private static WebApplicationFactory<Program>? _factory = null;
        private static IServiceScopeFactory? _scopeFactory = null;

        [ClassInitialize]
        public static void Initialize(TestContext _textContext)
        {
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        }

        [TestMethod]
        public async Task RegisterMovie_WhenSenNullValueOrEmpty_ValidationErrors()
        {
            using var scope = _scopeFactory?.CreateScope();
            var context = scope?.ServiceProvider.GetService<IMovieApplication>();

            //Arramge
            var Titulo = "";
            var Director = "";
            var Genero = "";
            var Clasificacion = "";
            var Descripcion = "";
            var expected = ReplyMessage.MESSAGE_VALIDATE;
            //Act
            var result = await context!.RegisterMovie(new MovieRequestDto()
            {
                Titulo = Titulo,
                Director = Director,
                Genero = Genero,
                Clasificacion = Clasificacion,
                Descripcion = Descripcion

            });

            var current = result.Message;

            // Assert
            Assert.AreEqual(expected, current);
        }
    }
}
