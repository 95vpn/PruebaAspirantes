namespace PruebaAspirantes.DTOs
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? SessionActive { get; set; }

        public string? Status { get; set; }
        public int? IntentosFallidos { get; set; }
        public int IdPersona { get; set; }
    }
}
