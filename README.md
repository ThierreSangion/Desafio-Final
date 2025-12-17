# OnlineStore API

API REST desenvolvida em .NET 8 seguindo o padrão arquitetural MVC para gerenciamento de produtos de uma loja online.

## 🏗️ Arquitetura

Este projeto implementa o padrão **MVC (Model-View-Controller)** com separação de responsabilidades em camadas:

```
OnlineStore.Api/
├── Controllers/       → Camada de Apresentação (gerencia requisições HTTP)
│   └── ProdutosController.cs
├── Services/         → Camada de Lógica de Negócios
│   ├── IProdutoService.cs
│   └── ProdutoService.cs
├── Models/           → Camada de Domínio (entidades)
│   └── Produto.cs
├── Data/             → Camada de Acesso a Dados
│   └── AppDbContext.cs
└── Program.cs        → Configuração e Bootstrap da aplicação
```

### Fluxo de Dados

```
Cliente HTTP → Controller → Service → AppDbContext → Banco de Dados
                    ↓           ↓           ↓
                 Validação   Regras    Persistência
```

## 🚀 Tecnologias Utilizadas

- **.NET 8** - Framework principal
- **ASP.NET Core** - Framework web
- **Entity Framework Core** - ORM para acesso a dados
- **InMemoryDatabase** - Banco de dados em memória para desenvolvimento
- **Swagger/OpenAPI** - Documentação automática da API

## 📋 Funcionalidades

A API implementa operações CRUD completas e funcionalidades adicionais:

### Endpoints Disponíveis

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/api/v1/produtos` | Lista todos os produtos |
| `GET` | `/api/v1/produtos/{id}` | Busca produto por ID |
| `GET` | `/api/v1/produtos/pesquisar?nome={nome}` | Busca produtos por nome |
| `GET` | `/api/v1/produtos/contar` | Retorna total de produtos |
| `POST` | `/api/v1/produtos` | Cria novo produto |
| `PUT` | `/api/v1/produtos/{id}` | Atualiza produto existente |
| `DELETE` | `/api/v1/produtos/{id}` | Remove produto |

### Modelo de Dados - Produto

```json
{
  "id": 1,
  "nome": "Notebook",
  "descricao": "Notebook básico",
  "preco": 3500.00,
  "estoque": 10
}
```

## 🔧 Como Executar

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) instalado

### Passos

1. Clone o repositório:
```bash
git clone <url-do-repositorio>
cd OnlineStore-api
```

2. Navegue até o diretório do projeto:
```bash
cd OnlineStore.Api
```

3. Execute a aplicação:
```bash
dotnet run
```

4. Acesse a documentação Swagger:
```
https://localhost:7XXX/swagger
```

## 📝 Exemplos de Uso

### Listar todos os produtos
```bash
GET /api/v1/produtos
```

**Resposta:**
```json
[
  {
    "id": 1,
    "nome": "Notebook",
    "descricao": "Notebook básico",
    "preco": 3500.00,
    "estoque": 10
  },
  {
    "id": 2,
    "nome": "Mouse",
    "descricao": "Mouse sem fio",
    "preco": 120.00,
    "estoque": 50
  }
]
```

### Buscar produto por ID
```bash
GET /api/v1/produtos/1
```

**Resposta:**
```json
{
  "id": 1,
  "nome": "Notebook",
  "descricao": "Notebook básico",
  "preco": 3500.00,
  "estoque": 10
}
```

### Criar novo produto
```bash
POST /api/v1/produtos
Content-Type: application/json

{
  "nome": "Teclado Mecânico",
  "descricao": "Teclado RGB",
  "preco": 450.00,
  "estoque": 15
}
```

**Resposta:** `201 Created`

### Atualizar produto
```bash
PUT /api/v1/produtos/1
Content-Type: application/json

{
  "nome": "Notebook Gamer",
  "descricao": "Notebook de alta performance",
  "preco": 5500.00,
  "estoque": 5
}
```

**Resposta:** `200 OK`

### Deletar produto
```bash
DELETE /api/v1/produtos/1
```

**Resposta:** `204 No Content`

### Buscar por nome
```bash
GET /api/v1/produtos/pesquisar?nome=Mouse
```

### Contar produtos
```bash
GET /api/v1/produtos/contar
```

**Resposta:**
```json
3
```

## 🏛️ Padrões e Boas Práticas

- ✅ **Separação de Responsabilidades** - Cada camada tem função específica
- ✅ **Injeção de Dependência** - Uso de interfaces para desacoplamento
- ✅ **Async/Await** - Operações assíncronas para melhor performance
- ✅ **RESTful Design** - Verbos HTTP e status codes corretos
- ✅ **Versionamento de API** - Suporte a múltiplas versões (v1)
- ✅ **Validações** - Tratamento de casos nulos e vazios
- ✅ **AsNoTracking** - Otimização de queries de leitura

## 📊 Estrutura de Componentes

### Controller (ProdutosController)
Responsável por:
- Receber requisições HTTP
- Validar entrada
- Delegar lógica para o Service
- Retornar respostas HTTP apropriadas

### Service (ProdutoService)
Responsável por:
- Implementar regras de negócio
- Validações de domínio
- Orquestrar operações de dados

### Model (Produto)
Responsável por:
- Representar entidade de domínio
- Definir estrutura de dados

### Data (AppDbContext)
Responsável por:
- Configurar Entity Framework
- Gerenciar conexão com banco de dados
- Mapear entidades

## 🧪 Testes

O projeto inclui dados de seed para facilitar testes:

- **Notebook** - R$ 3.500,00 (10 unidades)
- **Mouse** - R$ 120,00 (50 unidades)
- **Teclado** - R$ 280,00 (20 unidades)

### Testando com Postman

O projeto inclui uma collection do Postman pronta para uso: `OnlineStore.postman_collection.json`

**Como importar:**

1. Abra o Postman
2. Clique em **Import**
3. Selecione o arquivo `OnlineStore.postman_collection.json`
4. A collection será importada com todos os endpoints configurados

**Variáveis de ambiente:**

A collection já vem com variáveis pré-configuradas:
- `baseUrl`: `https://localhost:7000` (ajuste conforme sua porta)
- `id`: `1` (ID de exemplo para testes)
- `nome`: `Mouse` (nome de exemplo para busca)

**Endpoints incluídos:**

- ✅ Listar todos os produtos
- ✅ Buscar produto por ID
- ✅ Buscar produtos por nome
- ✅ Contar produtos
- ✅ Criar produto
- ✅ Atualizar produto
- ✅ Excluir produto

### Executando Testes Unitários

O projeto inclui testes unitários no projeto `OnlineStore.Api.Tests`.

**Como executar os testes:**

1. Navegue até a pasta raiz do projeto:
```bash
cd OnlineStore-api
```

2. Execute todos os testes:
```bash
dotnet test
```

3. Execute com detalhes:
```bash
dotnet test --verbosity detailed
```

4. Execute com cobertura de código:
```bash
dotnet test /p:CollectCoverage=true
```

**Estrutura de testes:**

```
OnlineStore.Api.Tests/
├── Controllers/
│   └── ProdutosControllerTests.cs    # Testes do Controller
├── Services/
│   └── ProdutoServiceTests.cs        # Testes do Service
└── OnlineStore.Api.Tests.csproj
```

**Exemplo de saída esperada:**

```
Iniciando execução de teste, espere...
Total de 1 arquivos de teste corresponderam ao padrão especificado.

Aprovado!  - Com falha:     0, Aprovado:    15, Ignorado:     0, Total:    15
```

**Testes implementados:**

- ✅ Testes de Controller (validação de rotas e respostas HTTP)
- ✅ Testes de Service (lógica de negócios)
- ✅ Testes de integração com banco de dados em memória
- ✅ Testes de validação de dados
- ✅ Testes de casos de erro (NotFound, BadRequest)

## 📄 Licença

Este projeto foi desenvolvido como parte de um desafio acadêmico de Arquitetura de Software.

## 👨‍💻 Autor

Desenvolvido como projeto final do módulo de Arquitetura de Software.
