using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class Inquilino
    {   [Key]
        public int Id { get; set; }

        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public int Dni { get; set; }
        public String Domicilio { get; set; }
        public String Email { get; set; }
        public long Telefono { get; set; }
        public String Lugar_Trabajo { get; set; }
      
    }
}
