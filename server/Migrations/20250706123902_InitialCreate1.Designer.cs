﻿// <auto-generated />
using System;
using JaMoveo.DATA;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JaMoveo.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250706123902_InitialCreate1")]
    partial class InitialCreate1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.31");

            modelBuilder.Entity("JaMoveo.Models.InstrumentItem", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Name");

                    b.ToTable("Instruments");
                });

            modelBuilder.Entity("JaMoveo.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("InstrumentName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.HasKey("Username");

                    b.HasIndex("InstrumentName");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("JaMoveo.Models.User", b =>
                {
                    b.HasOne("JaMoveo.Models.InstrumentItem", "Instrument")
                        .WithMany()
                        .HasForeignKey("InstrumentName");

                    b.Navigation("Instrument");
                });
#pragma warning restore 612, 618
        }
    }
}
