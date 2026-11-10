
# 🏫 API Piaget - Gestão Escolar



https://therapeutic-banjo-dcb.notion.site/Comando-Doker-DevOps-405396b901af80478fadc25961789e3c?source=copy_link

API RESTful desenvolvida em **.NET 8** para gerenciamento de entidades escolares (Alunos, Professores, Escolas e Administradores). O projeto é totalmente **containerizado com Docker**, utilizando **Entity Framework Core** e **SQL Server 2022**, garantindo portabilidade, isolamento e facilidade de deployment.

---

## 🛠️ Tecnologias Utilizadas

| Tecnologia | Função no Projeto |
| :--- | :--- |
| **.NET 8 (ASP.NET Core)** | Framework principal para construção da API (LTS). |
| **C#** | Linguagem de programação para lógica de negócio e modelos. |
| **Entity Framework Core** | ORM para tradução de objetos C# em comandos SQL (Abordagem Code-First). |
| **Microsoft SQL Server 2022** | Sistema de Gerenciamento de Banco de Dados Relacional (SGBD). |
| **Docker & Docker Compose** | Containerização e orquestração dos serviços (API + Banco de Dados). |
| **Swagger (OpenAPI)** | Documentação interativa e testes de endpoints. |

---

## 📋 Pré-requisitos

Antes de começar, certifique-se de ter instalado em sua máquina:
1. [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2. [Docker Desktop](https://www.docker.com/products/docker-desktop/) (com WSL 2 habilitado no Windows)
3. Ferramenta global do EF Core (instale via PowerShell):
   ```powershell
   dotnet tool install --global dotnet-ef
   ```

---

## 🚀 Guia de Instalação e Execução (Passo a Passo)

Siga esta ordem exata para garantir uma configuração limpa e sem conflitos.

### 1. Limpeza do Ambiente
Abra o PowerShell na raiz do projeto e execute para remover resquícios de tentativas anteriores:
```powershell
docker compose down -v
Remove-Item -Recurse -Force Migrations, bin, obj -ErrorAction SilentlyContinue
```

### 2. Configuração Local para Migrações
Para que o EF Core funcione no Windows, configure os arquivos abaixo:

**A. `appsettings.json`** (Verifique a ConnectionString):
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "ApiPiagetDoisContext": "Server=localhost;Database=PiagetAPI;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  }
}
```

**B. `Data/ApiPiagetDoisContextFactory.cs`** (Crie este arquivo se não existir):
```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ApiPiagetDois.Data
{
    public class ApiPiagetDoisContextFactory : IDesignTimeDbContextFactory<ApiPiagetDoisContext>
    {
        public ApiPiagetDoisContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApiPiagetDoisContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ApiPiagetDoisContext"));

            return new ApiPiagetDoisContext(optionsBuilder.Options);
        }
    }
}
```

### 3. Configuração do Docker (Correção de Arquitetura)
Para evitar o erro `exec format error` (comum em processadores ARM como Apple Silicon ou Windows on ARM), o `docker-compose.yml` deve forçar a arquitetura `linux/arm64`. 

Execute este bloco no PowerShell para gerar o arquivo corretamente:
```powershell
$yaml = @"
services:
  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    platform: linux/arm64
    container_name: piaget_db
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=SenhaForte123!
    ports:
      - "1433:1433"
    volumes:
      - sqlserver_data:/var/opt/mssql
    networks:
      - piaget_network
    healthcheck:
      test: ["CMD-SHELL", "/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P 'SenhaForte123!' -Q 'SELECT 1' -C || exit 1"]
      interval: 10s
      retries: 10
      start_period: 20s
      timeout: 5s

  api:
    build:
      context: .
      dockerfile: Dockerfile
    platform: linux/arm64
    container_name: piaget_api
    ports:
      - "8080:8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_HTTP_PORTS=8080
      - ConnectionStrings__ApiPiagetDoisContext=Server=db,1433;Database=PiagetAPI;User Id=sa;Password=SenhaForte123!;TrustServerCertificate=True;MultipleActiveResultSets=true
    depends_on:
      db:
        condition: service_healthy
    networks:
      - piaget_network

volumes:
  sqlserver_data:

networks:
  piaget_network:
    driver: bridge
"@
[System.IO.File]::WriteAllText("$PWD\docker-compose.yml", $yaml, [System.Text.Encoding]::UTF8)
Write-Host "docker-compose.yml configurado com sucesso!" -ForegroundColor Green
```

### 4. Gerar a Migração Limpa
Instale o pacote de design compatível e crie a migração inicial:
```powershell
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.10
dotnet ef migrations add InitialCleanSetup
```

### 5. Build e Execução
Construa a imagem do zero (ignorando caches corrompidos) e inicie os containers:
```powershell
docker compose build --no-cache
docker compose up
```

✅ **Sucesso:** Ao ver `Now listening on: http://[::]:8080` e `Application started`, abra o navegador em:  
👉 **[http://localhost:8080/swagger](http://localhost:8080/swagger)**

---

## 💻 Comandos do Dia a Dia

| Ação | Comando | Descrição |
| :--- | :--- | :--- |
| **Iniciar** | `docker compose start` | Liga os containers parados (rápido, sem recompilar). |
| **Parar** | `docker compose stop` | Para a API e o Banco, **mantendo os dados salvos**. |
| **Logs** | `docker compose logs -f` | Monitora os logs em tempo real (útil para debug). |
| **Reset Total** ⚠️ | `docker compose down -v` | **Apaga TUDO** (containers, redes e dados do banco). Use com cuidado! |

---

## 💡 Acesso Externo ao Banco de Dados

Para visualizar os dados inseridos pela API, conecte seu **SQL Server Management Studio (SSMS)** ou **Azure Data Studio** com as seguintes credenciais:

- **Servidor:** `localhost,1433`
- **Autenticação:** Autenticação do SQL Server
- **Usuário:** `sa`
- **Senha:** `SenhaForte123!`

---

## 🔧 Solução de Problemas (Troubleshooting)

### Erro: `exec /usr/bin/dotnet: exec format error`
**Causa:** Incompatibilidade entre a arquitetura da imagem Docker (amd64) e o processador da sua máquina (ARM).  
**Solução:** O arquivo `docker-compose.yml` fornecido no **Passo 3** já contém a diretriz `platform: linux/arm64` em ambos os serviços (`db` e `api`), o que resolve este problema definitivamente. Se o erro persistir, execute `docker system prune -a -f` para limpar imagens antigas e repita o **Passo 5**.

### Erro: `There is already an object named 'X' in the database`
**Causa:** Conflito entre o histórico de migrações do EF Core e tabelas já existentes no volume do Docker.  
**Solução:** Execute o **Reset Total** (`docker compose down -v`), apague a pasta `Migrations` manualmente e refaça o **Passo 4**.
```

---


