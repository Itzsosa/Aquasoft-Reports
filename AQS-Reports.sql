IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Aquasoft_Reports')
BEGIN
    -- Crear la base de datos EjemploDB
    CREATE DATABASE Aquasoft_Reports;
END
GO

-- Usar la base de datos
USE Aquasoft_Reports;
GO


IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AQS_Reportes')
BEGIN
    CREATE TABLE AQS_Reportes (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Titulo VARCHAR(50) NOT NULL,
		Descripcion VARCHAR(MAX) NOT NULL,
		FechaPublicacion DateTime DEFAULT GETDATE(),
		Fotografia VARBINARY(MAX) NOT NULL,
		Autor VARCHAR(50) NOT NULL
    );

	INSERT INTO AQS_Reportes (Titulo, Descripcion, Fotografia, Autor)
	VALUES ('Publicacion 1', 'Publicacion del backsui', 0x0123456789ABCDEF, 'Itzsosa');
END
GO

select * from AQS_Reportes;
drop table AQS_Reportes;

--Tabla de usuarios ----------------
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AQS_Usuarios')
BEGIN
    CREATE TABLE AQS_Usuarios(
		Id INT PRIMARY KEY IDENTITY(1,1),
		NombreUsuario VARCHAR(50) NOT NULL,
		Correo VARCHAR(60) NOT NULL,
		Contrasena VARCHAR(50) NOT NULL,
		Rol VARCHAR(20) NOT NULL,
		Estado char(1) DEFAULT 'A'
);

    -- Insertar datos de ejemplo en la tabla Usuario
	INSERT INTO AQS_Usuarios (NombreUsuario,Correo, Contrasena, Rol )
	VALUES ('Itzsosa','admin@example.com', '3{*2Hsa@#', 'Admin');

	INSERT INTO AQS_Usuarios (NombreUsuario,Correo, Contrasena, Rol )
	VALUES ('Backsui','user@example.com', '@{+093]":aE', 'User');
END
GO

drop table AQS_Usuarios;
select * from AQS_Usuarios;

--"Server = JOSUEPC\\SQLEXPRESS;database = Aquasoft_Reports ;user id = Aquasoft; password=1234 ; TrustServerCertificate = True"
