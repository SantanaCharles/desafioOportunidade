using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILivroService
    {
        Task<IEnumerable<LivroDto>> GetAllAsync();
        Task<LivroDto?> GetByIdAsync(int id);
        Task<LivroDto> CreateAsync(LivroCreateDto dto);
        Task UpdateAsync(int id, LivroCreateDto dto);
        Task DeleteAsync(int id);
    }
}
