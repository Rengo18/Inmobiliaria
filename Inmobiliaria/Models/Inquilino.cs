using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class Inquilino
    {   [Key]
        public int IdInquilino { get; set; }

        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public int Dni { get; set; }
        public String Direccion { get; set; }
        public String Email { get; set; }
        public int Telefono { get; set; }
        public String LugarTrabajo { get; set; }
        public int DniGarante { get; set; }
        public String NombreGarante { get; set; }
        public String ApellidoGarante { get; set; }
        public String DireccionGarante { get; set; }
        public String EmailGarante { get; set; }
        public int TelefonoGarante { get; set; }
        public String LugarTrabajoGarante { get; set; }


    }
}
