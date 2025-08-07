using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;
using DotNetEnv;

namespace Infrastructure.Data
{
    public class BibliotecaDbContextFactory : IDesignTimeDbContextFactory<BibliotecaDbContext>
    {
        public BibliotecaDbContext CreateDbContext(string[] args)
        {
            // Carrega variáveis do .env
            Env.Load("../API/.env"); // ajuste o caminho se necessário

            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            var optionsBuilder = new DbContextOptionsBuilder<BibliotecaDbContext>();
            optionsBuilder.UseNpgsql(connectionString); // ou UseSqlServer

            return new BibliotecaDbContext(optionsBuilder.Options);
        }
    }
}

