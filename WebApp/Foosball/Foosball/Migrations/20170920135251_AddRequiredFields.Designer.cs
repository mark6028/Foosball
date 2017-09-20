﻿// <auto-generated />
using Foosball.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Foosball.Migrations
{
    [DbContext(typeof(FoosballContext))]
    [Migration("20170920135251_AddRequiredFields")]
    partial class AddRequiredFields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Foosball.Models.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MatchId");

                    b.Property<int>("PlayerId");

                    b.Property<DateTime>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Goal");
                });

            modelBuilder.Entity("Foosball.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("State");

                    b.Property<int>("TeamBlackId");

                    b.Property<int>("TeamGreyId");

                    b.HasKey("Id");

                    b.HasIndex("TeamBlackId");

                    b.HasIndex("TeamGreyId");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("Foosball.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Tag");

                    b.HasKey("Id");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("Foosball.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PlayerOneId");

                    b.Property<int>("PlayerTwoId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerOneId");

                    b.HasIndex("PlayerTwoId");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("Foosball.Models.Goal", b =>
                {
                    b.HasOne("Foosball.Models.Match", "Match")
                        .WithMany("Goals")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Foosball.Models.Player", "Player")
                        .WithMany("Goals")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Foosball.Models.Match", b =>
                {
                    b.HasOne("Foosball.Models.Team", "TeamBlack")
                        .WithMany()
                        .HasForeignKey("TeamBlackId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Foosball.Models.Team", "TeamGrey")
                        .WithMany()
                        .HasForeignKey("TeamGreyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Foosball.Models.Team", b =>
                {
                    b.HasOne("Foosball.Models.Player", "PlayerOne")
                        .WithMany()
                        .HasForeignKey("PlayerOneId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Foosball.Models.Player", "PlayerTwo")
                        .WithMany()
                        .HasForeignKey("PlayerTwoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
