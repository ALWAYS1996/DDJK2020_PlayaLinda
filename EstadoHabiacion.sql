CREATE PROCEDURE PA_VerificarReservacion1(@fechaLlegada date,@fechaSalida date, @tipoHabitacion int,@salida int output)
AS SET NOCOUNT ON;

  set @salida=( SELECT 
count(*) as fecha

from Reservacion r 
join Habitacion o on r.idHabitacion=o.idHabitacion
join TipoHabitacion t on o.idTipoHabitacion=t.idTipoHabitacion
where o.idTipoHabitacion =t.idTipoHabitacion 
and r.idHabitacion=o.idHabitacion
and
(select min(r.fechaLlegada) from Reservacion r join Habitacion o on r.idHabitacion=o.idHabitacion where o.idTipoHabitacion =@tipoHabitacion ) < @fechaSalida
AND (select max(r.fechaSalida) from Reservacion r join Habitacion o on r.idHabitacion=o.idHabitacion  where o.idTipoHabitacion =@tipoHabitacion ) > @fechaLlegada
AND o.estado=1)


GO

select * from Publicidad
select * from Habitacion
--si devuelve un 0 quiere decir habitacion disponible y le doy una habitacion cualquiera
--si devulve un 1 le asigno una
alter PROCEDURE PA_VerificarReservacion(@fechaLlegada date,@fechaSalida date, @tipoHabitacion int, @parametroSalida int output  ) 
as set nocount on
declare @salida int; declare @disponibilidad int;

exec  PA_VerificarReservacion1 @fechaLlegada,@fechaSalida,@tipoHabitacion,@salida output

if @salida<=0  begin --SI Cuando ejecuto el SP ME DEVUELVE 0 QUIERE DECIR QUE PUEDE RESERVAR

--Entonces, verifico si hay un tipo de habitacion disponible en los intervalos que el usuario ingresa, y que me devuelva esa habitacion
set @disponibilidad=(select top 1  idHabitacion as [Fechas ocupadas pero Habitaciones disponibles por tipo habitacion elegida] from Habitacion where estado=0 AND  idTipoHabitacion=@tipoHabitacion);

--Si NO hay ningun tipo de habitacion entre los parametros que el usuario ingresa, seteo la variable a 0
if @disponibilidad is null set @disponibilidad=0;

--Y le digo que me devuelva cualquier habitacion pero que este disponible
if @disponibilidad=0 begin  
set @parametroSalida=(select top 1  idHabitacion as [fechas ocupadas y tipo habitacion. Le asignamos cualquiera] from Habitacion where estado=0);
end
else begin 
--E Imprimo el resultado en caso de hacer lo de arriva
set @parametroSalida=(SELECT @disponibilidad  as [Fechas ocupadas pero Habitaciones disponibles por tipo habitacion elegida]);
end

end--Fin if salida

--Pero si entre los intervalos de fechas que ingresa el usuario y tipo de habitacion esta ocupado o reservado, retorneme 0
else if @salida>1  begin  set @parametroSalida=(select -1 as OCUPADA) ; end
go

SELECT * FROM Reservacion;
select * from Habitacion;
Select * from TipoHabitacion;

declare  @parametroSalida int;
exec PA_VerificarReservacion  '2020-06-12','2020-06-20',5,@parametroSalida output
select @parametroSalida as [Estado/AsignacionHabitacion];

exec PA_VerificarReservacion  '2020-04-06','2020-04-09',0

exec PA_VerificarReservacion  '2020-05-05','2020-05-09',4

exec PA_VerificarReservacion  '2020-05-24','2020-06-08',

exec PA_VerificarReservacion  '2020-05-30','2020-05-31',

exec PA_VerificarReservacion  '2020-06-11','2020-06-26',

exec PA_VerificarReservacion  '2020-06-22','2020-06-30',

exec PA_BrindarReservacion '2020-05-11', '2020-05-11', 1 -- = 6, 7, 8, 9 o 10
exec PA_BrindarReservacion '2020-05-11', '2020-05-11', 4 --= 3
exec PA_BrindarReservacion '2020-05-04', '2020-05-08', 5 --= 0
