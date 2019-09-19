using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class Inmueble
    {
        [Key]
        public int IdInmueble { get; set; }
        public String Direccion { get; set; }
        public String Uso { get; set; }
        public String Tipo { get; set; }
        public int CantidadHabitanes { get; set; }
        public Decimal Precio { get; set; }
        public String Estado { get; set; }
        public Propietario Propietario { get; set; }

    }
}
