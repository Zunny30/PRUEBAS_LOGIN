# Implementación de Sistema CRUD de Usuarios - DB_ACCESO

## Descripción General
Este proyecto implementa un sistema completo de gestión de usuarios con métodos CRUD (Crear, Leer, Actualizar, Eliminar) usando la arquitectura Model-View-Controller (MVC) en ASP.NET con acceso directo a SQL Server.

## Archivos Creados/Modificados

### 1. **DOM/DB.cs** (Modificado)
- Clase estática para gestionar la conexión a la base de datos
- Utiliza `WebConfigurationManager` para obtener la cadena de conexión
- Método `GetConnection()` que devuelve una conexión abierta

### 2. **DOM/UsuarioDAO.cs** (Creado)
- Clase de acceso a datos (Data Access Object) para la tabla de Usuarios
- Implementa los métodos CRUD:
  - **Crear(Usuario usuario)**: Inserta un nuevo usuario
  - **ObtenerPorId(int idUsuario)**: Obtiene un usuario por ID
  - **ObtenerPorCorreo(string correo)**: Obtiene un usuario por correo
  - **ObtenerTodos()**: Obtiene todos los usuarios
  - **Actualizar(Usuario usuario)**: Actualiza datos de un usuario
  - **Eliminar(int idUsuario)**: Elimina un usuario

### 3. **Models/Usuario.cs** (Modificado)
- Modelo que representa un usuario
- Propiedades:
  - `IdUsuario`: Identificador único (int)
  - `Correo`: Correo electrónico (string)
  - `Clave`: Contraseña (string)
  - `ConfirmarClave`: Campo para confirmación de contraseña (string)

### 4. **Web.config** (Modificado)
- Se agregó la sección `connectionStrings` con la configuración de conexión a `DB_ACCESO`
- Cadena de conexión: `Server=localhost;Database=DB_ACCESO;Integrated Security=True;`

### 5. **SQL/CreateTableUsuarios.sql** (Creado)
- Script SQL para crear la tabla Usuarios en la base de datos
- Estructura de la tabla con índices para mejor rendimiento

### 6. **Controllers/UsuarioControllerEjemplo.cs** (Creado)
- Controlador de ejemplo mostrando cómo usar el UsuarioDAO
- Acciones incluidas:
  - Crear, Editar, Eliminar usuarios
  - Listar todos los usuarios
  - Validar login de usuarios

## Pasos de Implementación

### Paso 1: Crear la Base de Datos
1. Abre **SQL Server Management Studio**
2. Ejecuta el siguiente comando para crear la base de datos:
   ```sql
   CREATE DATABASE DB_ACCESO;
   ```

### Paso 2: Crear la Tabla Usuarios
1. Selecciona la base de datos `DB_ACCESO`
2. Ejecuta el script `SQL/CreateTableUsuarios.sql`
   - Este script crea la tabla `Usuarios` con todas las columnas necesarias
   - También crea un índice para mejorar la búsqueda por correo

### Paso 3: Verificar la Cadena de Conexión
1. Abre **Web.config**
2. Verifica o actualiza la cadena de conexión en `connectionStrings`:
   - Para **Autenticación Integrada** (Windows):
     ```xml
     <add name="DB_ACCESO" connectionString="Server=localhost;Database=DB_ACCESO;Integrated Security=True;" providerName="System.Data.SqlClient" />
     ```
   - Para **Autenticación por Usuario/Contraseña**:
     ```xml
     <add name="DB_ACCESO" connectionString="Server=localhost;Database=DB_ACCESO;User Id=tu_usuario;Password=tu_contraseña;" providerName="System.Data.SqlClient" />
     ```

### Paso 4: Compilar el Proyecto
1. En Visual Studio, presiona **Ctrl+Shift+B** para compilar
2. Asegúrate de que no hay errores

### Paso 5: (Opcional) Crear Vistas
Si deseas usar el controlador de ejemplo, crea las siguientes vistas en `Views/Usuario/`:
- `Crear.cshtml`
- `Editar.cshtml`
- `Detalle.cshtml`
- `Listar.cshtml`
- `Eliminar.cshtml`

## Uso del UsuarioDAO

### Crear un Usuario
```csharp
var usuario = new Usuario 
{ 
    Correo = "usuario@example.com", 
    Clave = "password123" 
};
int idUsuario = UsuarioDAO.Crear(usuario);
```

### Obtener un Usuario
```csharp
Usuario usuario = UsuarioDAO.ObtenerPorId(1);
Usuario usuario = UsuarioDAO.ObtenerPorCorreo("usuario@example.com");
```

### Actualizar un Usuario
```csharp
usuario.Clave = "nuevaPassword";
bool exitoso = UsuarioDAO.Actualizar(usuario);
```

### Eliminar un Usuario
```csharp
bool exitoso = UsuarioDAO.Eliminar(1);
```

### Obtener Todos los Usuarios
```csharp
List<Usuario> usuarios = UsuarioDAO.ObtenerTodos();
```

## Notas Importantes

1. **Seguridad**: Las contraseñas en producción debe estar hasheadas (SHA256, bcrypt, etc.)
2. **Validación**: Implementa validación de datos en el controlador
3. **Manejo de Errores**: El UsuarioDAO incluye try-catch para manejar excepciones
4. **Índices**: La tabla tiene un índice en el campo Correo para mejorar las búsquedas

## Estructura de la Base de Datos

```sql
Table: Usuarios
├── IdUsuario (INT, PRIMARY KEY, IDENTITY)
├── Correo (NVARCHAR(150), UNIQUE)
├── Clave (NVARCHAR(200))
└── FechaCreacion (DATETIME, DEFAULT=GETDATE())
```

## Soporte Técnico
Si encuentras problemas:
1. Verifica que la cadena de conexión sea correcta
2. Asegúrate de que la base de datos y tabla existen
3. Revisa los logs de error en el archivo de eventos del servidor
4. Verifica permisos de acceso a SQL Server

---
_Implementación completada exitosamente_
