CREATE DATABASE db_SalonBelleza;
USE db_SalonBelleza;

CREATE TABLE Personas (
    IdPersona INT PRIMARY KEY,
    dni VARCHAR(10),
    nombre VARCHAR(50),
    apellido VARCHAR(50),
    telefono VARCHAR(20),
    email VARCHAR(100),
    direccion VARCHAR(100),
    activo BOOLEAN,
    fecha_alta DATE
);

CREATE TABLE Categoria_Producto (
    IdCategoriaProducto INT PRIMARY KEY,
    nombre VARCHAR(50),
    descripcion VARCHAR(255),
    activo BOOLEAN
);

CREATE TABLE Proveedor (
    IdProveedor INT PRIMARY KEY,
    razon_social VARCHAR(100),
    cuit VARCHAR(20),
    telefono VARCHAR(20),
    email VARCHAR(100),
    direccion VARCHAR(100),
    activo VARCHAR(50)
);

CREATE TABLE Categoria_Servicio (
    IdCategoriaServicio INT PRIMARY KEY,
    nombre VARCHAR(50),
    descripcion VARCHAR(255),
    activo BOOLEAN
);

CREATE TABLE Membresia (
    IdMembresia INT PRIMARY KEY,
    nombre_plan VARCHAR(100),
    descripcion TEXT,
    costo DECIMAL(10,2),
    duracion_dias INT,
    porcentaje_descuento DECIMAL(5,2),
    estado VARCHAR(50)
);

CREATE TABLE Productos (
    IdProducto INT PRIMARY KEY,
    id_categoria_producto INT,
    codigo VARCHAR(50),
    nombre VARCHAR(100),
    descripcion TEXT,
    precio_costo DECIMAL(10,2),
    precio_venta DECIMAL(10,2),
    stock_actual INT,
    stock_minimo INT,
    estado VARCHAR(50),
    fecha_alta DATE,
    FOREIGN KEY (id_categoria_producto) REFERENCES Categoria_Producto(IdCategoriaProducto)
);

CREATE TABLE Servicios (
    IdServicio INT PRIMARY KEY,
    id_categoria_servicio INT,
    nombre VARCHAR(100),
    descripcion TEXT,
    precio DECIMAL(10,2),
    duracion_minuto INT,
    estado VARCHAR(50),
    FOREIGN KEY (id_categoria_servicio) REFERENCES Categoria_Servicio(IdCategoriaServicio)
);

CREATE TABLE Clientes (
    IdCliente INT PRIMARY KEY,
    id_persona INT,
    categoria_cliente VARCHAR(50),
    fecha_registro DATE,
    estado VARCHAR(50),
    FOREIGN KEY (id_persona) REFERENCES Personas(IdPersona)
);

CREATE TABLE Empleado (
    IdEmpleado INT PRIMARY KEY,
    id_persona INT,
    tipo_empleado VARCHAR(50),
    fecha_ingreso DATE,
    estado VARCHAR(50),
    FOREIGN KEY (id_persona) REFERENCES Personas(IdPersona)
);

CREATE TABLE Compra (
    IdCompra INT PRIMARY KEY,
    id_proveedor INT,
    fecha_hora DATETIME,
    estado VARCHAR(50),
    subtotal DECIMAL(10,2),
    total DECIMAL(10,2),
    observacion VARCHAR(255),
    FOREIGN KEY (id_proveedor) REFERENCES Proveedor(IdProveedor)
);

CREATE TABLE DetalleCompra (
    IdDetalleCompra INT PRIMARY KEY,
    id_compra INT,
    id_producto INT,
    cantidad INT,
    precio_costo DECIMAL(10,2),
    FOREIGN KEY (id_compra) REFERENCES Compra(IdCompra),
    FOREIGN KEY (id_producto) REFERENCES Productos(IdProducto)
);

CREATE TABLE Turnos (
    IdTurno INT PRIMARY KEY,
    id_cliente INT,
    id_empleado INT,
    fecha DATE,
    hora TIME,
    estado VARCHAR(50),
    observacion TEXT,
    fecha_creacion DATETIME,
    FOREIGN KEY (id_cliente) REFERENCES Clientes(IdCliente),
    FOREIGN KEY (id_empleado) REFERENCES Empleado(IdEmpleado)
);

CREATE TABLE DetalleTurno (
    IdDetalleTurno INT PRIMARY KEY,
    id_turno INT,
    id_servicio INT,
    precio_unitario DECIMAL(10,2),
    descuento DECIMAL(10,2),
    subTotal DECIMAL(10,2),
    FOREIGN KEY (id_turno) REFERENCES Turnos(IdTurno),
    FOREIGN KEY (id_servicio) REFERENCES Servicios(IdServicio)
);

CREATE TABLE Ventas (
    IdVenta INT PRIMARY KEY,
    id_cliente INT,
    id_turno INT,
    fecha_hora DATETIME,
    tipo_venta VARCHAR(50),
    estado VARCHAR(50),
    subTotal DECIMAL(10,2),
    descuento DECIMAL(10,2),
    total DECIMAL(10,2),
    observacion VARCHAR(255),
    FOREIGN KEY (id_cliente) REFERENCES Clientes(IdCliente),
    FOREIGN KEY (id_turno) REFERENCES Turnos(IdTurno)
);

CREATE TABLE DetalleVenta (
    IdDetalleVenta INT PRIMARY KEY,
    id_venta INT,
    id_producto INT,
    id_servicio INT,
    cantidad INT,
    precio_unitario DECIMAL(10,2),
    descuento DECIMAL(10,2),
    subTotal DECIMAL(10,2),
    FOREIGN KEY (id_venta) REFERENCES Ventas(IdVenta),
    FOREIGN KEY (id_producto) REFERENCES Productos(IdProducto),
    FOREIGN KEY (id_servicio) REFERENCES Servicios(IdServicio)
);

CREATE TABLE Factura (
    IdFactura INT PRIMARY KEY,
    id_venta INT,
    numero_factura VARCHAR(10),
    fecha_emision DATETIME,
    tipo_comprobante VARCHAR(50),
    estado VARCHAR(50),
    subtotal DECIMAL(10,2),
    descuento DECIMAL(10,2),
    total DECIMAL(10,2),
    FOREIGN KEY (id_venta) REFERENCES Ventas(IdVenta)
);

CREATE TABLE Pagos (
    IdPago INT PRIMARY KEY,
    id_venta INT,
    fecha_pago DATETIME,
    monto DECIMAL(10,2),
    metodo_pago VARCHAR(50),
    estado VARCHAR(50),
    referencia VARCHAR(100),
    observacion VARCHAR(255),
    FOREIGN KEY (id_venta) REFERENCES Ventas(IdVenta)
);

CREATE TABLE Movimiento_Stock (
    IdMovimiento INT PRIMARY KEY,
    id_producto INT,
    id_detalle_venta INT,
    tipo_movimiento VARCHAR(50),
    fecha_hora DATETIME,
    motivo VARCHAR(255),
    stock_anterior INT,
    stock_posterior INT,
    FOREIGN KEY (id_producto) REFERENCES Productos(IdProducto),
    FOREIGN KEY (id_detalle_venta) REFERENCES DetalleVenta(IdDetalleVenta)
);

CREATE TABLE Cliente_Membresia (
    IdClienteMembresia INT PRIMARY KEY,
    id_cliente INT,
    id_membresia INT,
    fecha_inicio DATE,
    fecha_fin DATE,
    estado VARCHAR(50),
    observacion VARCHAR(255),
    FOREIGN KEY (id_cliente) REFERENCES Clientes(IdCliente),
    FOREIGN KEY (id_membresia) REFERENCES Membresia(IdMembresia)
);

CREATE TABLE Pago_Membresia (
    IdPagoMembresia INT PRIMARY KEY,
    id_cliente_membresia INT,
    fecha_pago DATETIME,
    monto_pago DECIMAL(10,2),
    metodo_pago VARCHAR(50),
    estado VARCHAR(50),
    referencia VARCHAR(255),
    FOREIGN KEY (id_cliente_membresia) REFERENCES Cliente_Membresia(IdClienteMembresia)
);

CREATE TABLE Salon (
    IdSalon INT PRIMARY KEY,
    nombre VARCHAR(100),
    direccion VARCHAR(100),
    telefono VARCHAR(30),
    email VARCHAR(100),
    logo VARCHAR(255),
    activo BOOLEAN,
    fecha_alta DATE
);
