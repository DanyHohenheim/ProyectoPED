namespace RegistroEstudiantes.Core.Modelos;

public sealed class MovimientoEstudiante
{
    public int IdMovimiento { get; set; }

    public int IdEstudiante { get; set; }

    public int IdUsuario { get; set; }

    public string TipoMovimiento { get; set; } = string.Empty;

    public DateTime FechaMovimiento { get; set; } = DateTime.Now;

    public string? Descripcion { get; set; }

    public string? NombreUsuario { get; set; }

    public string? CarnetEstudiante { get; set; }
}
