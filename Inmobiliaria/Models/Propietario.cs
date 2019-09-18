using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class Propietario
    {
        public int IdPropietario { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public int Dni { get; set; }
        public String Contraseña { get; set; }
        public String Direccion { get; set; }
        public String Email { get; set; }
        public int Telefono { get; set; }

    }
}
