using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAutorService
    {
        Task<IEnumerable<AutorDto>> GetAllAsync();
        Task<AutorDto?> GetByIdAsync(int id);
        Task<AutorDto> CreateAsync(AutorCreateDto dto);
        Task UpdateAsync(int id, AutorCreateDto dto);
        Task DeleteAsync(int id);
    }
}
