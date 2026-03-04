Create database GrupoSumma
use GrupoSumma
go
--------------------------------------------------
--||--Creation [dbo].[Operaciones]----------||---
--||----------------------------------------||---
CREATE TABLE [dbo].[Operaciones] (
    [Id_Operaciones] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Deal] VARCHAR(50) NOT NULL,
    [FechaInicio] DATE NULL,
    [NombreCliente] VARCHAR(100) NULL,
    [Beneficiario] VARCHAR(100) NULL,
    [MontoUSD] DECIMAL(10, 2) NULL,
    [TipoCambio] DECIMAL(10, 2) NULL,
    [TCCliente] DECIMAL(10, 2) NULL,
    [Comision_Porcentaje] DECIMAL(10, 2) NULL,
    [Promotor] VARCHAR(50) NULL,
    [FechaCierre] DATE NULL,
    [Documento_PDF_Deal] VARCHAR(MAX) NULL,
    [Documento_PDF_FED] VARCHAR(MAX) NULL,
    [MontoMXN] DECIMAL(18, 2) NULL,
    [Comision_Por_Envio_Ahorro] DECIMAL(10, 2) NULL,
    [Plataforma] VARCHAR(100) NULL,
    [Mto_CTE_TC] DECIMAL(18, 2) NULL,
    [Casque] DECIMAL(18, 2) NULL,
    [Comision_$] DECIMAL(18, 2) NULL,
    [Dep_Cliente] DECIMAL(19, 2) NULL,
    [Utilidad] DECIMAL(18, 2) NULL,
    [RegistroId] INT NOT NULL, -- Se refiere a Registro_Usuario
    FOREIGN KEY (RegistroId) REFERENCES [dbo].[Registro_Usuario] ([RegistroId]) ON DELETE NO ACTION
);
GO

--------------------------------------------------
---||Creation Table [dbo].[Registro_Usuario]-||----
 --||----------------------------------------||---

 CREATE TABLE [dbo].[Registro_Usuario] (
    [RegistroId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [NombreUsuario] VARCHAR(50) NOT NULL,
    [TipodeUsuario] INT NOT NULL,
    [Contrasena] VARCHAR(255) NOT NULL,
    [Nombre] VARCHAR(50) NULL,
    [ApellidoPaterno] VARCHAR(50) NULL,
    [ApellidoMaterno] VARCHAR(50) NULL,
    [CorreoElectronico] VARCHAR(100) NULL,
    [FechaNacimiento] DATE NULL,
    [Genero] VARCHAR(50) NULL,
    [Calle] VARCHAR(100) NULL,
    [CodigoPostal] VARCHAR(10) NULL,
    [Estado] VARCHAR(50) NULL,
    [Municipio] VARCHAR(50) NULL,
    [NoExterior] VARCHAR(10) NULL,
    [NoInterior] VARCHAR(10) NULL,
    FOREIGN KEY (TipodeUsuario) REFERENCES [dbo].[Rol] ([Id_Rol]) ON DELETE NO ACTION
);
GO


--------------------------------------------------
---||----Creation Table  Table [dbo].[Rol]---||----
 --||----------------------------------------||---
CREATE TABLE [dbo].[Rol] (
    [Id_Rol] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [TipoDeRol] VARCHAR(50) NOT NULL
);
GO

--------------------------------------------------
---||----Creation Table [dbo].[Usuario-------||----
 --||----------------------------------------||---
 CREATE TABLE [dbo].[Usuario] (
    [Id_Usuario] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Id_Rol] INT NULL,
    FOREIGN KEY (Id_Rol) REFERENCES [dbo].[Rol] ([Id_Rol]) ON DELETE NO ACTION
);
GO



---SP To Modify the name of a column-------
EXEC sp_rename 'Registro_Usuario.UsuarioId', 'TipoUsuario', 'COLUMN';


select * from Operaciones
use Locaciones

select * from [dbo].[CUSTOMER_LOCATIONS_2]

---||----Role data insertion----------------||----
insert into Rol(TipoDeRol) values 
('Usuario'),
('Admin'),
('SuperAdmin')
----||--------------consultas para las peticiones de datos----||---
select * from Rol 

---||----------inserccion de datos consulta-----------||--------
insert into Usuario (Id_Rol) values
(1),--USUARIO
(2),----Admin
(3)---SuperAdmin


--------------------------INSERT OF DATA User Registration-------------------------------------

INSERT INTO Registro_Usuario (UsuarioId, NombreUsuario, Contrasena, Nombre, ApellidoPaterno, ApellidoMaterno, CorreoElectronico, FechaNacimiento, Genero, Calle, CodigoPostal, Estado, Municipio, NoExterior, NoInterior) 
VALUES 
(1, 'user1', 'password1', 'Juan', 'Pérez', 'López', 'juan.perez@example.com', '1990-05-15', 'Masculino', 'Av. Reforma', '01000', 'CDMX', 'Cuauhtémoc', '123', 'A'),
(2, 'user2', 'password23', 'David', 'Rodriguez', 'Santos', 'david.rodriguez@example.com', '1990-05-15', 'Masculino', 'Av. Reforma', '01000', 'CDMX', 'Cuauhtémoc', '123', 'A'),
(3, 'user3', 'Administrador54', 'Vanessa', 'Gonzales', 'Garcia', 'vanessa.Gonzales@example.com', '1990-05-15', 'Femenino', 'Av. Reforma', '01000', 'CDMX', 'Cuauhtémoc', '123', 'A');

----||--------------Query to get the role of a user-------------||----
SELECT 
    RU.NombreUsuario, 
    RU.Nombre, 
    RU.ApellidoPaterno, 
    RU.ApellidoMaterno, 
    R.TipoDeRol
FROM 
    Registro_Usuario RU
JOIN 
    Usuario U ON RU.UsuarioId = U.Id
JOIN 
    Rol R ON U.RolId = R.Id;
----||----------------------------------------------

EXEC sp_rename 'Rol.NombreRol', 'TipoDeRol', 'COLUMN';

EXEC sp_rename 'Operaciones.DocumentoPDFDeal', 'Documento_PDF_Deal', 'COLUMN';

----||agregacion de  nueva columa en operaciones --------||
ALTER TABLE Operaciones
ADD Documento_PDF_FED TEXT;



select * from operaciones 

-------||---------Insert of table operation---------||----
INSERT INTO Operaciones 
(UsuarioId, Deal, FechaInicio, NombreCliente, Beneficiario, MontoUSD, TipoCambio, TCCliente, Comision, Promotor, FechaCierre, Documento_PDF_Deal, Documento_PDF_FED)
VALUES
(1, 'Deal44388187', '2022-04-05', 'SALIM', 'CELL SKY LLC', 935066.00, 20.2700, 20.3200, 0.500, 'Juan', '2022-04-05', 'Base64PDFData1', 'Base64PDFDataFED1'),
(2, 'Deal10010', '2022-05-25', 'ERICK', 'FRATERNITY', 8040.20, 19.8900, 19.9000, 1.500, 'Juan', '2022-05-25', 'Base64PDFData2', 'Base64PDFDataFED2');


Go

CREATE TRIGGER trg_AutoFillPromotor
ON Operaciones
AFTER INSERT
AS
BEGIN
    -- We update the Promoter column in the Operations table with the UserName of User_Registration---
    UPDATE O
    SET O.Promotor = RU.Nombre
    FROM Operaciones O
    JOIN Registro_Usuario RU ON O.UsuarioId = RU.TipoUsuario
    WHERE O.Id IN (SELECT Id FROM inserted);
END;



select * from Registro_Usuario
select * from Usuario
select * from Rol
select * from Operaciones

-----||consulta para saber los tipos de usuarios y rol ----||-------
SELECT 
    RU.NombreUsuario,
    RU.Nombre, 
    RU.ApellidoPaterno, 
    RU.ApellidoMaterno, 	
	RU.Contrasena,
    R.TipoDeRol
FROM 
    Registro_Usuario RU
JOIN 
    Usuario U ON RU.RegistroId = U.Id_Usuario  -- Asegúrate de que aquí sea la relación correcta
JOIN 
    Rol R ON RU.TipodeUsuario = R.Id_Rol;  -- Relación con Rol a través de TipodeUsuario




	---||--Actualizar el tipo de rol para el usuario---||---
UPDATE Registro_Usuario
SET TipoUsuario = 1
WHERE Id = 4;

---||---Update of columns of table Operations----||---
-- Modificar la columna Documento_PDF_Deal de TEXT a VARCHAR(MAX)
ALTER TABLE Operaciones
ALTER COLUMN Documento_PDF_Deal VARCHAR(MAX);

-- Modificar la columna Documento_PDF_FED de TEXT a VARCHAR(MAX)
ALTER TABLE Operaciones
ALTER COLUMN Documento_PDF_FED VARCHAR(MAX);
select * from [Registro_Usuario]
select * from Usuario
select * from Rol
select * from Operaciones


----||-------

--------||---------Tabla de Tickets-----------------------------------||----------------
--Primary Key: Id_Ticket (Clave primaria única).
--Foreign Keys:
--RegistroId → Relación con Registro_Usuario para identificar al creador del ticket.
--Id_EstatusOperacion → Para mantener el estado del ticket (Ej. Abierto, Cerrado, En Proceso).
--Descripción y Fechas: Para hacer seguimiento del progreso del ticket.

CREATE TABLE [dbo].[Tickets] (
    Id_Ticket INT IDENTITY(1,1) NOT NULL,
    Descripcion TEXT NULL,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FechaCierre DATETIME NULL,
    Id_EstatusOperacion INT NULL,
    RegistroId INT NOT NULL,
    CONSTRAINT PK_Tickets PRIMARY KEY CLUSTERED (Id_Ticket ASC),
    CONSTRAINT FK_Tickets_Registro_Usuario FOREIGN KEY (RegistroId)
    REFERENCES [dbo].[Registro_Usuario](RegistroId),
    CONSTRAINT FK_Tickets_EstatusOperacion FOREIGN KEY (Id_EstatusOperacion)
    REFERENCES [dbo].[EstatusOperacion](Id_EstatusOperacion)
);


------||--- Tabla de Comentarios--------------------------||--------------------
--Primary Key: Id_Comentario (Clave primaria única).
--Foreign Keys:
--Id_Ticket → Relación con Tickets para identificar a qué ticket pertenece el comentario.
--RegistroId → Para identificar qué usuario hizo el comentario.
--Fecha del Comentario: Para registrar el historial de seguimiento.

 CREATE TABLE [dbo].[Comentarios] (
    Id_Comentario INT IDENTITY(1,1) NOT NULL,
    Id_Ticket INT NOT NULL,
    RegistroId INT NOT NULL,
    Comentario TEXT NOT NULL,
    FechaComentario DATETIME DEFAULT GETDATE(),
    CONSTRAINT PK_Comentarios PRIMARY KEY CLUSTERED (Id_Comentario ASC),
    CONSTRAINT FK_Comentarios_Tickets FOREIGN KEY (Id_Ticket)
    REFERENCES [dbo].[Tickets](Id_Ticket)
    ON DELETE CASCADE,
    CONSTRAINT FK_Comentarios_Registro_Usuario FOREIGN KEY (RegistroId)
    REFERENCES [dbo].[Registro_Usuario](RegistroId)
    ON DELETE CASCADE
);
---Agregacion de nueva columna-----
ALTER TABLE [dbo].[Operaciones]
ADD Id_Ticket INT NULL;
--------Eliminar columna----------
ALTER TABLE [Operaciones]
DROP COLUMN TicketId;

------Relacion de la llaves foraneas 
Alter table [dbo].[Operaciones]
add constraint FK_operaciones_Tickets
foreign key (Id_Ticket)
references [dbo].[Tickets](ID_Ticket)
on delete set null;


select * from Operaciones

ALTER TABLE [dbo].[Operaciones]
DROP CONSTRAINT FK_operaciones_Tickets;


select * from Tickets

-- Paso 2: Eliminar la columna
ALTER TABLE [dbo].[Operaciones]
DROP COLUMN TicketId;

----||----------Agregar tabla StatusTicket-------||------
CREATE TABLE [dbo].[StatusTicket] (
    Id_StatusTicket INT IDENTITY(1,1) NOT NULL,
    Descripcion NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_StatusTicket PRIMARY KEY CLUSTERED (Id_StatusTicket ASC)
);

INSERT INTO StatusTicket (Descripcion)
VALUES ('Abierto'), ('En Proceso'), ('Cerrado'), ('Resuelto');

select * from Tickets
select * from StatusTicket
select * from EstatusOperacion 
select * from Registro_Usuario
SELECT * FROM Operaciones

ALTER TABLE Tickets
ADD Id_StatusTicket INT;

ALTER TABLE Tickets
ADD CONSTRAINT FK_Tickets_StatusTicket
FOREIGN KEY (Id_StatusTicket)
REFERENCES StatusTicket(Id_StatusTicket);

select * from Registro_Usuario

--||-----------------------------------||-
select * from [dbo].[Menu_Rol]
select * from [dbo].[MenuOptions]

SELECT 
    r.Id_Rol,
    r.TipoDeRol,
    STRING_AGG(mo.Rute, ', ') AS RutasPermitidas
FROM Menu_Rol mr
INNER JOIN Rol r ON mr.Id_Rol = r.Id_Rol
INNER JOIN MenuOptions mo ON mr.Id_Menu = mo.Id_MenuOptions
GROUP BY r.Id_Rol, r.TipoDeRol
ORDER BY r.Id_Rol;

--||--------------------------------------------------------||---
-- Rol 1: todos los menús
INSERT INTO Menu_Rol (Id_Menu, Id_Rol)
SELECT Id_MenuOptions, 1 FROM MenuOptions;

-- Rol 2
INSERT INTO Menu_Rol (Id_Menu, Id_Rol)
SELECT Id_MenuOptions, 2 FROM MenuOptions 
WHERE Rute IN (
    '/add-deal',
    '/operations',
    '/dash-kpi',
    '/table-users',
    '/table-platform'
);

-- Rol 3
INSERT INTO Menu_Rol (Id_Menu, Id_Rol)
SELECT Id_MenuOptions, 3 FROM MenuOptions 
WHERE Rute IN (
    '/operations',
    '/dash-kpi',
    '/table-platform',
    '/table-users'
);

-- Rol 4
INSERT INTO Menu_Rol (Id_Menu, Id_Rol)
SELECT Id_MenuOptions, 4 FROM MenuOptions 
WHERE Rute IN (
    '/add-deal',
    '/operations',
    '/table-platform'
);

-- Rol 5
INSERT INTO Menu_Rol (Id_Menu, Id_Rol)
SELECT Id_MenuOptions, 5 FROM MenuOptions 
WHERE Rute IN (
    '/add-deal',
    '/operations'
);

---||---------------------------------------------------||-------
INSERT INTO MenuOptions (Rute, Icon, Nombre) VALUES
('/add-deal', 'M12 6v6m0 0v6m0-6h6m-6 0H6', 'Agregar Deal'),
('/operations', 'M4 6h16M4 10h16M4 14h16M4 18h16', 'Operaciones'),
('/dash-kpi', 'M16 8v8m-4-5v5m-4-2v2m-2 4h12a2 2 0 002-2V6a2 2 0 00-2-2H6a2 2 0 00-2 2v12a2 2 0 002 2z', 'Dash KPI'),
('/table-users', 'M12 4.354a4 4 0 110 5.292M15 21H3v-1a6 6 0 0112 0v1zm0 0h6v-1a6 6 0 00-9-5.197M13 7a4 4 0 11-8 0 4 4 0 018 0z', 'Tablero Usuarios'),
('/table-platform', 'M3 7h18M4 21h16a1 1 0 001-1V10H3v10a1 1 0 001 1zm8-14v4', 'Tablero Plataformas'),
('/table-rol', 'M12 4a4 4 0 110 8 4 4 0 010-8zm0 12a8 8 0 005.292-2.708M12 20a8 8 0 01-5.292-2.708m10.584 0A7.96 7.96 0 0112 20zM16 14h2a1 1 0 011 1v2a1 1 0 01-1 1h-2m-8-4H6a1 1 0 00-1 1v2a1 1 0 001 1h2', 'Tablero Roles'),
('/image-ocr', 'M4 4h16v16H4V4zm3 3h10v2H7V7zm0 4h10v2H7v-2zm0 4h6v2H7v-2z M14 2v4h4', 'Imagenes OCR'),
('/operations-canceled', 'M9 12l2 2 4-4M5 13l4 4L19 7M12 2a10 10 0 100 20 10 10 0 000-20z', 'Deals Cancelados');

---||---------------------------------------------------------------------------------------------------------||----

select * from Menu_Rol

CREATE TABLE [dbo].[Menu_Rol](
	[Id_MenuRol] [int] IDENTITY(1,1) NOT NULL,
	[Id_Menu] [int] NOT NULL,
	[Id_Rol] [int] NOT NULL,
 CONSTRAINT [PK_Menu_Rol] PRIMARY KEY CLUSTERED 
(
	[Id_MenuRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Menu_Rol]  WITH CHECK ADD  CONSTRAINT [FK_Menu_Rol_MenuSumma] FOREIGN KEY([Id_Menu])
REFERENCES [dbo].[MenuOptions] ([Id_MenuOptions])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Menu_Rol] CHECK CONSTRAINT [FK_Menu_Rol_MenuSumma]
GO

ALTER TABLE [dbo].[Menu_Rol]  WITH CHECK ADD  CONSTRAINT [FK_Menu_Rol_Rol] FOREIGN KEY([Id_Rol])
REFERENCES [dbo].[Rol] ([Id_Rol])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Menu_Rol] CHECK CONSTRAINT [FK_Menu_Rol_Rol]
GO


CREATE TABLE [dbo].[MenuOptions](
	[Id_MenuOptions] [int] IDENTITY(1,1) NOT NULL,
	[Rute] [varchar](100) NOT NULL,
	[Icon] [varchar](max) NULL,
	[Nombre] [varchar](100) NOT NULL,
 CONSTRAINT [PK_MenuOptions] PRIMARY KEY CLUSTERED 
(
	[Id_MenuOptions] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[MenuOptions] ADD  DEFAULT ('') FOR [Nombre]
GO
--||------------------------------------------------------------||-----

Create table CallOptionsDeals(
IdCallOptionsDeals INT PRIMARY KEY,
Descripcion VARCHAR(50) NOT NULL
)
INSERT INTO CallOptionsDeals (IdCallOptionsDeals, Descripcion) VALUES
(1, 'Hoy'),
(2, 'Semanal'),
(3, 'Quincenal'),
(4, 'Mensual');


