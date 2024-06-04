# Gimnasio Bergondo 🏋️‍♀️
El proyecto llevado a cabo tiene el objetivo de crear una página web para la gestión y la comercialización de productos de un gimnasio.

## Tecnologías
* .NET Core 8 MVC: El framework usado.
* C#: Tecnología de backend.
* Bootstrap5: Framework utilizado para el estilo.
* SQL: Gestor de base de datos.
* JavaScript: Tecnlogía de frontend.

## Base de datos
![esquemaBD](https://github.com/chrisasa109/GimnasioBergondo/blob/4ddca06ce2afa0d1ad465d6ed918ae0d053b6aca/images-readme/diagrama.png)

## Casos de uso

### Cliente
- Crear, modificar, eliminar y visualizar la cuenta propia.
- Iniciar y cerrar sesión.
- Visualizar la página de inicio de la aplicación web.
- Visualizar las tarifas disponibles. Además, se podrán contratar y visualizar el contrato en caso de tenerlo activo.
- Visualizar productos en venta, añadirlos al carrito y procesar pedido. Además, también tendrá la opción de visualizar los pedidos que haya realizado anteriormente.
- Visualizar la lista de actividades disponibles. En caso de tener un contrato activo, se podrá apuntar a las actividades que desee. Además, tendrá una pantalla para visualizar las actividades a las cuales está apuntado y poder modificar las notas que tenga apuntadas en caso de tenerlas.
- Acceso a las opiniones de los clientes, al apartado de preguntas frecuentes, a la página de la ubicación y a la política de privacidad.

### Trabajador
- Podrá realizar todas las acciones que puede realizar el cliente.
- Además, puede crear productos y actividades.
- Puede visualizar la lista de actividades con los usuarios que están apuntados a las mismas.
- Puede visualizar la lista de productos y modificar los datos de los mismos que considere oportunos.
- Puede acceder al gestor de clientes, en el cual puede visualizar, modificar y eliminar los perfiles de los clientes.
- Puede acceder a la lista de pedidos de todos los clientes.
- Puede comprobar que los clientes tengan algún contrato activo.

### Administrador
- Puede usar todas las funcionalidades que pueden usar los clientes y los trabajadores.
- A mayores, la gran ventaja que tienen, es que, aparte de acceder a los datos de los clientes, pueden acceder a los datos de los trabajadores y de otras cuentas de administradores.
- Además, tienen la opción de poder cambiar el rol de usuarios. Es decir, puede cambiar el rol de un trabajador a un cliente, y viceversa.

## Despliegue de la aplicación
Descargar Visual Studio (con las herramientas Desarrollo de ASP.NET y web y Almacenamiento y procesamiento de datos) además de SQL Server.
Para incorporar los datos predeterminados en SQL Server:
- Crear el servidor local:
```sh
sqllocaldb create "server_gimnasio"
```
- En SQL Server Management, abrir un nuevo archivo de consulta y ejecutar la creación de la base de datos:
```sh
CREATE DATABASE Gimnasio;
GO
```
- Ejecutar el archivo sql:
```sh
sqlcmd -S (localdb)\server_gimnasio -i archivo.sql
```
_El contenido de archivo.sql debe de ser la ruta del archivo SQL que está contenido en el directorio sql._
