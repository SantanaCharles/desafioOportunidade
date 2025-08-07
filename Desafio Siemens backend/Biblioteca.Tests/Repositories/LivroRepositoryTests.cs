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
    public class LivroRepositoryTests
    {
        private BibliotecaDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<BibliotecaDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // nome único
                .Options;

            return new BibliotecaDbContext(options);
        }

        [Fact]
        public void Deve_Adicionar_Livro_Com_Sucesso()
        {
            // Arrange
            var context = GetInMemoryDbContext();

            var autor = new Autor { Nome = "George Orwell" };
            var genero = new Genero { Nome = "Distopia" };
            context.Autores.Add(autor);
            context.Generos.Add(genero);
            context.SaveChanges();

            var livro = new Livro
            {
                Titulo = "1984",
                AutorId = autor.Id,
                GeneroId = genero.Id
            };

            // Act
            context.Livros.Add(livro);
            context.SaveChanges();

            // Assert
            var livros = context.Livros.Include(l => l.Autor).Include(l => l.Genero).ToList();

            Assert.Single(livros);
            Assert.Equal("1984", livros.First().Titulo);
            Assert.Equal(autor.Id, livros.First().AutorId);
            Assert.Equal(genero.Id, livros.First().GeneroId);
        }

        [Fact]
        public void Deve_Listar_Todos_Livros()
        {
            // Arrange
            var context = GetInMemoryDbContext();

            var autor = new Autor { Nome = "Autor Teste" };
            var genero = new Genero { Nome = "Ficção" };
            context.Autores.Add(autor);
            context.Generos.Add(genero);
            context.SaveChanges();

            context.Livros.AddRange(
                new Livro { Titulo = "Livro 1", AutorId = autor.Id, GeneroId = genero.Id },
                new Livro { Titulo = "Livro 2", AutorId = autor.Id, GeneroId = genero.Id }
            );
            context.SaveChanges();

            // Act
            var livros = context.Livros.ToList();

            // Assert
            Assert.Equal(2, livros.Count);
            Assert.Contains(livros, l => l.Titulo == "Livro 1");
            Assert.Contains(livros, l => l.Titulo == "Livro 2");
        }

        [Fact]
        public void Deve_Atualizar_Titulo_Do_Livro()
        {
            // Arrange
            var context = GetInMemoryDbContext();

            var autor = new Autor { Nome = "Autor" };
            var genero = new Genero { Nome = "Gênero" };
            context.Autores.Add(autor);
            context.Generos.Add(genero);
            context.SaveChanges();

            var livro = new Livro { Titulo = "Antigo", AutorId = autor.Id, GeneroId = genero.Id };
            context.Livros.Add(livro);
            context.SaveChanges();

            // Act
            livro.Titulo = "Novo Título";
            context.Livros.Update(livro);
            context.SaveChanges();

            // Assert
            var atualizado = context.Livros.First();
            Assert.Equal("Novo Título", atualizado.Titulo);
        }

        [Fact]
        public void Deve_Remover_Livro()
        {
            // Arrange
            var context = GetInMemoryDbContext();

            var autor = new Autor { Nome = "Autor" };
            var genero = new Genero { Nome = "Gênero" };
            context.Autores.Add(autor);
            context.Generos.Add(genero);
            context.SaveChanges();

            var livro = new Livro { Titulo = "Excluir", AutorId = autor.Id, GeneroId = genero.Id };
            context.Livros.Add(livro);
            context.SaveChanges();

            // Act
            context.Livros.Remove(livro);
            context.SaveChanges();

            // Assert
            Assert.Empty(context.Livros);
        }
    }
}
