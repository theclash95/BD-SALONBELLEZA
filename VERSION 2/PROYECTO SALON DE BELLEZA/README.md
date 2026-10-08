# SALONBELLEZA26 - Proyecto completado

Proyecto ASP.NET Core .NET 10 organizado en capas, tomando como referencia la arquitectura del proyecto Remis.

## Base de datos
La aplicación usa exclusivamente la base `salonbellezabd` definida en `SALON BELLEZA FINAL-(procedimientos).sql`.

## Capas
- SALONBELLEZA.API: endpoints HTTP.
- SALONBELLEZA.CORE: entidades, interfaces y reglas de negocio.
- SALONBELLEZA.INFRASTRUCTURE: MySQL, repositorio y procedimientos almacenados.
- SALONBELLEZA.SHARED: proyecto compartido.
- SALONBELLEZA.TEST: prueba de conexión.

## Endpoints
- POST /api/salon/clientes
- POST /api/salon/empleados
- POST /api/salon/categorias-producto
- POST /api/salon/productos
- POST /api/salon/productos/stock
- POST /api/salon/proveedores
- POST /api/salon/compras
- POST /api/salon/servicios
- POST /api/salon/turnos
- POST /api/salon/turnos/servicios
- POST /api/salon/ventas/productos
- POST /api/salon/pagos
- POST /api/salon/facturas
- POST /api/salon/membresias
- POST /api/salon/membresias/asignar
- GET /api/salon/estado

## Importante
1. Ejecutar primero el SQL del salón en MySQL/HeidiSQL.
2. Verificar usuario, contraseña y puerto en `SALONBELLEZA.API/appsettings.json`.
3. Abrir el proyecto en Visual Studio.
4. Restaurar paquetes NuGet.
5. Ejecutar la API.
6. Probar los endpoints con Bruno.

No se copiaron los procedimientos `sp_persona_*` de Remis porque no forman parte de la base del salón.
