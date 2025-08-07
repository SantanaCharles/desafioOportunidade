using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Tests.services
{
    public class LivroServiceTests
    {
        private readonly Mock<IRepository<Livro>> _repoMock;
        private readonly Mock<IRepository<Autor>> _autorRepoMock;
        private readonly Mock<IRepository<Genero>> _generoRepoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly LivroService _service;

        public LivroServiceTests()
        {
            _repoMock = new Mock<IRepository<Livro>>();
            _autorRepoMock = new Mock<IRepository<Autor>>();
            _generoRepoMock = new Mock<IRepository<Genero>>();
            _mapperMock = new Mock<IMapper>();

            _service = new LivroService(
                _repoMock.Object,        // Livro
                _autorRepoMock.Object,   // Autor
                _generoRepoMock.Object,  // Gênero
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarListaDeLivros()
        {
            // Arrange
            var livros = new List<Livro> { new Livro { Id = 1, Titulo = "Livro Teste" } };
            var livroDtos = new List<LivroDto> { new LivroDto { Id = 1, Titulo = "Livro Teste" } };

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(livros);
            _mapperMock.Setup(m => m.Map<IEnumerable<LivroDto>>(livros)).Returns(livroDtos);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("Livro Teste", result.First().Titulo);
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarLivroDto_QuandoEncontrado()
        {
            // Arrange
            var livro = new Livro { Id = 1, Titulo = "Detalhes" };
            var livroDto = new LivroDto { Id = 1, Titulo = "Detalhes" };

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(livro);
            _mapperMock.Setup(m => m.Map<LivroDto>(livro)).Returns(livroDto);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Detalhes", result!.Titulo);
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarNull_QuandoNaoEncontrado()
        {
            // Arrange
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Livro?)null);

            // Act
            var result = await _service.GetByIdAsync(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_DeveCriarLivroComSucesso()
        {
            // Arrange
            var createDto = new LivroCreateDto
            {
                Titulo = "Novo Livro",
                AutorId = 2,
                GeneroId = 1
            };

            var autor = new Autor { Id = 2, Nome = "Autor Teste" };
            var genero = new Genero { Id = 1, Nome = "Ficção" };

            var livro = new Livro
            {
                Id = 0, // ainda não persistido
                Titulo = "Novo Livro",
                AutorId = 2,
                GeneroId = 1,
                Autor = autor,
                Genero = genero
            };

            var livroSalvo = new Livro
            {
                Id = 4, // simula ID após persistência
                Titulo = "Novo Livro",
                AutorId = 2,
                GeneroId = 1,
                Autor = autor,
                Genero = genero
            };

            var livroDto = new LivroDto
            {
                Id = 4,
                Titulo = "Novo Livro",
                AutorId = 2,
                GeneroId = 1,
                Autor = "Autor Teste",
                Genero = "Ficção"
            };

            _autorRepoMock.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(autor);
            _generoRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(genero);

            _mapperMock.Setup(m => m.Map<Livro>(createDto)).Returns(livro);

            _repoMock.Setup(r => r.AddAsync(It.IsAny<Livro>()))
                     .Callback<Livro>(l => l.Id = 4) // simula atribuição de ID após salvar
                     .Returns(Task.CompletedTask);

            _repoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            _repoMock.Setup(r => r.GetByIdAsync(4)).ReturnsAsync(livroSalvo);
            _mapperMock.Setup(m => m.Map<LivroDto>(livroSalvo)).Returns(livroDto);

            // Act
            var result = await _service.CreateAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Novo Livro", result.Titulo);
            Assert.Equal("Autor Teste", result.Autor);
            Assert.Equal("Ficção", result.Genero);

            // Verificações adicionais
            _autorRepoMock.Verify(r => r.GetByIdAsync(2), Times.Exactly(2));
            _generoRepoMock.Verify(r => r.GetByIdAsync(1), Times.Exactly(2));
            _repoMock.Verify(r => r.AddAsync(It.IsAny<Livro>()), Times.Once);
            _repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
            _repoMock.Verify(r => r.GetByIdAsync(4), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_DeveAtualizarLivro()
        {
            // Arrange
            var livro = new Livro { Id = 1, Titulo = "Antigo", AutorId = 1, GeneroId = 1 };
            var updateDto = new LivroCreateDto { Titulo = "Atualizado", AutorId = 1, GeneroId = 1 };

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(livro);
            _repoMock.Setup(r => r.Update(livro));
            _repoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);
            _autorRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Autor { Id = 1, Nome = "Autor Existente" });
            _generoRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Genero { Id = 1, Nome = "Drama" });


            // Act
            await _service.UpdateAsync(1, updateDto);

            // Assert
            Assert.Equal("Atualizado", livro.Titulo);
        }

        [Fact]
        public async Task UpdateAsync_DeveLancarExcecao_SeLivroNaoExistir()
        {
            // Arrange
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Livro?)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _service.UpdateAsync(99, new LivroCreateDto()));
            Assert.Equal("Livro não encontrado", ex.Message);
        }

        [Fact]
        public async Task DeleteAsync_DeveExcluirLivro()
        {
            // Arrange
            var livro = new Livro { Id = 1, Titulo = "Excluir" };

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(livro);
            _repoMock.Setup(r => r.Delete(livro));
            _repoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            await _service.DeleteAsync(1);

            // Assert
            _repoMock.Verify(r => r.Delete(livro), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_DeveLancarExcecao_SeLivroNaoExistir()
        {
            // Arrange
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Livro?)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _service.DeleteAsync(100));
            Assert.Equal("Livro não encontrado", ex.Message);
        }
    }
}
