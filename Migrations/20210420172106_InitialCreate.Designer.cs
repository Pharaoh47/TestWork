﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Phonebook;

namespace PhoneBook.Migrations
{
    [DbContext(typeof(PhonebookContext))]
    [Migration("20210420172106_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("Phonebook.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Persons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthDay = new DateTime(2008, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "An first test name"
                        },
                        new
                        {
                            Id = 2,
                            BirthDay = new DateTime(2012, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "An second test name"
                        });
                });

            modelBuilder.Entity("Phonebook.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Number")
                        .HasColumnType("TEXT");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Phones");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Number = "+79999999999",
                            PersonId = 1
                        },
                        new
                        {
                            Id = 2,
                            Number = "+77777777777",
                            PersonId = 1
                        },
                        new
                        {
                            Id = 3,
                            Number = "+73333333333",
                            PersonId = 2
                        });
                });

            modelBuilder.Entity("Phonebook.Phone", b =>
                {
                    b.HasOne("Phonebook.Person", null)
                        .WithMany("Phones")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Phonebook.Person", b =>
                {
                    b.Navigation("Phones");
                });
#pragma warning restore 612, 618
        }
    }
}