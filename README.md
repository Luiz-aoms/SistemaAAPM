# Sistema AAPM

## Descrição do Projeto
O Sistema AAPM (Associação de Alunos e Pais de Mestres) é uma aplicação web desenvolvida em C# utilizando o framework ASP.NET Core MVC. O objetivo do sistema é gerenciar e centralizar as informações da associação, oferecendo controle sobre associados, cursos oferecidos, salas disponíveis e eventos organizados.

Um dos diferenciais técnicos deste projeto é a utilização de ADO.NET puro (com `DataTable` e comandos SQL manuais) em vez de ferramentas de ORM (como o Entity Framework) para a camada de persistência de dados. Isso garante um controle rigoroso e otimizado sobre as consultas enviadas ao banco de dados.

## Funcionalidades
* Autenticação e Autorização de usuários (Login, Registro e Logout seguro).
* Dashboard gerencial interativo com totalizadores em tempo real.
* Gerenciamento (CRUD completo: Criar, Ler, Atualizar e Excluir) das seguintes entidades:
  * Associados
  * Cursos
  * Salas
  * Eventos
* Tratamento de regras de negócio e restrições de banco de dados (Foreign Keys) com feedback visual amigável para o usuário.

## Tecnologias Utilizadas
* Backend: C# 
* Framework: ASP.NET Core MVC
* Acesso a Dados: ADO.NET (System.Data.SqlClient)
* Frontend: HTML5, CSS3, e Razor Views (.cshtml)
* Banco de Dados: SQL Server

## Estrutura do Projeto
* /Controllers: Contém os controladores da aplicação (`AccountController`, `AssociadoController`, `CursoController`, `SalaController`, `EventoController` e `DashboardController`), responsáveis por processar as requisições HTTP e retornar as Views.
* /BancoDados: Camada de persistência contendo as classes que interagem diretamente com o banco de dados via ADO.NET.
* /ViewModels: Classes de transferência de dados (DTOs) que transportam informações de forma segura entre a View e o Controller.
* /Views: Páginas da interface de usuário construídas com a sintaxe Razor.

## Como Executar o Projeto Localmente

### Pré-requisitos
* .NET SDK instalado na máquina.
* Visual Studio ou Visual Studio Code.
* SQL Server (LocalDB ou instância dedicada).

### Configuração do Banco de Dados
1. Abra o seu gerenciador do SQL Server (ex: SQL Server Management Studio).
2. Crie um banco de dados chamado `BdAAPM`.
3. Execute os scripts SQL de criação das tabelas (`tb_associados`, `tb_curso`, etc.) e das restrições de chaves estrangeiras.
4. Caso exista um arquivo de script `.sql` na pasta do projeto, execute-o para gerar a estrutura inicial.

