﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Evidencija.Database;

namespace EvidencijaWeb.Migrations
{
    [DbContext(typeof(EvidencijaDbContext))]
    [Migration("20160823134441_BaseMigration3")]
    partial class BaseMigration3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-alpha1-21937")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Evidencija.Database.Models.TimeStamp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Closed");

                    b.Property<TimeSpan>("Duration");

                    b.Property<DateTime>("End");

                    b.Property<DateTime>("Start");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Stamps");
                });

            modelBuilder.Entity("Evidencija.Database.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsAdmin");

                    b.Property<int>("LoginKey");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Evidencija.Database.Models.TimeStamp", b =>
                {
                    b.HasOne("Evidencija.Database.Models.User", "User")
                        .WithMany("UserTimeStamps")
                        .HasForeignKey("UserId");
                });
        }
    }
}
