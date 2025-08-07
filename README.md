# 📦 Frontend - Projeto [Desafio]

Este é o frontend do projeto [Desafio], desenvolvido com [React + Vite ou outro stack que estiver usando]. A aplicação consome uma API REST e possui autenticação baseada em JWT, além de tratamento de erros centralizado.

---

## 🚀 Pré-requisitos

Certifique-se de que você tem as seguintes ferramentas instaladas na sua máquina:

- [Node.js (v18+ recomendado)](https://nodejs.org/)
- [Git](https://git-scm.com/)
- Gerenciador de pacotes:
  - `npm` (vem com o Node.js)
  - ou `yarn` (opcional)

---

## 🛠️ Clonando o Projeto


git clone https://github.com/seu-usuario/nome-do-repositorio.git
cd nome-do-repositorio


Crie um arquivo .env na raiz do projeto e adicione as variáveis de ambiente necessárias:
# .env
APP_API_BASE_URL=http://localhost:8080
APP_API_VERSION=v1


Instalando as Dependências
# Com npm
npm install

# Ou com yarn
yarn install

Executando o Projeto
# Com npm
npm run dev

# Ou com yarn
yarn dev


# 🛠️ Backend - Projeto [NOME DO PROJETO]

Este é o backend do projeto **[Nome do Projeto]**, desenvolvido com **ASP.NET Core 9.0**, utilizando:

- **Entity Framework Core** com **PostgreSQL**
- **AutoMapper**
- **DotNetEnv** para variáveis de ambiente
- **Swagger (Swashbuckle)** para documentação da API

---

## ✅ Requisitos

Antes de rodar este projeto, certifique-se de ter:

- [.NET SDK 9.0+](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [PostgreSQL](https://www.postgresql.org/) rodando localmente ou remotamente
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) ou outro editor compatível
- Opcional: [Docker](https://www.docker.com/) se quiser rodar o banco via container

---

## ⚙️ Configuração do Ambiente

1. Crie um arquivo `.env` na raiz do projeto (no mesmo nível do `.csproj`) com o seguinte conteúdo:

```env
CONNECTION_STRING=Host=localhost;Port=5432;Database=DataDb;Username=admin;Password=password


**Certifique-se de que seu DbContext carrega essa variável com a biblioteca DotNetEnv, por exemplo:**
## Exemplo
Env.Load();
var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
builder.Services.AddDbContext<SeuDbContext>(options =>
    options.UseNpgsql(connectionString));

#Migrations
**Migrations & Banco de Dados**
** Para criar e aplicar as migrations com o Entity Framework Core:**
# Criar uma nova migration
dotnet ef migrations add InitialCreate --project Infrastructure --startup-project Api

# Aplicar a migration ao banco de dados
dotnet ef database update --project Infrastructure --startup-project Api


**Rodando o Projeto**
**Para rodar a API localmente:**
dotnet run --project Api
Ou abra o projeto no Visual Studio e clique em "Start".

🌐 Acessando a API
** Após iniciar o projeto, a API estará disponível em:
https://localhost:7099

** A documentação Swagger estará disponível em:
https://localhost:7099/swagger


🧪 Testes
Caso tenha testes, você pode rodar com:
dotnet test 
