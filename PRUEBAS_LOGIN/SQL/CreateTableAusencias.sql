-- =============================================
-- Script para crear la tabla Ausencias en DB_ACCESO
-- =============================================
-- Este script crea la estructura de la tabla de ausencias
-- con todos los índices y restricciones necesarias.
-- Ejecutar en SQL Server Management Studio

USE DB_ACCESO;
GO

-- =============================================
-- Crear tabla Ausencias
-- =============================================
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Ausencias')
BEGIN
    CREATE TABLE Ausencias
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        IdUsuario INT NOT NULL,
        Tipo NVARCHAR(50) NOT NULL,  -- Ausencia o Incapacidad
        Asunto NVARCHAR(255) NOT NULL,
        FechaInicio DATETIME NOT NULL,
        FechaFin DATETIME NOT NULL,
        FechaCreacion DATETIME DEFAULT GETDATE(),
        CONSTRAINT FK_Ausencias_Usuarios FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario)
    );
    
    CREATE INDEX IX_Ausencias_IdUsuario ON Ausencias(IdUsuario);
    CREATE INDEX IX_Ausencias_FechaInicio ON Ausencias(FechaInicio);
    CREATE INDEX IX_Ausencias_FechaFin ON Ausencias(FechaFin);
    
    PRINT 'Tabla Ausencias creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La tabla Ausencias ya existe.';
    
    -- Verificar si faltan columnas y agregarlas si es necesario
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Ausencias' AND COLUMN_NAME = 'IdUsuario')
    BEGIN
        ALTER TABLE Ausencias ADD IdUsuario INT NOT NULL;
        ALTER TABLE Ausencias ADD CONSTRAINT FK_Ausencias_Usuarios FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario);
        PRINT 'Columna IdUsuario agregada.';
    END
END
GO