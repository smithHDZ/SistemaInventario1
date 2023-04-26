create database proyectobd;
use proyectobd;

/*Creacion de tablas*/
CREATE TABLE CLIENTE (
    IdCliente       INTEGER NOT NULL AUTO_INCREMENT
                            UNIQUE,
    NumeroDocumento VARCHAR(25)    NOT NULL,
    NombreCompleto  VARCHAR(45)    NOT NULL,
    PRIMARY KEY (
        IdCliente 
    )
);

CREATE TABLE DATOS (
    IdDato      INT NOT NULL,
    RazonSocial VARCHAR(45)    NOT NULL,
    Ruc         VARCHAR(45)    NOT NULL,
    Direccion   VARCHAR(45),
    Logo        BLOB,
    PRIMARY KEY (
        IdDato
    )
);

CREATE TABLE ENTRADA (
    IdEntrada          INT NOT NULL AUTO_INCREMENT
                               UNIQUE,
    NumeroDocumento    VARCHAR(25)    NOT NULL,
    FechaRegistro      VARCHAR(25)    NOT NULL,
    UsuarioRegistro    VARCHAR(25)    NOT NULL,
    DocumentoProveedor VARCHAR(25)    NOT NULL,
    NombreProveedor    VARCHAR(25)    NOT NULL,
    CantidadProductos  VARCHAR(25)    NOT NULL,
    MontoTotal         VARCHAR(25)    NOT NULL,
    PRIMARY KEY (
        IdEntrada
    )
);

CREATE TABLE FACTURACION (
    RFC           VARCHAR(25)   PRIMARY KEY
                          NOT NULL
                          UNIQUE,
    Direccion     VARCHAR(45)    NOT NULL,
    Nombre_clie   VARCHAR(25)    NOT NULL,
    cod_prod      INT NOT NULL,
    descrip_prod  VARCHAR(45)    NOT NULL,
    cantidad_prod INT NOT NULL,
    precio        INT NOT NULL,
    total         INT NOT NULL,
    total_iva     INT NOT NULL
);

CREATE TABLE PERMISOS (
    IdPermisos    INT NOT NULL,
    Descripcion   VARCHAR(45)    NOT NULL,
    Salidas       INT NOT NULL,
    Entradas      INT NOT NULL,
    Productos     INT NOT NULL,
    Clientes      INT NOT NULL,
    Proveedores   INT NOT NULL,
    Inventario    INT NOT NULL,
    Configuracion INT NOT NULL,
    PRIMARY KEY (
        IdPermisos
    )
);

CREATE TABLE PRODUCTO (
    IdProducto   INT NOT NULL AUTO_INCREMENT
                         UNIQUE,
    Codigo       VARCHAR(25)    NOT NULL,
    Descripcion  VARCHAR(45)    NOT NULL,
    Categoria    VARCHAR(25)    DEFAULT '',
    Almacen      VARCHAR(45)    DEFAULT '',
    PrecioCompra VARCHAR(25)    NOT NULL
                         DEFAULT '',
    PrecioVenta  VARCHAR(25)    NOT NULL
                         DEFAULT '',
    Stock        INT NOT NULL
                         DEFAULT 0,
    PRIMARY KEY (
        IdProducto
    )
);

CREATE TABLE PROVEEDOR (
    IdProveedor     INT NOT NULL AUTO_INCREMENT
                            UNIQUE,
    NumeroDocumento VARCHAR(25)    NOT NULL,
    NombreCompleto  VARCHAR(45)    NOT NULL,
    PRIMARY KEY (
        IdProveedor
    )
);

CREATE TABLE SALIDA (
    IdSalida          INT NOT NULL AUTO_INCREMENT
                              UNIQUE,
    NumeroDocumento   VARCHAR(25)    NOT NULL,
    FechaRegistro     VARCHAR(25)    NOT NULL,
    UsuarioRegistro   VARCHAR(25)    NOT NULL,
    DocumentoCliente  VARCHAR(25)    NOT NULL,
    NombreCliente     VARCHAR(45)    NOT NULL,
    CantidadProductos INT NOT NULL,
    MontoTotal        VARCHAR(45)    NOT NULL,
    PRIMARY KEY (
        IdSalida 
    )
);

CREATE TABLE TIPO_BARRA (
    IdTipoBarra INT NOT NULL
                        UNIQUE,
    Value       INT NOT NULL,
    PRIMARY KEY (
        IdTipoBarra
    )
);

CREATE TABLE USUARIO (
    IdUsuario      INT NOT NULL AUTO_INCREMENT
                           UNIQUE,
    NombreCompleto VARCHAR(45)    NOT NULL,
    NombreUsuario  VARCHAR(45)    NOT NULL,
    Clave          VARCHAR(25)    NOT NULL,
    IdPermisos     INT NOT NULL,
    PRIMARY KEY (
        IdUsuario
    )
);

CREATE TABLE DETALLE_ENTRADA (
    IdDetalleEntrada    INT NOT NULL AUTO_INCREMENT
                                UNIQUE,
    IdEntrada           INT NOT NULL,
    IdProducto          INT NOT NULL,
    CodigoProducto      VARCHAR(45)    NOT NULL,
    DescripcionProducto VARCHAR(45)    NOT NULL,
    CategoriaProducto   VARCHAR(45)    NOT NULL,
    AlmacenProducto     VARCHAR(45)    NOT NULL,
    PrecioCompra        VARCHAR(45)    NOT NULL,
    PrecioVenta         VARCHAR(45)    NOT NULL,
    Cantidad            INT NOT NULL,
    SubTotal            VARCHAR(45)    NOT NULL,
    FOREIGN KEY (
        IdEntrada
    )
    REFERENCES ENTRADA (IdEntrada),
    PRIMARY KEY (
        IdDetalleEntrada 
    )
);

CREATE TABLE DETALLE_SALIDA (
    IdDetalleSalida     INT NOT NULL AUTO_INCREMENT
                                UNIQUE,
    IdSalida            INT NOT NULL,
    IdProducto          INT NOT NULL,
    CodigoProducto      VARCHAR(45)    NOT NULL,
    DescripcionProducto VARCHAR(45)    NOT NULL,
    CategoriaProducto   VARCHAR(45)    NOT NULL,
    AlmacenProducto     VARCHAR(45)    NOT NULL,
    PrecioVenta         VARCHAR(45)    NOT NULL,
    Cantidad            INT NOT NULL,
    SubTotal            VARCHAR(45)    NOT NULL,
    FOREIGN KEY (
        IdSalida
    )
    REFERENCES SALIDA (IdSalida),
    PRIMARY KEY (
        IdDetalleSalida 
    )
);

/*Insertar datos*/
insert into cliente (IdCliente, NumeroDocumento, NombreCompleto) values ('1', '250', 'Diego Valdez');

insert into cliente (IdCliente, NumeroDocumento, NombreCompleto) values ('2', '251', 'Cesar Garcia');

INSERT INTO PERMISOS (IdPermisos,Descripcion,Salidas,Entradas,Productos,Clientes,Proveedores,Inventario,Configuracion) VALUES ('1','Administrador','1','1','1','1','1','1','1');

INSERT INTO PROVEEDOR (IdProveedor,NumeroDocumento,NombreCompleto) VALUES ('2','444777','Master Tecnologia HUB');

INSERT INTO PROVEEDOR (IdProveedor,NumeroDocumento,NombreCompleto) VALUES ('1','555777','Empresa Util Rapida');

INSERT INTO TIPO_BARRA (IdTipoBarra,value) VALUES ('1','28');

INSERT INTO USUARIO(IdUsuario,NombreCompleto,NombreUsuario,Clave,IdPermisos) VALUES ('1','CodigoEstudiante','Admin','123','1');

INSERT INTO USUARIO(IdUsuario,NombreCompleto,NombreUsuario,Clave,IdPermisos) VALUES ('2','Usuario Test','Empleado','456','2');

INSERT INTO PERMISOS (IdPermisos,Descripcion,Salidas,Entradas,Productos,Clientes,Proveedores,Inventario,Configuracion) VALUES ('1','Empleado','1','1','1','0','0','0','0');

   