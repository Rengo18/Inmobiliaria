using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class Contrato
    {
        public int IdContrato { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaCierre { get; set; }
        public float Monto { get; set; }
        public int IdInmueble { get; set; }
        public int IdInquilino { get; set; }

    }
}
