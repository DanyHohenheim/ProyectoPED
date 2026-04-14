using Microsoft.Data.Sqlite;

namespace RegistroEstudiantes.Core.Datos;

public sealed class InicializadorBaseDatos
{
    private readonly FabricaConexionSqlite _fabricaConexion;

    public InicializadorBaseDatos(FabricaConexionSqlite fabricaConexion)
    {
        _fabricaConexion = fabricaConexion ?? throw new ArgumentNullException(nameof(fabricaConexion));
    }

    public void Inicializar(bool incluirDatosSemilla = true)
    {
        Directory.CreateDirectory(_fabricaConexion.Opciones.DirectorioBaseDatos);

        using var conexion = _fabricaConexion.CrearConexionAbierta();
        EjecutarScript(conexion, ScriptsBaseDatos.Esquema);

        if (incluirDatosSemilla)
        {
            EjecutarScript(conexion, ScriptsBaseDatos.Semilla);
        }
    }

    private static void EjecutarScript(SqliteConnection conexion, string script)
    {
        using var comando = conexion.CreateCommand();
        comando.CommandText = script;
        comando.ExecuteNonQuery();
    }
}
