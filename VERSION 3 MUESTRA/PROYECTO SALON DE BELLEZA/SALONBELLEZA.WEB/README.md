# Atelier Barber Club — Frontend web para SALONBELLEZA

Interfaz web administrativa construida en HTML, CSS y JavaScript sin dependencias de npm. Está preparada para consumir tu backend ASP.NET Core `SALONBELLEZA.API`.

## Requisitos
- Backend C# ejecutándose.
- MySQL activo y base `salonbellezabd` creada.
- Navegador moderno.
- Para servir los archivos: Visual Studio Code + Live Server, o cualquier servidor estático.

## Ejecutar
1. Abrí tu solución `PROYECTOSALON26.slnx` en Visual Studio.
2. Ejecutá `SALONBELLEZA.API` y verificá que responda en `http://localhost:5180`.
3. Abrí esta carpeta con Visual Studio Code.
4. Ejecutá `index.html` con Live Server (por ejemplo `http://127.0.0.1:5500`).
5. En el menú Configuración, verificá la dirección base de la API.

## Conexión detectada en tu backend
El archivo `launchSettings.json` declara:
- HTTP: `http://localhost:5180`
- HTTPS: `https://localhost:7180`

`Program.cs` ya permite CORS para `http://localhost:3000`, `http://localhost:5173` y `http://localhost:4200`. Si servís el frontend desde otro puerto (por ejemplo Live Server en 5500), agregá ese origen a la política `AllowFrontend` en `Program.cs`, reiniciá la API y volvé a probar.

## Módulos y endpoints usados
- Panel: `GET /api/Dashboard`
- Clientes: `/api/Clientes` (GET, GET id, POST, PUT id, DELETE id)
- Empleados: `/api/Empleados` (GET, GET id, POST, PUT id, DELETE id)
- Servicios: `/api/Servicios` (GET, GET id, POST, PUT id, DELETE id)
- Productos: `/api/Productos` (GET, GET id, POST, PUT id, DELETE id, PATCH id/stock)
- Proveedores: `/api/Proveedores` (GET, GET id, POST, PUT id, DELETE id)
- Turnos: `/api/Turnos` (GET, GET id, POST, POST id/servicios)
- Compras: `/api/Compras` (GET, GET id, POST)
- Ventas: `/api/Ventas` (GET, GET id, POST turno, POST producto)
- Pagos: `/api/Pagos/venta/{idVenta}` (GET), `/api/Pagos` (POST)
- Facturas: `/api/Facturas` (GET, GET id, POST)
- Membresías: `/api/Membresias` (GET, POST, POST asignar)
- Categorías: `/api/Categorias/producto` y `/api/Categorias/servicio`

## Importante: alcance de esta primera entrega
Esta es una primera interfaz funcional para conectar con la API existente. Los listados y formularios CRUD principales están implementados para clientes, empleados, servicios, productos y proveedores. Turnos, compras, ventas, pagos, facturas y membresías tienen vistas iniciales; algunas operaciones requieren formularios específicos que deben ajustarse a los DTO y a las reglas del negocio. Los IDs de categoría, proveedor, producto, cliente y empleado se ingresan como identificadores en esta versión inicial.

No se incluyó autenticación porque el backend revisado no expone endpoints de login ni autorización por roles. No publiques esta interfaz en Internet sin agregar autenticación y HTTPS.

## Archivos
- `index.html`: estructura.
- `styles.css`: diseño adaptable.
- `app.js`: navegación, conexión HTTP, vistas, formularios y operaciones.


## Versión 4: rediseño e inicio de sesión
Se incorporó una pantalla de inicio de sesión visual y un cierre de sesión. Credenciales locales de demostración:
- Usuario: `admin@atelier.com`
- Contraseña: `Atelier Barber Club2026!`

**Seguridad:** este acceso es únicamente una puerta de demostración del frontend; las credenciales están en el JavaScript y no protegen la API. Antes de usar el sistema con datos reales o publicarlo, implementar autenticación en ASP.NET Core (hash de contraseñas, validación en servidor, JWT/cookies seguras, autorización por roles y protección de endpoints).


## Versión 5: página de inicio
Se agregó una pantalla de inicio tipo dashboard de bienvenida como primera vista después del login, con saludo dinámico, fecha, accesos rápidos y resumen del barbería. El ítem de navegación ahora figura como `Inicio`.


## Versión multipágina (v6)
Cada módulo tiene ahora su propio documento HTML:
- `login.html`: inicio de sesión.
- `inicio.html`: dashboard.
- `turnos.html`, `clientes.html`, `servicios.html`, `empleados.html`.
- `productos.html`, `categorias.html`, `proveedores.html`, `compras.html`.
- `ventas.html`, `pagos.html`, `facturas.html`, `membresias.html`.
- `configuracion.html`: URL de la API.

Los documentos comparten `styles.css` y `app.js`, pero la navegación cambia entre archivos HTML separados. `login.js` administra el acceso de demostración.

### Cómo iniciar
Serví la carpeta completa con Live Server. Abrí `login.html` (no `index.html`). Tras iniciar sesión se abre `inicio.html`. Todos los HTML, CSS y JS deben permanecer en la misma carpeta.

**Seguridad:** el login sigue siendo de demostración del lado del navegador. La sesión en `sessionStorage` no reemplaza autenticación/autorización en ASP.NET Core. Para uso real, implementar login y roles en el backend y proteger cada endpoint.


## Versión 7: identidad visual de barbería
Se actualizó la identidad visual a Atelier Barber Club: paleta grafito, negro, marfil y cobre/dorado; tipografía y contrastes más sobrios; textos de marca orientados a barbería; y fotografía de barber shop en el acceso.


## Corrección v7.1: mostrar siempre el login
Se quitó la redirección automática desde `login.html` hacia `inicio.html` cuando existe una sesión previa en `sessionStorage`. Ahora al abrir `login.html` siempre se muestra la pantalla de acceso; solo después de validar las credenciales se ingresa al panel. El guard de las páginas internas se mantiene.
