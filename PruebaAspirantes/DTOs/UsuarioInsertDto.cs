namespace PruebaAspirantes.DTOs
{
    public class UsuarioInsertDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? SessionActive { get; set; }
        public int IdPersona { get; set; }
    }
}
