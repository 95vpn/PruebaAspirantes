namespace PruebaAspirantes.DTOs
{
    public class SessionUpdateDto
    {
        public int IdSession { get; set; }
        public string? FechaIngreso { get; set; }
        public string? FechaCierre { get; set; }

        public int IdUsuario { get; set; }
    }
}
