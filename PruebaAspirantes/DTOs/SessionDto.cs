namespace PruebaAspirantes.DTOs
{
    public class SessionDto
    {
        public int IdSession { get; set; }
        public string? FechaIngreso { get; set; }
        public string? FechaCierre { get; set; }

        public int IdUsuario { get; set; }
        public string? Token { get; set; }
    }
}
