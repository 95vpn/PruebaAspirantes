namespace PruebaAspirantes.DTOs
{
    public class UsuarioUpdateDto
    {
        public int IdUsuario { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int IdPersona { get; set; }
    }
}
