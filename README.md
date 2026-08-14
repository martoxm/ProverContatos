# ProverContatos API

API RESTful desenvolvida em ASP.NET Core para gerenciamento de contatos, como parte de um processo seletivo para estágio em .NET.

## Funcionalidades

- Criar contato
- Listar contatos ativos
- Buscar contato por ID
- Editar contato
- Ativar contato
- Desativar contato
- Excluir contato

## Regras de negócio

- O contato deve ser maior de idade
- A idade é calculada automaticamente em tempo de execução
- A data de nascimento não pode ser maior que a data atual
- Apenas contatos ativos podem ser listados, visualizados ou editados

## Tecnologias utilizadas

- .NET
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- FluentValidation
- xUnit
- Moq
- FluentAssertions
- Swagger

## Arquitetura

O projeto foi organizado seguindo princípios de DDD e SOLID, com separação em camadas:

- `ProverContatos.Domain`
- `ProverContatos.Application`
- `ProverContatos.Infrastructure`
- `ProverContatos.Communication`
- `ProverContatos.Exception`
- `ProverContatos.Api`
- `ProverContatos.Tests`

## Como executar

1. Defina o projeto `ProverContatos.Api` como projeto de inicialização
2. Execute a aplicação
3. Acesse o Swagger para testar os endpoints

## Testes

Os testes unitários foram implementados com xUnit, utilizando Moq para mocks e FluentAssertions para as validações.

## Objetivo

Demonstrar conhecimentos em:

- C# e .NET
- API REST
- Arquitetura em camadas
- Boas práticas com SOLID
- Validação e tratamento de erros
- Testes automatizados