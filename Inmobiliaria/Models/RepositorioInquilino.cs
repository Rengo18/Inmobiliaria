using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class RepositorioInquilino : RepositorioBase, IRepositorio<Inquilino>
    {
        public RepositorioInquilino(IConfiguration configuration) : base(configuration)
        {

        }
        public int Alta(Inquilino i)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String sql = "INSERT INTO Inquilinos (dni,nombre,apellido,direccion,email,telefono,lugar_trabajo) " +$"VALUES('{i.Dni}', '{i.Nombre}', '{i.Apellido}','{i.Direccion}','{i.Email}','{i.Telefono}','{i.LugarTrabajo}')";
                using(SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    var id = command.ExecuteScalar();
                    i.IdInquilino = Convert.ToInt32(id);
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
                string sql = $"DELETE FROM Inquilino WHERE Id = {id}";
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

        public int Modificacion(Inquilino i)
        {

            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Inquilino SET nombre='{i.Nombre}', Apellido='{i.Apellido}', dni'={i.Dni}', telefono'={i.Telefono}', email'={i.Email}', direccion'={i.Direccion}', lugar_trabajo'={i.LugarTrabajo}'" + $"WHERE Id = {i.IdInquilino}";

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

        public Inquilino ObtenerPorId(int id)
        {
            Inquilino i = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT Id, nombre, apellido, dni, telefono, email, contraseña,direccion,lugar_trabajo FROM Inquilino" + $" WHERE Id=@id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        i = new Inquilino
                        {
                            IdInquilino = reader.GetInt32(0),
                            Nombre = reader["nombre"].ToString(),
                            Apellido = reader["apellido"].ToString(),
                            Dni = (int)reader["dni"],
                            Telefono = (long)reader["telefono"],
                            Email = reader["email"].ToString(),
                            Direccion = reader["direccion"].ToString(),
                            LugarTrabajo = reader["lugar_trabajo"].ToString(),
                           
                        };
                    }
                    connection.Close();
                }
            }
            return i;
        }
    

        public IList<Inquilino> ObtenerTodos()
        {
        IList<Inquilino> res = new List<Inquilino>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string sql = $"SELECT Id, nombre, apellido, dni, telefono, email, contraseña,direccion,lugar_trabajo FROM Inquilino";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Inquilino i = new Inquilino
                    {
                        IdInquilino = reader.GetInt32(0),
                        Nombre = reader["nombre"].ToString(),
                        Apellido = reader["apellido"].ToString(),
                        Dni = (int)reader["dni"],
                        Telefono = (long)reader["telefono"],
                        Email = reader["email"].ToString(),
                        Direccion = reader["direccion"].ToString(),
                        LugarTrabajo = reader["lugar_trabajo"].ToString(),                  
                    };
                    res.Add(i);
                }
                connection.Close();
            }
        }
        return res;
    }
    }
}
