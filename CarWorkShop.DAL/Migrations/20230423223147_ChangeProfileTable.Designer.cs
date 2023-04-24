﻿// <auto-generated />
using System;
using CarWorkShop.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarWorkShop.DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230423223147_ChangeProfileTable")]
    partial class ChangeProfileTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CarWorkShop.Models.Entity.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Avatar")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("CarNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mark")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RecordId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecordId")
                        .IsUnique()
                        .HasFilter("[RecordId] IS NOT NULL");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("CarWorkShop.Models.Entity.Owner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Owners", (string)null);
                });

            modelBuilder.Entity("CarWorkShop.Models.Entity.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<short>("Age")
                        .HasColumnType("smallint");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId")
                        .IsUnique();

                    b.ToTable("Profiles", (string)null);
                });

            modelBuilder.Entity("CarWorkShop.Models.Entity.Record", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("Complaint")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("Records");
                });

            modelBuilder.Entity("CarWorkShop.Models.Entity.Car", b =>
                {
                    b.HasOne("CarWorkShop.Models.Entity.Record", "Record")
                        .WithOne("Car")
                        .HasForeignKey("CarWorkShop.Models.Entity.Car", "RecordId");

                    b.Navigation("Record");
                });

            modelBuilder.Entity("CarWorkShop.Models.Entity.Profile", b =>
                {
                    b.HasOne("CarWorkShop.Models.Entity.Owner", "Owner")
                        .WithOne("Profile")
                        .HasForeignKey("CarWorkShop.Models.Entity.Profile", "OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("CarWorkShop.Models.Entity.Record", b =>
                {
                    b.HasOne("CarWorkShop.Models.Entity.Profile", "Profile")
                        .WithMany("Records")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("CarWorkShop.Models.Entity.Owner", b =>
                {
                    b.Navigation("Profile");
                });

            modelBuilder.Entity("CarWorkShop.Models.Entity.Profile", b =>
                {
                    b.Navigation("Records");
                });

            modelBuilder.Entity("CarWorkShop.Models.Entity.Record", b =>
                {
                    b.Navigation("Car");
                });
#pragma warning restore 612, 618
        }
    }
}
