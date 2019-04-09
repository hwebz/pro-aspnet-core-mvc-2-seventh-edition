﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PartyInvitesVSCode.Models;

namespace PartyInvitesVSCode.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20190401080548_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity("PartyInvitesVSCode.Models.GuestResponse", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.Property<bool?>("WillAttend")
                        .IsRequired();

                    b.HasKey("id");

                    b.ToTable("Invites");
                });
#pragma warning restore 612, 618
        }
    }
}
