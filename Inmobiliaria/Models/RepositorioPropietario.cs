using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class RepositorioPropietario : RepositorioBase , IRepositorio<Propietario>
    {
        public RepositorioPropietario(IConfiguration configuration) : base(configuration)
        {

        }

        public int Alta(Propietario p)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Propietario (nombre, apellido, dni, telefono, email, contraseña,direccion) " +
                    $"VALUES ('{p.Nombre}', '{p.Apellido}','{p.Dni}','{p.Telefono}','{p.Email}','{p.Contraseña}','{p.Direccion}')";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    var id = command.ExecuteScalar();
                    p.IdPropietario = Convert.ToInt32(id);
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
                string sql = $"DELETE FROM Propietario WHERE Id = {id}";
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
        public int Modificacion(Propietario p)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Propietario SET nombre='{p.Nombre}', Apellido='{p.Apellido}', dni'={p.Dni}', telefono'={p.Telefono}', email'={p.Email}', contraseña'={p.Contraseña}', direccion'={p.Direccion}' " +
                    $"WHERE Id = {p.IdPropietario}";
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

        public IList<Propietario> ObtenerTodos()
        {
            IList<Propietario> res = new List<Propietario>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT Id, nombre, apellido, dni, telefono, email, contraseña,direccion" +
                    $" FROM Propietario";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Propietario p = new Propietario
                        {
                            IdPropietario = reader.GetInt32(0),
                            Nombre = reader["nombre"].ToString(),
                            Apellido = reader["apellido"].ToString(),
                            Dni = (int)reader["dni"],
                            Telefono = (int)reader["telefono"],
                            Email = reader["email"].ToString(),
                            Contraseña = reader["contraseña"].ToString(),
                            Direccion = reader["direccion"].ToString(),
                        };
                        res.Add(p);
                    }
                    connection.Close();
                }
            }
            return res;
        }

        public Propietario ObtenerPorId(int id)
        {
            Propietario p = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT Id, nombre, apellido, dni, telefono, email, contraseña,direccion FROM Propietario" +
                    $" WHERE Id=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        p = new Propietario
                        {
                            IdPropietario = reader.GetInt32(0),
                            Nombre = reader["nombre"].ToString(),
                            Apellido = reader["apellido"].ToString(),
                            Dni = (int)reader["dni"],
                            Telefono = (int)reader["telefono"],
                            Email = reader["email"].ToString(),
                            Contraseña = reader["contraseña"].ToString(),
                            Direccion = reader["direccion"].ToString(),
                        };
                    }
                    connection.Close();
                }
            }
            return p;
        }

    }
}
