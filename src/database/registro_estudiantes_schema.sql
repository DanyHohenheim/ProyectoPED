PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS Carreras (
    IdCarrera INTEGER PRIMARY KEY AUTOINCREMENT,
    CodigoCarrera TEXT NOT NULL UNIQUE,
    NombreCarrera TEXT NOT NULL,
    Facultad TEXT,
    Activa INTEGER NOT NULL DEFAULT 1 CHECK (Activa IN (0, 1))
);

CREATE TABLE IF NOT EXISTS Usuarios (
    IdUsuario INTEGER PRIMARY KEY AUTOINCREMENT,
    NombreCompleto TEXT NOT NULL,
    NombreUsuario TEXT NOT NULL UNIQUE,
    ClaveHash TEXT NOT NULL,
    Rol TEXT NOT NULL CHECK (Rol IN ('Administrador', 'Operador')),
    Activo INTEGER NOT NULL DEFAULT 1 CHECK (Activo IN (0, 1))
);

CREATE TABLE IF NOT EXISTS Estudiantes (
    IdEstudiante INTEGER PRIMARY KEY AUTOINCREMENT,
    Carnet TEXT NOT NULL UNIQUE,
    Nombres TEXT NOT NULL,
    Apellidos TEXT NOT NULL,
    FechaNacimiento TEXT,
    Correo TEXT,
    Telefono TEXT,
    Direccion TEXT,
    IdCarrera INTEGER NOT NULL,
    FechaRegistro TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Activo INTEGER NOT NULL DEFAULT 1 CHECK (Activo IN (0, 1)),
    FOREIGN KEY (IdCarrera) REFERENCES Carreras (IdCarrera)
);

CREATE TABLE IF NOT EXISTS MovimientosEstudiante (
    IdMovimiento INTEGER PRIMARY KEY AUTOINCREMENT,
    IdEstudiante INTEGER NOT NULL,
    IdUsuario INTEGER NOT NULL,
    TipoMovimiento TEXT NOT NULL CHECK (TipoMovimiento IN ('Alta', 'Actualizacion', 'Baja')),
    FechaMovimiento TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Descripcion TEXT,
    FOREIGN KEY (IdEstudiante) REFERENCES Estudiantes (IdEstudiante),
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios (IdUsuario)
);

CREATE INDEX IF NOT EXISTS IX_Estudiantes_Carnet ON Estudiantes (Carnet);
CREATE INDEX IF NOT EXISTS IX_Estudiantes_Apellidos ON Estudiantes (Apellidos);
CREATE INDEX IF NOT EXISTS IX_Movimientos_IdEstudiante ON MovimientosEstudiante (IdEstudiante);
