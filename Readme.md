# Desafio - Aplicação Web Banco 

Desafio desenvolvido com intuito de simular uma aplicação web de banco.

Sendo possível cadastrar usuários, criar contas bancárias para os mesmos.

Consultar seus dados, seu saldo e fazer transações de crédito ou débito.

## Como rodar o projeto.

navegue até a pasta onde se localiza o arquivo "docker-compose.yml" e no terminal dê o comando:

```bash
 docker-compose up -d
```

O container será iniciado com a imagem do postgreSQL e o adminer como client de acesso.

após isso é possível iniciar a aplicação navegando até a pasta "/src" e dando o comando 

```bash
dotnet run
```
ou 
```bash
dotnet watch run
```

no caso de debuggar a aplicação.