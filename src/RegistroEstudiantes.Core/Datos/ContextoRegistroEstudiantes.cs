using Microsoft.Data.Sqlite;
using RegistroEstudiantes.Core.Configuracion;

namespace RegistroEstudiantes.Core.Datos;

public sealed class ContextoRegistroEstudiantes
{
    public ContextoRegistroEstudiantes(OpcionesBaseDatos? opciones = null)
    {
        Opciones = opciones ?? new OpcionesBaseDatos();
        FabricaConexion = new FabricaConexionSqlite(Opciones);
        InicializadorBaseDatos = new InicializadorBaseDatos(FabricaConexion);
    }

    public OpcionesBaseDatos Opciones { get; }

    public FabricaConexionSqlite FabricaConexion { get; }

    public InicializadorBaseDatos InicializadorBaseDatos { get; }

    public void Inicializar(bool incluirDatosSemilla = true)
    {
        InicializadorBaseDatos.Inicializar(incluirDatosSemilla);
    }

    public SqliteConnection CrearConexion()
    {
        return FabricaConexion.CrearConexion();
    }

    public SqliteConnection CrearConexionAbierta()
    {
        return FabricaConexion.CrearConexionAbierta();
    }
}
