[NET]:https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white
[MSSQL]:https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white
[Docker]:https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white
[C#]:https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white

<h1 align="center" style="font-weight: bold;">ShopApp 💻🛒 </h1>

![.Net][NET]
![Docker][Docker]
![C#][C#]
![MicrosoftSQLServer][MSSQL]

<h2 id="about">❓ About </h2> 
<p>Este projeto foi desenvolvido com o objetivo de praticar e aprimorar minhas habilidades no desenvolvimento de back-end web, utilizando as tecnologias mencionadas.</p> 
<p>A aplicação simula uma plataforma fictícia onde é possível cadastrar lojas e compradores, além de realizar buscas, cadastrar e comprar produtos. Os usuários também podem visualizar seus pedidos e gerenciar seus inventários.</p>

<h2 id="started">🚀 Getting started</h2>

Usando Docker você consegue rodar esse projeto com muita facilidade.

<h3>Prerequisites</h3>

- .NET 7.0
- Docker
- Microsoft SQL Server 2022

<h3>Setup</h3>

Pode optar por qualquer uma das duas opções abaixo:

- Caso tenha Visual Studio, você pode simplesmente abrir o projeto e clicar na opção abaixo:

![Screenshot 2024-10-20 174023](https://github.com/user-attachments/assets/95c0c9ff-5731-4976-940b-23c44d7e464a)


- Use esse comando na pasta raiz para orquestrar e iniciar o MS SQL Server e api juntos:
```bash
docker-compose up --build
``````


<h2 id="routes">📍 API Endpoints </h2>

Assim que o projeto estiver no ar, ele vai estar aberto na porta 8001, assim podendo acessar [aqui a documentação Swagger](http://localhost:8001/swagger/index.html).

![image](https://github.com/user-attachments/assets/967e78d5-ef5b-49cc-b60f-8efa6e7c9563)

