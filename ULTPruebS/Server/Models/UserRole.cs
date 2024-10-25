using System;
using System.Collections.Generic;

namespace ULTPruebS.Server.Models
{
    public class UserRole
    {
        public int idUserRole { get; set; }
        public int idUsuario { get; set; }
        public string Rol { get; set; }

        // Relación con Usuarios
        public Usuario Usuario { get; set; }

    }
}
