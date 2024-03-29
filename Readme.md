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
para acessar o postgres pelo adminer é necessário acessar o endereço http://localhost:8080 e preencher os campos:
 - System: PosgreSQL
 - database: db
 - Username: admin
 - Password: admin
 - Database: postgres

após isso é possível iniciar a aplicação navegando até a pasta "/src" e dando o comando 

```bash
dotnet run
```
ou para debug da aplicação.

```bash
dotnet watch run
```

Ao iniciar a aplicação todas tabelas do banco de dados serão criadas.

## Todo

 - Finalizar interação entre entidades na aplicação
 - Adicionar filas de mensagem para lidar com as operações de transação.
 - Autenticação e autorização de usuários.
 - Desenvolvimento do Front-end da aplicação.

