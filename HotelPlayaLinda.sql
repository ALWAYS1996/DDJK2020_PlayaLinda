

CREATE  DATABASE DDJK;
USE DDJK
GO
--------------------------
-- Definicion de tablas --
--------------------------

DROP TABLE IF EXISTS Mapa;
CREATE TABLE Mapa(dummy int);
ALTER TABLE Mapa ADD idMapa int identity(1,1);
ALTER TABLE Mapa ADD latitudOrigen varchar(max);
ALTER TABLE Mapa ADD longitudOrigen varchar(max);
ALTER TABLE Mapa ADD latitudDestino varchar(max);
ALTER TABLE Mapa ADD longitudDestino varchar(max);
ALTER TABLE Mapa ADD CONSTRAINT PK_Mapa PRIMARY KEY (idMapa);
ALTER TABLE Mapa DROP COLUMN dummy;


CREATE TABLE Itinerario(dummy int);
ALTER TABLE Itinerario ADD idItinerario int identity(1,1);
ALTER TABLE Itinerario ADD dia varchar(max);
ALTER TABLE Itinerario ADD desayuno varchar(max);
ALTER TABLE Itinerario ADD  imagenDesayuno varchar(max);
ALTER TABLE Itinerario ADD almuerzo varchar(max);
ALTER TABLE Itinerario ADD  imagenAlmuerzo varchar(max);
ALTER TABLE Itinerario ADD cena varchar(max);
ALTER TABLE Itinerario ADD  imagenCena varchar(max);
ALTER TABLE Itinerario ADD CONSTRAINT PK_Itinerario PRIMARY KEY (idItinerario);
ALTER TABLE Itinerario DROP COLUMN dummy;

select * from TipoHabitacion


DROP TABLE IF EXISTS Cliente;
CREATE TABLE Cliente(dummy int);
ALTER TABlE Cliente ADD idCliente int IDENTITY(1,1);
ALTER TABLE Cliente ADD pasaporte varchar(25);
ALTER TABLE Cliente ADD nombre varchar(50);
ALTER TABLE Cliente ADD primerApellido varchar(50);
ALTER TABLE Cliente ADD segundoApellido varchar(50);
ALTER TABLE Cliente ADD edad tinyint;
ALTER TABLE Cliente ADD nacionalidad varchar(25);
ALTER TABLE Cliente ADD correo varchar(50);
ALTER TABLE Cliente ADD CONSTRAINT PK_Cliente PRIMARY KEY (idCliente);
ALTER TABLE Cliente DROP COLUMN dummy;


CREATE TABLE Contenido (idContenido smallint identity(1,1) PRIMARY KEY, titulo varchar(255), contenido varchar(max))
CREATE TABLE Imagen (idImagen smallint identity(1,1) PRIMARY KEY, ruta varchar(max))
ALTER TABLE Imagen ADD tipo tinyint;

select * from Imagen

insert into Imagen values('\Content\img\playa5.jpg',2)

DROP TABLE IF EXISTS Habitacion;
CREATE TABLE Habitacion(dummy int);
ALTER TABLE Habitacion ADD idHabitacion tinyint IDENTITY(1,1);
ALTER TABLE Habitacion ADD idTipoHabitacion int;
ALTER TABLE Habitacion ADD estado tinyint;
ALTER TABLE Habitacion ADD capacidad tinyint;
ALTER TABLE Habitacion ADD CONSTRAINT PK_Habitacion PRIMARY KEY (idHabitacion);
ALTER TABLE Habitacion ADD CONSTRAINT FK_idTipoHabitacion FOREIGN KEY (idTipoHabitacion ) REFERENCES TipoHabitacion(idTipoHabitacion)  ON UPDATE CASCADE
ALTER TABLE Habitacion DROP COLUMN dummy;

DROP TABLE IF EXISTS Reservacion;
CREATE TABLE Reservacion(dummy int);
ALTER TABLE Reservacion ADD idReservacion int IDENTITY(1,1);
ALTER TABLE Reservacion ADD idHabitacion tinyint;
ALTER TABLE Reservacion ADD idCliente int;
ALTER TABLE Reservacion ADD fechaLlegada date;
ALTER TABLE Reservacion ADD fechaSalida date;
ALTER TABLE Reservacion ADD CONSTRAINT PK_Reservacion PRIMARY KEY (idReservacion);
ALTER TABLE Reservacion   ADD CONSTRAINT FK_idCliente1 FOREIGN KEY (idCliente)  REFERENCES Cliente(idCliente)  
ALTER TABLE Reservacion   ADD CONSTRAINT FK_idHabitacion  FOREIGN KEY (idHabitacion )REFERENCES Habitacion(idHabitacion )  ON UPDATE CASCADE
ALTER TABLE Reservacion DROP COLUMN dummy;

DROP TABLE IF EXISTS TipoHabitacion;
CREATE TABLE TipoHabitacion(dummy int);
ALTER TABLE TipoHabitacion ADD idTipoHabitacion int IDENTITY(1,1);
ALTER TABLE TipoHabitacion ADD nombreTipoHabitacion varchar(25);
ALTER TABLE TipoHabitacion ADD precioBase int;
ALTER TABLE TipoHabitacion ADD CONSTRAINT PK_TipoHabitacion PRIMARY KEY(idTipoHabitacion);
ALTER TABLE TipoHabitacion DROP COLUMN dummy;

DROP TABLE IF EXISTS Empleado;
CREATE TABLE Empleado(dummy int);
ALTER TABLE Empleado ADD idEmpleado smallint IDENTITY(1,1);
ALTER TABLE Empleado ADD tipoEmpleado tinyint;
ALTER TABLE Empleado ADD idUsuario varchar(50);
ALTER TABLE Empleado ADD contrasenna varchar(256);
ALTER TABLE Empleado ADD CONSTRAINT PK_Empleado PRIMARY KEY (idEmpleado);
ALTER TABLE Empleado DROP COLUMN dummy;




DROP TABLE IF EXISTS Promocion;
CREATE TABLE Promocion(dummy int);
ALTER TABLE Promocion ADD idPromocion smallint IDENTITY(1,1);
ALTER TABLE Promocion ADD fechaInicio date;
ALTER TABLE Promocion ADD fechaFinal date;
ALTER TABLE Promocion ADD informacion varchar(300);
ALTER TABLE Promocion ADD rebaja int;
ALTER TABLE Promocion DROP COLUMN dummy;
ALTER TABLE Promocion ADD CONSTRAINT PK_Promocion PRIMARY KEY(idPromocion);

select * from Publicidad

DROP TABLE IF EXISTS Publicidad;
CREATE TABLE Publicidad(dummy int);
ALTER TABLE Publicidad ADD idPublicidad smallint IDENTITY(1,1);
ALTER TABLE Publicidad ADD rutaImagen varchar(255);
ALTER TABLE Publicidad ADD textoPublicitario varchar(255);
ALTER TABLE Publicidad ADD link varchar(255);
ALTER TABLE Publicidad DROP COLUMN dummy;
ALTER TABLE Publicidad ADD CONSTRAINT PK_Publicidad PRIMARY KEY(idPublicidad);

insert into Publicidad  (rutaImagen,link,textoPublicitario) values('\Content\img\publicidad4.gif', 'https://www.coca-cola.com.co/cr/es/home','Coca Cola')
select * from Imagen
--------------------------
-- FIN Definicion de tablas --
--------------------------

--------------------------------
-- Procedimientos Almacenados --
--------------------------------
ALTER PROCEDURE PA_RegistrarCliente(
@pasaporte  varchar(25) ,
@nombre   nvarchar(50),
@apellido1   nvarchar(50),
@apellido2   nvarchar(50),
@edad int  NULL,
@correo nvarchar(255),
@nacionalidad  varchar(25) NULL
)
AS SET NOCOUNT ON;
INSERT INTO Cliente (pasaporte,nombre,primerApellido,segundoApellido,edad,nacionalidad,correo)
VALUES(@pasaporte,@nombre,@apellido1,@apellido2,@edad,@correo,@nacionalidad )
GO
-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_ModificarCliente(
@idCliente  int  ,
@pasaporte  varchar(25) ,
@nombre   nvarchar(50)   NULL,
@apellido1   nvarchar(50)   NULL,
@apellido2   nvarchar(50)  NULL,
@edad int   NULL,
@correo nvarchar(255)    NULL,
@nacionalidad  varchar(25)   NULL
)
AS SET NOCOUNT ON;
UPDATE Cliente  set pasaporte=@pasaporte,
nombre=@nombre,
primerApellido=@apellido1,
segundoApellido=@apellido2,
edad=@edad,
correo=@correo,
nacionalidad=@nacionalidad  WHERE idCliente=@idCliente  
GO
-------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE PA_EliminarCliente(
@idCliente  int) 
AS SET NOCOUNT ON;
Delete From Cliente  WHERE idCliente=@idCliente  
GO
-------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE PA_ListarClientes
AS SET NOCOUNT ON;
Select  idCliente, pasaporte, nombre, primerApellido, segundoApellido, edad, correo, nacionalidad From Cliente   
GO
-------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE PA_RegistrarHabitacion(
@idTipoHabitacion INT,
@idCliente  INT,
@vacante   nvarchar(50),
@capacidad   tinyint)
AS SET NOCOUNT ON;
INSERT INTO Habitacion (idCliente,vacante,capacidad,idTipoHabitacion)VALUES(@idCliente,
@vacante,@capacidad,@idTipoHabitacion )
GO
-------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE PA_ModificarHabitacion(
@idHabitacion INT,
@estado int)
AS SET NOCOUNT ON;
if @estado=3 begin set @estado=0
UPDATE Habitacion  set 
estado=@estado  WHERE idHabitacion=@idHabitacion 
end
else  begin
UPDATE Habitacion  set 
estado=@estado  WHERE idHabitacion=@idHabitacion  end
  
GO

-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_EliminarHabitacion(
@idHabitacion  int ) 
AS SET NOCOUNT ON;
Delete From Habitacion WHERE idHabitacion=@idHabitacion  
GO

-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_ListarHabitacion
AS SET NOCOUNT ON;
Select  idHabitacion , idCliente, vacante, capacidad From Habitacion   
GO
------------------------------------------------------------------------------------------------------------------------
Alter PROCEDURE PA_ListarHabitacionTipoHabitacion(@idTipoHabitacion int)
AS SET NOCOUNT ON;

Select  h.idHabitacion , h.estado From Habitacion  h
join TipoHabitacion t on h.idTipoHabitacion=t.idTipoHabitacion where 
t.idTipoHabitacion=@idTipoHabitacion 

--group by  t.nombreTipoHabitacion , h.estado, t.nombreTipoHabitacion
GO



----------------------
---------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_RegistrarReservacion(
@idHabitacion tinyint,
@idCliente  int,
@fechaLlegada date,
@fechaSalida  date)
AS SET NOCOUNT ON;
INSERT INTO Reservacion (idHabitacion ,idCliente,fechaLlegada ,fechaSalida )
VALUES(@idHabitacion,@idCliente,
@fechaLlegada,@fechaSalida)
GO
-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_ModificarReservacion(
@idReservacion int,
@idHabitacion tinyint,
@idCliente  int,
@fechaEntrada date,
@fechaSalida  date )
AS SET NOCOUNT ON;
UPDATE Reservacion  set
idHabitacion=@idHabitacion,idCliente=@idCliente,fechaLlegada=@fechaEntrada,
fechaSalida=@fechaSalida   
  WHERE idReservacion =@idReservacion 
GO
select * from Reservacion
-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_EliminarReservacion(
@idReservacion int) 
AS SET NOCOUNT ON;
Delete From Reservacion WHERE idReservacion =@idReservacion 
GO
-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_ListarReservacion
AS SET NOCOUNT ON;
Select idReservacion , idHabitacion ,idCliente,fechaLlegada ,fechaSalida
 From Reservacion
GO
-------------------------------------------------------------------------------------------------------------------------
alter PROCEDURE PA_FiltrarReservacionById(@idReservacion int)
AS SET NOCOUNT ON;
Select   idReservacion , idHabitacion ,idCliente,fechaLlegada ,fechaSalida
From Reservacion WHERE
idReservacion =@idReservacion   
GO

-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_RegistrarTipoHabitacion(
@nombreTipoHabitacion varchar(25)  NULL,
@precioBase  int   NULL
)
AS SET NOCOUNT ON;
INSERT INTO TipoHabitacion(nombreTipoHabitacion,precioBase )
VALUES(@nombreTipoHabitacion,@precioBase )
GO
-------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE PA_ModificarTipoHabitacion(
@idTipoHabitacion int,
@nombreTipoHabitacion varchar(25),
@descripcion varchar(max),
@precioBase  int)
AS SET NOCOUNT ON;
UPDATE TipoHabitacion set
nombreTipoHabitacion=@nombreTipoHabitacion,
precioBase=@precioBase,
descripcion=@descripcion
WHERE idTipoHabitacion= @idTipoHabitacion 
GO

-----------------------------------------------------
CREATE PROCEDURE PA_ModificarImagenTipoHabitacion(
@idTipoHabitacion int,
@imgRuta varchar(max))
AS SET NOCOUNT ON;
UPDATE TipoHabitacion set
imagenUrl=@imgRuta
WHERE idTipoHabitacion= @idTipoHabitacion 
GO
-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_EliminarTipoHabitacion(
@idTipoHabitacion int)
AS SET NOCOUNT ON;
Delete From TipoHabitacion WHERE idTipoHabitacion=@idTipoHabitacion 
GO
-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_ListarTipoHabitacion
AS SET NOCOUNT ON;
Select idTipoHabitacion ,nombreTipoHabitacion,precioBase From TipoHabitacion 
GO
---------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_FiltrarTipoHabitacion(@idTipoHabitacion int)
AS SET NOCOUNT ON;
Select idTipoHabitacion ,nombreTipoHabitacion,precioBase,imagenUrl,descripcion From TipoHabitacion 
WHERE idTipoHabitacion= @idTipoHabitacion 
GO
-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_RegistrarEmpleado(
@tipoEmpleado tinyint,
@idUsuario varchar(50),
@contrasenna varchar(255))
AS SET NOCOUNT ON;
INSERT INTO Empleado(tipoEmpleado,idUsuario,contrasenna)
VALUES(@tipoEmpleado,@idUsuario,@contrasenna)
GO
-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_ModificarEmpleado (
@idEmpleado smallint,
@tipoEmpleado tinyint,
@idUsuario varchar(50),
@contrasenna varchar(255))
AS SET NOCOUNT ON;
UPDATE Empleado set
tipoEmpleado=@tipoEmpleado,idUsuario=@idUsuario,contrasenna=@contrasenna 
WHERE idEmpleado =@idEmpleado 
GO
-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_EliminarEmpleado(
@idEmpleado smallint )
AS SET NOCOUNT ON;
Delete From Empleado WHERE idEmpleado=@idEmpleado
GO
-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_ListarEmpleado 
AS SET NOCOUNT ON;
Select idEmpleado,tipoEmpleado,idUsuario,contrasenna
 From Empleado 
GO
-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_RegistrarPromocion(
@fechaInicio date,
@fechaFinal date,
@informacion varchar(300),
@rebaja int)
AS SET NOCOUNT ON;
INSERT INTO Promocion(fechaInicio,fechaFinal,informacion,rebaja)
VALUES(@fechaInicio,@fechaFinal,@informacion, @rebaja)
GO
-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_ModificarPromocion (
@idPromocion smallint,
@fechaInicio date,
@fechaFinal date,
@informacion varchar(300),
@rebaja int)
AS SET NOCOUNT ON;
UPDATE Promocion set
fechaInicio=@fechaInicio ,fechaFinal=@fechaFinal,
informacion=@informacion ,rebaja=@rebaja WHERE idPromocion =@idPromocion 
GO
-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_EliminarPromocion(
@idPromocion smallint)
AS SET NOCOUNT ON;
Delete From Promocion WHERE idPromocion =@idPromocion 
GO
-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_ListarPromocion
AS SET NOCOUNT ON;
Select idPromocion,fechaInicio,fechaFinal,informacion,rebaja From Promocion 
GO
-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_RegistrarPublicidad (
@rutaImagen varchar(255),
@link varchar(255))
AS SET NOCOUNT ON;
INSERT INTO Publicidad (rutaImagen,link )
VALUES(@rutaImagen,@link )
GO
-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_ModificarPublicidad (
@idPublicidad smallinT,
@rutaImagen varchar(255),
@link varchar(255)
)
AS SET NOCOUNT ON;
UPDATE Publicidad set
rutaImagen=@rutaImagen ,link=@link  WHERE idPublicidad=@idPublicidad 
GO
-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_EliminarPublicidad (
@idPublicidad smallint)
AS SET NOCOUNT ON;
Delete From Publicidad WHERE idPublicidad= @idPublicidad 
GO
-------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE PA_ListarPublicidad 
AS SET NOCOUNT ON;
Select idPublicidad,rutaImagen,link,textoPublicitario From Publicidad 
GO
exec PA_ListarPublicidad
-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_ModificarContenido( @contenido varchar(max),@titulo varchar(40), @idContenido int)
AS SET NOCOUNT ON;
update Contenido set contenido=@contenido,titulo=@titulo WHERE idContenido=@idContenido   
GO
select * from Reservacion
select * from TipoHabitacion
select * from Habitacion
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_ModificarCoordenadasDestino(@latitudDestino varchar(max),@longitudDestino varchar(max))
AS SET NOCOUNT ON;
Update Mapa set latitudDestino=@latitudDestino, longitudDestino=@longitudDestino
GO
-------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE PA_ModificarCoordenadasOrigen(@latitudOrigen varchar(max),@longitudOrigen varchar(max))
AS SET NOCOUNT ON;
Update Mapa set latitudOrigen=@latitudOrigen, longitudOrigen=@longitudOrigen
GO
-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_ListarCoordenadasOrigen
AS SET NOCOUNT ON;
Select latitudOrigen,longitudOrigen from Mapa
GO
-------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_ListarItinerario
AS SET NOCOUNT ON;
SELECT dia,desayuno,imagenDesayuno,almuerzo,imagenAlmuerzo,cena,imagenCena FROM Itinerario
GO
-------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE PA_ListarContenido(@idContenido smallint)
AS SET NOCOUNT ON;
select titulo,contenido from Contenido where idContenido=@idContenido
GO

Alter PROCEDURE PA_ListarImagenes(@tipo int)
AS SET NOCOUNT ON;
SELECT ruta, idImagen from Imagen where  tipo=@tipo
GO

 exec PA_ListarImagenes 1
-------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE PA_SugerenciaReservacion
AS SET NOCOUNT ON;
SELECT fechaLlegada,fechaSalida, idHabitacion FROM Reservacion 
GO

-------------------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE PA_VerificarReservacion(@fechaLlegada date,@fechaSalida date, @tipoHabitacion int)
AS SET NOCOUNT ON;
SELECT 
 o.idHabitacion

from Reservacion r 
join Habitacion o on r.idHabitacion=o.idHabitacion
join TipoHabitacion t on o.idTipoHabitacion=t.idTipoHabitacion
where o.idTipoHabitacion =t.idTipoHabitacion 
and r.idHabitacion=o.idHabitacion
and
(select min(r.fechaLlegada) from Reservacion r join Habitacion o on r.idHabitacion=o.idHabitacion where o.idTipoHabitacion =@tipoHabitacion ) < @fechaSalida
AND (select max(r.fechaSalida) from Reservacion r join Habitacion o on r.idHabitacion=o.idHabitacion  where o.idTipoHabitacion =@tipoHabitacion ) > @fechaLlegada
AND o.estado=1
 GO

 ALTER PROCEDURE PA_VerificarReservacion2(@fechaLlegada date,@fechaSalida date, @tipoHabitacion int)
 AS SET NOCOUNT ON;

if ((select min(r.fechaLlegada) from Reservacion r join Habitacion o on r.idHabitacion=o.idHabitacion where o.idTipoHabitacion =@tipoHabitacion ) < @fechaSalida
AND (select max(r.fechaSalida) from Reservacion r join Habitacion o on r.idHabitacion=o.idHabitacion  where o.idTipoHabitacion =@tipoHabitacion ) > @fechaLlegada
AND o.estado=1)then

end
 Select * from Reservacion r join Habitacion h  on r.idHabitacion=h.idHabitacion
 join TipoHabitacion t on  t.idTipoHabitacion=h.idTipoHabitacion
 where 

 GO
 select top 1 idHabitacion from Habitacion where estado=0

 

 exec PA_VerificarReservacion '2020-06-11','2020-06-21',1
------------------------------------------------------------------------------------------------------------------------
alter procedure PA_Login( @Usuario varchar(30),  @Contra  varchar(30), @TipoUsuario int)
as	SET NOCOUNT ON;
  select count(*) as cantidad from Empleado e where e.TipoEmpleado=@TipoUsuario and e.IdUsuario= @Usuario and  e.Contrasenna= @contra; 
 GO
 ------------------------------------------------------------------------------------------------------------------------
 alter procedure PA_Login(@Usuario varchar(30), @Contra  varchar(30), @TipoUsuario int)
 as	SET NOCOUNT ON;
  select count(*) as cantidad from Empleado e where e.tipoEmpleado=@TipoUsuario and e.idUsuario= @Usuario and  Cast(DecryptByPassPhrase('password', contrasenna) As varchar(max))= @contra; 
 GO
 ------------------------------------------------------------------------------------------------------------------------
 create procedure PA_InsertarEmpleado(@Usuario varchar(30),@Contra  varchar(30), @TipoUsuario int)
 as set nocount on;
INSERT INTO [dbo].[Empleado] ([tipoEmpleado],[idUsuario],[contrasenna]) VALUES (1,  'admin' , ENCRYPTBYPASSPHRASE('password','admin' ))
GO
------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE PA_ModificarImagen(@idImagen int, @ruta varchar(30))
 as set nocount on;
 select * from Imagen
 update Imagen set ruta=@ruta where idImagen=@idImagen
 go
 ---------------------------------------
---------------------------------------------------------------------------------------------------------------------------
alter procedure PA_EstadoHoyHabitacion
as set nocount on;
select h.idHabitacion, t.nombreTipoHabitacion, h.estado from Reservacion r join Habitacion h on r.idHabitacion=h.idHabitacion
join TipoHabitacion t on t.idTipoHabitacion=h.idTipoHabitacion  
where r.fechaLlegada<=getdate() and GETDATE()<=r.fechaSalida



select r.idReservacion, r.idHabitacion,t.idTipoHabitacion, r.fechaLlegada,r.fechaSalida,h.estado,t.nombreTipoHabitacion  from Reservacion r join Habitacion  h  on r.idHabitacion=h.idHabitacion
join TipoHabitacion t on t.idTipoHabitacion=h.idTipoHabitacion

----------------------------------------------------------------------------------------------------------------------------
select * from TipoHabitacion
select * from Reservacion
select * from Cliente
ALTER TRIGGER TR_CambiarEstado on Reservacion
After update
AS
declare @fechaActual datetime,@idHabitacion int ,@IdReserva int; set @fechaActual=getdate();
if exists (select * from inserted ) and  exists(select * from deleted)begin
if(select fechaSalida from inserted ) < @fechaActual begin 
set @idHabitacion=(select idHabitacion from inserted)
set @idReserva=(select idReservacion from inserted)
update Habitacion set estado=0 where idHabitacion=@idHabitacion;
--delete  from Reservacion  where idReservacion=@IdReserva;
end
end
GO

select *from Habitacion
select *from Reservacion
