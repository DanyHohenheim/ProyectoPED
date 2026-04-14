INSERT INTO Carreras (CodigoCarrera, NombreCarrera, Facultad)
VALUES
    ('ING-SIS', 'Ingenieria en Sistemas', 'Ingenieria'),
    ('LIC-ADM', 'Licenciatura en Administracion de Empresas', 'Ciencias Economicas'),
    ('TEC-INF', 'Tecnico en Informatica', 'Tecnologia');

INSERT INTO Usuarios (NombreCompleto, NombreUsuario, ClaveHash, Rol)
VALUES
    ('Administrador General', 'admin', 'CAMBIAR_HASH', 'Administrador'),
    ('Operador Principal', 'operador', 'CAMBIAR_HASH', 'Operador');
