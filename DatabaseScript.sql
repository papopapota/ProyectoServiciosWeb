use master
go
drop database if EXISTS DB_RefriVirtual  
go

create database DB_RefriVirtual
go

use DB_RefriVirtual
go

create table tb_categoriaReceta(
idCategoria int primary key ,
descripcion varchar(100)
);

create table tb_receta(
idReceta char(4) primary key,
nombre varchar(50),
imagen varchar(100),
preparacion varchar(350)

);

create table tb_categoriaRecetaDetalle(
id int ,
idCategoria int  ,
idReceta  char(4) ,
primary key(id,idCategoria,idReceta),
Foreign key (idReceta)REFERENCES tb_receta(idReceta),
Foreign key (idCategoria)REFERENCES tb_categoriaReceta(idCategoria)
);

create table tb_producto(
idProducto char(4) primary key,
nombre varchar(50),
precio float,
stock int ,
descripcion varchar(100),
imagen varchar(100),
estado bit
);

create table tb_ingredientes(
idIngrediente int primary key,
idReceta char(4),
idProducto char(4),
descripcionCantidad varchar(100),
Foreign key (idReceta)REFERENCES tb_receta(idReceta),
Foreign key (idProducto)REFERENCES tb_producto(idProducto)
);


create table tb_tipoUsuario(
id int primary key,
descripcion varchar(100)
);

create table tb_usuario(
idUsuario int primary key,
nombres varchar(100),
apellidos varchar(100),
email varchar(50),
telefono char(9),
contrasena varchar(50),
tipoUsuario int ,
estado bit,
idMembrecia int ,
Foreign key (tipoUsuario)REFERENCES tb_tipoUsuario(id)
);

create table tb_cabeceraCarrito(
idCarrito int primary key,
idUsuario int,
estadoCarrito bit ,
Foreign key (idUsuario)REFERENCES tb_usuario(idUsuario)
);

create table tb_detalleCarrito(
idDetalleCarrito int primary key ,
idCarrito int NOT NULL,
idProducto char(4) NOT NULL,
cantidad int NOT NULL,
precioTotal DECIMAL(10, 2) NOT NULL,
Foreign key (idCarrito)REFERENCES tb_cabeceraCarrito(idCarrito),
Foreign key (idProducto)REFERENCES tb_producto(idProducto)
);

create table tb_refri(
idRefri int primary key,
idUsuario int,
Foreign key (idUsuario)REFERENCES tb_usuario(idUsuario)
);

create table tb_detalleRefri(
idDetalleCarrito int primary key ,
idRefri int NOT NULL,
idProducto char(4) NOT NULL,
cantidad int NOT NULL,
Foreign key (idRefri)REFERENCES tb_refri(idRefri),
Foreign key (idProducto)REFERENCES tb_producto(idProducto)
);
create table tb_membrecia(
idMembrecia int primary key,
descripcion varchar(250),
imagen varchar(60),
precio DECIMAL(10, 2) NOT NULL,

);
create table tb_detalleMenmbrecia(
id int primary key,
idMembrecia int ,
idReceta char(4),
Foreign key (idMembrecia)REFERENCES tb_membrecia(idMembrecia),
Foreign key (idReceta)REFERENCES tb_receta(idReceta)
);



--tb_categoriaReceta
insert into tb_categoriaReceta values(1,'Vegano');
insert into tb_categoriaReceta values(2,'Carnivoro');
insert into tb_categoriaReceta values(3,'Sin gluten');
insert into tb_categoriaReceta values(4,'Sin lactosa');


--tb_receta
insert into tb_receta values('R001','Tallarin Rojo','tallarinRojo.jpg','Lorem ipsu Lorem ipsu Lorem ipsu Lorem ipsu Lorem ipsu Lorem ipsu ');
insert into tb_receta values('R002','Tallarin verde','tallarinverde.jpg','Lorem ipsu Lorem ipsu Lorem ipsu Lorem ipsu Lorem ipsu Lorem ipsu ');
insert into tb_receta values('R003','Tallarin Alfredo','tallarinAlfredo.jpg','Lorem ipsu Lorem ipsu Lorem ipsu Lorem ipsu Lorem ipsu Lorem ipsu ');
insert into tb_receta values('R004','Tallarin Fettuccine','tallarinFettuccine.jpg','Lorem ipsu Lorem ipsu Lorem ipsu Lorem ipsu Lorem ipsu Lorem ipsu ');

--tb_categoriaRecetaDetalle
insert into tb_categoriaRecetaDetalle values(1,1,'R001');
insert into tb_categoriaRecetaDetalle values(2,2,'R001');
insert into tb_categoriaRecetaDetalle values(3,3,'R001');
insert into tb_categoriaRecetaDetalle values(4,4,'R001');




