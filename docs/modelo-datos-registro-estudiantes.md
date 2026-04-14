# Modelo relacional resumido

```mermaid
erDiagram
    CARRERAS ||--o{ ESTUDIANTES : "contiene"
    USUARIOS ||--o{ MOVIMIENTOS_ESTUDIANTE : "registra"
    ESTUDIANTES ||--o{ MOVIMIENTOS_ESTUDIANTE : "genera"

    CARRERAS {
        int IdCarrera PK
        string CodigoCarrera
        string NombreCarrera
        string Facultad
        bool Activa
    }

    ESTUDIANTES {
        int IdEstudiante PK
        string Carnet
        string Nombres
        string Apellidos
        string FechaNacimiento
        string Correo
        string Telefono
        string Direccion
        int IdCarrera FK
        string FechaRegistro
        bool Activo
    }

    USUARIOS {
        int IdUsuario PK
        string NombreCompleto
        string NombreUsuario
        string ClaveHash
        string Rol
        bool Activo
    }

    MOVIMIENTOS_ESTUDIANTE {
        int IdMovimiento PK
        int IdEstudiante FK
        int IdUsuario FK
        string TipoMovimiento
        string FechaMovimiento
        string Descripcion
    }
```

## Notas de uso

- `Estudiantes` es la entidad principal del sistema.
- `Carnet` debe ser único para evitar duplicados.
- La baja se manejará de forma lógica con el campo `Activo`.
- `MovimientosEstudiante` conserva trazabilidad de las acciones realizadas.
- El árbol binario de búsqueda sugerido por el proyecto puede construirse en memoria a partir de los datos persistidos en `Estudiantes`.
