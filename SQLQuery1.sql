-- Tabla de Personal (Ya la estás usando)
CREATE TABLE Personal_Naval (
    IdPersonal INT PRIMARY KEY IDENTITY,
    Matricula VARCHAR(20) UNIQUE,
    Nombres VARCHAR(100),
    Apellidos VARCHAR(100),
    IdGrado INT,
    IdJefatura INT,
    FotoPerfil VARBINARY(MAX) -- Aquí guardas los bytes de la foto
);

-- Tabla de Asistencia (Para el Módulo 4 y 5)
CREATE TABLE Asistencia (
    IdAsistencia INT PRIMARY KEY IDENTITY,
    Matricula VARCHAR(20) FOREIGN KEY REFERENCES Personal_Naval(Matricula),
    FechaHora DATETIME DEFAULT GETDATE(),
    Estado VARCHAR(20) -- 'Presente', 'Retardo'
);