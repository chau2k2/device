﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using device.Data;

#nullable disable

namespace device.Migrations
{
    [DbContext(typeof(LaptopDbContext))]
    partial class LaptopDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("SoLuongBan")
                        .HasColumnType("integer");

                    b.Property<int>("SoLuongNhap")
                        .HasColumnType("integer");

                    b.Property<int>("idDetail")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("idDetail")
                        .IsUnique();

                    b.ToTable("khoHangs");
                });

            modelBuilder.Entity("device.Models.Laptop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("GiaVon")
                        .HasColumnType("double precision");

                    b.Property<double>("Giaban")
                        .HasColumnType("double precision");

                    b.Property<int>("IdProducer")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IdProducer");

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

                    b.Property<int>("IdMonitor")
                        .HasColumnType("integer");

                    b.Property<int>("IdRam")
                        .HasColumnType("integer");

                    b.Property<int>("IdVga")
                        .HasColumnType("integer");

                    b.Property<byte[]>("Image")
                        .HasColumnType("bytea");

                    b.Property<double>("Length")
                        .HasColumnType("double precision");

                    b.Property<string>("Seri")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Webcam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision");

                    b.Property<double>("Width")
                        .HasColumnType("double precision");

                    b.Property<int>("idLaptop")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("IdMonitor");

                    b.HasIndex("IdRam");

                    b.HasIndex("IdVga");

                    b.HasIndex("idLaptop");

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

            modelBuilder.Entity("device.Models.KhoHang", b =>
                {
                    b.HasOne("device.Models.LaptopDetail", "laptopDetail")
                        .WithOne("khoHang")
                        .HasForeignKey("device.Models.KhoHang", "idDetail")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("laptopDetail");
                });

            modelBuilder.Entity("device.Models.Laptop", b =>
                {
                    b.HasOne("device.Models.Producer", "producer")
                        .WithMany("Laptops")
                        .HasForeignKey("IdProducer")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("producer");
                });

            modelBuilder.Entity("device.Models.LaptopDetail", b =>
                {
                    b.HasOne("device.Models.MonitorM", "Monitor")
                        .WithMany("LaptopDetail")
                        .HasForeignKey("IdMonitor")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("device.Models.Ram", "Rams")
                        .WithMany("LaptopDetail")
                        .HasForeignKey("IdRam")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("device.Models.Vga", "Vga")
                        .WithMany("laptopDetail")
                        .HasForeignKey("IdVga")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("device.Models.Laptop", "Laptops")
                        .WithMany("LaptopDetails")
                        .HasForeignKey("idLaptop")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Laptops");

                    b.Navigation("Monitor");

                    b.Navigation("Rams");

                    b.Navigation("Vga");
                });

            modelBuilder.Entity("device.Models.Laptop", b =>
                {
                    b.Navigation("LaptopDetails");
                });

            modelBuilder.Entity("device.Models.LaptopDetail", b =>
                {
                    b.Navigation("khoHang");
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
