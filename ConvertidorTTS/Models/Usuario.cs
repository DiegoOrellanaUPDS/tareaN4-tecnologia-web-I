using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConvertidorTTS.Models;

// 1. Enum para restringir estrictamente los valores permitidos de Estado
[JsonConverter(typeof(JsonStringEnumConverter))] // Permite enviar/recibir el texto ("Activo") en el JSON en lugar de números (0, 1, 2)
public enum EstadoUsuario
{
    Activo,
    Inactivo,
    Suspendido
}

public class Usuario
{
    // 2. Llave primaria y autoincremental en la base de datos
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    // 3. Campo obligatorio y con longitud máxima recomendada para BD
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [MaxLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    // 4. Campo obligatorio y validación de formato de correo electrónico
    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
    [MaxLength(150)]
    public string Correo { get; set; } = string.Empty;

    // 5. Estado restringido al enum, con "Activo" como valor por defecto
    [Required]
    public EstadoUsuario Estado { get; set; } = EstadoUsuario.Activo;
}