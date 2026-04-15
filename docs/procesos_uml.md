# Sección de Procesos y Diagrama UML

## 1. Procesos del sistema

[cite_start]Para definir el alcance de la Fase 1 del proyecto **Registro de Estudiantes**, se han establecido los procesos clave que rigen el manejo de la información y la lógica interna de la aplicación[cite: 3, 4]:

* [cite_start]**Eficiencia algorítmica:** El sistema utiliza un Árbol Binario de Búsqueda (ABB) como estructura de datos central para mantener los registros ordenados en memoria[cite: 5]. [cite_start]Esto permite optimizar las consultas y el ordenamiento por carné, utilizando la base de datos SQLite como respaldo permanente de la información[cite: 5].
* [cite_start]**Probabilidad y Simplicidad técnica:** Se emplea SQLite para operar mediante un archivo local (`registro_estudiantes.db`), lo que elimina la necesidad de administrar servidores independientes y facilita la integración con C# WinForms a través de ADO.NET[cite: 6].
* [cite_start]**Integridad Relacional Fuerte:** La consistencia de los datos se protege mediante el uso de llaves foráneas y restricciones lógicas en el esquema[cite: 7]. [cite_start]Esto asegura que un estudiante siempre pertenezca a una carrera existente y evita duplicidad en carnés o nombres de usuario mediante la restricción `UNIQUE`[cite: 7].
* [cite_start]**Preservación Histórica:** Las operaciones de eliminación de estudiantes y carreras no borran registros físicamente[cite: 8]. [cite_start]En su lugar, se ejecutan "bajas lógicas" actualizando el campo `Activo` a `0` (o `false` en el código C#)[cite: 8].
* [cite_start]**Auditoría Transparente:** Cualquier acción que modifique el estado de un estudiante (Alta, Actualización o Baja) dispara automáticamente un registro de trazabilidad en la tabla `MovimientosEstudiante`[cite: 9].

## 2. Actores y Acciones

[cite_start]De acuerdo con el modelo de datos y los scripts de inicialización, el sistema cuenta con dos niveles de acceso definidos[cite: 10, 11]:

| Actor | Descripción de Acciones |
| :--- | :--- |
| **Administrador (Admin)** | [cite_start]Posee control total del sistema[cite: 12]. [cite_start]Realiza operaciones CRUD parciales sobre los usuarios para el control de acceso y administra el catálogo de carreras[cite: 12, 13]. [cite_start]Hereda todas las funciones del operador[cite: 13]. |
| **Operador** | [cite_start]Encargado del manejo del padrón estudiantil[cite: 14]. [cite_start]Ejecuta el ciclo de vida completo (CRUD) de los estudiantes a través de los formularios WinForms, específicamente desde `FrmEstudiantes`[cite: 14]. |

## 3. Casos de Uso (Fase 1)

[cite_start]Durante la implementación inicial se han priorizado los siguientes casos de uso[cite: 15]:

### Gestión de Estudiantes (CRUD Principal)
* [cite_start]**Crear:** Registro de nuevos estudiantes vinculados a un carné único, datos personales y una carrera específica (`IdCarrera`)[cite: 16, 17].
* [cite_start]**Actualizar:** Modificación de la información general almacenada del estudiante[cite: 18].
* [cite_start]**Baja Lógica:** Cambio de estado de un estudiante de activo a inactivo para preservar el historial[cite: 19].
* [cite_start]**Consultar y Listar:** Localización de estudiantes por carné o ID y visualización de listados ordenados en componentes `DataGridView` mediante el procesamiento del ABB en memoria[cite: 20, 21].

### Gestión de Catálogos y Seguridad
* [cite_start]**Gestionar Carreras (CRUD Parcial):** Carga de carreras en componentes visuales (`ComboBox`) y registro opcional de nuevas entidades[cite: 22, 23].
* [cite_start]**Gestionar Usuarios (CRUD Parcial):** Carga y administración básica de las cuentas de acceso para la aplicación[cite: 24, 25].

### Procesos del Sistema
* [cite_start]**Registrar Movimiento:** Proceso automático que inserta un registro de auditoría en `MovimientosEstudiante`, capturando el tipo de acción y la marca de tiempo[cite: 26, 27].

## 4. Diagrama UML

[cite_start]El siguiente diagrama representa la capa de dominio y la lógica de estructuras en memoria que sustentan el funcionamiento del sistema[cite: 28]:


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
