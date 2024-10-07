Create database Heladeria
USE Heladeria

create table Usuarios(
id_usuario int primary key identity,
nombre_usuario nvarchar(60),
contra_usuario nvarchar(150)
)

create table Localidades(
id_localidad int primary key identity,
nombre_localidad nvarchar(60)
)

create table Sucursales(
id_sucursal int primary key identity,
localidad int foreign key references Localidades(id_localidad),
calle_sucursal nvarchar(60),
nro_sucursal int
)
create table Gustos(
id_gusto int primary key identity,
nombre_gusto nvarchar(60),
descrip_gusto nvarchar (120),
)


create table Ventas(
id_venta int primary key identity,
fecha date,
id_sucursal int foreign key references Sucursales(id_sucursal),
)

CREATE TABLE Ventas_Detalle (
    id_venta INT FOREIGN KEY (id_venta) REFERENCES Ventas(id_venta),
    id_gusto INT FOREIGN KEY (id_gusto) REFERENCES Gustos(id_gusto),
    cantidad INT NOT NULL,
	primary key(id_venta, id_gusto)
);

	insert into Gustos values ('Dulce Caramelo', 'Helado cremoso de caramelo con salsa de toffee y trocitos de caramelo duro.'), 
('Choco Menta', 'Helado de menta fresca con trozos crujientes de chocolate semiamargo.'), 
('Frutilla a la Crema', 'Helado de frutilla natural mezclado con crema fresca.'), 
('Crema Americana', 'Helado de crema suave y clásica, ideal para combinar o disfrutar solo.'), 
('Vainilla Clásica', 'La elegancia de la vainilla pura en una textura cremosa.'), ('Chocolate Intenso', 'Helado de chocolate con cacao puro y sabor profundo.'), 
('Dulce de Leche Granizado', 'Dulce de leche cremoso con trocitos de chocolate crujiente.'), 
('Tiramisú', 'Helado de café suave con mascarpone y cacao inspirado en el clásico postre italiano.'), 
('Café Crocante', 'Helado de café con pequeños trozos de nueces caramelizadas.'), ('Limonada Fría', 'Refrescante sorbete de limón con un toque de menta.'), 
('Banana Split', 'Helado de banana con salsa de chocolate y trozos de almendra.'), 
('Choco Avellana', 'Helado de chocolate con crema de avellanas y trocitos crujientes.'), 
('Coco Rallado', 'Helado cremoso con sabor a coco y pequeños trozos de coco rallado.'), 
('Mango Tropical', 'Helado de mango natural con un toque de maracuyá para un sabor exótico.'), 
('Brownie Supreme', 'Helado de chocolate con trozos de brownie y salsa de chocolate.')

insert into Localidades values ('Rosario'),('Cordoba'),('Santa Fe')

insert into Sucursales values (1, 'Lagos', 1111), (1, 'Espora',3333),(2, 'Colon', 1005),(2,'Calle de cordoba', 1905),(3, 'Libertador', 4568)

-- Insertar más ventas en diferentes sucursales
INSERT INTO Ventas (fecha, id_sucursal) VALUES ('2024-09-12', 1); -- Venta en Rosario
INSERT INTO Ventas (fecha, id_sucursal) VALUES ('2024-09-15', 2); -- Venta en otra sucursal de Rosario
INSERT INTO Ventas (fecha, id_sucursal) VALUES ('2024-09-11', 3); -- Venta en Córdoba
INSERT INTO Ventas (fecha, id_sucursal) VALUES ('2024-09-14', 4); -- Venta en Córdoba (otra sucursal)
INSERT INTO Ventas (fecha, id_sucursal) VALUES ('2024-09-20', 5); -- Venta en Santa Fe
INSERT INTO Ventas (fecha, id_sucursal) VALUES ('2024-09-21', 1); -- Otra venta en Rosario





-- Insertar detalles de la primera venta
INSERT INTO Ventas_Detalle (id_venta, id_gusto, cantidad) VALUES (1, 1, 3); -- Dulce Caramelo
INSERT INTO Ventas_Detalle (id_venta, id_gusto, cantidad) VALUES (1, 3, 2); -- Frutilla a la Crema
INSERT INTO Ventas_Detalle (id_venta, id_gusto, cantidad) VALUES (1, 5, 1); -- Vainilla Clásica

-- Insertar detalles de la segunda venta
INSERT INTO Ventas_Detalle (id_venta, id_gusto, cantidad) VALUES (2, 2, 4); -- Choco Menta
INSERT INTO Ventas_Detalle (id_venta, id_gusto, cantidad) VALUES (2, 6, 2); -- Chocolate Intenso

-- Insertar detalles de la tercera venta
INSERT INTO Ventas_Detalle (id_venta, id_gusto, cantidad) VALUES (3, 7, 3); -- Dulce de Leche Granizado
INSERT INTO Ventas_Detalle (id_venta, id_gusto, cantidad) VALUES (3, 9, 2); -- Café Crocante

-- Insertar detalles de la cuarta venta
INSERT INTO Ventas_Detalle (id_venta, id_gusto, cantidad) VALUES (4, 8, 5); -- Tiramisú
INSERT INTO Ventas_Detalle (id_venta, id_gusto, cantidad) VALUES (4, 10, 3); -- Limonada Fría

-- Insertar detalles de la quinta venta
INSERT INTO Ventas_Detalle (id_venta, id_gusto, cantidad) VALUES (5, 11, 2); -- Banana Split
INSERT INTO Ventas_Detalle (id_venta, id_gusto, cantidad) VALUES (5, 13, 4); -- Coco Rallado

-- Insertar detalles de la sexta venta
INSERT INTO Ventas_Detalle (id_venta, id_gusto, cantidad) VALUES (6, 4, 3); -- Crema Americana
INSERT INTO Ventas_Detalle (id_venta, id_gusto, cantidad) VALUES (6, 12, 2); -- Choco Avellana
INSERT INTO Ventas_Detalle (id_venta, id_gusto, cantidad) VALUES (6, 14, 1); -- Mango Tropical



									--Metodo para calcular los gustos más vendidos 
SELECT 
    g.id_gusto,
    g.nombre_gusto,
    SUM(vd.cantidad) AS cantidad_total_pedida
FROM 
    Ventas_Detalle vd
JOIN 
    Gustos g ON vd.id_gusto = g.id_gusto
GROUP BY 
    g.id_gusto, g.nombre_gusto
ORDER BY 
    cantidad_total_pedida DESC



