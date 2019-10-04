using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class Propietario
    {
        
        [Key]
        public int Id { get; set; }
        [Required]
        public String Nombre { get; set; }
        [Required]
        public String Apellido { get; set; }
        [Required]
        public int Dni { get; set; }
        [DataType(DataType.Password)]
        public String Clave { get; set; }
        
        public String Domicilio { get; set; }
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }
       
        public long Telefono { get; set; }

    }
}
