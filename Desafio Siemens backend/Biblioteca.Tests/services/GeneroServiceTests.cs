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
    public class GeneroServiceTests
    {
        private readonly Mock<IRepository<Genero>> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GeneroService _service;

        public GeneroServiceTests()
        {
            _repoMock = new Mock<IRepository<Genero>>();
            _mapperMock = new Mock<IMapper>();
            _service = new GeneroService(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarListaGeneroDto()
        {
            // Arrange
            var generos = new List<Genero> { new Genero { Id = 1, Nome = "Romance" } };
            var generoDtos = new List<GeneroDto> { new GeneroDto { Id = 1, Nome = "Romance" } };

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(generos);
            _mapperMock.Setup(m => m.Map<IEnumerable<GeneroDto>>(generos)).Returns(generoDtos);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("Romance", result.First().Nome);
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarGeneroDto_QuandoEncontrado()
        {
            // Arrange
            var genero = new Genero { Id = 1, Nome = "Aventura" };
            var generoDto = new GeneroDto { Id = 1, Nome = "Aventura" };

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(genero);
            _mapperMock.Setup(m => m.Map<GeneroDto>(genero)).Returns(generoDto);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Aventura", result!.Nome);
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarNull_QuandoNaoEncontrado()
        {
            // Arrange
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Genero?)null);

            // Act
            var result = await _service.GetByIdAsync(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_DeveAdicionarGeneroERetornarDto()
        {
            // Arrange
            var createDto = new GeneroCreateDto { Nome = "Suspense" };
            var genero = new Genero { Id = 1, Nome = "Suspense" };
            var generoDto = new GeneroDto { Id = 1, Nome = "Suspense" };

            _mapperMock.Setup(m => m.Map<Genero>(createDto)).Returns(genero);
            _repoMock.Setup(r => r.AddAsync(genero)).Returns(Task.CompletedTask);
            _repoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<GeneroDto>(genero)).Returns(generoDto);

            // Act
            var result = await _service.CreateAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Suspense", result.Nome);
            _repoMock.Verify(r => r.AddAsync(genero), Times.Once);
            _repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_DeveAtualizarGenero()
        {
            // Arrange
            var genero = new Genero { Id = 1, Nome = "Original" };
            var dto = new GeneroCreateDto { Nome = "Atualizado" };

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(genero);
            _repoMock.Setup(r => r.Update(genero));
            _repoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            await _service.UpdateAsync(1, dto);

            // Assert
            Assert.Equal("Atualizado", genero.Nome);
            _repoMock.Verify(r => r.Update(genero), Times.Once);
            _repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_DeveLancarException_QuandoGeneroNaoExistir()
        {
            // Arrange
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Genero?)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() =>
                _service.UpdateAsync(99, new GeneroCreateDto { Nome = "Teste" }));

            Assert.Equal("Gênero não encontrado", ex.Message);
        }

        [Fact]
        public async Task DeleteAsync_DeveRemoverGenero()
        {
            // Arrange
            var genero = new Genero { Id = 1, Nome = "Excluir" };

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(genero);
            _repoMock.Setup(r => r.Delete(genero));
            _repoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            await _service.DeleteAsync(1);

            // Assert
            _repoMock.Verify(r => r.Delete(genero), Times.Once);
            _repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_DeveLancarException_QuandoGeneroNaoExistir()
        {
            // Arrange
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Genero?)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _service.DeleteAsync(100));

            Assert.Equal("Gênero não encontrado", ex.Message);
        }
    }
}
