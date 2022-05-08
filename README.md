<h1 align="center"> Fullstack App de Análise de Transações </h1>

<p align="">
  <img src="http://img.shields.io/static/v1?label=STATUS&message=DEPLOYED&color=GREEN&style=for-the-badge"/>
</p>
<p>
Uma aplicação fullstack desenvolvida em C# utilizando .NET 6 padrão MVC com DDD para solucionar o desafio proposto na semana 3 da Alura Backend Challenges. Esta aplicação permite a autenticação de usuários com salvamento de senha criptografada em banco de dados Postgre, criação, edição e exclusão lógica de usuários, envio de arquivos .csv e .xml para cadastro de transações, visualização de transações e análise de transações suspeitas em mês e ano especificados. <a href="https://transactionsaluracsv-production.up.railway.app/">Você pode testar a aplicação aqui </a>
</p>

## 🛠️ Abrir e rodar o projeto

Para rodar o projeto, será necessário:
- fazer o clone do repositório do github para uma pasta local
- criar uma arquivo para definir as variáveis de ambiente .env dentro da pasta `TransactionsAluraCSV.Presentation`
  - definir a variável de ambiente para acesso ao banco de dados Postgre local
  - definir mais duas variáveis de ambiente: EMAIL_SECRET_KEY e EMAIL_API_KEY, para integração com o serviço de email _MailJet_ (é necessário se cadastrar e criar uma conta)
- Realizar a instalação dos pacotes do Nu-get utilizados
- Realizar a configuração do banco de dados de acordo com o padrão **_code-first_** utilizado pelo Entity Framework


## Stacks utilizadas

<div style="padding: 0.5rem">
    <img align="center" height="30" width="40" src="https://raw.githubusercontent.com/devicons/devicon/1119b9f84c0290e0f0b38982099a2bd027a48bf1/icons/csharp/csharp-original.svg">
    C#
</div>
<div style="padding: 0.5rem">
    <img align="center" height="30" width="40" src="https://raw.githubusercontent.com/devicons/devicon/1119b9f84c0290e0f0b38982099a2bd027a48bf1/icons/dotnetcore/dotnetcore-original.svg">
  Microsoft ASP.NET Core
</div>
<div style="padding: 0.5rem">
    <img align="center" height="30" width="40" src="https://raw.githubusercontent.com/devicons/devicon/1119b9f84c0290e0f0b38982099a2bd027a48bf1/icons/postgresql/postgresql-original.svg">
PostgreSQL
</div>
<div style="padding: 0.5rem">
    <img align="center" height="30" width="40" src="https://raw.githubusercontent.com/devicons/devicon/1119b9f84c0290e0f0b38982099a2bd027a48bf1/icons/bulma/bulma-plain.svg">
    Bulma
</div>
<div style="padding: 0.5rem">
    <img align="center" height="30" width="40" src="https://raw.githubusercontent.com/devicons/devicon/1119b9f84c0290e0f0b38982099a2bd027a48bf1/icons/docker/docker-plain-wordmark.svg">
    Docker
</div>

## Boas práticas
- ``Domain Driven Design - DDD``
- ``SOLID``
- ``Clean Code``
