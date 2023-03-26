
# MINI PLOOMES API

### Introdução

Projeto desenvolvido como desafio 2 do teste técnico para a posição de Desenvolvedor back-end na empresa Ploomes. O repositório consiste no código da API Mini Ploomes, que simula a plataforma Ploomes. Com ela, é possível fazer operações de CRUD para usuários e clientes, além de outras operações adicionais.

### Serviços

Os serviços divididos por entidade, são:

##### Cliente:

- GET: Retorna uma lista de todos os clientes armazenados.
- GET/{idCliente}: Retorna um cliente a partir de seu identificador(ID)
- GET/Usuario/{idUsuario}: Retorna uma lista de clientes a partir do usuário a qual eles estiverem associados.
- PUT/{clienteId}: Atualiza dados do cliente a partir de um objeto de requisição e do Id do cliente.
- POST: Armazena um novo cliente no banco de dados.
- DELETE/{idCliente}: Deleta cliente a partir de seu Id.

##### Usuario:
- GET: Retorna todos os usuários armazenados.
- GET/{idUsuario}: Retorna o usuário pelo seu Id.
- POST: Armazena um novo usuário no banco de dados.
- PUT/{usuarioId}: Atualiza as informações de um usuário a partir de um objeto de requisição e de seu Id.
- DELETE/{usuarioId}: Deleta usuário a partir de seu Id.

### Tecnologias e Pacotes Usados:


- C#
- .NET v6
- Sql Server
- Swagger
- Swashbuckle.AspNetCore
- System.Data.SqlClient

### Informações Adicionais:
O scripT usado para criação estrutural e relacional (banco de dados, tabela, relacionamento, trigger) do banco de dados foi:

- Criar Banco:
```
    CREATE DATABASE Mini_Ploomes
```

- Para criar tabela de Usuário:
```
    CREATE TABLE usuario 
    (
    	IdUsuario int not null identity(1,1),
    	NomeUsuario varchar(max),
    	Email varchar(max),
        CONSTRAINT PK_USUARIO PRIMARY KEY (IdUsuario)
    )
```
- Para Criar tabela de Cliente e associá-la a tabela de Cliente: N:1
```
    CREATE TABLE cliente 
    (
    	IdCliente int not null identity(1,1),
    	IdUsuario int not null,
    	NomeCliente varchar(max),
    	DataDeCriacao datetime,
        CONSTRAINT PK_CLIENTE PRIMARY KEY (IdCliente),
    	CONSTRAINT FK_USUARIO FOREIGN KEY (IdUsuario)
    	REFERENCES usuario (IdUsuario)
    )
```
- Criar Tabela que possui as informações das entidades relacionadas:
```
    CREATE TABLE usuario_cliente (
       IdUsuario INT NOT NULL,
       IdCliente INT NOT NULL,
       NomeUsuario varchar(max),
       EmailUsuario varchar(max),
       NomeCliente varchar(max),
       DataCriacaoCliente datetime,
       PRIMARY KEY (IdUsuario, IdCliente ),
       FOREIGN KEY (IdUsuario) REFERENCES usuario(IdUsuario),
       FOREIGN KEY (IdCliente) REFERENCES cliente(IdCliente)
    );
 ```   
- Criar Trigger que executa o procedimento de povoar a tabela usuario_cliente automaticamente após um novo ciente ser adicionado:

```
    CREATE TRIGGER trigger_cliente_insert
    ON cliente
    AFTER INSERT
    AS
    BEGIN
        INSERT INTO usuario_cliente (IdUsuario, IdCliente, NomeUsuario, EmailUsuario, NomeCliente, DataCriacaoCliente)
        SELECT u.IdUsuario, i.IdCliente, u.NomeUsuario, u.Email, i.NomeCliente, i.DataDeCriacao
        FROM usuario u
        INNER JOIN inserted i ON u.IdUsuario = i.IdUsuario;
    END;
    
```

