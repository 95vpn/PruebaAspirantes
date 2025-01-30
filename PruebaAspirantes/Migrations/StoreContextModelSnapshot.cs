﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PruebaAspirantes.Models;

#nullable disable

namespace PruebaAspirantes.Migrations
{
    [DbContext(typeof(StoreContext))]
    partial class StoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PruebaAspirantes.Models.Persona", b =>
                {
                    b.Property<int>("IdPersona")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPersona"));

                    b.Property<string>("FechaNacimiento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Identificacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastNames")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Names")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPersona");

                    b.ToTable("Personas");
                });

            modelBuilder.Entity("PruebaAspirantes.Models.Rol", b =>
                {
                    b.Property<int>("IdRol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRol"));

                    b.Property<string>("RolName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdRol");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("PruebaAspirantes.Models.RolOpcion", b =>
                {
                    b.Property<int>("IdOpcion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdOpcion"));

                    b.Property<string>("NameOption")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdOpcion");

                    b.ToTable("RolOpciones");
                });

            modelBuilder.Entity("PruebaAspirantes.Models.RolRolOpcion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdOption")
                        .HasColumnType("int");

                    b.Property<int>("IdRol")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdOption");

                    b.HasIndex("IdRol");

                    b.ToTable("RolRolOpciones");
                });

            modelBuilder.Entity("PruebaAspirantes.Models.RolUsuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdRol")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdRol");

                    b.HasIndex("IdUsuario");

                    b.ToTable("RolUsuarios");
                });

            modelBuilder.Entity("PruebaAspirantes.Models.Session", b =>
                {
                    b.Property<int>("IdSession")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSession"));

                    b.Property<string>("FechaCierre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FechaIngreso")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.HasKey("IdSession");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("PruebaAspirantes.Models.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUsuario"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdPersona")
                        .HasColumnType("int");

                    b.Property<int?>("IntentosFallidos")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SessionActive")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUsuario");

                    b.HasIndex("IdPersona");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("PruebaAspirantes.Models.RolRolOpcion", b =>
                {
                    b.HasOne("PruebaAspirantes.Models.RolOpcion", "RolOpcion")
                        .WithMany("RolRolOpciones")
                        .HasForeignKey("IdOption")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PruebaAspirantes.Models.Rol", "Rol")
                        .WithMany("RolRolOpciones")
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");

                    b.Navigation("RolOpcion");
                });

            modelBuilder.Entity("PruebaAspirantes.Models.RolUsuario", b =>
                {
                    b.HasOne("PruebaAspirantes.Models.Rol", "Rol")
                        .WithMany("RolUsuarios")
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PruebaAspirantes.Models.Usuario", "Usuario")
                        .WithMany("RolUsuarios")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("PruebaAspirantes.Models.Session", b =>
                {
                    b.HasOne("PruebaAspirantes.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("PruebaAspirantes.Models.Usuario", b =>
                {
                    b.HasOne("PruebaAspirantes.Models.Persona", "Persona")
                        .WithMany()
                        .HasForeignKey("IdPersona")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Persona");
                });

            modelBuilder.Entity("PruebaAspirantes.Models.Rol", b =>
                {
                    b.Navigation("RolRolOpciones");

                    b.Navigation("RolUsuarios");
                });

            modelBuilder.Entity("PruebaAspirantes.Models.RolOpcion", b =>
                {
                    b.Navigation("RolRolOpciones");
                });

            modelBuilder.Entity("PruebaAspirantes.Models.Usuario", b =>
                {
                    b.Navigation("RolUsuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
