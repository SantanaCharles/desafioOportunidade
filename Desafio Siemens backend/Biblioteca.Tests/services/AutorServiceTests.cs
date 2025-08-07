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
    public class AutorServiceTests
    {
        private readonly Mock<IRepository<Autor>> _repoMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AutorService _service;

        public AutorServiceTests()
        {
            _repoMock = new Mock<IRepository<Autor>>();
            _mapperMock = new Mock<IMapper>();
            _service = new AutorService(_repoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_RetornaListaDeAutorDto()
        {
            // Arrange
            var autores = new List<Autor> { new Autor { Id = 1, Nome = "Autor 1" } };
            var autoresDto = new List<AutorDto> { new AutorDto { Id = 1, Nome = "Autor 1" } };

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(autores);
            _mapperMock.Setup(m => m.Map<IEnumerable<AutorDto>>(autores)).Returns(autoresDto);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Autor 1", ((AutorDto)result.First()).Nome);
        }

        [Fact]
        public async Task GetByIdAsync_RetornaAutorDto_QuandoEncontrado()
        {
            // Arrange
            var autor = new Autor { Id = 1, Nome = "Autor 1" };
            var autorDto = new AutorDto { Id = 1, Nome = "Autor 1" };

            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(autor);
            _mapperMock.Setup(m => m.Map<AutorDto>(autor)).Returns(autorDto);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Autor 1", result!.Nome);
        }

        [Fact]
        public async Task GetByIdAsync_RetornaNull_QuandoNaoEncontrado()
        {
            // Arrange
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Autor?)null);

            // Act
            var result = await _service.GetByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_DeveAdicionarAutorERetornarDto()
        {
            // Arrange
            var createDto = new AutorCreateDto { Nome = "Novo Autor" };
            var autor = new Autor { Id = 1, Nome = "Novo Autor" };
            var autorDto = new AutorDto { Id = 1, Nome = "Novo Autor" };

            _mapperMock.Setup(m => m.Map<Autor>(createDto)).Returns(autor);
            _repoMock.Setup(r => r.AddAsync(autor)).Returns(Task.CompletedTask);
            _repoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<AutorDto>(autor)).Returns(autorDto);

            // Act
            var result = await _service.CreateAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Novo Autor", result.Nome);
            _repoMock.Verify(r => r.AddAsync(autor), Times.Once);
            _repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_DeveAtualizarAutor()
        {
            // Arrange
            var id = 1;
            var createDto = new AutorCreateDto { Nome = "Autor Atualizado" };
            var autor = new Autor { Id = id, Nome = "Autor Original" };

            _repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(autor);
            _repoMock.Setup(r => r.Update(autor));
            _repoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            await _service.UpdateAsync(id, createDto);

            // Assert
            Assert.Equal("Autor Atualizado", autor.Nome);
            _repoMock.Verify(r => r.Update(autor), Times.Once);
            _repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_DeveLancarException_QuandoAutorNaoExistir()
        {
            // Arrange
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Autor?)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.UpdateAsync(999, new AutorCreateDto { Nome = "Teste" }));
        }

        [Fact]
        public async Task DeleteAsync_DeveRemoverAutor()
        {
            // Arrange
            var id = 1;
            var autor = new Autor { Id = id, Nome = "Autor 1" };

            _repoMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(autor);
            _repoMock.Setup(r => r.Delete(autor));
            _repoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            await _service.DeleteAsync(id);

            // Assert
            _repoMock.Verify(r => r.Delete(autor), Times.Once);
            _repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_DeveLancarException_QuandoAutorNaoExistir()
        {
            // Arrange
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Autor?)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.DeleteAsync(999));
        }
    }
}
