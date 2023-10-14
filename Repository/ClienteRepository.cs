using Dapper;
using Domain.Model;

namespace Repository
{
    public class ClienteRepository
    {
        private DbSession _connection { get; set; }
        
        public ClienteRepository()
        {
            _connection = new DbSession();
        }

        public Cliente GetbyId(int id)
        {
            var sql = "SELECT id, nome, cpf, saldo FROM clientes WHERE id=@id";

            using (var conexao = _connection.Connection())
            {
                conexao.Open();

                return  conexao.QueryFirstOrDefault<Cliente>(sql, new { id });
            }
        }

        public void Insert(Cliente cliente)
        {
            var sql = "INSERT INTO clientes (id, nome, cpf, saldo) VALUES (@id, @nome, @cpf, @saldo)";

            using (var conexao = _connection.Connection())
            {
                conexao.Open();

                conexao.Execute(sql, new { id = cliente.Id, 
                                                             nome = cliente.Nome, 
                                                             cpf = cliente.Cpf, 
                                                             saldo = cliente.Saldo  });
            }
        }

        public void UpdateSaldo(Cliente cliente)
        {
            var sql = "UPDATE clientes SET saldo=@saldo WHERE id=@id";

            using (var conexao = _connection.Connection())
            {
                conexao.Open();

                conexao.Execute(sql, new
                {
                    id = cliente.Id,
                    saldo = cliente.Saldo
                });
            }
        }


    }
}
