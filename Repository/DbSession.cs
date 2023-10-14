using MySqlConnector;
using System.Data;

namespace Repository
{
    public class DbSession
    {
        public IDbConnection Connection()
        {
            return new MySqlConnection("server=localhost;database=banco_frame;User Id=root;password=1234;port=3306;Allow User Variables=True");
        }
    }
}
