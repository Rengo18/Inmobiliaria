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
        public int Id{ get; set; }
        [Display(Name = "Fecha de inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        public DateTime FechaInicio { get; set; }
        [Display(Name = "Fecha de Cierre")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaCierre { get; set; }
        public Decimal Monto { get; set; }
        public int InmuebleId { get; set; }

        public int InquilinoId { get; set; }



        public virtual Inmueble Inmueble { get; set; }
        public virtual Inquilino Inquilino { get; set; }

    }
}
