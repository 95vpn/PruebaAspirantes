using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PruebaAspirantes.DTOs;
using PruebaAspirantes.Models;
using PruebaAspirantes.Repository;
using PruebaAspirantes.Services;
using PruebaAspirantes.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddKeyedScoped<ICommonService<PersonaDto, PersonaInsertDto, PersonaUpdateDto>, PersonaService>("personaService");
builder.Services.AddKeyedScoped<ICommonService<UsuarioDto, UsuarioInsertDto, UsuarioUpdateDto>, UsuarioService>("usuarioService");
builder.Services.AddKeyedScoped<ICommonService<RolDto, RolIsertDto, RolUpdateDto>, RolService>("rolService");
builder.Services.AddKeyedScoped<ICommonService<RolOptionDto, RolOptionInsertDto, RolOptionUpdateDto>, RolOptionService>("rolOptionService");
builder.Services.AddKeyedScoped<ICommonService<RolRolOpcionDto, RolRolOpcionInsertDto, RolRolOpcionUpdateDto>, RolRolOpcionService>("rolRolOpcionService");
builder.Services.AddKeyedScoped<ICommonService<RolUsuarioDto, RolUsuarioInsertDto, RolUsuarioUpdateDto>, RolUsuarioService>("rolUsuarioService");
builder.Services.AddKeyedScoped<ICommonService<SessionDto, SessionInsertDto, SessionUpdateDto>, SessionService>("sessionService");
builder.Services.AddScoped<ILoginService, LoginService>();

//repository
builder.Services.AddScoped<IRepository<Persona>, PersonaRepository>();
builder.Services.AddScoped<IRepository<Usuario>, UsuarioRepository>();
builder.Services.AddScoped<IRepository<Rol>, RolRepository>();
builder.Services.AddScoped<IRepository<RolOpcion>, RolOptionRepository>();
builder.Services.AddScoped<IRepository<RolRolOpcion>, RolRolOpcionRepository>();
builder.Services.AddScoped<IRolRolOpcionRepository, RolRolOpcionRepository>();
builder.Services.AddScoped<IRepository<RolUsuario>, RolUsuarioRepository>();
builder.Services.AddScoped<IRepository<Session>, SessionRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();

//entity framework
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

//validators
builder.Services.AddScoped<IValidator<PersonaInsertDto>, PersonaInsertValidator>();
builder.Services.AddScoped<IValidator<PersonaUpdateDto>, PersonaUpdateValidator>();
builder.Services.AddScoped<IValidator<UsuarioInsertDto>, UsuarioInsertValidator>();
builder.Services.AddScoped<IValidator<UsuarioUpdateDto>, UsuarioUpdateValidator>();

//jwt
var appSettingSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSetting>(appSettingSection);

var appSetting = appSettingSection.Get<AppSetting>();


var llave = Encoding.ASCII.GetBytes(appSetting.Secreto);

builder.Services.AddAuthentication(d =>
{
    d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(d =>
    {
        d.RequireHttpsMetadata = false;
        d.SaveToken = true;
        d.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(llave),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
