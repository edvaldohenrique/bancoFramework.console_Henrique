# Projeto base para bancoframework.console
Meu primeiro projeto PDI(bancoframework.console) - c#

# Scripts
UPDATE clientes SET saldo=@saldo WHERE id=@id

INSERT INTO clientes (id, nome, cpf, saldo) VALUES (@id, @nome, @cpf, @saldo)

SELECT id, nome, cpf, saldo FROM clientes WHERE id=@id