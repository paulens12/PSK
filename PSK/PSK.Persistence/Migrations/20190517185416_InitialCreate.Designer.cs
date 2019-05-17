﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PSK.Persistence;

namespace PSK.Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190517185416_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PSK.Domain.Accommodation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressId");

                    b.Property<string>("Name");

                    b.Property<int?>("OfficeId");

                    b.Property<int?>("TotalSpaces");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("OfficeId");

                    b.ToTable("Accommodations");
                });

            modelBuilder.Entity("PSK.Domain.AccommodationReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccommodationId");

                    b.Property<DateTime>("EndDate");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<int>("TripEmployeeId");

                    b.HasKey("Id");

                    b.HasIndex("AccommodationId");

                    b.HasIndex("TripEmployeeId")
                        .IsUnique();

                    b.ToTable("AccommodationReservations");
                });

            modelBuilder.Entity("PSK.Domain.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("HouseNumber");

                    b.Property<string>("Latitude");

                    b.Property<string>("Longitude");

                    b.Property<string>("Street");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("PSK.Domain.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<int>("Role");

                    b.Property<string>("Surname");

                    b.Property<string>("TelephoneNumber");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasFilter("[Username] IS NOT NULL");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("PSK.Domain.Office", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressId");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("PSK.Domain.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment");

                    b.Property<DateTime>("EndDate");

                    b.Property<int?>("EndLocationId");

                    b.Property<int?>("OrganizerId");

                    b.Property<DateTime>("StartDate");

                    b.Property<int?>("StartLocationId");

                    b.HasKey("Id");

                    b.HasIndex("EndLocationId");

                    b.HasIndex("OrganizerId");

                    b.HasIndex("StartLocationId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("PSK.Domain.TripEmployee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("CarReservationPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CarReservationStatus")
                        .IsRequired();

                    b.Property<string>("Comment");

                    b.Property<int?>("EmployeeId");

                    b.Property<decimal?>("PlaneTicketPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PlaneTicketStatus")
                        .IsRequired();

                    b.Property<int?>("TripId");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("TripId");

                    b.ToTable("TripEmployees");
                });

            modelBuilder.Entity("PSK.Domain.Accommodation", b =>
                {
                    b.HasOne("PSK.Domain.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("PSK.Domain.Office", "Office")
                        .WithMany("Accommodations")
                        .HasForeignKey("OfficeId");
                });

            modelBuilder.Entity("PSK.Domain.AccommodationReservation", b =>
                {
                    b.HasOne("PSK.Domain.Accommodation", "Accommodation")
                        .WithMany("Reservations")
                        .HasForeignKey("AccommodationId");

                    b.HasOne("PSK.Domain.TripEmployee", "TripEmployee")
                        .WithOne("AccommodationReservation")
                        .HasForeignKey("PSK.Domain.AccommodationReservation", "TripEmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PSK.Domain.Office", b =>
                {
                    b.HasOne("PSK.Domain.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");
                });

            modelBuilder.Entity("PSK.Domain.Trip", b =>
                {
                    b.HasOne("PSK.Domain.Office", "EndLocation")
                        .WithMany()
                        .HasForeignKey("EndLocationId");

                    b.HasOne("PSK.Domain.Employee", "Organizer")
                        .WithMany("OrganizedTrips")
                        .HasForeignKey("OrganizerId");

                    b.HasOne("PSK.Domain.Office", "StartLocation")
                        .WithMany()
                        .HasForeignKey("StartLocationId");
                });

            modelBuilder.Entity("PSK.Domain.TripEmployee", b =>
                {
                    b.HasOne("PSK.Domain.Employee", "Employee")
                        .WithMany("Trips")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("PSK.Domain.Trip", "Trip")
                        .WithMany("Employees")
                        .HasForeignKey("TripId");
                });
#pragma warning restore 612, 618
        }
    }
}