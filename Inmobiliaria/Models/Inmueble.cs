using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class Inmueble
    {
        public int IdInmueble { get; set; }
        public String Direccion { get; set; }
        public String Uso { get; set; }
        public String Tipo { get; set; }
        public int CantidadHabitanes { get; set; }
        public float Precio { get; set; }
        public String Estado { get; set; }
        public int IdPropietario { get; set; }

    }
}
