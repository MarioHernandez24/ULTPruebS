﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULTPruebS.Shared
{
    public class CategoriaDTO
    {
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string NombreCategoria { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public bool Estado { get; set; } // Cambiado a bool
        public string EstadoTexto => Estado ? "activo" : "inactivo";
    }
}
