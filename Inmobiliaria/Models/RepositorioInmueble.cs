using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class RepositorioInmueble : RepositorioBase, IRepositorio<Inmueble>
    {
        public RepositorioInmueble(IConfiguration configuration) : base(configuration)
        {
        }
        public int Alta(Inmueble i)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String sql = "INSERT INTO Contrato (direccion,uso,tipo,cantidad_habitantes,precio,estado,propietario_id)" + $"VALUES('{i.Direccion}', {i.Uso}', {i.Tipo}', {i.CantidadHabitanes}', {i.Precio}', {i.Estado}', {i.Propietario}' )"
                    ;
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    var id = command.ExecuteScalar();
                    i.IdInmueble = Convert.ToInt32(id);
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
                string sql = $"DELETE FROM Inmueble WHERE Id = {id}";
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

        public int Modificacion(Inmueble i)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Inmueble SET direccion='{i.Direccion}', uso='{i.Uso}', tipo'={i.Tipo}', cantidad_habitantes'={i.CantidadHabitanes}', precio'={i.Precio}', estado'={i.Estado}', propietario_id'={i.Propietario}'" +
                    $"WHERE id = {i.IdInmueble}";
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

        public Inmueble ObtenerPorId(int id)
        {
            Inmueble i = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT direccion,uso,tipo,cantidad_habitantes,precio,estado,propietario_id FROM Inmueble" +
                    $" WHERE Id=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        i = new Inmueble
                        {
                            IdInmueble = reader.GetInt32(0),
                            Direccion = reader["direccion"].ToString(),
                            Uso = reader["uso"].ToString(),
                            Tipo = reader["tipo"].ToString(),
                            CantidadHabitanes = (int)reader["cantidad_habitantes"],
                            Precio = (Decimal)reader["precio"],
                            Estado = reader["tipo"].ToString(),
                            Propietario = (Propietario)reader["propietario_id"],

                        };
                    }
                    connection.Close();
                }
            }
            return i;
        }

        public IList<Inmueble> ObtenerTodos()
        {
            IList<Inmueble> res = new List<Inmueble>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT direccion,uso,tipo,cantidad_habitantes,precio,estado,propietario_id FROM Inmueble";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Inmueble c = new Inmueble
                        {
                            IdInmueble = reader.GetInt32(0),
                            Direccion = reader["direccion"].ToString(),
                            Uso = reader["uso"].ToString(),
                            Tipo = reader["tipo"].ToString(),
                            CantidadHabitanes = (int)reader["cantidad_habitantes"],
                            Precio = (Decimal)reader["precio"],
                            Estado = reader["tipo"].ToString(),
                            Propietario = (Propietario)reader["propietario_id"],

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


