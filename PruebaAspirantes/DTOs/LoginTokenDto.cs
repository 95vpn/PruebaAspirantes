﻿using System.Text.Json.Serialization;

namespace PruebaAspirantes.DTOs
{
    public class LoginTokenDto
    {
        public int IdUsuario { get; set; }
        public string? Email { get; set; }

        public string? Token { get; set; }

        public string? RolName { get; set; }
    }
}
