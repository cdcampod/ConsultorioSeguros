CREATE DATABASE `ConsultorioDb` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE ConsultorioDb;

CREATE TABLE `Seguro` (
  `codigo` varchar(50) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `prima`  decimal(10, 2) NOT NULL,
  `suma_asegurada` decimal(10, 2) NOT NULL,  
  PRIMARY KEY (`codigo`),
  UNIQUE KEY `nombre_UNIQUE` (`nombre`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `Cliente` (
  `cedula` varchar(20) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `telefono` varchar(20) NOT NULL,
  `edad` int NOT NULL,  
  PRIMARY KEY (`cedula`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `Seguro_cliente` (
  `id` int NOT NULL AUTO_INCREMENT,
  `codigo_seguro` varchar(50) NOT NULL,
  `cedula` varchar(20) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_seguro_idx` (`codigo_seguro`),
  KEY `fk_cliente_idx` (`cedula`),
  CONSTRAINT `fk_cliente` FOREIGN KEY (`cedula`) REFERENCES `Cliente` (`cedula`),
  CONSTRAINT `fk_seguro` FOREIGN KEY (`codigo_seguro`) REFERENCES `Seguro` (`codigo`)
) ENGINE=InnoDB CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;