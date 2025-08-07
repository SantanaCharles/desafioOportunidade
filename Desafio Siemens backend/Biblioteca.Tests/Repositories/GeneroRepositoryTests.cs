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
    public class GeneroRepositoryTests
    {
        private BibliotecaDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<BibliotecaDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // nome único
                .Options;

            return new BibliotecaDbContext(options);
        }

        [Fact]
        public void Deve_Adicionar_Genero_Com_Sucesso()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var genero = new Genero { Nome = "Romance" };

            // Act
            context.Generos.Add(genero);
            context.SaveChanges();

            // Assert
            Assert.Equal(1, context.Generos.Count());
            Assert.Equal("Romance", context.Generos.First().Nome);
        }

        [Fact]
        public void Deve_Listar_Todos_Generos()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            context.Generos.Add(new Genero { Nome = "Fantasia" });
            context.Generos.Add(new Genero { Nome = "Suspense" });
            context.SaveChanges();

            // Act
            var generos = context.Generos.ToList();

            // Assert
            Assert.Equal(2, generos.Count);
            Assert.Contains(generos, g => g.Nome == "Fantasia");
            Assert.Contains(generos, g => g.Nome == "Suspense");
        }

        [Fact]
        public void Deve_Remover_Genero()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var genero = new Genero { Nome = "Aventura" };
            context.Generos.Add(genero);
            context.SaveChanges();

            // Act
            context.Generos.Remove(genero);
            context.SaveChanges();

            // Assert
            Assert.Empty(context.Generos);
        }

        [Fact]
        public void Deve_Atualizar_Genero()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var genero = new Genero { Nome = "História" };
            context.Generos.Add(genero);
            context.SaveChanges();

            // Act
            genero.Nome = "História Atualizada";
            context.Generos.Update(genero);
            context.SaveChanges();

            // Assert
            var atualizado = context.Generos.First();
            Assert.Equal("História Atualizada", atualizado.Nome);
        }


    }
}
