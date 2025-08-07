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
    public class AutorService : IAutorService
    {
        private readonly IRepository<Autor> _repo;
        private readonly IMapper _mapper;

        public AutorService(IRepository<Autor> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AutorDto>> GetAllAsync()
        {
            var generos = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<AutorDto>>(generos);
        }

        public async Task<AutorDto?> GetByIdAsync(int id)
        {
            var autor = await _repo.GetByIdAsync(id);
            return autor is null ? null : _mapper.Map<AutorDto>(autor);
        }

        public async Task<AutorDto> CreateAsync(AutorCreateDto dto)
        {
            var autor = _mapper.Map<Autor>(dto);
            await _repo.AddAsync(autor);
            await _repo.SaveChangesAsync();
            return _mapper.Map<AutorDto>(autor);
        }

        public async Task UpdateAsync(int id, AutorCreateDto dto)
        {
            var autor = await _repo.GetByIdAsync(id) ?? throw new Exception("Autor não encontrado");
            autor.Nome = dto.Nome;
            _repo.Update(autor);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var autor = await _repo.GetByIdAsync(id) ?? throw new Exception("Autor não encontrado");
            _repo.Delete(autor);
            await _repo.SaveChangesAsync();
        }
    }
}

