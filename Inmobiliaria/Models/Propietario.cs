﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    public class Propietario
    {
        
        [Key]
        public int IdPropietario { get; set; }
        [Required]
        public String Nombre { get; set; }
        [Required]
        public String Apellido { get; set; }
        [Required]
        public int Dni { get; set; }
        [DataType(DataType.Password)]
        public String Clave { get; set; }
        [Required]
        public String Direccion { get; set; }
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }
        [Required]
        public long Telefono { get; set; }

    }
}
