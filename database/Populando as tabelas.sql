
use ReservaLab;

-- tipo usuario
go
INSERT INTO tb_tipo_usuario (descricao) VALUES ('Aluno');
INSERT INTO tb_tipo_usuario (descricao) VALUES ('Professor');
INSERT INTO tb_tipo_usuario (descricao) VALUES ('Administrador');

-- laboratorio
go
INSERT INTO tb_laboratorio VALUES ('LAB001',2,'INFORMATICA',1);
INSERT INTO tb_laboratorio VALUES ('LAB002',1,'ENGENHARIA',1);
INSERT INTO tb_laboratorio VALUES ('LAB003',3,'INFORMATICA',1);

-- usuario
go
INSERT INTO tb_usuario VALUES (1,'Renato Silva','renato.slima@aluno.faculdadeimpacta.com.br','12345678',1,'12312312312','912345678');

-- reserva
go
INSERT INTO tb_reserva VALUES (1,2,'2023-08-05 16:30:00');

-- selecionando valores
SELECT * from tb_tipo_usuario;
SELECT * from tb_laboratorio;
SELECT * from tb_usuario;
SELECT * from tb_reserva;

-- truncando valores
TRUNCATE TABLE tb_tipo_usuario;
TRUNCATE TABLE tb_laboratorio;
TRUNCATE TABLE tb_usuario;
TRUNCATE TABLE tb_reserva;