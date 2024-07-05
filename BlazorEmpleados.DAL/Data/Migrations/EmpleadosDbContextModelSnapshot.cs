﻿// <auto-generated />
using System;
using BlazorEmpleados.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlazorEmpleados.DAL.Data.Migrations
{
    [DbContext(typeof(EmpleadosDbContext))]
    partial class EmpleadosDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BlazorEmpleados.Models.Departamento", b =>
                {
                    b.Property<int>("IdDepartamento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDepartamento"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdDepartamento");

                    b.ToTable("Departamentos");
                });

            modelBuilder.Entity("BlazorEmpleados.Models.Empleado", b =>
                {
                    b.Property<int>("IdEmpleado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEmpleado"));

                    b.Property<DateTime>("FechaContrato")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdDepartamento")
                        .HasColumnType("int");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NroDocumento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sueldo")
                        .HasColumnType("int");

                    b.HasKey("IdEmpleado");

                    b.HasIndex("IdDepartamento");

                    b.ToTable("Empleados");
                });

            modelBuilder.Entity("BlazorEmpleados.Models.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUser"));

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUser");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BlazorEmpleados.Models.Empleado", b =>
                {
                    b.HasOne("BlazorEmpleados.Models.Departamento", "IdDepartamentoNavigation")
                        .WithMany("Empleados")
                        .HasForeignKey("IdDepartamento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdDepartamentoNavigation");
                });

            modelBuilder.Entity("BlazorEmpleados.Models.Departamento", b =>
                {
                    b.Navigation("Empleados");
                });
#pragma warning restore 612, 618
        }
    }
}
