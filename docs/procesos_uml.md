# Procesos y Arquitectura del Sistema

## Procesos del Sistema

[cite_start]Para poder definir nuestro proyecto hemos creado los procesos que nos ayudarán a comprender cómo el sistema manejara la información[cite: 4]:

* [cite_start]**Eficiencia algorítmica:** El sistema utilizará un Árbol Binario de Búsqueda como estructura de datos central para mantener los registros ordenados esto nos permitirá optimizar las consultas y el ordenamiento por carne teniendo como respaldo permanente de la información a la base de datos que se creó[cite: 5].
* [cite_start]**Probabilidad y Simplicidad técnica:** Usaremos SQLite ya que nos permite operar un archivo local en nuestro caso `registro_estudiantes.db`, eliminando la necesidad de administrar un servidor de base de datos independientes y facilitando la integración mediante ADO.NET en C# WinForms[cite: 6].
* [cite_start]**Integridad Relacional Fuerte:** El sistema protege la consistencia de los datos ya que se utilizó llaves foráneas y restricciones lógicas para así asegurarnos que por ejemplo un estudiante no puede registrarse sin pertenecer a una carrera existente o evitar que existan carnet ni nombre de usuarios duplicados (`UNIQUE`)[cite: 7].
* [cite_start]**Preservación Histórica:** Las operaciones de eliminación ("Delete") sobre estudiantes y carreras se manejan como "bajas lógicas", actualizando un campo Activo a 0 (o false en C#) en lugar de borrar el registro físicamente[cite: 8].
* [cite_start]**Auditoría Transparente:** Cualquier operación que modifique el estado de un estudiante (Alta, Actualización o Baja) genera automáticamente un registro de trazabilidad en la tabla `MovimientosEstudiante`[cite: 9].

## Actores y Acciones

[cite_start]En la carpeta de modelo de datos y la carpeta `seed_inicial.sql` que contiene los script de inicialización confirman dos niveles de acceso[cite: 11]:

* [cite_start]**Administrador (Admin):** Control total[cite: 12]. [cite_start]Puede realizar lecturas y creaciones (CRUD parcial) de usuarios para el control de acceso[cite: 12]. [cite_start]Administra el catálogo de carreras y hereda todas las capacidades del operador[cite: 13].
* [cite_start]**Operador:** Maneja el padrón[cite: 14]. [cite_start]Ejecuta el CRUD completo de estudiantes desde los formularios Winforms (`FrmEstudiantes`)[cite: 14].

## Casos de Uso (Fase 1)

* [cite_start]**Gestionar Estudiantes (CRUD Principal):** [cite: 16]
    * [cite_start]*Crear:* Registrar un estudiante asociándolo con un carné, datos personales y un IdCarrera[cite: 17].
    * [cite_start]*Actualizar:* Modificar la información general del estudiante[cite: 18].
    * [cite_start]*Baja Lógica:* Cambiar el estado activo del estudiante a inactivo[cite: 19].
* [cite_start]**Consultar y Listar Estudiantes:** [cite: 20] [cite_start]Buscar estudiantes específicos por carné o ID y llenar tablas visuales (`DataGridView`) utilizando las estructuras en memoria[cite: 21].
* [cite_start]**Gestionar Carreras (CRUD Parcial):** [cite: 22] [cite_start]Cargar la lista de carreras en componentes visuales (como un `ComboBox`) y, opcionalmente, registrar nuevas carreras[cite: 23].
* [cite_start]**Gestionar Usuarios (CRUD Parcial):** [cite: 24] [cite_start]Cargar usuarios para el control básico de acceso a la aplicación WinForms[cite: 25].
* [cite_start]**Registrar Movimiento (Sistema):** [cite: 26] [cite_start]Caso de uso incluido (automático) que inserta un registro en la entidad `MovimientoEstudiante` detallando el TipoMovimiento y la fecha de la acción[cite: 27].

## Diagrama UML

A continuación se presenta el modelo de Capa de Dominio y Lógica de Estructuras del sistema:

```plantuml
@startuml
skinparam style strictuml
skinparam packageStyle frame
skinparam shadowing false

package "Capa de Dominio (Modelos)" {
    class Movimiento {
        + Tipo : String
        + Fecha : DateTime
    }

    class Usuario {
        + NombreUsuario : String
        + Rol : String
    }

    class Estudiante {
        + Carnet : String
        + NombreCompleto : String
        + Activo : Boolean
    }

    class Carrera {
        + Codigo : String
        + Nombre : String
    }
}

package "Lógica de Estructuras" {
    class ArbolBinarioBusqueda <T> {
        + Insertar(item : T)
        + Buscar(criterio) : T
        + RecorridoInorden() : List<T>
    }
}

' Relaciones principales
Movimiento "n" -- "1" Usuario : realizado por >
Movimiento "n" -- "1" Estudiante : registra acción sobre >
Estudiante "n" -- "1" Carrera : pertenece >

' Relación con la estructura de datos
ArbolBinarioBusqueda "1" o-- "n" Estudiante : gestiona en memoria >

@enduml
