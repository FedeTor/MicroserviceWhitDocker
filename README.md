## 📂 **_Proyecto: Dockerización y Contenerización de la API para la Gestión de Productos_**

Este es un proyecto de API para la gestión de productos, desarrollado como parte de mi portfolio profesional. La arquitectura implementa el patrón de diseño Repository, lo cual permite 
separar la lógica de acceso a datos de la lógica de negocio, mejorando así la flexibilidad y mantenibilidad del código.
Este proyecto también destaca por su uso de **Docker** para la contenerización de la API y la base de datos.

---

🛠️ **_Tecnologías Utilizadas_**

- **Lenguaje**: C# (.NET Core)
- **Framework**: ASP.NET Core Web API
- **ORM**: Entity Framework Core para la gestión de la base de datos.
- **Base de Datos**: SQL Server.
- **Swagger** - Para documentar las APIs y probarlas de manera interactiva.
- **Logger**: SeriLog para la gestión de logs.
- **Inyección de Dependencias**: Gestión nativa de .NET Core
- **Contenerización**: **Docker** (Dockerfile y Docker Compose)

🎨 **_Patrones de Diseño y Arquitectura_**

Este proyecto incorpora varios patrones de diseño y principios para mantener el código limpio, escalable y fácil de mantener:

- **Repository Pattern**: Abstrae el acceso a la base de datos, permitiendo cambiar de proveedor de datos sin impactar la lógica de negocio.
- **Dependency Injection**: Gestiona las dependencias entre componentes y servicios.
- **DTOs (Data Transfer Objects)**: Facilitan el transporte de datos entre capas, protegiendo la integridad de las entidades.
- **Error Handling y Logging**: Estructura la gestión de errores y logs, optimizando la detección y solución de problemas en producción.

🏛️ **_Arquitectura_**

El proyecto está diseñado con **Clean Architecture**.  
**Capas principales**:
- **Domain**: Entidades y lógica de negocio.
- **Application**: Lógica de aplicación.
- **Infrastructure**: Configuración de acceso a datos.
- **Presentation**: Exposición de la API mediante controladores.

**Las capas principales incluyen**:

- Domain: Entidades y lógica de negocio.
- Application: Lógica de aplicación, patrones de diseño.
- Infrastructure: Configuración de acceso a bases de datos y lógica específica del proveedor.
- Presentation: Exposición de la API mediante controladores.

👨‍🏫 **_Buenas Prácticas Implementadas_**

- **Principios SOLID**: Código modular, con baja dependencia entre clases y alta cohesión.
- **POO** (Programación Orientada a Objetos): Uso de encapsulación, herencia y polimorfismo para crear componentes reutilizables y flexibles.
- **DRY** (Don't Repeat Yourself): Minimiza la repetición innecesaria de código.

🗃️ **_Base de Datos_**

La base de datos predeterminada es SQL Server.

Dentro de la carpeta "Documentation" se encuentra el script para crear la base de datos con la tabla correspondiente, opcionalmente se puede hacer mediante un enfoque Code First.

📜 **_Endpoints Principales_**
**Los endpoints principales disponibles en la API son**:

- GET **```/api/products/all```**: Obtiene todos los productos
- GET **```/api/products/get```**: Obtiene un producto por ID
- POST **```/api/products/create```**: Agrega un nuevo producto
- PUT **```/api/products/update```**: Actualiza un producto existente
- DELETE **```/api/products/delete```**: Elimina un producto

🧪 **_Pruebas Unitarias_**

El proyecto incluye pruebas unitarias implementadas en la capa **Application.Tests** utilizando **Moq** y **NUnit**. Estas pruebas aseguran la calidad y la estabilidad del código, permitiendo identificar y corregir errores de manera temprana.

#### Tecnologías Utilizadas
- **Moq**: Una biblioteca para crear objetos simulados (mocks) en pruebas unitarias, lo que permite simular el comportamiento de las dependencias de las clases que se están probando.
- **NUnit**: Un marco de trabajo para pruebas unitarias que permite escribir y ejecutar pruebas en .NET.

#### Ejecución de Pruebas
Para ejecutar las pruebas unitarias, sigue estos pasos:

1. Abre la solución en Visual Studio o en tu IDE de preferencia.
2. Asegúrate de que todos los proyectos estén construidos correctamente.
3. Accede a la ventana **Test** en Visual Studio.
4. Haz clic en **Run All** para ejecutar todas las pruebas.

También puedes ejecutar las pruebas, dentro de la carpeta donde se encuentran las pruebas, desde la línea de comandos utilizando el siguiente comando:

```bash
dotnet test

-----------------------------------------------------------------------------------------------------------------------------------------

## 🐳 **_Docker y Contenerización_**

Este proyecto incluye la configuración necesaria para ejecutar la API y la base de datos en contenedores mediante **Docker**.  
El uso de Docker asegura un entorno de desarrollo consistente, simplifica la implementación y mejora la portabilidad.

#### **Dockerfile**
El `Dockerfile` define la imagen de la API con .NET, incluyendo:

- **Base de la imagen**: Se utiliza la imagen `mcr.microsoft.com/dotnet/aspnet:8.0` para ejecutar la API, configurando el entorno de trabajo en `/app` y exponiendo los puertos `8080` y `8081` para la comunicación.
- **Configuración de la cadena de conexión**: Se define la variable de entorno `DB_CONNECTION_STRING` para la conexión con la base de datos SQL Server.
- **Restauración de dependencias**: Se copian los archivos `.csproj` y se ejecuta `dotnet restore` para restaurar las dependencias de los proyectos.
- **Construcción y publicación**: La aplicación se compila en modo `Release` con `dotnet build` y se publica con `dotnet publish` en la carpeta `/app/publish` dentro del contenedor.
- **Ejecución**: Se establece el comando `ENTRYPOINT` para ejecutar la API con `dotnet Api.Presentation.dll`. 

#### **Docker Compose**
El archivo `docker-compose.yml` configura los servicios de contenedores necesarios para ejecutar el proyecto, incluyendo la base de datos SQL Server y la API, con las siguientes características:

- **Base de Datos (SQL Server)**:
  - Imagen: `mcr.microsoft.com/mssql/server:2022-latest`.
  - Puertos: Se mapea el puerto `1433` del contenedor al puerto `8006` del host.
  - Configuración de entorno:
    - `ACCEPT_EULA=Y`: Acepta los términos de la licencia de SQL Server.
    - `MSSQL_SA_PASSWORD`: Contraseña del administrador de la base de datos.

- **API (Api.Presentation)**:
  - Construcción: Se utiliza el `Dockerfile` ubicado en `Api.Presentation/Dockerfile`.
  - Puertos: El puerto `8080` se mapea al puerto `5001` del host para acceder a la API.
  - Variables de entorno:
    - `ASPNETCORE_URLS=http://+:8080`: Configura la URL base para la API.
    - `ASPNETCORE_ENVIRONMENT=Development`: Define el entorno como desarrollo.
  - Dependencias: La API depende del servicio de base de datos, garantizando que SQL Server esté disponible antes de iniciar la API.

- **Redes**: Los servicios se comunican a través de una red llamada `mynetworkapi`.

---

## ⚙️ **_Instrucciones de Ejecución_**

**Requisitos Previos**
- .NET 8. (.NET 7.0 SDK o superior).
- SQL Server u otro motor de base de datos compatible.
- IDE compatible con .NET (Visual Studio o VS Code).
**Configuración del Proyecto**
- Clona el repositorio:
  ```
  https://github.com/FedeTor/MicroserviceWhitDocker.git
  ```
#### **Comandos Docker y enfoque Code First para migrar la base de datos**
1. **Construir los contenedores**: Desde la carpeta raiz del proyecto ejecutar el siguiente comando:
   Este comando construye y levanta los contenedores en segundo plano.
   ```bash
   docker-compose up --build -d

2. **Verificar los contenedores en ejecución**: Esto muestra los contenedores en ejecución y los puertos expuestos.
   ```bash
   docker ps
   
3. **Acceder a la API a través de Swagger**: Una vez que los contenedores están en funcionamiento, puedes acceder a la API usando Swagger en la siguiente URL:
   Esta es la URL donde puedes interactuar con los endpoints de la API de manera interactiva.
   ```bash
   http://localhost:port/swagger
   
4. **Enfoque Code First**: A continuación se describen los pasos para configurar y migrar la base de datos:

**Requisitos Previos**
- Las entidades y el DbContext ya se encuentran definidas.
- Verificar que la configuración de la cadena de conexión en el archivo appsettings.json sea correcta. Por ejemplo:
```
"ConnectionStrings": {
    "CadenaSQL": "Server=192.168.100.3,1433;Database=TEST;User ID=sa;Password=Admin123;TrustServerCertificate=True;"
}

```
En este ejemplo, se está utilizando la máquina local como servidor mediante la dirección IP 192.168.100.3. Si deseas utilizar tu máquina como servidor de base de datos, asegúrate de especificar su dirección IP en el campo Server. Si estás trabajando en un entorno diferente, reemplaza esta IP con la dirección correspondiente al servidor que desees usar.
En caso de no utilizar tu PC como servidor y optar por un contenedor de Docker como servidor SQL, es necesario utilizar la línea comentada en el Dockerfile para configurar la conexión, como se muestra a continuación:
```
#ENV DB_CONNECTION_STRING="Server=sqlserverdocker;Database=TEST2;User ID=sa;Password=MyPassword*1234;TrustServerCertificate=True;"
```
Asegúrate de descomentar esta línea y ajustar los valores según tu configuración, como el nombre del contenedor, el usuario y la contraseña del servidor SQL dentro del entorno Docker.
  
**Pasos**
- Establecer la capa Api.Presentation como proyecto de inicio.
- Ejecuta el siguiente comando en la consola del administrador de paquetes para generar la migración inicial basada en las entidades definidas:

```
Add-Migration InitialCreate -Project Infrastructure -StartupProject Api.Presentation
```

- Para aplicar las migraciones a la base de datos y crear las tablas en la base de datos según las configuraciones especificadas en el DbContext, ejecuta el siguiente comando:

```
Update-Database -Project Infrastructure -StartupProject Api.Presentation
```

**Probar la API**

La API  documentada con Swagger estará disponible en ```https://localhost:port/swagger```
Además se agregó una carpeta "Documentation" con la coleccion de postman, solo queda descargarla e importarla si se desea utilizar.
