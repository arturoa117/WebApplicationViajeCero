# 📦 Viaje Cero

> Es una aplicación que facilita la reporteria de todas las solicitudes realizadas por los quioscos de cada región del país. 

---

## 🚀 Tecnologías Principales

- [.NET Core 8.0.19](https://dotnet.microsoft.com/en-us/)  
- C#
- ASP.NET Core Web API  
- Entity Framework Core  
- MySQL
- Swagger para documentación  

---

## 📁 Estructura del Proyecto
```bash
/ViajeCero
│
├── Context/ # Contexto de la base de datos (DbContext)
├── Controllers/ # Controladores de la API
├── DTOs/ # Data Transfer Objects
├── Filters/ # Filtros personalizados (e.g. validación, excepciones)
├── Migrations/ # Migraciones generadas por EF Core
├── Models/ # Modelos de dominio
├── Seeders/ # Poblado inicial de datos (data seeding)
├── Utils/ # Clases utilitarias y helpers
├── Validations/ # Validaciones personalizadas
└── Program.cs # Punto de entrada de la aplicación
```
---
## ⚙️ Requisitos Previos

Antes de comenzar, asegúrate de tener instalado:

- [.NET SDK 8.0.300](https://dotnet.microsoft.com/download)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/)
- Motor de base de datos [MySQL](https://www.mysql.com/)

---

## 🔧 Instalación y Ejecución

### 🛠️ 1. Clonar el repositorio

```bash
git clone https://github.com/usuario/nombre-del-proyecto.git
cd nombre-del-proyecto
```

### 📦 2. Restaurar paquetes
```bash
dotnet restore
```

### 🗃️ 3. Aplicar migraciones a la base de datos
```bash
dotnet ef database update
```

Asegúrate de configurar correctamente la cadena de conexión en appsettings.json.

### ▶️ 4. Ejecutar el proyecto
```bash
dotnet run
```

El proyecto se ejecutará por defecto en:
* https://localhost:5001
* http://localhost:5000

### 📚 Documentación de la API

Una vez corriendo el proyecto, puedes acceder a Swagger en:

* https://localhost:5001/swagger


### 🔐 Configuración de Entorno

Puedes configurar variables sensibles como cadenas de conexión o claves API en:
* `appsettings.Development.json`
* `Variables de entorno`
* `Azure Key Vault (opcional para producción)`

### 🤝 Contribuciones

¡Las contribuciones son bienvenidas! Para contribuir:
1. Haz un fork del repositorio.
2. Crea una nueva rama `(git checkout -b feature/nueva-feature)`.
3. Realiza tus cambios y haz commit `(git commit -m 'Agregar nueva feature')`.
4. Haz push a tu rama `(git push origin feature/nueva-feature)`.
5. Abre un Pull Request.