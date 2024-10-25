using System;
using System.Collections.Generic;

namespace ULTPruebS.Server.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string ContraseñaHash { get; set; } = null!;
    // Relación con UserRole
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();    // Agregar esta propiedad

}
