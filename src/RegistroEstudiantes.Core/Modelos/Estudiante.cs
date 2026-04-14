namespace RegistroEstudiantes.Core.Modelos;

public sealed class Estudiante
{
    public int IdEstudiante { get; set; }

    public string Carnet { get; set; } = string.Empty;

    public string Nombres { get; set; } = string.Empty;

    public string Apellidos { get; set; } = string.Empty;

    public DateTime? FechaNacimiento { get; set; }

    public string? Correo { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public int IdCarrera { get; set; }

    public string? NombreCarrera { get; set; }

    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    public bool Activo { get; set; } = true;

    public string NombreCompleto => $"{Nombres} {Apellidos}".Trim();
}
