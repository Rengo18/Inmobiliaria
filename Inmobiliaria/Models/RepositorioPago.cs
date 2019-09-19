using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class RepositorioPago : RepositorioBase, IRepositorio<Pago>
    {
        public RepositorioPago(IConfiguration configuration) : base(configuration)
        {

        }
        public int Alta(Pago p)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String sql = "INSERT INTO Pago (fecha_pago,importe,estado,contrato_id) " + $"VALUES('{p.FechaPago}', '{p.Importe}', '{p.Estado}','{p.Contrato}')";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    var id = command.ExecuteScalar();
                    p.IdPago = Convert.ToInt32(id);
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
                string sql = $"DELETE FROM Pago WHERE Id = {id}";
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

        public int Modificacion(Pago p)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Pago SET fecha_pago='{p.FechaPago}', importe='{p.Importe}', estado'={p.Estado}', contrato_id'={p.Contrato}'" +
                    $"WHERE id = {p.IdPago}";
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

        public Pago ObtenerPorId(int id)
        {
            Pago p = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT fecha_pago,importe,estado,contrato_id FROM Pago" +
                    $" WHERE Id=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        p = new Pago
                        {
                            IdPago = reader.GetInt32(0),
                            FechaPago = (DateTime)reader["fecha_pago"],
                            Importe = (Decimal)reader["importe"],
                            Estado = reader["estado"].ToString(),
                            Contrato = (Contrato)reader["contrato_id"],
                            

                        };
                    }
                    connection.Close();
                }
            }
            return p;
        }

        public IList<Pago> ObtenerTodos()
        {
            IList<Pago> res = new List<Pago>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT fecha_pago,importe,estado,contrato_id FROM Pago";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Pago c = new Pago
                        {
                            IdPago = reader.GetInt32(0),
                            FechaPago = (DateTime)reader["fecha_pago"],
                            Importe = (Decimal)reader["importe"],
                            Estado = reader["estado"].ToString(),
                            Contrato = (Contrato)reader["contrato_id"],

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

