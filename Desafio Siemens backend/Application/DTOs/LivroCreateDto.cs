using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class LivroCreateDto
    {
        public string Titulo { get; set; } = string.Empty;
        public int AutorId { get; set; }
        public int GeneroId { get; set; }
    }
}
