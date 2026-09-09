CREATE DATABASE IF NOT EXISTS salonbellezabd
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE salonbellezabd;

-- =========================================================
-- 1. PERSONAS
-- =========================================================
CREATE TABLE Personas (
    id_persona INT AUTO_INCREMENT PRIMARY KEY,
    dni VARCHAR(10),
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50) NOT NULL,
    telefono VARCHAR(20),
    email VARCHAR(100),
    direccion VARCHAR(100),
    activo BOOLEAN DEFAULT TRUE,
    fecha_alta DATE
);

-- =========================================================
-- 2. PROVEEDORES
-- =========================================================
CREATE TABLE Proveedores (
    id_proveedor INT AUTO_INCREMENT PRIMARY KEY,
    razon_social VARCHAR(100) NOT NULL,
    cuit VARCHAR(20),
    telefono VARCHAR(20),
    email VARCHAR(100),
    direccion VARCHAR(100),
    activo VARCHAR(50)
);

-- =========================================================
-- 3. CATEGORIAS DE PRODUCTOS
-- =========================================================
CREATE TABLE Categorias_Producto (
    id_categoria_producto INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    descripcion VARCHAR(255),
    activo BOOLEAN DEFAULT TRUE
);

-- =========================================================
-- 4. PRODUCTOS
-- =========================================================
CREATE TABLE Productos (
    id_producto INT AUTO_INCREMENT PRIMARY KEY,
    id_categoria_producto INT NOT NULL,
    codigo VARCHAR(50),
    nombre VARCHAR(100) NOT NULL,
    descripcion TEXT,
    precio_costo DECIMAL(10,2),
    precio_venta DECIMAL(10,2) NOT NULL,
    stock_actual INT DEFAULT 0,
    stock_minimo INT DEFAULT 0,
    estado VARCHAR(50),
    fecha_alta DATE,

    CONSTRAINT fk_productos_categoria
        FOREIGN KEY (id_categoria_producto)
        REFERENCES Categorias_Producto(id_categoria_producto)
);

-- =========================================================
-- 5. COMPRAS
-- =========================================================
CREATE TABLE Compras (
    id_compra INT AUTO_INCREMENT PRIMARY KEY,
    id_proveedor INT NOT NULL,
    fecha_hora DATETIME,
    estado VARCHAR(50),
    subtotal DECIMAL(10,2),
    total DECIMAL(10,2),
    observacion VARCHAR(255),

    CONSTRAINT fk_compras_proveedor
        FOREIGN KEY (id_proveedor)
        REFERENCES Proveedores(id_proveedor)
);

-- =========================================================
-- 6. DETALLE DE COMPRAS
-- =========================================================
CREATE TABLE Detalle_Compra (
    id_detalle_compra INT AUTO_INCREMENT PRIMARY KEY,
    id_compra INT NOT NULL,
    id_producto INT NOT NULL,
    cantidad INT NOT NULL,
    precio_costo DECIMAL(10,2) NOT NULL,

    CONSTRAINT fk_detalle_compra_compra
        FOREIGN KEY (id_compra)
        REFERENCES Compras(id_compra),

    CONSTRAINT fk_detalle_compra_producto
        FOREIGN KEY (id_producto)
        REFERENCES Productos(id_producto)
);

-- =========================================================
-- 7. CLIENTES
-- =========================================================
CREATE TABLE Clientes (
    id_cliente INT AUTO_INCREMENT PRIMARY KEY,
    id_persona INT NOT NULL,
    categoria_cliente VARCHAR(50),
    fecha_registro DATE,
    estado VARCHAR(50),

    CONSTRAINT uq_clientes_persona UNIQUE (id_persona),

    CONSTRAINT fk_clientes_persona
        FOREIGN KEY (id_persona)
        REFERENCES Personas(id_persona)
);

-- =========================================================
-- 8. EMPLEADOS
-- =========================================================
CREATE TABLE Empleados (
    id_empleado INT AUTO_INCREMENT PRIMARY KEY,
    id_persona INT NOT NULL,
    tipo_empleado VARCHAR(50),
    fecha_ingreso DATE,
    estado VARCHAR(50),

    CONSTRAINT uq_empleados_persona UNIQUE (id_persona),

    CONSTRAINT fk_empleados_persona
        FOREIGN KEY (id_persona)
        REFERENCES Personas(id_persona)
);

-- =========================================================
-- 9. CATEGORIAS DE SERVICIOS
-- =========================================================
CREATE TABLE Categorias_Servicio (
    id_categoria_servicio INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    descripcion VARCHAR(255),
    activo BOOLEAN DEFAULT TRUE
);

-- =========================================================
-- 10. SERVICIOS
-- =========================================================
CREATE TABLE Servicios (
    id_servicio INT AUTO_INCREMENT PRIMARY KEY,
    id_categoria_servicio INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    descripcion TEXT,
    precio DECIMAL(10,2) NOT NULL,
    duracion_minuto INT,
    estado VARCHAR(50),

    CONSTRAINT fk_servicios_categoria
        FOREIGN KEY (id_categoria_servicio)
        REFERENCES Categorias_Servicio(id_categoria_servicio)
);

-- =========================================================
-- 11. TURNOS
-- =========================================================
CREATE TABLE Turnos (
    id_turno INT AUTO_INCREMENT PRIMARY KEY,
    id_cliente INT NOT NULL,
    id_empleado INT NOT NULL,
    fecha DATE NOT NULL,
    hora TIME NOT NULL,
    estado VARCHAR(50),
    observacion TEXT,
    fecha_creacion DATETIME,

    CONSTRAINT fk_turnos_cliente
        FOREIGN KEY (id_cliente)
        REFERENCES Clientes(id_cliente),

    CONSTRAINT fk_turnos_empleado
        FOREIGN KEY (id_empleado)
        REFERENCES Empleados(id_empleado)
);

-- =========================================================
-- 12. DETALLE DE TURNOS
-- =========================================================
CREATE TABLE Detalle_Turno (
    id_detalle_turno INT AUTO_INCREMENT PRIMARY KEY,
    id_turno INT NOT NULL,
    id_servicio INT NOT NULL,
    precio_unitario DECIMAL(10,2),
    descuento DECIMAL(10,2),
    subtotal DECIMAL(10,2),

    CONSTRAINT fk_detalle_turno_turno
        FOREIGN KEY (id_turno)
        REFERENCES Turnos(id_turno),

    CONSTRAINT fk_detalle_turno_servicio
        FOREIGN KEY (id_servicio)
        REFERENCES Servicios(id_servicio)
);

-- =========================================================
-- 13. VENTAS
-- =========================================================
CREATE TABLE Ventas (
    id_venta INT AUTO_INCREMENT PRIMARY KEY,
    id_cliente INT NOT NULL,
    id_turno INT NULL,
    fecha_hora DATETIME,
    tipo_venta VARCHAR(50),
    estado VARCHAR(50),
    subtotal DECIMAL(10,2),
    descuento DECIMAL(10,2),
    total DECIMAL(10,2),
    observacion VARCHAR(255),

    CONSTRAINT fk_ventas_cliente
        FOREIGN KEY (id_cliente)
        REFERENCES Clientes(id_cliente),

    CONSTRAINT fk_ventas_turno
        FOREIGN KEY (id_turno)
        REFERENCES Turnos(id_turno)
);

-- =========================================================
-- 14. DETALLE DE VENTAS
-- =========================================================
CREATE TABLE Detalle_Venta (
    id_detalle_venta INT AUTO_INCREMENT PRIMARY KEY,
    id_venta INT NOT NULL,
    id_producto INT NULL,
    id_servicio INT NULL,
    cantidad INT NOT NULL DEFAULT 1,
    precio_unitario DECIMAL(10,2),
    descuento DECIMAL(10,2),
    subtotal DECIMAL(10,2),

    CONSTRAINT fk_detalle_venta_venta
        FOREIGN KEY (id_venta)
        REFERENCES Ventas(id_venta),

    CONSTRAINT fk_detalle_venta_producto
        FOREIGN KEY (id_producto)
        REFERENCES Productos(id_producto),

    CONSTRAINT fk_detalle_venta_servicio
        FOREIGN KEY (id_servicio)
        REFERENCES Servicios(id_servicio)
);

-- =========================================================
-- 15. FACTURAS
-- =========================================================
CREATE TABLE Facturas (
    id_factura INT AUTO_INCREMENT PRIMARY KEY,
    id_venta INT NOT NULL,
    numero_factura VARCHAR(10),
    fecha_emision DATETIME,
    tipo_comprobante VARCHAR(50),
    estado VARCHAR(50),
    subtotal DECIMAL(10,2),
    descuento DECIMAL(10,2),
    total DECIMAL(10,2),

    CONSTRAINT fk_facturas_venta
        FOREIGN KEY (id_venta)
        REFERENCES Ventas(id_venta)
);

-- =========================================================
-- 16. PAGOS
-- =========================================================
CREATE TABLE Pagos (
    id_pago INT AUTO_INCREMENT PRIMARY KEY,
    id_venta INT NOT NULL,
    fecha_pago DATETIME,
    monto DECIMAL(10,2),
    metodo_pago VARCHAR(50),
    estado VARCHAR(50),
    referencia VARCHAR(100),

    CONSTRAINT fk_pagos_venta
        FOREIGN KEY (id_venta)
        REFERENCES Ventas(id_venta)
);

-- =========================================================
-- 17. MOVIMIENTOS DE STOCK
-- =========================================================
CREATE TABLE Movimiento_Stock (
    id_movimiento INT AUTO_INCREMENT PRIMARY KEY,
    id_producto INT NOT NULL,
    id_detalle_venta INT NULL,
    tipo_movimiento VARCHAR(50),
    cantidad INT,
    fecha_hora DATETIME,
    motivo VARCHAR(255),
    stock_anterior INT,
    stock_posterior INT,

    CONSTRAINT fk_movimiento_producto
        FOREIGN KEY (id_producto)
        REFERENCES Productos(id_producto),

    CONSTRAINT fk_movimiento_detalle_venta
        FOREIGN KEY (id_detalle_venta)
        REFERENCES Detalle_Venta(id_detalle_venta)
);

-- =========================================================
-- 18. MEMBRESIAS
-- =========================================================
CREATE TABLE Membresias (
    id_membresia INT AUTO_INCREMENT PRIMARY KEY,
    nombre_plan VARCHAR(100) NOT NULL,
    descripcion TEXT,
    costo DECIMAL(10,2),
    duracion_dias INT,
    porcentaje_descuento DECIMAL(5,2),
    estado VARCHAR(50)
);

-- =========================================================
-- 19. CLIENTE - MEMBRESIA
-- =========================================================
CREATE TABLE Cliente_Membresia (
    id_cliente_membresia INT AUTO_INCREMENT PRIMARY KEY,
    id_cliente INT NOT NULL,
    id_membresia INT NOT NULL,
    fecha_inicio DATE,
    fecha_fin DATE,
    estado VARCHAR(50),
    observacion VARCHAR(255),

    CONSTRAINT fk_cliente_membresia_cliente
        FOREIGN KEY (id_cliente)
        REFERENCES Clientes(id_cliente),

    CONSTRAINT fk_cliente_membresia_membresia
        FOREIGN KEY (id_membresia)
        REFERENCES Membresias(id_membresia)
);

-- =========================================================
-- 20. PAGOS DE MEMBRESIA
-- =========================================================
CREATE TABLE Pago_Membresia (
    id_pago_membresia INT AUTO_INCREMENT PRIMARY KEY,
    id_cliente_membresia INT NOT NULL,
    fecha_pago DATETIME,
    monto_pago DECIMAL(10,2),
    metodo_pago VARCHAR(50),
    estado VARCHAR(50),
    referencia VARCHAR(255),

    CONSTRAINT fk_pago_membresia_cliente_membresia
        FOREIGN KEY (id_cliente_membresia)
        REFERENCES Cliente_Membresia(id_cliente_membresia)
);

-- =========================================================
-- 21. SALON
-- =========================================================
CREATE TABLE Salon (
    id_salon INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100),
    direccion VARCHAR(100),
    telefono VARCHAR(30),
    email VARCHAR(100),
    logo VARCHAR(255),
    activo BOOLEAN DEFAULT TRUE,
    fecha_alta DATE
);
