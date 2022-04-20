create table Seguridad(
IdSeguridad int identity(1,1) not null,
Usuario varchar(50) not null,
NombreUsuario varchar(100) not null,
Contrasena varchar(200) not null,
Rol varchar(15) not null
constraint PK_Seguridad primary key (IdSeguridad)
)

