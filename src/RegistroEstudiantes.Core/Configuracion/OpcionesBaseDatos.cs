namespace RegistroEstudiantes.Core.Configuracion;

public sealed class OpcionesBaseDatos
{
    public OpcionesBaseDatos(string? rutaArchivoBaseDatos = null)
    {
        RutaArchivoBaseDatos = string.IsNullOrWhiteSpace(rutaArchivoBaseDatos)
            ? Path.Combine(AppContext.BaseDirectory, "data", "registro_estudiantes.db")
            : Path.GetFullPath(rutaArchivoBaseDatos);

        DirectorioBaseDatos = Path.GetDirectoryName(RutaArchivoBaseDatos)
            ?? throw new InvalidOperationException("No se pudo determinar el directorio de la base de datos.");
    }

    public string RutaArchivoBaseDatos { get; }

    public string DirectorioBaseDatos { get; }

    public string CadenaConexion => $"Data Source={RutaArchivoBaseDatos};Foreign Keys=True";
}
