
# ConvertidorTTS

Proyecto académico desarrollado en equipo para la construcción de una API relacionada con la generación de audio mediante tecnología Text-to-Speech (TTS).

## Tecnologías utilizadas

- C# 
- .NET 10 Web API
- PostgreSQL
- Entity Framework Core
- Docker
- Docker Compose
- Npgsql

## Requisitos previos

Para ejecutar el proyecto se necesita:

- Docker instalado.
- Docker Compose disponible.
- Git instalado.

## Estructura del proyecto

```text
ConvertidorTTS/
├── .gitignore
├── .env.example
├── README.md
├── constructor.yml
└── ConvertidorTTS/
    ├── Dockerfile
    ├── .dockerignore
    ├── ConvertidorTTS.csproj
    ├── Program.cs
    ├── appsettings.json
    ├── Controllers/
    ├── Models/
    └── ...
```

## Configuración del proyecto

### 1. Clonar el repositorio

```bash
git clone git@github.com:tjleonardozentenos-collab/tareaN4-tecnologia-web-I.git
```

### 2. Ingresar a la carpeta del proyecto

```bash
cd ConvertidorTTS
```

### 3. Crear el archivo de variables de entorno

Copiar el archivo de ejemplo:

```bash
cp .env.example .env
```

### 4. Configurar las variables de entorno

Abrir el archivo `.env` y configurar los valores correspondientes:

```env
POSTGRES_DB=sistema_db
POSTGRES_USER=admin
POSTGRES_PASSWORD=admin123
API_PORT=8080
```

### 5. Construir e iniciar los contenedores

Ejecutar desde la raíz del proyecto:

```bash
docker compose --env-file .env -f contructor.yml up --build
```

### 6. Detener los contenedores

```bash
docker compose -f contructor.yml down
```

Para detener los contenedores y eliminar también el volumen de la base de datos:

```bash
docker compose -f contructor.yml down -v
```

**Advertencia:** el último comando elimina los datos almacenados en el volumen de PostgreSQL.

## Modelo de datos

### Tabla: Usuario

Esta tabla almacena la información de los usuarios del sistema.

| Campo | Tipo de dato | Restricciones y características |
|---|---|---|
| Id | int | Llave primaria, autoincremental |
| Nombre | string | Obligatorio, máximo 100 caracteres |
| Correo | string | Obligatorio, formato de correo electrónico válido, máximo 150 caracteres |
| Estado | EstadoUsuario (enum) | Obligatorio, valor predeterminado: Activo |

### Enumeración: EstadoUsuario

Restringe los estados permitidos para un usuario:

- Activo
- Inactivo
- Suspendido

La enumeración utiliza `JsonStringEnumConverter` para representar los estados como texto en JSON, en lugar de valores numéricos.

## Integrantes y distribución del trabajo

| Integrante | Trabajo realizado |
|---|---|
| [Leonardo Zenteno] | Modelado e implementación de la entidad Usuario, definición de la enumeración EstadoUsuario, configuración de la llave primaria, generación automática del identificador y validaciones mediante Data Annotations. |


## Seguridad

- El archivo `.env` contiene las credenciales de configuración y no debe subirse al repositorio.
- El archivo `.env.example` proporciona valores de referencia.
- Las carpetas `bin/` y `obj/` están excluidas del control de versiones mediante `.gitignore`.
