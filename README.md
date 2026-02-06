# Locadora

Um sistema para gerenciamento de locadora de filmes.

## Proposta de Arquitetura

```
Locadora/
├── Locadora.API/ # Projeto principal da API
│ ├── Controllers/ # Controladores API
│ ├── Data/ # Banco de dadoos
│ │ └── Dto # Pasta para salvar os dtos
│ ├── Migrations/ # Migrações do Entity
│ ├── Models/ # Entidades
│ ├── Profile/ # Arquivos de configuração do Automapper
│ └── Program.cs # Ponto de entrada
└── README.md
```

## Bibliotecas
- .NET 8
- Swashbuckle (swagger)
- Entityframework
- Pomelo
- Automapper
- Identity


## Banco de dados

Criando um container para base de dados MySql com o volume.

```bash
docker run --name mysql-db -e MYSQL_ROOT_PASSWORD=senha123 -e MYSQL_USER=appuser -e MYSQL_PASSWORD=apppass -p 3306:3306 -v mysql-data:/var/lib/mysql --restart unless-stopped -d mysql:8.0 --default-authentication-plugin=mysql_native_password --bind-address=0.0.0.0
```

### Usando Secrets:
```bash
$env:DB_SERVER="localhost"
$env:DB_NAME="locadora"
$env:DB_USER="appuser"
$env:DB_PASSWORD="apppass"
$env:JWT_KEY="MinhaChaveSuperSecreta"
```

### Com variáveis de ambiente:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=${DB_SERVER};Database=${DB_NAME};User=${DB_USER};Password=${DB_PASSWORD};"
  },
  "Jwt": {
    "Key": "${JWT_KEY}",
    "Issuer": "${JWT_ISSUER}",
    "Audience": "${JWT_AUDIENCE}"
  }
}
```
