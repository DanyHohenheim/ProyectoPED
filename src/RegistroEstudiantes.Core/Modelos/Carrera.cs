namespace RegistroEstudiantes.Core.Modelos;

public sealed class Carrera
{
    public int IdCarrera { get; set; }

    public string CodigoCarrera { get; set; } = string.Empty;

    public string NombreCarrera { get; set; } = string.Empty;

    public string? Facultad { get; set; }

    public bool Activa { get; set; } = true;
}
