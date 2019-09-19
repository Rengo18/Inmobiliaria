using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class RepositorioContrato : RepositorioBase, IRepositorio<Contrato>
    {
        public RepositorioContrato(IConfiguration configuration) : base(configuration)
        {

        }
        public int Alta(Contrato c)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String sql = "INSERT INTO Contrato (fecha_inicio,fecha_cierre,monto,inmueble_id,inquilino_id) "+ $"VALUES('{c.FechaInicio}', '{c.FechaCierre}', '{c.Monto}','{c.Inmueble}','{c.Inquilino}' )";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    var id = command.ExecuteScalar();
                    c.IdContrato = Convert.ToInt32(id);
                    connection.Close();
                }
            }
            return res;
        }

        public int Baja(int id)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"DELETE FROM Contrato WHERE Id = {id}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public int Modificacion(Contrato c)
        {

            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Contrato SET fecha_inicio='{c.FechaInicio}', fecha_cierre='{c.FechaCierre}', monto'={c.Monto}', inmueble_id'={c.Inmueble}', inquilino_id'={c.Inquilino}'" +
                    $"WHERE id = {c.IdContrato}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public Contrato ObtenerPorId(int id)
        {
            Contrato c = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT fecha_inicio,fecha_cierre,monto,inmueble_id,inquilino_id FROM Contrato" +
                    $" WHERE Id=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        c = new Contrato
                        {
                            IdContrato = reader.GetInt32(0),
                            FechaInicio = (DateTime)reader["fecha_inicio"],
                            FechaCierre = (DateTime)reader["fecha_cierre"],
                            Monto = (Decimal)reader["monto"],
                            Inmueble = (Inmueble)reader["inmueble_id"],
                            Inquilino = (Inquilino)reader["inquilino_id"],
                       
                        };
                    }
                    connection.Close();
                }
            }
            return c;
        }


        public IList<Contrato> ObtenerTodos()
        {
            IList<Contrato> res = new List<Contrato>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT fecha_inicio,fecha_cierre,monto,inmueble_id,inquilino_id FROM Contrato";
                    
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Contrato c = new Contrato
                        {
                            IdContrato = reader.GetInt32(0),
                            FechaInicio = (DateTime)reader["fecha_inicio"],
                            FechaCierre= (DateTime)reader["fecha_cierre"],
                            Monto = (Decimal)reader["monto"],
                            Inmueble = (Inmueble)reader["inmueble_id"],
                            Inquilino =(Inquilino) reader["inquilino_id"],
                            
                        };
                        res.Add(c);
                    }
                    connection.Close();
                }
            }
            return res;
        }
    }


}

        

