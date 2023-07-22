﻿// <auto-generated />
using Comunalka.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Comunalka.Migrations
{
    [DbContext(typeof(ComunalkaContext))]
    [Migration("20230722063913_CreateCountersTable")]
    partial class CreateCountersTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Comunalka.Models.CommunalResource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("Comunalka.Models.Counter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TariffId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TariffId");

                    b.ToTable("Counters");
                });

            modelBuilder.Entity("Comunalka.Models.Tariff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ResourceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ResourceId");

                    b.ToTable("Tariffs");
                });

            modelBuilder.Entity("Comunalka.Models.Counter", b =>
                {
                    b.HasOne("Comunalka.Models.Tariff", "Tariff")
                        .WithMany("Counters")
                        .HasForeignKey("TariffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tariff");
                });

            modelBuilder.Entity("Comunalka.Models.Tariff", b =>
                {
                    b.HasOne("Comunalka.Models.CommunalResource", "Resource")
                        .WithMany("Tariffs")
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Resource");
                });

            modelBuilder.Entity("Comunalka.Models.CommunalResource", b =>
                {
                    b.Navigation("Tariffs");
                });

            modelBuilder.Entity("Comunalka.Models.Tariff", b =>
                {
                    b.Navigation("Counters");
                });
#pragma warning restore 612, 618
        }
    }
}