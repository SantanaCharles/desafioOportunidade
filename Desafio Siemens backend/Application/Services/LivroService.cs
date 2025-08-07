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
    public class LivroService : ILivroService
    {
        private readonly IRepository<Livro> _repo;
        private readonly IRepository<Autor> _autorRepo;
        private readonly IRepository<Genero> _generoRepo;
        private readonly IMapper _mapper;

        public LivroService(
            IRepository<Livro> repo,
            IRepository<Autor> autorRepo,
            IRepository<Genero> generoRepo,
            IMapper mapper)
        {
            _repo = repo;
            _autorRepo = autorRepo;
            _generoRepo = generoRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LivroDto>> GetAllAsync()
        {
            var livros = await _repo.GetAllAsync();
            var result = new List<LivroDto>();

            foreach (var livro in livros)
            {
                var autor = await _autorRepo.GetByIdAsync(livro.AutorId);
                var genero = await _generoRepo.GetByIdAsync(livro.GeneroId);

                result.Add(new LivroDto
                {
                    Id = livro.Id,
                    Titulo = livro.Titulo,
                    AutorId = autor?.Id ?? 0,
                    GeneroId = genero?.Id ?? 0,
                    Autor = autor?.Nome ?? "Desconhecido",
                    Genero = genero?.Nome ?? "Desconhecido"
                });
            }

            return result;
        }

        public async Task<LivroDto?> GetByIdAsync(int id)
        {
            var livro = await _repo.GetByIdAsync(id);
            if (livro == null) return null;

            var autor = await _autorRepo.GetByIdAsync(livro.AutorId);
            var genero = await _generoRepo.GetByIdAsync(livro.GeneroId);

            return new LivroDto
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                AutorId = autor?.Id ?? 0,
                GeneroId = genero?.Id ?? 0,
                Autor = autor?.Nome ?? "Desconhecido",
                Genero = genero?.Nome ?? "Desconhecido"
            };
        }

        public async Task<LivroDto> CreateAsync(LivroCreateDto dto)
        {
            // Validação
            if (await _autorRepo.GetByIdAsync(dto.AutorId) is null)
                throw new Exception("Autor não encontrado");

            if (await _generoRepo.GetByIdAsync(dto.GeneroId) is null)
                throw new Exception("Gênero não encontrado");

            var livro = _mapper.Map<Livro>(dto);
            await _repo.AddAsync(livro);
            await _repo.SaveChangesAsync();
            
            return await GetByIdAsync(livro.Id) ?? throw new Exception("Erro ao criar livro");
        }

        public async Task UpdateAsync(int id, LivroCreateDto dto)
        {
            var livro = await _repo.GetByIdAsync(id) ?? throw new Exception("Livro não encontrado");

            if (await _autorRepo.GetByIdAsync(dto.AutorId) is null)
                throw new Exception("Autor inválido");

            if (await _generoRepo.GetByIdAsync(dto.GeneroId) is null)
                throw new Exception("Gênero inválido");

            livro.Titulo = dto.Titulo;
            livro.AutorId = dto.AutorId;
            livro.GeneroId = dto.GeneroId;


            _repo.Update(livro);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var livro = await _repo.GetByIdAsync(id) ?? throw new Exception("Livro não encontrado");
            _repo.Delete(livro);
            await _repo.SaveChangesAsync();
        }
    }
}
