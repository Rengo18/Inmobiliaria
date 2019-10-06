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
        public int Id { get; set; }
        public String Direccion { get; set; }
        public String Uso { get; set; }
        public String Tipo { get; set; }
        public int Cantidad_Habitantes { get; set; }
        public Decimal Precio { get; set; }
        public String Estado { get; set; }
        public int PropietarioId { get; set; }


        public virtual Propietario Propietario { get; set; }

    }
}
