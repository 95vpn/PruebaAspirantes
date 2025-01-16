﻿using Microsoft.EntityFrameworkCore;

namespace PruebaAspirantes.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) 
            : base(options)
        { }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<RolUsuario> RolUsuarios { get; set; }

        public DbSet<RolOpcion> RolOpciones { get; set; }
        public DbSet<RolRolOpcion> RolRolOpciones { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolRolOpcion>()
                .HasKey(ro => ro.Id);

            modelBuilder.Entity<RolRolOpcion>()
                .HasOne(rro => rro.Rol)
                .WithMany(r => r.RolRolOpciones)
                .HasForeignKey(rro => rro.IdRol);

            modelBuilder.Entity<RolRolOpcion>()
                .HasOne(rro => rro.RolOpcion)
                .WithMany(ro => ro.RolRolOpciones)
                .HasForeignKey(rro => rro.IdOption);

            base.OnModelCreating(modelBuilder);
        }
    }
}
