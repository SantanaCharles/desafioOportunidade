using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class LivroDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;

        public int AutorId { get; set; }

        public int GeneroId { get; set; }
        public string Autor { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
    }
}
