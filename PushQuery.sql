CREATE VIEW [EncabezadoDetalle] AS
SELECT e.EncabezadoId as encabezadoId, e.TipoRegistro as tipoRegistroEncabezado, e.TipoArchivo as tipoArchivoEncabezado,
       e.Identificacion as RNC, e.Periodo, 
       d.DetalleId, d.TipoRegistro as tipoRegistroDetalle, d.TipoIdEmpleado, d.EmpleadoId, d.Sueldo, d.SueldoNeto, 
       d.NoSeguridadSocial
FROM Encabezado e
INNER JOIN Detalle d ON e.EncabezadoId = d.EncabezadoId
;

select * from EncabezadoDetalle

select * from Detalle

Insert into Detalle values(7,'D','C', '6155646466', '85000','80000','15151515151')