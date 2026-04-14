using Microsoft.Data.Sqlite;
using RegistroEstudiantes.Core.Configuracion;

namespace RegistroEstudiantes.Core.Datos;

public sealed class FabricaConexionSqlite
{
    public FabricaConexionSqlite(OpcionesBaseDatos opciones)
    {
        Opciones = opciones ?? throw new ArgumentNullException(nameof(opciones));
    }

    public OpcionesBaseDatos Opciones { get; }

    public SqliteConnection CrearConexion()
    {
        return new SqliteConnection(Opciones.CadenaConexion);
    }

    public SqliteConnection CrearConexionAbierta()
    {
        var conexion = CrearConexion();
        conexion.Open();
        return conexion;
    }
}
