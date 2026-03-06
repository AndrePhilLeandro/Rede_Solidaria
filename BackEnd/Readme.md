# Documentação da API - Cadastro de Usuários e ONGs

## Visão Geral

Esta API foi desenvolvida em **ASP.NET Core** e tem como objetivo gerenciar o cadastro, autenticação e edição de **Usuários** e **ONGs**.  
A API utiliza:

- **Controllers** para expor endpoints HTTP
- **Injeção de dependência** para serviços e acesso a dados
- **Validação de dados** através de serviços de validação
- **Hash de senha** utilizando `PasswordHasher`
- **Autorização baseada em roles** usando `Authorize`

Os principais recursos disponíveis são:

- Cadastro de usuários
- Login de usuários
- Edição de usuários

- Cadastro de ONGs
- Login de ONGs
- Consulta de ONGs
- Edição de ONGs

---

# Estrutura da API

A API possui dois controllers principais:

- `UsuarioController`
- `OngsController`

Cada controller é responsável por manipular operações relacionadas ao seu domínio.