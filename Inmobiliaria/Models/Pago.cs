using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class Pago
    {
        public int IdPago { get; set; }
        public String Estado { get; set; }
        public DateTime FechaPago { get; set; }
        public int Importe { get; set; }
        public int IdContrato { get; set; }

    }
}
