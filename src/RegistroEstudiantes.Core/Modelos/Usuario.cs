namespace RegistroEstudiantes.Core.Modelos;

public sealed class Usuario
{
    public int IdUsuario { get; set; }

    public string NombreCompleto { get; set; } = string.Empty;

    public string NombreUsuario { get; set; } = string.Empty;

    public string ClaveHash { get; set; } = string.Empty;

    public string Rol { get; set; } = "Operador";

    public bool Activo { get; set; } = true;
}
