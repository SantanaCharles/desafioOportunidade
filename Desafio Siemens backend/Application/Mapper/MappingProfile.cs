using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Genero, GeneroDto>().ReverseMap();
            CreateMap<GeneroCreateDto, Genero>();

            CreateMap<Autor, AutorDto>().ReverseMap();
            CreateMap<AutorCreateDto, Autor>();

            CreateMap<Livro, LivroDto>().ReverseMap();
            CreateMap<LivroCreateDto, Livro>();
        }
    }
}
