﻿// <auto-generated />
using AsyncHotel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AsyncHotel.Migrations
{
    [DbContext(typeof(AsyncDbContext))]
    [Migration("20210127011524_test")]
    partial class test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AsyncHotel.Models.Amenities", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AmenityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Amenities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AmenityName = "Creepy old lady"
                        },
                        new
                        {
                            Id = 2,
                            AmenityName = "Stabbiness"
                        },
                        new
                        {
                            Id = 3,
                            AmenityName = "Mini bar"
                        });
                });

            modelBuilder.Entity("AsyncHotel.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hotels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Estes Park",
                            Country = "United States",
                            Name = "The Overlook",
                            Phone = "833-888-0237",
                            State = "Colorado",
                            StreetAddress = "333 Wonderview Avenue"
                        },
                        new
                        {
                            Id = 2,
                            City = "Death Valley",
                            Country = "United States",
                            Name = "Motel Murder",
                            Phone = "702-654-3693",
                            State = "Nevada",
                            StreetAddress = "Route 66"
                        },
                        new
                        {
                            Id = 3,
                            City = "Townsville",
                            Country = "Somewheres",
                            Name = "Fake Hotel",
                            Phone = "111-313-56743",
                            State = "Stateton",
                            StreetAddress = "123 Some Street"
                        });
                });

            modelBuilder.Entity("AsyncHotel.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Layout")
                        .HasColumnType("int");

                    b.Property<string>("RoomName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Layout = 1,
                            RoomName = "237"
                        },
                        new
                        {
                            Id = 2,
                            Layout = 0,
                            RoomName = "Bates Room"
                        },
                        new
                        {
                            Id = 3,
                            Layout = 2,
                            RoomName = "Grand Ole Room"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
