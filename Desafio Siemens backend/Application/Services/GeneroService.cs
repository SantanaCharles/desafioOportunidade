using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GeneroService : IGeneroService
    {
        private readonly IRepository<Genero> _repo;
        private readonly IMapper _mapper;

        public GeneroService(IRepository<Genero> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GeneroDto>> GetAllAsync()
        {
            var generos = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<GeneroDto>>(generos);
        }

        public async Task<GeneroDto?> GetByIdAsync(int id)
        {
            var genero = await _repo.GetByIdAsync(id);
            return genero is null ? null : _mapper.Map<GeneroDto>(genero);
        }

        public async Task<GeneroDto> CreateAsync(GeneroCreateDto dto)
        {
            var genero = _mapper.Map<Genero>(dto);
            await _repo.AddAsync(genero);
            await _repo.SaveChangesAsync();
            return _mapper.Map<GeneroDto>(genero);
        }

        public async Task UpdateAsync(int id, GeneroCreateDto dto)
        {
            var genero = await _repo.GetByIdAsync(id) ?? throw new Exception("Gênero não encontrado");
            genero.Nome = dto.Nome;
            _repo.Update(genero);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var genero = await _repo.GetByIdAsync(id) ?? throw new Exception("Gênero não encontrado");
            _repo.Delete(genero);
            await _repo.SaveChangesAsync();
        }
    }

}
