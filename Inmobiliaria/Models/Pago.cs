using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class Pago
    {
        [Key]
        public int IdPago { get; set; }
        public String Estado { get; set; }
        public DateTime FechaPago { get; set; }
        public Decimal Importe { get; set; }
        public Contrato Contrato { get; set; }

    }
}
