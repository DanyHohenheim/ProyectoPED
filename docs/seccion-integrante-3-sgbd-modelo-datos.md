# Sección del Integrante 3: SGBD y modelo de datos

## 1. SGBD seleccionado

Para la Fase 1 del proyecto **Registro de Estudiantes** se propone utilizar **SQLite** como Sistema Gestor de Base de Datos (SGBD).

### Justificación de la elección

SQLite se adapta bien a un prototipo desarrollado en **C# con WinForms** porque:

- No requiere instalar ni administrar un servidor de base de datos independiente.
- Almacena la información en un solo archivo local, lo que facilita las pruebas y la distribución del prototipo.
- Permite implementar operaciones CRUD de forma sencilla desde C# mediante ADO.NET.
- Soporta claves primarias, claves foráneas, restricciones de unicidad e índices, suficientes para la Fase 1.
- Reduce la complejidad técnica del proyecto y permite concentrarse en la lógica del sistema y en la interfaz.

Como el objetivo de la Fase 1 es presentar un avance funcional o semifuncional, SQLite ofrece una solución realista, liviana y fácil de integrar con WinForms. Si posteriormente el docente solicita una alternativa más robusta, el diseño puede migrarse a SQL Server con cambios menores en la capa de acceso a datos.

## 2. Modelo de base de datos propuesto

La base de datos almacenará la información persistente del sistema. Las estructuras en memoria, como el árbol binario de búsqueda para ordenar o consultar estudiantes por carné o ID, se usarán durante la ejecución del programa; sin embargo, el respaldo permanente de los datos se realizará en la base de datos.

### 2.1 Tabla `Carreras`

Esta tabla almacena las carreras disponibles para asociarlas con cada estudiante.

| Campo | Tipo | Restricción | Descripción |
| --- | --- | --- | --- |
| `IdCarrera` | `INTEGER` | PK, AUTOINCREMENT | Identificador único de la carrera |
| `CodigoCarrera` | `TEXT` | NOT NULL, UNIQUE | Código institucional de la carrera |
| `NombreCarrera` | `TEXT` | NOT NULL | Nombre de la carrera |
| `Facultad` | `TEXT` | NULL | Facultad a la que pertenece |
| `Activa` | `INTEGER` | NOT NULL, DEFAULT 1 | Estado lógico de la carrera |

### 2.2 Tabla `Estudiantes`

Es la tabla principal del sistema y registra los datos generales de cada estudiante.

| Campo | Tipo | Restricción | Descripción |
| --- | --- | --- | --- |
| `IdEstudiante` | `INTEGER` | PK, AUTOINCREMENT | Identificador interno del estudiante |
| `Carnet` | `TEXT` | NOT NULL, UNIQUE | Código o carné del estudiante |
| `Nombres` | `TEXT` | NOT NULL | Nombres del estudiante |
| `Apellidos` | `TEXT` | NOT NULL | Apellidos del estudiante |
| `FechaNacimiento` | `TEXT` | NULL | Fecha de nacimiento |
| `Correo` | `TEXT` | NULL | Correo electrónico |
| `Telefono` | `TEXT` | NULL | Número telefónico |
| `Direccion` | `TEXT` | NULL | Dirección de residencia |
| `IdCarrera` | `INTEGER` | NOT NULL, FK | Carrera asociada al estudiante |
| `FechaRegistro` | `TEXT` | NOT NULL | Fecha de creación del registro |
| `Activo` | `INTEGER` | NOT NULL, DEFAULT 1 | Permite baja lógica del estudiante |

### 2.3 Tabla `Usuarios`

Esta tabla permitirá identificar quién realiza acciones dentro del sistema, por ejemplo un administrador u operador.

| Campo | Tipo | Restricción | Descripción |
| --- | --- | --- | --- |
| `IdUsuario` | `INTEGER` | PK, AUTOINCREMENT | Identificador del usuario |
| `NombreCompleto` | `TEXT` | NOT NULL | Nombre de la persona usuaria |
| `NombreUsuario` | `TEXT` | NOT NULL, UNIQUE | Usuario de acceso |
| `ClaveHash` | `TEXT` | NOT NULL | Contraseña almacenada de forma segura |
| `Rol` | `TEXT` | NOT NULL | Rol del usuario: Administrador u Operador |
| `Activo` | `INTEGER` | NOT NULL, DEFAULT 1 | Estado del usuario |

### 2.4 Tabla `MovimientosEstudiante`

Se usará para llevar un historial de altas, modificaciones y bajas realizadas sobre los estudiantes.

| Campo | Tipo | Restricción | Descripción |
| --- | --- | --- | --- |
| `IdMovimiento` | `INTEGER` | PK, AUTOINCREMENT | Identificador del movimiento |
| `IdEstudiante` | `INTEGER` | NOT NULL, FK | Estudiante afectado |
| `IdUsuario` | `INTEGER` | NOT NULL, FK | Usuario que ejecutó la acción |
| `TipoMovimiento` | `TEXT` | NOT NULL | Alta, Actualizacion o Baja |
| `FechaMovimiento` | `TEXT` | NOT NULL | Fecha y hora de la acción |
| `Descripcion` | `TEXT` | NULL | Observación adicional |

## 3. Relaciones entre tablas

Las relaciones propuestas son las siguientes:

- Una **carrera** puede estar asociada a muchos **estudiantes**.
- Un **estudiante** pertenece a una sola **carrera**.
- Un **usuario** puede registrar muchos **movimientos**.
- Un **estudiante** puede tener muchos **movimientos** asociados.

### Resumen relacional

- `Carreras (1) -> (N) Estudiantes`
- `Usuarios (1) -> (N) MovimientosEstudiante`
- `Estudiantes (1) -> (N) MovimientosEstudiante`

## 4. Operaciones CRUD previstas para la Fase 1

Durante la primera fase del proyecto se implementarán las operaciones esenciales del sistema:

### CRUD de estudiantes

- **Create**: registrar un nuevo estudiante con su carné, nombres, apellidos y carrera.
- **Read**: consultar estudiantes por ID o por carné, además de listar registros ordenados.
- **Update**: modificar información general del estudiante.
- **Delete**: realizar baja lógica, cambiando el campo `Activo` a 0 para no perder historial.

### CRUD parcial de carreras

- **Create**: registrar carreras si el equipo decide habilitar mantenimiento básico.
- **Read**: mostrar la lista de carreras en formularios WinForms, por ejemplo en un `ComboBox`.
- **Update/Delete**: opcional para Fase 1; puede dejarse para una fase posterior.

### CRUD parcial de usuarios

- **Read**: cargar usuarios del sistema para control básico de acceso.
- **Create**: opcional si se desea crear cuentas desde el sistema.

### Registro de movimientos

- Cada alta, modificación o baja de estudiante generará automáticamente un registro en `MovimientosEstudiante`.

## 5. Integración con C# WinForms

La aplicación WinForms se conectará a SQLite mediante una capa de acceso a datos. La idea general es la siguiente:

1. La interfaz WinForms capturará los datos desde formularios como `FrmEstudiantes`.
2. Una clase de servicio o repositorio validará la información antes de guardarla.
3. La capa de datos ejecutará consultas SQL sobre el archivo `.db`.
4. Al iniciar el sistema, los estudiantes podrán cargarse desde la base de datos a memoria para construir la estructura de búsqueda y ordenamiento que el equipo defina.

### Ejemplo de conexión esperado

La cadena de conexión será similar a esta:

```csharp
Data Source=./data/registro_estudiantes.db
```

Desde WinForms, esta base puede usarse para:

- Guardar estudiantes registrados desde formularios.
- Consultar rápidamente por carné o ID.
- Llenar tablas visuales como `DataGridView`.
- Registrar auditoría básica de movimientos.

## 6. Conclusión del apartado

La propuesta de base de datos para el sistema **Registro de Estudiantes** está compuesta por tablas simples pero suficientes para cubrir la Fase 1 del proyecto. SQLite se eligió por su facilidad de integración con **C# WinForms**, su bajo costo de administración y su capacidad para soportar las operaciones CRUD esenciales. El modelo diseñado garantiza persistencia, orden lógico de la información y una base sólida para futuras ampliaciones.
