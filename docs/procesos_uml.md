# Sección de Procesos y Diagrama UML

## 1. Procesos del sistema

Para definir el alcance de la Fase 1 del proyecto **Registro de Estudiantes**, se han establecido los procesos clave que rigen el manejo de la información y la lógica interna de la aplicación:

* **Eficiencia algorítmica:** El sistema utiliza un Árbol Binario de Búsqueda (ABB) como estructura de datos central para mantener los registros ordenados en memoria[cite: Esto permite optimizar las consultas y el ordenamiento por carné, utilizando la base de datos SQLite como respaldo permanente de la información[.
* **Probabilidad y Simplicidad técnica:** Se emplea SQLite para operar mediante un archivo local (`registro_estudiantes.db`), lo que elimina la necesidad de administrar servidores independientes y facilita la integración con C# WinForms a través de ADO.NET.
  **Integridad Relacional Fuerte:** La consistencia de los datos se protege mediante el uso de llaves foráneas y restricciones lógicas en el esquema. Esto asegura que un estudiante siempre pertenezca a una carrera existente y evita duplicidad en carnés o nombres de usuario mediante la restricción `UNIQUE`.
* **Preservación Histórica:** Las operaciones de eliminación de estudiantes y carreras no borran registros físicamente[. [En su lugar, se ejecutan "bajas lógicas" actualizando el campo `Activo` a `0` (o `false` en el código C#).
* **Auditoría Transparente:** Cualquier acción que modifique el estado de un estudiante (Alta, Actualización o Baja) dispara automáticamente un registro de trazabilidad en la tabla `MovimientosEstudiante`.

## 2. Actores y Acciones

De acuerdo con el modelo de datos y los scripts de inicialización, el sistema cuenta con dos niveles de acceso definidos:

| Actor | Descripción de Acciones |
| :--- | :--- |
| **Administrador (Admin)** | Posee control total del sistema. Realiza operaciones CRUD parciales sobre los usuarios para el control de acceso y administra el catálogo de carreras. Hereda todas las funciones del operador. |
| **Operador** | Encargado del manejo del padrón estudiantil.Ejecuta el ciclo de vida completo (CRUD) de los estudiantes a través de los formularios WinForms, específicamente desde `FrmEstudiantes`. |

## 3. Casos de Uso (Fase 1)

Durante la implementación inicial se han priorizado los siguientes casos de uso:

### Gestión de Estudiantes (CRUD Principal)
* **Crear:** Registro de nuevos estudiantes vinculados a un carné único, datos personales y una carrera específica (`IdCarrera`).
* **Actualizar:** Modificación de la información general almacenada del estudiante.
* **Baja Lógica:** Cambio de estado de un estudiante de activo a inactivo para preservar el historial.
* **Consultar y Listar:** Localización de estudiantes por carné o ID y visualización de listados ordenados en componentes `DataGridView` mediante el procesamiento del ABB en memoria.

### Gestión de Catálogos y Seguridad
* **Gestionar Carreras (CRUD Parcial):** Carga de carreras en componentes visuales (`ComboBox`) y registro opcional de nuevas entidades.
* **Gestionar Usuarios (CRUD Parcial):** Carga y administración básica de las cuentas de acceso para la aplicación.

### Procesos del Sistema
***Registrar Movimiento:** Proceso automático que inserta un registro de auditoría en `MovimientosEstudiante`, capturando el tipo de acción y la marca de tiempo

## 4. Diagrama UML

El siguiente diagrama representa la capa de dominio y la lógica de estructuras en memoria que sustentan el funcionamiento del sistema:

<img width="871" height="512" alt="Image" src="https://github.com/user-attachments/assets/257f1a3c-4838-452a-a0f2-677b745c1357" />
