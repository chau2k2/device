﻿// <auto-generated />
using System;
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
    [Migration("20240319072834_05")]
    partial class _05
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("device.Entity.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateInvoice")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("invoices");
                });

            modelBuilder.Entity("device.Entity.InvoiceDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("InvoiceId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<int>("LaptopId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("LaptopId");

                    b.ToTable("InvoicesDetail");
                });

            modelBuilder.Entity("device.Entity.Laptop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("CostPrice")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("ProducerId")
                        .HasColumnType("integer");

                    b.Property<decimal>("SoldPrice")
                        .HasColumnType("numeric");

                    b.Property<int>("inventory")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProducerId");

                    b.ToTable("laptops");
                });

            modelBuilder.Entity("device.Entity.LaptopDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("BatteryCapacity")
                        .HasColumnType("numeric");

                    b.Property<string>("Cpu")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("HardDriver")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Height")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<int>("LaptopId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Length")
                        .HasColumnType("numeric");

                    b.Property<int>("MonitorId")
                        .HasColumnType("integer");

                    b.Property<int>("RamId")
                        .HasColumnType("integer");

                    b.Property<string>("Seri")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("VgaId")
                        .HasColumnType("integer");

                    b.Property<string>("Webcam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Weight")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Width")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("LaptopId");

                    b.HasIndex("MonitorId");

                    b.HasIndex("RamId");

                    b.HasIndex("VgaId");

                    b.ToTable("laptopsDetail");
                });

            modelBuilder.Entity("device.Entity.MonitorM", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("monitors");
                });

            modelBuilder.Entity("device.Entity.Producer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("producers");
                });

            modelBuilder.Entity("device.Entity.Ram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("ram");
                });

            modelBuilder.Entity("device.Entity.Storage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ImportNumber")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<int>("LaptopId")
                        .HasColumnType("integer");

                    b.Property<int>("SoldNumber")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LaptopId")
                        .IsUnique();

                    b.ToTable("storages");
                });

            modelBuilder.Entity("device.Entity.Vga", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("vgas");
                });

            modelBuilder.Entity("device.Entity.InvoiceDetail", b =>
                {
                    b.HasOne("device.Entity.Invoice", "invoices")
                        .WithMany("invoiceDetail")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("device.Entity.Laptop", "Laptop")
                        .WithMany("InvoiceDetails")
                        .HasForeignKey("LaptopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Laptop");

                    b.Navigation("invoices");
                });

            modelBuilder.Entity("device.Entity.Laptop", b =>
                {
                    b.HasOne("device.Entity.Producer", "Producer")
                        .WithMany("Laptops")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producer");
                });

            modelBuilder.Entity("device.Entity.LaptopDetail", b =>
                {
                    b.HasOne("device.Entity.Laptop", "Laptops")
                        .WithMany("LaptopDetail")
                        .HasForeignKey("LaptopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("device.Entity.MonitorM", "Monitor")
                        .WithMany("LaptopDetail")
                        .HasForeignKey("MonitorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("device.Entity.Ram", "Rams")
                        .WithMany("LaptopDetail")
                        .HasForeignKey("RamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("device.Entity.Vga", "Vga")
                        .WithMany("laptopDetail")
                        .HasForeignKey("VgaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Laptops");

                    b.Navigation("Monitor");

                    b.Navigation("Rams");

                    b.Navigation("Vga");
                });

            modelBuilder.Entity("device.Entity.Storage", b =>
                {
                    b.HasOne("device.Entity.Laptop", "Laptop")
                        .WithOne("Storage")
                        .HasForeignKey("device.Entity.Storage", "LaptopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Laptop");
                });

            modelBuilder.Entity("device.Entity.Invoice", b =>
                {
                    b.Navigation("invoiceDetail");
                });

            modelBuilder.Entity("device.Entity.Laptop", b =>
                {
                    b.Navigation("InvoiceDetails");

                    b.Navigation("LaptopDetail");

                    b.Navigation("Storage")
                        .IsRequired();
                });

            modelBuilder.Entity("device.Entity.MonitorM", b =>
                {
                    b.Navigation("LaptopDetail");
                });

            modelBuilder.Entity("device.Entity.Producer", b =>
                {
                    b.Navigation("Laptops");
                });

            modelBuilder.Entity("device.Entity.Ram", b =>
                {
                    b.Navigation("LaptopDetail");
                });

            modelBuilder.Entity("device.Entity.Vga", b =>
                {
                    b.Navigation("laptopDetail");
                });
#pragma warning restore 612, 618
        }
    }
}
