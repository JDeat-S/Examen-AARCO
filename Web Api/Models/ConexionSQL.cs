using Microsoft.Data.SqlClient;
using System.Data;

namespace Web_Api.Models
{

    public class ConexionSQL
    {
        public DataSet Conexion(string query)
        {
            try
            {
                SqlDataAdapter adapter = null;
                DataSet dsDatos = new DataSet();


                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "JDEAT\\SQLEXPRESS";
                builder.UserID = "sa";
                builder.Password = "JDeat5577";
                builder.InitialCatalog = "Examen_AARCO";
                builder.TrustServerCertificate= true;
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (command)
                        {
                            adapter = new SqlDataAdapter(command);

                            adapter.Fill(dsDatos);
                            return dsDatos;
                        }
                    }
                }
            }
            catch (Exception)
            {

                return null;
            }

        }

    }
}
