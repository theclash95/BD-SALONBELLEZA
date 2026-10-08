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


-- =========================================================
-- PROCEDIMIENTOS ALMACENADOS
-- =========================================================

DELIMITER //

DROP PROCEDURE IF EXISTS RegistrarCliente //
CREATE PROCEDURE RegistrarCliente (
    IN p_dni VARCHAR(10),
    IN p_nombre VARCHAR(50),
    IN p_apellido VARCHAR(50),
    IN p_telefono VARCHAR(20),
    IN p_email VARCHAR(100),
    IN p_direccion VARCHAR(100),
    IN p_categoria VARCHAR(50)
)
BEGIN
    DECLARE v_id_persona INT;

    START TRANSACTION;
        INSERT INTO Personas (dni, nombre, apellido, telefono, email, direccion, activo, fecha_alta)
        VALUES (p_dni, p_nombre, p_apellido, p_telefono, p_email, p_direccion, TRUE, CURDATE());
        
        SET v_id_persona = LAST_INSERT_ID();

        INSERT INTO Clientes (id_persona, categoria_cliente, fecha_registro, estado)
        VALUES (v_id_persona, p_categoria, CURDATE(), 'ACTIVO');
    COMMIT;
END //

DROP PROCEDURE IF EXISTS RegistrarEmpleado //
CREATE PROCEDURE RegistrarEmpleado (
    IN p_dni VARCHAR(10),
    IN p_nombre VARCHAR(50),
    IN p_apellido VARCHAR(50),
    IN p_telefono VARCHAR(20),
    IN p_email VARCHAR(100),
    IN p_direccion VARCHAR(100),
    IN p_tipo_empleado VARCHAR(50)
)
BEGIN
    DECLARE v_id_persona INT;

    START TRANSACTION;
        INSERT INTO Personas (dni, nombre, apellido, telefono, email, direccion, activo, fecha_alta)
        VALUES (p_dni, p_nombre, p_apellido, p_telefono, p_email, p_direccion, TRUE, CURDATE());
        
        SET v_id_persona = LAST_INSERT_ID();

        INSERT INTO Empleados (id_persona, tipo_empleado, fecha_ingreso, estado)
        VALUES (v_id_persona, p_tipo_empleado, CURDATE(), 'ACTIVO');
    COMMIT;
END //

DROP PROCEDURE IF EXISTS RegistrarCategoriaProducto //
CREATE PROCEDURE RegistrarCategoriaProducto (
    IN p_nombre VARCHAR(50),
    IN p_descripcion VARCHAR(255)
)
BEGIN
    INSERT INTO Categorias_Producto (nombre, descripcion, activo)
    VALUES (p_nombre, p_descripcion, TRUE);
END //

DROP PROCEDURE IF EXISTS RegistrarProducto //
CREATE PROCEDURE RegistrarProducto (
    IN p_id_categoria INT,
    IN p_codigo VARCHAR(50),
    IN p_nombre VARCHAR(100),
    IN p_descripcion TEXT,
    IN p_precio_costo DECIMAL(10,2),
    IN p_precio_venta DECIMAL(10,2),
    IN p_stock INT,
    IN p_stock_minimo INT
)
BEGIN
    INSERT INTO Productos (
        id_categoria_producto, codigo, nombre, descripcion, 
        precio_costo, precio_venta, stock_actual, stock_minimo, 
        estado, fecha_alta
    )
    VALUES (
        p_id_categoria, p_codigo, p_nombre, p_descripcion, 
        p_precio_costo, p_precio_venta, p_stock, p_stock_minimo, 
        'ACTIVO', CURDATE()
    );
END //

DROP PROCEDURE IF EXISTS ActualizarStockProducto //
CREATE PROCEDURE ActualizarStockProducto (
    IN p_id_producto INT,
    IN p_cantidad INT,
    IN p_tipo_movimiento VARCHAR(50),
    IN p_motivo VARCHAR(255)
)
BEGIN
    DECLARE v_stock_actual INT;
    DECLARE v_nuevo_stock INT;

    SELECT stock_actual INTO v_stock_actual FROM Productos WHERE id_producto = p_id_producto;

    IF p_tipo_movimiento = 'INGRESO' THEN
        SET v_nuevo_stock = v_stock_actual + p_cantidad;
    ELSE
        SET v_nuevo_stock = v_stock_actual - p_cantidad;
    END IF;

    START TRANSACTION;
        UPDATE Productos SET stock_actual = v_nuevo_stock WHERE id_producto = p_id_producto;

        INSERT INTO Movimiento_Stock (
            id_producto, tipo_movimiento, cantidad, fecha_hora, motivo, stock_anterior, stock_posterior
        )
        VALUES (
            p_id_producto, p_tipo_movimiento, p_cantidad, NOW(), p_motivo, v_stock_actual, v_nuevo_stock
        );
    COMMIT;
END //

DROP PROCEDURE IF EXISTS RegistrarProveedor //
CREATE PROCEDURE RegistrarProveedor (
    IN p_razon_social VARCHAR(100),
    IN p_cuit VARCHAR(20),
    IN p_telefono VARCHAR(20),
    IN p_email VARCHAR(100),
    IN p_direccion VARCHAR(100)
)
BEGIN
    INSERT INTO Proveedores (razon_social, cuit, telefono, email, direccion, activo)
    VALUES (p_razon_social, p_cuit, p_telefono, p_email, p_direccion, 'ACTIVO');
END //

DROP PROCEDURE IF EXISTS RegistrarCompra //
CREATE PROCEDURE RegistrarCompra (
    IN p_id_proveedor INT,
    IN p_id_producto INT,
    IN p_cantidad INT,
    IN p_precio_costo DECIMAL(10,2),
    IN p_observacion VARCHAR(255)
)
BEGIN
    DECLARE v_id_compra INT;
    DECLARE v_subtotal DECIMAL(10,2);
    DECLARE v_stock_actual INT;

    SET v_subtotal = p_cantidad * p_precio_costo;

    START TRANSACTION;
        INSERT INTO Compras (id_proveedor, fecha_hora, estado, subtotal, total, observacion)
        VALUES (p_id_proveedor, NOW(), 'COMPLETADO', v_subtotal, v_subtotal, p_observacion);
        SET v_id_compra = LAST_INSERT_ID();

        INSERT INTO Detalle_Compra (id_compra, id_producto, cantidad, precio_costo)
        VALUES (v_id_compra, p_id_producto, p_cantidad, p_precio_costo);

        SELECT stock_actual INTO v_stock_actual FROM Productos WHERE id_producto = p_id_producto;
        
        UPDATE Productos 
        SET stock_actual = stock_actual + p_cantidad,
            precio_costo = p_precio_costo
        WHERE id_producto = p_id_producto;

        INSERT INTO Movimiento_Stock (id_producto, tipo_movimiento, cantidad, fecha_hora, motivo, stock_anterior, stock_posterior)
        VALUES (p_id_producto, 'INGRESO', p_cantidad, NOW(), 'Compra a Proveedor', v_stock_actual, v_stock_actual + p_cantidad);
    COMMIT;
END //

DROP PROCEDURE IF EXISTS RegistrarServicio //
CREATE PROCEDURE RegistrarServicio (
    IN p_id_categoria INT,
    IN p_nombre VARCHAR(100),
    IN p_descripcion TEXT,
    IN p_precio DECIMAL(10,2),
    IN p_duracion INT
)
BEGIN
    INSERT INTO Servicios (id_categoria_servicio, nombre, descripcion, precio, duracion_minuto, estado)
    VALUES (p_id_categoria, p_nombre, p_descripcion, p_precio, p_duracion, 'ACTIVO');
END //

DROP PROCEDURE IF EXISTS CrearTurno //
CREATE PROCEDURE CrearTurno (
    IN p_id_cliente INT,
    IN p_id_empleado INT,
    IN p_fecha DATE,
    IN p_hora TIME,
    IN p_observacion TEXT
)
BEGIN
    INSERT INTO Turnos (id_cliente, id_empleado, fecha, hora, estado, observacion, fecha_creacion)
    VALUES (p_id_cliente, p_id_empleado, p_fecha, p_hora, 'PENDIENTE', p_observacion, NOW());
END //

DROP PROCEDURE IF EXISTS AgregarServicioATurno //
CREATE PROCEDURE AgregarServicioATurno (
    IN p_id_turno INT,
    IN p_id_servicio INT,
    IN p_descuento DECIMAL(10,2)
)
BEGIN
    DECLARE v_precio DECIMAL(10,2);
    DECLARE v_subtotal DECIMAL(10,2);

    SELECT precio INTO v_precio FROM Servicios WHERE id_servicio = p_id_servicio;
    SET v_subtotal = v_precio - IFNULL(p_descuento, 0);

    INSERT INTO Detalle_Turno (id_turno, id_servicio, precio_unitario, descuento, subtotal)
    VALUES (p_id_turno, p_id_servicio, v_precio, IFNULL(p_descuento, 0), v_subtotal);
END //

DROP PROCEDURE IF EXISTS RealizarVentaProducto //
CREATE PROCEDURE RealizarVentaProducto (
    IN p_id_cliente INT,
    IN p_id_producto INT,
    IN p_cantidad INT
)
BEGIN
    DECLARE v_precio DECIMAL(10,2);
    DECLARE v_stock INT;
    DECLARE v_id_venta INT;
    DECLARE v_id_detalle INT;
    DECLARE v_subtotal DECIMAL(10,2);

    SELECT precio_venta, stock_actual INTO v_precio, v_stock 
    FROM Productos WHERE id_producto = p_id_producto;

    IF v_stock >= p_cantidad THEN
        SET v_subtotal = v_precio * p_cantidad;

        START TRANSACTION;
            INSERT INTO Ventas (id_cliente, fecha_hora, tipo_venta, estado, subtotal, total)
            VALUES (p_id_cliente, NOW(), 'PRODUCTO', 'COMPLETADO', v_subtotal, v_subtotal);
            SET v_id_venta = LAST_INSERT_ID();

            INSERT INTO Detalle_Venta (id_venta, id_producto, cantidad, precio_unitario, subtotal)
            VALUES (v_id_venta, p_id_producto, p_cantidad, v_precio, v_subtotal);
            SET v_id_detalle = LAST_INSERT_ID();

            UPDATE Productos SET stock_actual = stock_actual - p_cantidad WHERE id_producto = p_id_producto;

            INSERT INTO Movimiento_Stock (id_producto, id_detalle_venta, tipo_movimiento, cantidad, fecha_hora, motivo, stock_anterior, stock_posterior)
            VALUES (p_id_producto, v_id_detalle, 'EGRESO', p_cantidad, NOW(), 'Venta Directa', v_stock, v_stock - p_cantidad);
        COMMIT;
    ELSE
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Stock insuficiente para efectuar la venta';
    END IF;
END //

DROP PROCEDURE IF EXISTS RegistrarPago //
CREATE PROCEDURE RegistrarPago (
    IN p_id_venta INT,
    IN p_monto DECIMAL(10,2),
    IN p_metodo_pago VARCHAR(50),
    IN p_referencia VARCHAR(100)
)
BEGIN
    INSERT INTO Pagos (id_venta, fecha_pago, monto, metodo_pago, estado, referencia)
    VALUES (p_id_venta, NOW(), p_monto, p_metodo_pago, 'APROBADO', p_referencia);
END //

DROP PROCEDURE IF EXISTS GenerarFactura //
CREATE PROCEDURE GenerarFactura (
    IN p_id_venta INT,
    IN p_numero_factura VARCHAR(10),
    IN p_tipo_comprobante VARCHAR(50)
)
BEGIN
    DECLARE v_total DECIMAL(10,2);
    DECLARE v_subtotal DECIMAL(10,2);
    DECLARE v_descuento DECIMAL(10,2);

    SELECT subtotal, descuento, total INTO v_subtotal, v_descuento, v_total
    FROM Ventas WHERE id_venta = p_id_venta;

    INSERT INTO Facturas (id_venta, numero_factura, fecha_emision, tipo_comprobante, estado, subtotal, descuento, total)
    VALUES (p_id_venta, p_numero_factura, NOW(), p_tipo_comprobante, 'EMITIDA', v_subtotal, v_descuento, v_total);
END //

DROP PROCEDURE IF EXISTS CrearMembresia //
CREATE PROCEDURE CrearMembresia (
    IN p_nombre_plan VARCHAR(100),
    IN p_descripcion TEXT,
    IN p_costo DECIMAL(10,2),
    IN p_duracion_dias INT,
    IN p_descuento DECIMAL(5,2)
)
BEGIN
    INSERT INTO Membresias (nombre_plan, descripcion, costo, duracion_dias, porcentaje_descuento, estado)
    VALUES (p_nombre_plan, p_descripcion, p_costo, p_duracion_dias, p_descuento, 'ACTIVO');
END //

DROP PROCEDURE IF EXISTS AsignarMembresiaCliente //
CREATE PROCEDURE AsignarMembresiaCliente (
    IN p_id_cliente INT,
    IN p_id_membresia INT,
    IN p_observacion VARCHAR(255)
)
BEGIN
    DECLARE v_duracion INT;
    
    SELECT duracion_dias INTO v_duracion FROM Membresias WHERE id_membresia = p_id_membresia;

    INSERT INTO Cliente_Membresia (id_cliente, id_membresia, fecha_inicio, fecha_fin, estado, observacion)
    VALUES (p_id_cliente, p_id_membresia, CURDATE(), DATE_ADD(CURDATE(), INTERVAL v_duracion DAY), 'ACTIVA', p_observacion);
END //

DELIMITER ;