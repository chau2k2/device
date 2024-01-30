﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using device.Data;

#nullable disable

namespace device.Migrations
{
    [DbContext(typeof(LaptopDbContext))]
    [Migration("20240130034450_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("device.Models.KhoHang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("GiaVon")
                        .HasColumnType("double precision");

                    b.Property<double>("Giaban")
                        .HasColumnType("double precision");

                    b.Property<int>("SoLuongBan")
                        .HasColumnType("integer");

                    b.Property<int>("SoLuongNhap")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("khoHangs");
                });

            modelBuilder.Entity("device.Models.Laptop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("LaptopDetail")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Producer")
                        .HasColumnType("integer");

                    b.Property<int>("laptopDetailId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Producer");

                    b.HasIndex("laptopDetailId");

                    b.ToTable("laptops");
                });

            modelBuilder.Entity("device.Models.LaptopDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BatteryCatttery")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Cpu")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HardDriver")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Height")
                        .HasColumnType("double precision");

                    b.Property<int>("IdKhoHang")
                        .HasColumnType("integer");

                    b.Property<int>("IdMonitor")
                        .HasColumnType("integer");

                    b.Property<int>("IdRam")
                        .HasColumnType("integer");

                    b.Property<int>("IdVga")
                        .HasColumnType("integer");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<int>("KhoHangId")
                        .HasColumnType("integer");

                    b.Property<double>("Length")
                        .HasColumnType("double precision");

                    b.Property<int>("MonitorId")
                        .HasColumnType("integer");

                    b.Property<int>("RamId")
                        .HasColumnType("integer");

                    b.Property<string>("Seri")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("VgaId")
                        .HasColumnType("integer");

                    b.Property<string>("Webcam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision");

                    b.Property<double>("Width")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("KhoHangId");

                    b.HasIndex("MonitorId");

                    b.HasIndex("RamId");

                    b.HasIndex("VgaId");

                    b.ToTable("laptopsDetail");
                });

            modelBuilder.Entity("device.Models.MonitorM", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("monitors");
                });

            modelBuilder.Entity("device.Models.Producer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("producers");
                });

            modelBuilder.Entity("device.Models.Ram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ram");
                });

            modelBuilder.Entity("device.Models.Vga", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("vgas");
                });

            modelBuilder.Entity("device.Models.Laptop", b =>
                {
                    b.HasOne("device.Models.Producer", "producers")
                        .WithMany("Laptops")
                        .HasForeignKey("Producer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("device.Models.LaptopDetail", "laptopDetail")
                        .WithMany("laptops")
                        .HasForeignKey("laptopDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("laptopDetail");

                    b.Navigation("producers");
                });

            modelBuilder.Entity("device.Models.LaptopDetail", b =>
                {
                    b.HasOne("device.Models.KhoHang", "KhoHang")
                        .WithMany("LaptopDetail")
                        .HasForeignKey("KhoHangId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("device.Models.MonitorM", "Monitor")
                        .WithMany("LaptopDetail")
                        .HasForeignKey("MonitorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("device.Models.Ram", "Ram")
                        .WithMany("LaptopDetail")
                        .HasForeignKey("RamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("device.Models.Vga", "Vga")
                        .WithMany("laptopDetail")
                        .HasForeignKey("VgaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KhoHang");

                    b.Navigation("Monitor");

                    b.Navigation("Ram");

                    b.Navigation("Vga");
                });

            modelBuilder.Entity("device.Models.KhoHang", b =>
                {
                    b.Navigation("LaptopDetail");
                });

            modelBuilder.Entity("device.Models.LaptopDetail", b =>
                {
                    b.Navigation("laptops");
                });

            modelBuilder.Entity("device.Models.MonitorM", b =>
                {
                    b.Navigation("LaptopDetail");
                });

            modelBuilder.Entity("device.Models.Producer", b =>
                {
                    b.Navigation("Laptops");
                });

            modelBuilder.Entity("device.Models.Ram", b =>
                {
                    b.Navigation("LaptopDetail");
                });

            modelBuilder.Entity("device.Models.Vga", b =>
                {
                    b.Navigation("laptopDetail");
                });
#pragma warning restore 612, 618
        }
    }
}
