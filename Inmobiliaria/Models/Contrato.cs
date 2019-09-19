using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class Contrato
    {
        [Key]
        public int IdContrato { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaCierre { get; set; }
        public Decimal Monto { get; set; }
        public Inmueble Inmueble { get; set; }
        public Inquilino Inquilino { get; set; }

    }
}
