## 📂 **_Proyecto - API de Gestión de Productos con Patron de Diseño Repository_**

Este es un proyecto de API para la gestión de productos, desarrollado como parte de mi portfolio profesional. La arquitectura implementa el patrón de diseño Repository, lo cual permite 
separar la lógica de acceso a datos de la lógica de negocio, mejorando así la flexibilidad y mantenibilidad del código.

🛠️ **_Tecnologías Utilizadas_**

- **Lenguaje**: C# (.NET Core)
- **Framework**: ASP.NET Core Web API
- **ORM**: Entity Framework Core para la gestión de la base de datos.
- **Base de Datos**: SQL Server.
- **Swagger** - Para documentar las APIs y probarlas de manera interactiva.
- **Logger**: SeriLog para la gestión de logs.
- **Inyección de Dependencias**: Gestión nativa de .NET Core

🎨 **_Patrones de Diseño y Arquitectura_**

Este proyecto incorpora varios patrones de diseño y principios para mantener el código limpio, escalable y fácil de mantener:

- **Repository Pattern**: Abstrae el acceso a la base de datos, permitiendo cambiar de proveedor de datos sin impactar la lógica de negocio.
- **Dependency Injection**: Gestiona las dependencias entre componentes y servicios.
- **DTOs (Data Transfer Objects)**: Facilitan el transporte de datos entre capas, protegiendo la integridad de las entidades.
- **Error Handling y Logging**: Estructura la gestión de errores y logs, optimizando la detección y solución de problemas en producción.

🏛️ **_Arquitectura_**

El proyecto está diseñado con una arquitectura modular basada en Clean Architecture, que separa la lógica de negocio (Domino y Aplicación) de la infraestructura y la capa de presentación. 
Esto facilita el mantenimiento y escalabilidad de la aplicación.

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

**Enfoque Code First**: A continuación se describen los pasos para configurar y migrar la base de datos:

**Requisitos Previos**
- Las entidades y el DbContext ya se encuentran definidas.
- Verificar que la configuración de la cadena de conexión en el archivo appsettings.json sea correcta.
  
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

## ⚙️ **_Instrucciones de Ejecución_**

**Requisitos Previos**
- .NET 8. (.NET 7.0 SDK o superior).
- SQL Server u otro motor de base de datos compatible.
- IDE compatible con .NET (Visual Studio o VS Code).
**Configuración del Proyecto**
- Clona el repositorio:
  ```
  https://github.com/FedeTor/PatternDesignRepository.git
  ```
- Configura la base de datos: En el archivo appsettings.json, ajusta la cadena de conexión a la base de datos.
- Ejecuta la aplicación.

**Probar la API**

La API documentada con Swagger estará disponible en ```https://localhost:7084/swagger```

Además se agregó una carpeta "Documentation" con la coleccion de postman, solo queda descargarla e importarla si se desea utilizar.

## 📜 **_Endpoints Principales_**
**Los endpoints principales disponibles en la API son**:

- GET **```/api/products/all```**: Obtiene todos los productos
- GET **```/api/products/get```**: Obtiene un producto por ID
- POST **```/api/products/create```**: Agrega un nuevo producto
- PUT **```/api/products/update```**: Actualiza un producto existente
- DELETE **```/api/products/delete```**: Elimina un producto


## 🧪 **_Pruebas Unitarias_**

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
