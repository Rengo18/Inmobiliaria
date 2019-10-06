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
        public int Id { get; set; }    
        
        [Display(Name = "Fecha de Pago")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaPago { get; set; }
        public Decimal Importe { get; set; }
        public String Estado { get; set; }

        public int ContratoId { get; set; }
        public int NroPago { get; set; }
        public virtual Contrato Contrato { get; set; }

    }
}
