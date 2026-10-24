-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versión del servidor:         8.4.3 - MySQL Community Server - GPL
-- SO del servidor:              Win64
-- HeidiSQL Versión:             12.12.0.7122
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Volcando estructura de base de datos para salonbellezabd
CREATE DATABASE IF NOT EXISTS `salonbellezabd` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `salonbellezabd`;

-- Volcando estructura para procedimiento salonbellezabd.ActualizarCategoriaProducto
DELIMITER //
CREATE PROCEDURE `ActualizarCategoriaProducto`(
    IN p_id_categoria INT, IN p_nombre VARCHAR(50), IN p_descripcion VARCHAR(255)
)
BEGIN
    UPDATE categorias_producto SET nombre = p_nombre, descripcion = p_descripcion WHERE id_categoria_producto = p_id_categoria;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ActualizarCategoriaServicio
DELIMITER //
CREATE PROCEDURE `ActualizarCategoriaServicio`(
    IN p_id_categoria INT, IN p_nombre VARCHAR(50), IN p_descripcion VARCHAR(255)
)
BEGIN
    UPDATE categorias_servicio SET nombre = p_nombre, descripcion = p_descripcion WHERE id_categoria_servicio = p_id_categoria;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ActualizarCliente
DELIMITER //
CREATE PROCEDURE `ActualizarCliente`(
    IN p_id_cliente INT,
    IN p_dni VARCHAR(10),
    IN p_nombre VARCHAR(50),
    IN p_apellido VARCHAR(50),
    IN p_telefono VARCHAR(20),
    IN p_email VARCHAR(100),
    IN p_direccion VARCHAR(100)
)
BEGIN

    UPDATE personas p
    INNER JOIN clientes c
        ON c.id_persona = p.id_persona
    SET
        p.dni = p_dni,
        p.nombre = p_nombre,
        p.apellido = p_apellido,
        p.telefono = p_telefono,
        p.email = p_email,
        p.direccion = p_direccion
    WHERE c.id_cliente = p_id_cliente;

END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ActualizarEmpleado
DELIMITER //
CREATE PROCEDURE `ActualizarEmpleado`(
    IN p_id_empleado INT,
    IN p_dni VARCHAR(10),
    IN p_nombre VARCHAR(50),
    IN p_apellido VARCHAR(50),
    IN p_telefono VARCHAR(20),
    IN p_email VARCHAR(100),
    IN p_direccion VARCHAR(100),
    IN p_tipo_empleado VARCHAR(50)
)
BEGIN
    UPDATE personas p
    INNER JOIN empleados e ON e.id_persona = p.id_persona
    SET
        p.dni = p_dni,
        p.nombre = p_nombre,
        p.apellido = p_apellido,
        p.telefono = p_telefono,
        p.email = p_email,
        p.direccion = p_direccion,
        e.tipo_empleado = p_tipo_empleado
    WHERE e.id_empleado = p_id_empleado;

    SELECT ROW_COUNT() AS filas_afectadas;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ActualizarProducto
DELIMITER //
CREATE PROCEDURE `ActualizarProducto`(
    IN p_id_producto INT, IN p_id_categoria INT, IN p_codigo VARCHAR(50), IN p_nombre VARCHAR(100),
    IN p_descripcion TEXT, IN p_precio_costo DECIMAL(10,2), IN p_precio_venta DECIMAL(10,2), IN p_stock_minimo INT
)
BEGIN
    UPDATE productos 
    SET id_categoria_producto = p_id_categoria, codigo = p_codigo, nombre = p_nombre, descripcion = p_descripcion,
        precio_costo = p_precio_costo, precio_venta = p_precio_venta, stock_minimo = p_stock_minimo
    WHERE id_producto = p_id_producto;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ActualizarProveedor
DELIMITER //
CREATE PROCEDURE `ActualizarProveedor`(
    IN p_id_proveedor INT, IN p_razon_social VARCHAR(100), IN p_cuit VARCHAR(20), 
    IN p_telefono VARCHAR(20), IN p_email VARCHAR(100), IN p_direccion VARCHAR(100)
)
BEGIN
    UPDATE proveedores 
    SET razon_social = p_razon_social, cuit = p_cuit, telefono = p_telefono, email = p_email, direccion = p_direccion
    WHERE id_proveedor = p_id_proveedor;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ActualizarSalon
DELIMITER //
CREATE PROCEDURE `ActualizarSalon`(
    IN p_id_salon INT, IN p_nombre VARCHAR(100), IN p_direccion VARCHAR(100), 
    IN p_telefono VARCHAR(30), IN p_email VARCHAR(100), IN p_logo VARCHAR(255)
)
BEGIN
    UPDATE salon 
    SET nombre = p_nombre, direccion = p_direccion, telefono = p_telefono, email = p_email, logo = p_logo
    WHERE id_salon = p_id_salon;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ActualizarServicio
DELIMITER //
CREATE PROCEDURE `ActualizarServicio`(
    IN p_id_servicio INT, IN p_id_categoria INT, IN p_nombre VARCHAR(100), 
    IN p_descripcion TEXT, IN p_precio DECIMAL(10,2), IN p_duracion INT
)
BEGIN
    UPDATE servicios 
    SET id_categoria_servicio = p_id_categoria, nombre = p_nombre, descripcion = p_descripcion, 
        precio = p_precio, duracion_minuto = p_duracion
    WHERE id_servicio = p_id_servicio;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ActualizarStockProducto
DELIMITER //
CREATE PROCEDURE `ActualizarStockProducto`(
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
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.AgregarServicioATurno
DELIMITER //
CREATE PROCEDURE `AgregarServicioATurno`(
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
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.AsignarMembresiaCliente
DELIMITER //
CREATE PROCEDURE `AsignarMembresiaCliente`(
    IN p_id_cliente INT,
    IN p_id_membresia INT,
    IN p_observacion VARCHAR(255)
)
BEGIN
    DECLARE v_duracion INT;
    
    SELECT duracion_dias INTO v_duracion FROM Membresias WHERE id_membresia = p_id_membresia;

    INSERT INTO Cliente_Membresia (id_cliente, id_membresia, fecha_inicio, fecha_fin, estado, observacion)
    VALUES (p_id_cliente, p_id_membresia, CURDATE(), DATE_ADD(CURDATE(), INTERVAL v_duracion DAY), 'ACTIVA', p_observacion);
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.BuscarClientes
DELIMITER //
CREATE PROCEDURE `BuscarClientes`(IN p_termino VARCHAR(100))
BEGIN

    SELECT
        c.id_cliente,
        p.id_persona,
        p.dni,
        p.nombre,
        p.apellido,
        p.telefono,
        p.email,
        p.direccion,
        c.categoria_cliente,
        c.fecha_registro,
        c.estado
    FROM clientes c
    INNER JOIN personas p
        ON c.id_persona = p.id_persona
    WHERE
        p.nombre LIKE CONCAT('%', p_termino, '%')
        OR p.apellido LIKE CONCAT('%', p_termino, '%')
        OR p.dni LIKE CONCAT('%', p_termino, '%')
        OR p.telefono LIKE CONCAT('%', p_termino, '%')
        OR p.email LIKE CONCAT('%', p_termino, '%')
    ORDER BY p.apellido, p.nombre;

END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.BuscarEmpleados
DELIMITER //
CREATE PROCEDURE `BuscarEmpleados`(
    IN p_termino VARCHAR(100)
)
BEGIN
    SELECT
        e.id_empleado,
        p.id_persona,
        p.dni,
        p.nombre,
        p.apellido,
        p.telefono,
        p.email,
        p.direccion,
        e.tipo_empleado,
        e.fecha_ingreso,
        e.estado
    FROM empleados e
    INNER JOIN personas p ON e.id_persona = p.id_persona
    WHERE p.nombre LIKE CONCAT('%', p_termino, '%')
       OR p.apellido LIKE CONCAT('%', p_termino, '%')
       OR p.dni LIKE CONCAT('%', p_termino, '%')
       OR p.telefono LIKE CONCAT('%', p_termino, '%')
       OR p.email LIKE CONCAT('%', p_termino, '%')
       OR e.tipo_empleado LIKE CONCAT('%', p_termino, '%')
    ORDER BY p.apellido, p.nombre;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.CambiarEstadoTurno
DELIMITER //
CREATE PROCEDURE `CambiarEstadoTurno`(IN p_id_turno INT, IN p_estado VARCHAR(50))
BEGIN
    UPDATE turnos SET estado = p_estado WHERE id_turno = p_id_turno;
END//
DELIMITER ;

-- Volcando estructura para tabla salonbellezabd.categorias_producto
CREATE TABLE IF NOT EXISTS `categorias_producto` (
  `id_categoria_producto` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `descripcion` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `activo` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id_categoria_producto`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla salonbellezabd.categorias_producto: ~0 rows (aproximadamente)
REPLACE INTO `categorias_producto` (`id_categoria_producto`, `nombre`, `descripcion`, `activo`) VALUES
	(1, 'Productos Capilares', 'Shampoo, acondicionadores y tratamientos', 1);

-- Volcando estructura para tabla salonbellezabd.categorias_servicio
CREATE TABLE IF NOT EXISTS `categorias_servicio` (
  `id_categoria_servicio` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `descripcion` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `activo` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id_categoria_servicio`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla salonbellezabd.categorias_servicio: ~0 rows (aproximadamente)
REPLACE INTO `categorias_servicio` (`id_categoria_servicio`, `nombre`, `descripcion`, `activo`) VALUES
	(1, 'Peluquería', 'Servicios de corte y peinado', 1);

-- Volcando estructura para tabla salonbellezabd.clientes
CREATE TABLE IF NOT EXISTS `clientes` (
  `id_cliente` int NOT NULL AUTO_INCREMENT,
  `id_persona` int NOT NULL,
  `categoria_cliente` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `fecha_registro` date DEFAULT NULL,
  `estado` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`id_cliente`),
  UNIQUE KEY `uq_clientes_persona` (`id_persona`),
  CONSTRAINT `fk_clientes_persona` FOREIGN KEY (`id_persona`) REFERENCES `personas` (`id_persona`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla salonbellezabd.clientes: ~4 rows (aproximadamente)
REPLACE INTO `clientes` (`id_cliente`, `id_persona`, `categoria_cliente`, `fecha_registro`, `estado`) VALUES
	(1, 1, 'CLIENTE', '2026-10-07', 'INACTIVO'),
	(2, 3, 'CLIENTE', '2026-10-08', 'INACTIVO'),
	(3, 4, 'CLIENTE', '2026-10-08', 'ACTIVO'),
	(4, 5, 'CLIENTE', '2026-10-09', 'ACTIVO');

-- Volcando estructura para tabla salonbellezabd.cliente_membresia
CREATE TABLE IF NOT EXISTS `cliente_membresia` (
  `id_cliente_membresia` int NOT NULL AUTO_INCREMENT,
  `id_cliente` int NOT NULL,
  `id_membresia` int NOT NULL,
  `fecha_inicio` date DEFAULT NULL,
  `fecha_fin` date DEFAULT NULL,
  `estado` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `observacion` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`id_cliente_membresia`),
  KEY `fk_cliente_membresia_cliente` (`id_cliente`),
  KEY `fk_cliente_membresia_membresia` (`id_membresia`),
  CONSTRAINT `fk_cliente_membresia_cliente` FOREIGN KEY (`id_cliente`) REFERENCES `clientes` (`id_cliente`),
  CONSTRAINT `fk_cliente_membresia_membresia` FOREIGN KEY (`id_membresia`) REFERENCES `membresias` (`id_membresia`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla salonbellezabd.cliente_membresia: ~0 rows (aproximadamente)
REPLACE INTO `cliente_membresia` (`id_cliente_membresia`, `id_cliente`, `id_membresia`, `fecha_inicio`, `fecha_fin`, `estado`, `observacion`) VALUES
	(1, 1, 1, '2026-10-08', '2026-11-07', 'ACTIVA', 'Membresía Premium - primer mes');

-- Volcando estructura para tabla salonbellezabd.compras
CREATE TABLE IF NOT EXISTS `compras` (
  `id_compra` int NOT NULL AUTO_INCREMENT,
  `id_proveedor` int NOT NULL,
  `fecha_hora` datetime DEFAULT NULL,
  `estado` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `subtotal` decimal(10,2) DEFAULT NULL,
  `total` decimal(10,2) DEFAULT NULL,
  `observacion` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`id_compra`),
  KEY `fk_compras_proveedor` (`id_proveedor`),
  CONSTRAINT `fk_compras_proveedor` FOREIGN KEY (`id_proveedor`) REFERENCES `proveedores` (`id_proveedor`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla salonbellezabd.compras: ~0 rows (aproximadamente)
REPLACE INTO `compras` (`id_compra`, `id_proveedor`, `fecha_hora`, `estado`, `subtotal`, `total`, `observacion`) VALUES
	(1, 1, '2026-10-08 00:47:22', 'COMPLETADO', 50000.00, 50000.00, 'Compra inicial de productos');

-- Volcando estructura para procedimiento salonbellezabd.CrearMembresia
DELIMITER //
CREATE PROCEDURE `CrearMembresia`(
    IN p_nombre_plan VARCHAR(100),
    IN p_descripcion TEXT,
    IN p_costo DECIMAL(10,2),
    IN p_duracion_dias INT,
    IN p_descuento DECIMAL(5,2)
)
BEGIN
    INSERT INTO Membresias (nombre_plan, descripcion, costo, duracion_dias, porcentaje_descuento, estado)
    VALUES (p_nombre_plan, p_descripcion, p_costo, p_duracion_dias, p_descuento, 'ACTIVO');
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.CrearTurno
DELIMITER //
CREATE PROCEDURE `CrearTurno`(
    IN p_id_cliente INT,
    IN p_id_empleado INT,
    IN p_fecha DATE,
    IN p_hora TIME,
    IN p_observacion TEXT
)
BEGIN
    INSERT INTO Turnos (id_cliente, id_empleado, fecha, hora, estado, observacion, fecha_creacion)
    VALUES (p_id_cliente, p_id_empleado, p_fecha, p_hora, 'PENDIENTE', p_observacion, NOW());
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.CrearVentaDesdeTurno
DELIMITER //
CREATE PROCEDURE `CrearVentaDesdeTurno`(
    IN p_id_turno INT,
    IN p_descuento DECIMAL(10,2),
    IN p_observacion VARCHAR(255)
)
BEGIN
    DECLARE v_id_cliente INT;
    DECLARE v_subtotal DECIMAL(10,2);
    DECLARE v_descuento DECIMAL(10,2);

    SELECT id_cliente
    INTO v_id_cliente
    FROM turnos
    WHERE id_turno = p_id_turno;

    SELECT COALESCE(SUM(subtotal), 0)
    INTO v_subtotal
    FROM detalle_turno
    WHERE id_turno = p_id_turno;

    SET v_descuento = IFNULL(p_descuento, 0);

    INSERT INTO ventas
    (
        id_cliente,
        id_turno,
        fecha_hora,
        tipo_venta,
        estado,
        subtotal,
        descuento,
        total,
        observacion
    )
    VALUES
    (
        v_id_cliente,
        p_id_turno,
        NOW(),
        'SERVICIO',
        'PENDIENTE',
        v_subtotal,
        v_descuento,
        v_subtotal - v_descuento,
        p_observacion
    );
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.DesactivarCategoriaProducto
DELIMITER //
CREATE PROCEDURE `DesactivarCategoriaProducto`(IN p_id_categoria INT)
BEGIN
    UPDATE categorias_producto SET activo = 0 WHERE id_categoria_producto = p_id_categoria;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.DesactivarCategoriaServicio
DELIMITER //
CREATE PROCEDURE `DesactivarCategoriaServicio`(IN p_id_categoria INT)
BEGIN
    UPDATE categorias_servicio SET activo = 0 WHERE id_categoria_servicio = p_id_categoria;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.DesactivarCliente
DELIMITER //
CREATE PROCEDURE `DesactivarCliente`(IN p_id_cliente INT)
BEGIN

    UPDATE clientes
    SET estado = 'INACTIVO'
    WHERE id_cliente = p_id_cliente;

END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.DesactivarEmpleado
DELIMITER //
CREATE PROCEDURE `DesactivarEmpleado`(
    IN p_id_empleado INT
)
BEGIN
    UPDATE empleados
    SET estado = 'INACTIVO'
    WHERE id_empleado = p_id_empleado;

    SELECT ROW_COUNT() AS filas_afectadas;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.DesactivarProducto
DELIMITER //
CREATE PROCEDURE `DesactivarProducto`(IN p_id_producto INT)
BEGIN
    UPDATE productos SET estado = 'INACTIVO' WHERE id_producto = p_id_producto;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.DesactivarProveedor
DELIMITER //
CREATE PROCEDURE `DesactivarProveedor`(IN p_id_proveedor INT)
BEGIN
    UPDATE proveedores SET activo = 'INACTIVO' WHERE id_proveedor = p_id_proveedor;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.DesactivarServicio
DELIMITER //
CREATE PROCEDURE `DesactivarServicio`(IN p_id_servicio INT)
BEGIN
    UPDATE servicios SET estado = 'INACTIVO' WHERE id_servicio = p_id_servicio;
END//
DELIMITER ;

-- Volcando estructura para tabla salonbellezabd.detalle_compra
CREATE TABLE IF NOT EXISTS `detalle_compra` (
  `id_detalle_compra` int NOT NULL AUTO_INCREMENT,
  `id_compra` int NOT NULL,
  `id_producto` int NOT NULL,
  `cantidad` int NOT NULL,
  `precio_costo` decimal(10,2) NOT NULL,
  PRIMARY KEY (`id_detalle_compra`),
  KEY `fk_detalle_compra_compra` (`id_compra`),
  KEY `fk_detalle_compra_producto` (`id_producto`),
  CONSTRAINT `fk_detalle_compra_compra` FOREIGN KEY (`id_compra`) REFERENCES `compras` (`id_compra`),
  CONSTRAINT `fk_detalle_compra_producto` FOREIGN KEY (`id_producto`) REFERENCES `productos` (`id_producto`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla salonbellezabd.detalle_compra: ~0 rows (aproximadamente)
REPLACE INTO `detalle_compra` (`id_detalle_compra`, `id_compra`, `id_producto`, `cantidad`, `precio_costo`) VALUES
	(1, 1, 1, 10, 5000.00);

-- Volcando estructura para tabla salonbellezabd.detalle_turno
CREATE TABLE IF NOT EXISTS `detalle_turno` (
  `id_detalle_turno` int NOT NULL AUTO_INCREMENT,
  `id_turno` int NOT NULL,
  `id_servicio` int NOT NULL,
  `precio_unitario` decimal(10,2) DEFAULT NULL,
  `descuento` decimal(10,2) DEFAULT NULL,
  `subtotal` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`id_detalle_turno`),
  KEY `fk_detalle_turno_turno` (`id_turno`),
  KEY `fk_detalle_turno_servicio` (`id_servicio`),
  CONSTRAINT `fk_detalle_turno_servicio` FOREIGN KEY (`id_servicio`) REFERENCES `servicios` (`id_servicio`),
  CONSTRAINT `fk_detalle_turno_turno` FOREIGN KEY (`id_turno`) REFERENCES `turnos` (`id_turno`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla salonbellezabd.detalle_turno: ~0 rows (aproximadamente)
REPLACE INTO `detalle_turno` (`id_detalle_turno`, `id_turno`, `id_servicio`, `precio_unitario`, `descuento`, `subtotal`) VALUES
	(1, 1, 1, 10000.00, 0.00, 10000.00);

-- Volcando estructura para tabla salonbellezabd.detalle_venta
CREATE TABLE IF NOT EXISTS `detalle_venta` (
  `id_detalle_venta` int NOT NULL AUTO_INCREMENT,
  `id_venta` int NOT NULL,
  `id_producto` int DEFAULT NULL,
  `id_servicio` int DEFAULT NULL,
  `cantidad` int NOT NULL DEFAULT '1',
  `precio_unitario` decimal(10,2) DEFAULT NULL,
  `descuento` decimal(10,2) DEFAULT NULL,
  `subtotal` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`id_detalle_venta`),
  KEY `fk_detalle_venta_venta` (`id_venta`),
  KEY `fk_detalle_venta_producto` (`id_producto`),
  KEY `fk_detalle_venta_servicio` (`id_servicio`),
  CONSTRAINT `fk_detalle_venta_producto` FOREIGN KEY (`id_producto`) REFERENCES `productos` (`id_producto`),
  CONSTRAINT `fk_detalle_venta_servicio` FOREIGN KEY (`id_servicio`) REFERENCES `servicios` (`id_servicio`),
  CONSTRAINT `fk_detalle_venta_venta` FOREIGN KEY (`id_venta`) REFERENCES `ventas` (`id_venta`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla salonbellezabd.detalle_venta: ~0 rows (aproximadamente)

-- Volcando estructura para tabla salonbellezabd.empleados
CREATE TABLE IF NOT EXISTS `empleados` (
  `id_empleado` int NOT NULL AUTO_INCREMENT,
  `id_persona` int NOT NULL,
  `tipo_empleado` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `fecha_ingreso` date DEFAULT NULL,
  `estado` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`id_empleado`),
  UNIQUE KEY `uq_empleados_persona` (`id_persona`),
  CONSTRAINT `fk_empleados_persona` FOREIGN KEY (`id_persona`) REFERENCES `personas` (`id_persona`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla salonbellezabd.empleados: ~0 rows (aproximadamente)
REPLACE INTO `empleados` (`id_empleado`, `id_persona`, `tipo_empleado`, `fecha_ingreso`, `estado`) VALUES
	(1, 2, 'Peluquero', '2026-10-07', 'ACTIVO');

-- Volcando estructura para tabla salonbellezabd.facturas
CREATE TABLE IF NOT EXISTS `facturas` (
  `id_factura` int NOT NULL AUTO_INCREMENT,
  `id_venta` int NOT NULL,
  `numero_factura` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `fecha_emision` datetime DEFAULT NULL,
  `tipo_comprobante` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `estado` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `subtotal` decimal(10,2) DEFAULT NULL,
  `descuento` decimal(10,2) DEFAULT NULL,
  `total` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`id_factura`),
  KEY `fk_facturas_venta` (`id_venta`),
  CONSTRAINT `fk_facturas_venta` FOREIGN KEY (`id_venta`) REFERENCES `ventas` (`id_venta`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla salonbellezabd.facturas: ~0 rows (aproximadamente)
REPLACE INTO `facturas` (`id_factura`, `id_venta`, `numero_factura`, `fecha_emision`, `tipo_comprobante`, `estado`, `subtotal`, `descuento`, `total`) VALUES
	(1, 1, '0001-00000001', '2026-10-08 00:40:47', 'FACTURA C', 'EMITIDA', 10000.00, 0.00, 10000.00);

-- Volcando estructura para procedimiento salonbellezabd.GenerarFactura
DELIMITER //
CREATE PROCEDURE `GenerarFactura`(
	IN `p_id_venta` INT,
	IN `p_numero_factura` VARCHAR(20),
	IN `p_tipo_comprobante` VARCHAR(50)
)
BEGIN
    DECLARE v_total DECIMAL(10,2);
    DECLARE v_subtotal DECIMAL(10,2);
    DECLARE v_descuento DECIMAL(10,2);

    SELECT subtotal, descuento, total INTO v_subtotal, v_descuento, v_total
    FROM Ventas WHERE id_venta = p_id_venta;

    INSERT INTO Facturas (id_venta, numero_factura, fecha_emision, tipo_comprobante, estado, subtotal, descuento, total)
    VALUES (p_id_venta, p_numero_factura, NOW(), p_tipo_comprobante, 'EMITIDA', v_subtotal, v_descuento, v_total);
END//
DELIMITER ;

-- Volcando estructura para tabla salonbellezabd.membresias
CREATE TABLE IF NOT EXISTS `membresias` (
  `id_membresia` int NOT NULL AUTO_INCREMENT,
  `nombre_plan` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `descripcion` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `costo` decimal(10,2) DEFAULT NULL,
  `duracion_dias` int DEFAULT NULL,
  `porcentaje_descuento` decimal(5,2) DEFAULT NULL,
  `estado` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`id_membresia`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla salonbellezabd.membresias: ~0 rows (aproximadamente)
REPLACE INTO `membresias` (`id_membresia`, `nombre_plan`, `descripcion`, `costo`, `duracion_dias`, `porcentaje_descuento`, `estado`) VALUES
	(1, 'Membresía Premium', 'Beneficios exclusivos del salón', 25000.00, 30, 10.00, 'ACTIVO');

-- Volcando estructura para tabla salonbellezabd.movimiento_stock
CREATE TABLE IF NOT EXISTS `movimiento_stock` (
  `id_movimiento` int NOT NULL AUTO_INCREMENT,
  `id_producto` int NOT NULL,
  `id_detalle_venta` int DEFAULT NULL,
  `tipo_movimiento` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `cantidad` int DEFAULT NULL,
  `fecha_hora` datetime DEFAULT NULL,
  `motivo` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `stock_anterior` int DEFAULT NULL,
  `stock_posterior` int DEFAULT NULL,
  PRIMARY KEY (`id_movimiento`),
  KEY `fk_movimiento_producto` (`id_producto`),
  KEY `fk_movimiento_detalle_venta` (`id_detalle_venta`),
  CONSTRAINT `fk_movimiento_detalle_venta` FOREIGN KEY (`id_detalle_venta`) REFERENCES `detalle_venta` (`id_detalle_venta`),
  CONSTRAINT `fk_movimiento_producto` FOREIGN KEY (`id_producto`) REFERENCES `productos` (`id_producto`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla salonbellezabd.movimiento_stock: ~2 rows (aproximadamente)
REPLACE INTO `movimiento_stock` (`id_movimiento`, `id_producto`, `id_detalle_venta`, `tipo_movimiento`, `cantidad`, `fecha_hora`, `motivo`, `stock_anterior`, `stock_posterior`) VALUES
	(1, 1, NULL, 'INGRESO', 10, '2026-10-08 00:45:18', 'Carga inicial de stock', 20, 30),
	(2, 1, NULL, 'INGRESO', 10, '2026-10-08 00:47:22', 'Compra a Proveedor', 30, 40);

-- Volcando estructura para procedimiento salonbellezabd.ObtenerCategoriasProducto
DELIMITER //
CREATE PROCEDURE `ObtenerCategoriasProducto`()
BEGIN
    SELECT id_categoria_producto, nombre, descripcion, activo FROM categorias_producto ORDER BY nombre;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ObtenerCategoriasServicio
DELIMITER //
CREATE PROCEDURE `ObtenerCategoriasServicio`()
BEGIN
    SELECT id_categoria_servicio, nombre, descripcion, activo FROM categorias_servicio ORDER BY nombre;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ObtenerClienteMembresias
DELIMITER //
CREATE PROCEDURE `ObtenerClienteMembresias`(IN p_id_cliente INT)
BEGIN
    SELECT cm.id_cliente_membresia, m.nombre_plan, cm.fecha_inicio, cm.fecha_fin, cm.estado, cm.observacion
    FROM cliente_membresia cm
    INNER JOIN membresias m ON cm.id_membresia = m.id_membresia
    WHERE cm.id_cliente = p_id_cliente;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ObtenerClientePorId
DELIMITER //
CREATE PROCEDURE `ObtenerClientePorId`(IN p_id_cliente INT)
BEGIN

    SELECT
        c.id_cliente,
        p.id_persona,
        p.dni,
        p.nombre,
        p.apellido,
        p.telefono,
        p.email,
        p.direccion,
        c.categoria_cliente,
        c.fecha_registro,
        c.estado
    FROM clientes c
    INNER JOIN personas p
        ON c.id_persona = p.id_persona
    WHERE c.id_cliente = p_id_cliente;

END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ObtenerClientes
DELIMITER //
CREATE PROCEDURE `ObtenerClientes`()
BEGIN

    SELECT
        c.id_cliente,
        p.id_persona,
        p.dni,
        p.nombre,
        p.apellido,
        p.telefono,
        p.email,
        p.direccion,
        c.categoria_cliente,
        c.fecha_registro,
        c.estado
    FROM clientes c
    INNER JOIN personas p
        ON c.id_persona = p.id_persona
    ORDER BY p.apellido, p.nombre;

END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ObtenerCompraPorId
DELIMITER //
CREATE PROCEDURE `ObtenerCompraPorId`(IN p_id_compra INT)
BEGIN
    SELECT c.id_compra, c.id_proveedor, pr.razon_social AS proveedor, 
           c.fecha_hora, c.estado, c.subtotal, c.total, c.observacion
    FROM compras c
    INNER JOIN proveedores pr ON c.id_proveedor = pr.id_proveedor
    WHERE c.id_compra = p_id_compra;

    SELECT dc.id_detalle_compra, dc.id_producto, p.nombre AS producto, 
           dc.cantidad, dc.precio_costo
    FROM detalle_compra dc
    INNER JOIN productos p ON dc.id_producto = p.id_producto
    WHERE dc.id_compra = p_id_compra;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ObtenerCompras
DELIMITER //
CREATE PROCEDURE `ObtenerCompras`()
BEGIN
    SELECT c.id_compra, c.id_proveedor, pr.razon_social AS proveedor, 
           c.fecha_hora, c.estado, c.subtotal, c.total, c.observacion
    FROM compras c
    INNER JOIN proveedores pr ON c.id_proveedor = pr.id_proveedor
    ORDER BY c.fecha_hora DESC;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ObtenerEmpleadoPorId
DELIMITER //
CREATE PROCEDURE `ObtenerEmpleadoPorId`(
    IN p_id_empleado INT
)
BEGIN
    SELECT
        e.id_empleado,
        p.id_persona,
        p.dni,
        p.nombre,
        p.apellido,
        p.telefono,
        p.email,
        p.direccion,
        e.tipo_empleado,
        e.fecha_ingreso,
        e.estado
    FROM empleados e
    INNER JOIN personas p ON e.id_persona = p.id_persona
    WHERE e.id_empleado = p_id_empleado;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ObtenerEmpleados
DELIMITER //
CREATE PROCEDURE `ObtenerEmpleados`()
BEGIN
    SELECT 
        e.id_empleado, p.id_persona, p.dni, p.nombre, p.apellido, 
        p.telefono, p.email, p.direccion, e.tipo_empleado, 
        e.fecha_ingreso, e.estado
    FROM empleados e
    INNER JOIN personas p ON e.id_persona = p.id_persona
    ORDER BY p.apellido, p.nombre;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ObtenerFacturaPorId
DELIMITER //
CREATE PROCEDURE `ObtenerFacturaPorId`(IN p_id_factura INT)
BEGIN
    SELECT f.id_factura, f.id_venta, f.numero_factura, f.fecha_emision, 
           f.tipo_comprobante, f.estado, f.subtotal, f.descuento, f.total,
           CONCAT(p.nombre, ' ', p.apellido) AS cliente
    FROM facturas f
    INNER JOIN ventas v ON f.id_venta = v.id_venta
    INNER JOIN clientes c ON v.id_cliente = c.id_cliente
    INNER JOIN personas p ON c.id_persona = p.id_persona
    WHERE f.id_factura = p_id_factura;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ObtenerFacturas
DELIMITER //
CREATE PROCEDURE `ObtenerFacturas`()
BEGIN
    SELECT f.id_factura, f.id_venta, f.numero_factura, f.fecha_emision, 
           f.tipo_comprobante, f.estado, f.subtotal, f.descuento, f.total,
           CONCAT(p.nombre, ' ', p.apellido) AS cliente
    FROM facturas f
    INNER JOIN ventas v ON f.id_venta = v.id_venta
    INNER JOIN clientes c ON v.id_cliente = c.id_cliente
    INNER JOIN personas p ON c.id_persona = p.id_persona
    ORDER BY f.fecha_emision DESC;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ObtenerMembresias
DELIMITER //
CREATE PROCEDURE `ObtenerMembresias`()
BEGIN
    SELECT id_membresia, nombre_plan, descripcion, costo, duracion_dias, porcentaje_descuento, estado FROM membresias ORDER BY nombre_plan;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ObtenerPagosPorVenta
DELIMITER //
CREATE PROCEDURE `ObtenerPagosPorVenta`(IN p_id_venta INT)
BEGIN
    SELECT id_pago, id_venta, fecha_pago, monto, metodo_pago, estado, referencia
    FROM pagos
    WHERE id_venta = p_id_venta
    ORDER BY fecha_pago DESC;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ObtenerProductoPorId
DELIMITER //
CREATE PROCEDURE `ObtenerProductoPorId`(IN p_id_producto INT)
BEGIN
    SELECT p.id_producto, p.id_categoria_producto, c.nombre AS nombre_categoria, p.codigo, p.nombre, p.descripcion, p.precio_costo, p.precio_venta, p.stock_actual, p.stock_minimo, p.estado
    FROM productos p
    INNER JOIN categorias_producto c ON p.id_categoria_producto = c.id_categoria_producto
    WHERE p.id_producto = p_id_producto;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ObtenerProductos
DELIMITER //
CREATE PROCEDURE `ObtenerProductos`()
BEGIN
    SELECT p.id_producto, p.id_categoria_producto, c.nombre AS nombre_categoria, p.codigo, p.nombre, p.descripcion, p.precio_costo, p.precio_venta, p.stock_actual, p.stock_minimo, p.estado
    FROM productos p
    INNER JOIN categorias_producto c ON p.id_categoria_producto = c.id_categoria_producto
    ORDER BY p.nombre;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ObtenerProveedores
DELIMITER //
CREATE PROCEDURE `ObtenerProveedores`()
BEGIN
    SELECT id_proveedor, razon_social, cuit, telefono, email, direccion, activo FROM proveedores ORDER BY razon_social;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ObtenerProveedorPorId
DELIMITER //
CREATE PROCEDURE `ObtenerProveedorPorId`(IN p_id_proveedor INT)
BEGIN
    SELECT id_proveedor, razon_social, cuit, telefono, email, direccion, activo FROM proveedores WHERE id_proveedor = p_id_proveedor;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ObtenerSalon
DELIMITER //
CREATE PROCEDURE `ObtenerSalon`()
BEGIN
    SELECT id_salon, nombre, direccion, telefono, email, logo, activo, fecha_alta FROM salon LIMIT 1;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ObtenerServicioPorId
DELIMITER //
CREATE PROCEDURE `ObtenerServicioPorId`(IN p_id_servicio INT)
BEGIN
    SELECT s.id_servicio, s.id_categoria_servicio, c.nombre AS nombre_categoria, s.nombre, s.descripcion, s.precio, s.duracion_minuto, s.estado
    FROM servicios s
    INNER JOIN categorias_servicio c ON s.id_categoria_servicio = c.id_categoria_servicio
    WHERE s.id_servicio = p_id_servicio;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ObtenerServicios
DELIMITER //
CREATE PROCEDURE `ObtenerServicios`()
BEGIN
    SELECT s.id_servicio, s.id_categoria_servicio, c.nombre AS nombre_categoria, s.nombre, s.descripcion, s.precio, s.duracion_minuto, s.estado
    FROM servicios s
    INNER JOIN categorias_servicio c ON s.id_categoria_servicio = c.id_categoria_servicio
    ORDER BY s.nombre;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ObtenerTurnos
DELIMITER //
CREATE PROCEDURE `ObtenerTurnos`()
BEGIN
    SELECT t.id_turno, t.id_cliente, CONCAT(pc.nombre, ' ', pc.apellido) AS nombre_cliente, 
           t.id_empleado, CONCAT(pe.nombre, ' ', pe.apellido) AS nombre_empleado,
           t.fecha, t.hora, t.estado, t.observacion
    FROM turnos t
    INNER JOIN clientes c ON t.id_cliente = c.id_cliente
    INNER JOIN personas pc ON c.id_persona = pc.id_persona
    INNER JOIN empleados e ON t.id_empleado = e.id_empleado
    INNER JOIN personas pe ON e.id_persona = pe.id_persona
    ORDER BY t.fecha DESC, t.hora DESC;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ObtenerVentaPorId
DELIMITER //
CREATE PROCEDURE `ObtenerVentaPorId`(IN p_id_venta INT)
BEGIN
    -- Devuelve la cabecera
    SELECT v.id_venta, v.id_cliente, v.fecha_hora, v.tipo_venta, v.estado, v.subtotal, v.descuento, v.total, v.observacion
    FROM ventas v WHERE v.id_venta = p_id_venta;
    
    -- Devuelve el detalle
    SELECT dv.id_detalle_venta, dv.id_producto, dv.id_servicio, dv.cantidad, dv.precio_unitario, dv.descuento, dv.subtotal
    FROM detalle_venta dv WHERE dv.id_venta = p_id_venta;
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.ObtenerVentas
DELIMITER //
CREATE PROCEDURE `ObtenerVentas`()
BEGIN
    SELECT v.id_venta, v.id_cliente, CONCAT(p.nombre, ' ', p.apellido) AS nombre_cliente,
           v.fecha_hora, v.tipo_venta, v.estado, v.subtotal, v.descuento, v.total
    FROM ventas v
    INNER JOIN clientes c ON v.id_cliente = c.id_cliente
    INNER JOIN personas p ON c.id_persona = p.id_persona
    ORDER BY v.fecha_hora DESC;
END//
DELIMITER ;

-- Volcando estructura para tabla salonbellezabd.pagos
CREATE TABLE IF NOT EXISTS `pagos` (
  `id_pago` int NOT NULL AUTO_INCREMENT,
  `id_venta` int NOT NULL,
  `fecha_pago` datetime DEFAULT NULL,
  `monto` decimal(10,2) DEFAULT NULL,
  `metodo_pago` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `estado` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `referencia` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`id_pago`),
  KEY `fk_pagos_venta` (`id_venta`),
  CONSTRAINT `fk_pagos_venta` FOREIGN KEY (`id_venta`) REFERENCES `ventas` (`id_venta`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla salonbellezabd.pagos: ~0 rows (aproximadamente)
REPLACE INTO `pagos` (`id_pago`, `id_venta`, `fecha_pago`, `monto`, `metodo_pago`, `estado`, `referencia`) VALUES
	(1, 1, '2026-10-08 00:32:28', 10000.00, 'EFECTIVO', 'APROBADO', 'Pago del turno 1');

-- Volcando estructura para tabla salonbellezabd.pago_membresia
CREATE TABLE IF NOT EXISTS `pago_membresia` (
  `id_pago_membresia` int NOT NULL AUTO_INCREMENT,
  `id_cliente_membresia` int NOT NULL,
  `fecha_pago` datetime DEFAULT NULL,
  `monto_pago` decimal(10,2) DEFAULT NULL,
  `metodo_pago` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `estado` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `referencia` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`id_pago_membresia`),
  KEY `fk_pago_membresia_cliente_membresia` (`id_cliente_membresia`),
  CONSTRAINT `fk_pago_membresia_cliente_membresia` FOREIGN KEY (`id_cliente_membresia`) REFERENCES `cliente_membresia` (`id_cliente_membresia`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla salonbellezabd.pago_membresia: ~0 rows (aproximadamente)

-- Volcando estructura para tabla salonbellezabd.personas
CREATE TABLE IF NOT EXISTS `personas` (
  `id_persona` int NOT NULL AUTO_INCREMENT,
  `dni` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `nombre` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `apellido` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `telefono` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `direccion` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `activo` tinyint(1) DEFAULT '1',
  `fecha_alta` date DEFAULT NULL,
  PRIMARY KEY (`id_persona`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla salonbellezabd.personas: ~5 rows (aproximadamente)
REPLACE INTO `personas` (`id_persona`, `dni`, `nombre`, `apellido`, `telefono`, `email`, `direccion`, `activo`, `fecha_alta`) VALUES
	(1, '30123456', 'Juan', 'Perez', '6666666666', 'juan_nuevo@gmail.com', 'Santa María', 1, '2026-10-07'),
	(2, '30111222', 'Juan', 'Perez', '3838123456', 'juan.perez@example.com', 'Centro', 1, '2026-10-07'),
	(3, '999999999', 'Saul', 'Isasmendi', '38385554444', 'saulisasmendi@hotmail.com', 'Santa María', 1, '2026-10-08'),
	(4, '5555999', 'alejandro', 'fuenzaida', '38385554444', 'Alejandroqq@hotmailcom', 'Santa María', 1, '2026-10-08'),
	(5, '45456874', 'CANO', 'MOYA', '38385554444', 's55555i@hotmail.com', 'Santa María', 1, '2026-10-09');

-- Volcando estructura para tabla salonbellezabd.productos
CREATE TABLE IF NOT EXISTS `productos` (
  `id_producto` int NOT NULL AUTO_INCREMENT,
  `id_categoria_producto` int NOT NULL,
  `codigo` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `nombre` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `descripcion` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `precio_costo` decimal(10,2) DEFAULT NULL,
  `precio_venta` decimal(10,2) NOT NULL,
  `stock_actual` int DEFAULT '0',
  `stock_minimo` int DEFAULT '0',
  `estado` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `fecha_alta` date DEFAULT NULL,
  PRIMARY KEY (`id_producto`),
  KEY `fk_productos_categoria` (`id_categoria_producto`),
  CONSTRAINT `fk_productos_categoria` FOREIGN KEY (`id_categoria_producto`) REFERENCES `categorias_producto` (`id_categoria_producto`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla salonbellezabd.productos: ~0 rows (aproximadamente)
REPLACE INTO `productos` (`id_producto`, `id_categoria_producto`, `codigo`, `nombre`, `descripcion`, `precio_costo`, `precio_venta`, `stock_actual`, `stock_minimo`, `estado`, `fecha_alta`) VALUES
	(1, 1, 'SH001', 'Shampoo Profesional', 'Shampoo para cabello', 5000.00, 8000.00, 40, 5, 'ACTIVO', '2026-10-08');

-- Volcando estructura para tabla salonbellezabd.proveedores
CREATE TABLE IF NOT EXISTS `proveedores` (
  `id_proveedor` int NOT NULL AUTO_INCREMENT,
  `razon_social` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `cuit` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `telefono` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `direccion` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `activo` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`id_proveedor`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla salonbellezabd.proveedores: ~0 rows (aproximadamente)
REPLACE INTO `proveedores` (`id_proveedor`, `razon_social`, `cuit`, `telefono`, `email`, `direccion`, `activo`) VALUES
	(1, 'Distribuidora Belleza SRL', '30-71234567-8', '3834123456', 'distribuidora@gmail.com', 'Santa María', 'ACTIVO');

-- Volcando estructura para procedimiento salonbellezabd.RealizarVentaProducto
DELIMITER //
CREATE PROCEDURE `RealizarVentaProducto`(
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
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.RegistrarCategoriaProducto
DELIMITER //
CREATE PROCEDURE `RegistrarCategoriaProducto`(
    IN p_nombre VARCHAR(50),
    IN p_descripcion VARCHAR(255)
)
BEGIN
    INSERT INTO Categorias_Producto (nombre, descripcion, activo)
    VALUES (p_nombre, p_descripcion, TRUE);
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.RegistrarCliente
DELIMITER //
CREATE PROCEDURE `RegistrarCliente`(
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
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.RegistrarCompra
DELIMITER //
CREATE PROCEDURE `RegistrarCompra`(
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
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.RegistrarEmpleado
DELIMITER //
CREATE PROCEDURE `RegistrarEmpleado`(
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
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.RegistrarPago
DELIMITER //
CREATE PROCEDURE `RegistrarPago`(
    IN p_id_venta INT,
    IN p_monto DECIMAL(10,2),
    IN p_metodo_pago VARCHAR(50),
    IN p_referencia VARCHAR(100)
)
BEGIN
    INSERT INTO Pagos (id_venta, fecha_pago, monto, metodo_pago, estado, referencia)
    VALUES (p_id_venta, NOW(), p_monto, p_metodo_pago, 'APROBADO', p_referencia);
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.RegistrarPagoMembresia
DELIMITER //
CREATE PROCEDURE `RegistrarPagoMembresia`(
    IN p_id_cliente_membresia INT, IN p_monto DECIMAL(10,2), IN p_metodo VARCHAR(50), IN p_referencia VARCHAR(255)
)
BEGIN
    INSERT INTO pago_membresia (id_cliente_membresia, fecha_pago, monto_pago, metodo_pago, estado, referencia)
    VALUES (p_id_cliente_membresia, NOW(), p_monto, p_metodo, 'APROBADO', p_referencia);
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.RegistrarProducto
DELIMITER //
CREATE PROCEDURE `RegistrarProducto`(
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
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.RegistrarProveedor
DELIMITER //
CREATE PROCEDURE `RegistrarProveedor`(
    IN p_razon_social VARCHAR(100),
    IN p_cuit VARCHAR(20),
    IN p_telefono VARCHAR(20),
    IN p_email VARCHAR(100),
    IN p_direccion VARCHAR(100)
)
BEGIN
    INSERT INTO Proveedores (razon_social, cuit, telefono, email, direccion, activo)
    VALUES (p_razon_social, p_cuit, p_telefono, p_email, p_direccion, 'ACTIVO');
END//
DELIMITER ;

-- Volcando estructura para procedimiento salonbellezabd.RegistrarServicio
DELIMITER //
CREATE PROCEDURE `RegistrarServicio`(
    IN p_id_categoria INT,
    IN p_nombre VARCHAR(100),
    IN p_descripcion TEXT,
    IN p_precio DECIMAL(10,2),
    IN p_duracion INT
)
BEGIN
    INSERT INTO Servicios (id_categoria_servicio, nombre, descripcion, precio, duracion_minuto, estado)
    VALUES (p_id_categoria, p_nombre, p_descripcion, p_precio, p_duracion, 'ACTIVO');
END//
DELIMITER ;

-- Volcando estructura para tabla salonbellezabd.salon
CREATE TABLE IF NOT EXISTS `salon` (
  `id_salon` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `direccion` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `telefono` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `logo` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `activo` tinyint(1) DEFAULT '1',
  `fecha_alta` date DEFAULT NULL,
  PRIMARY KEY (`id_salon`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla salonbellezabd.salon: ~0 rows (aproximadamente)

-- Volcando estructura para tabla salonbellezabd.servicios
CREATE TABLE IF NOT EXISTS `servicios` (
  `id_servicio` int NOT NULL AUTO_INCREMENT,
  `id_categoria_servicio` int NOT NULL,
  `nombre` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `descripcion` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `precio` decimal(10,2) NOT NULL,
  `duracion_minuto` int DEFAULT NULL,
  `estado` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`id_servicio`),
  KEY `fk_servicios_categoria` (`id_categoria_servicio`),
  CONSTRAINT `fk_servicios_categoria` FOREIGN KEY (`id_categoria_servicio`) REFERENCES `categorias_servicio` (`id_categoria_servicio`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla salonbellezabd.servicios: ~0 rows (aproximadamente)
REPLACE INTO `servicios` (`id_servicio`, `id_categoria_servicio`, `nombre`, `descripcion`, `precio`, `duracion_minuto`, `estado`) VALUES
	(1, 1, 'Corte de cabello', 'Corte y peinado', 10000.00, NULL, 'ACTIVO');

-- Volcando estructura para tabla salonbellezabd.turnos
CREATE TABLE IF NOT EXISTS `turnos` (
  `id_turno` int NOT NULL AUTO_INCREMENT,
  `id_cliente` int NOT NULL,
  `id_empleado` int NOT NULL,
  `fecha` date NOT NULL,
  `hora` time NOT NULL,
  `estado` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `observacion` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `fecha_creacion` datetime DEFAULT NULL,
  PRIMARY KEY (`id_turno`),
  KEY `fk_turnos_cliente` (`id_cliente`),
  KEY `fk_turnos_empleado` (`id_empleado`),
  CONSTRAINT `fk_turnos_cliente` FOREIGN KEY (`id_cliente`) REFERENCES `clientes` (`id_cliente`),
  CONSTRAINT `fk_turnos_empleado` FOREIGN KEY (`id_empleado`) REFERENCES `empleados` (`id_empleado`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla salonbellezabd.turnos: ~3 rows (aproximadamente)
REPLACE INTO `turnos` (`id_turno`, `id_cliente`, `id_empleado`, `fecha`, `hora`, `estado`, `observacion`, `fecha_creacion`) VALUES
	(1, 1, 1, '2026-10-10', '10:00:00', 'PENDIENTE', 'Turno de prueba', '2026-10-07 23:44:28'),
	(2, 1, 1, '2026-10-10', '10:00:00', 'PENDIENTE', 'Turno de prueba', '2026-10-08 06:30:50'),
	(3, 1, 1, '2026-10-10', '10:00:00', 'PENDIENTE', 'Turno de prueba', '2026-10-08 06:30:59');

-- Volcando estructura para tabla salonbellezabd.ventas
CREATE TABLE IF NOT EXISTS `ventas` (
  `id_venta` int NOT NULL AUTO_INCREMENT,
  `id_cliente` int NOT NULL,
  `id_turno` int DEFAULT NULL,
  `fecha_hora` datetime DEFAULT NULL,
  `tipo_venta` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `estado` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `subtotal` decimal(10,2) DEFAULT NULL,
  `descuento` decimal(10,2) DEFAULT NULL,
  `total` decimal(10,2) DEFAULT NULL,
  `observacion` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`id_venta`),
  KEY `fk_ventas_cliente` (`id_cliente`),
  KEY `fk_ventas_turno` (`id_turno`),
  CONSTRAINT `fk_ventas_cliente` FOREIGN KEY (`id_cliente`) REFERENCES `clientes` (`id_cliente`),
  CONSTRAINT `fk_ventas_turno` FOREIGN KEY (`id_turno`) REFERENCES `turnos` (`id_turno`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Volcando datos para la tabla salonbellezabd.ventas: ~1 rows (aproximadamente)
REPLACE INTO `ventas` (`id_venta`, `id_cliente`, `id_turno`, `fecha_hora`, `tipo_venta`, `estado`, `subtotal`, `descuento`, `total`, `observacion`) VALUES
	(1, 1, 1, '2026-10-08 00:30:40', 'SERVICIO', 'PENDIENTE', 10000.00, 0.00, 10000.00, 'Venta correspondiente al turno');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
