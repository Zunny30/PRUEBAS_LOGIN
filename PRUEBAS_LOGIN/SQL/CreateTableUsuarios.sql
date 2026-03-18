-- =============================================
-- Script para crear la tabla Usuarios en DB_ACCESO
-- =============================================
-- Este script crea la estructura de la tabla de usuarios
-- con todos los índices y restricciones necesarias.
-- Ejecutar en SQL Server Management Studio

USE DB_ACCESO;
GO

-- =============================================
-- Crear tabla Usuarios
-- =============================================
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Usuarios')
BEGIN
    CREATE TABLE Usuarios
    (
        IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
        Correo NVARCHAR(150) NOT NULL,
        Clave NVARCHAR(255) NOT NULL,
        FechaCreacion DATETIME DEFAULT GETDATE(),
        FechaModificacion DATETIME DEFAULT GETDATE(),
        CONSTRAINT UK_Usuarios_Correo UNIQUE (Correo)
    );
    
    CREATE INDEX IX_Usuarios_Correo ON Usuarios(Correo);
    CREATE INDEX IX_Usuarios_FechaCreacion ON Usuarios(FechaCreacion DESC);
    
    PRINT 'Tabla Usuarios creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La tabla Usuarios ya existe.';
    
    -- Verificar si faltan columnas y agregarlas si es necesario
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Usuarios' AND COLUMN_NAME = 'FechaModificacion')
    BEGIN
        ALTER TABLE Usuarios ADD FechaModificacion DATETIME DEFAULT GETDATE();
        PRINT 'Columna FechaModificacion agregada.';
    END
END
GO

-- =============================================
-- Datos de prueba (comentados)
-- =============================================
-- DECLARE @Clave NVARCHAR(255);
-- SELECT @Clave = 'a9993e364706816aba3e25717850c26c9cd0d89d'; -- Hash SHA256 de 'abc'
-- INSERT INTO Usuarios (Correo, Clave) VALUES ('usuario@example.com', @Clave);
-- INSERT INTO Usuarios (Correo, Clave) VALUES ('admin@example.com', @Clave);

-- Para visualizar los usuarios
-- SELECT IdUsuario, Correo, FechaCreacion FROM Usuarios;
