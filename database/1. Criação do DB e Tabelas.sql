
-- criando o banco de dados do projeto
create database ReservaLab;

-- utilizando o banco
use ReservaLab;

GO
-- criando a tabela tipo usuário
CREATE TABLE tb_tipo_usuario (

id_tipo_usuario INT IDENTITY PRIMARY KEY,
descricao		VARCHAR(15)

);
GO

-- criando a tabela laboratorio
CREATE TABLE tb_laboratorio (

id_laboratorio INT IDENTITY PRIMARY KEY,
nome_laboratorio VARCHAR(10),
andar_laboratorio INT,
descricao_laboratorio VARCHAR(50),
is_activate BIT
);
GO

-- criando a tabela de usuários
CREATE TABLE tb_usuario (

id_usuario INT IDENTITY PRIMARY KEY,
id_tipo_usuario INT,
nome_usuario VARCHAR(100),
email_usuario VARCHAR(100),
senha_usuario VARCHAR(100),
is_activate BIT,
cpf_cnpj_usuario VARCHAR(20),
telefone_usuario VARCHAR(12)

CONSTRAINT FK_USER_TYPE FOREIGN KEY (id_tipo_usuario) REFERENCES tb_tipo_usuario(id_tipo_usuario)
);
GO

-- criando a tabela de reservas
CREATE TABLE tb_reserva (

id_reserva		INT IDENTITY PRIMARY KEY,
id_usuario		INT,
id_laboratorio	INT,
dia_horario_reserva SMALLDATETIME

CONSTRAINT FK_USER FOREIGN KEY (id_usuario) REFERENCES tb_usuario(id_usuario),
CONSTRAINT FK_LAB FOREIGN KEY (id_laboratorio) REFERENCES tb_laboratorio(id_laboratorio)

)

-- deletar
GO
DROP TABLE tb_laboratorio;
GO
DROP TABLE tb_usuario;
GO
DROP TABLE tb_tipo_usuario;
GO
DROP TABLE tb_reserva;