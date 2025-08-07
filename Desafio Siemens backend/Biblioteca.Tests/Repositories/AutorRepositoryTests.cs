using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Tests.Repositories
{
    public class AutorRepositoryTests
    {
        private BibliotecaDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<BibliotecaDbContext>()
                .UseInMemoryDatabase(databaseName: "BibliotecaTestDb")
                .Options;

            return new BibliotecaDbContext(options);
        }

        [Fact]
        public void Deve_Adicionar_Autor_Com_Sucesso()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var autor = new Autor { Nome = "Machado de Assis" };

            // Act
            context.Autores.Add(autor);
            context.SaveChanges();

            // Assert
            Assert.Equal(1, context.Autores.Count());
            Assert.Equal("Machado de Assis", context.Autores.First().Nome);
        }

        [Fact]
        public void Deve_Listar_Todos_Autores()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            context.Autores.Add(new Autor { Nome = "Autor 1" });
            context.Autores.Add(new Autor { Nome = "Autor 2" });
            context.SaveChanges();

            // Act
            var autores = context.Autores.ToList();

            // Assert
            Assert.Equal(3, autores.Count);
        }
    }
}
