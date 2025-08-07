using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGeneroService
    {
        Task<IEnumerable<GeneroDto>> GetAllAsync();
        Task<GeneroDto?> GetByIdAsync(int id);
        Task<GeneroDto> CreateAsync(GeneroCreateDto dto);
        Task UpdateAsync(int id, GeneroCreateDto dto);
        Task DeleteAsync(int id);
    }
}
