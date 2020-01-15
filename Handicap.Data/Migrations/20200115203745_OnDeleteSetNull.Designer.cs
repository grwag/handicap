﻿// <auto-generated />
using System;
using Handicap.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Handicap.Data.Migrations
{
    [DbContext(typeof(HandicapContext))]
    [Migration("20200115203745_OnDeleteSetNull")]
    partial class OnDeleteSetNull
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Handicap.Domain.Models.Game", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("MatchDayId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("PlayerOneId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("PlayerOnePoints")
                        .HasColumnType("int");

                    b.Property<int>("PlayerOneRequiredPoints")
                        .HasColumnType("int");

                    b.Property<string>("PlayerTwoId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("PlayerTwoPoints")
                        .HasColumnType("int");

                    b.Property<int>("PlayerTwoRequiredPoints")
                        .HasColumnType("int");

                    b.Property<string>("TenantId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MatchDayId");

                    b.HasIndex("PlayerOneId");

                    b.HasIndex("PlayerTwoId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Handicap.Domain.Models.HandicapConfiguration", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("TenantId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("UpdatePlayersImmediately")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("HandicapConfigurations");

                    b.HasData(
                        new
                        {
                            Id = "99",
                            TenantId = "",
                            UpdatePlayersImmediately = false
                        });
                });

            modelBuilder.Entity("Handicap.Domain.Models.MatchDay", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("TenantId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("MatchDays");
                });

            modelBuilder.Entity("Handicap.Domain.Models.MatchDayPlayer", b =>
                {
                    b.Property<string>("MatchDayId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("PlayerId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("MatchDayId", "PlayerId");

                    b.HasIndex("PlayerId");

                    b.ToTable("MatchDayPlayers");
                });

            modelBuilder.Entity("Handicap.Domain.Models.Player", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Handicap")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("TenantId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Players");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            FirstName = "alf",
                            Handicap = 65,
                            LastName = "ralf",
                            TenantId = "816ef7d5-4589-4408-b64c-87594e2075bb"
                        },
                        new
                        {
                            Id = "2",
                            FirstName = "hans",
                            Handicap = 35,
                            LastName = "maulwurf",
                            TenantId = "816ef7d5-4589-4408-b64c-87594e2075bb"
                        },
                        new
                        {
                            Id = "3",
                            FirstName = "karl",
                            Handicap = 30,
                            LastName = "klammer",
                            TenantId = "816ef7d5-4589-4408-b64c-87594e2075bb"
                        },
                        new
                        {
                            Id = "4",
                            FirstName = "bart",
                            Handicap = 55,
                            LastName = "simpson",
                            TenantId = "816ef7d5-4589-4408-b64c-87594e2075bb"
                        },
                        new
                        {
                            Id = "5",
                            FirstName = "nasen",
                            Handicap = 25,
                            LastName = "baer",
                            TenantId = ""
                        },
                        new
                        {
                            Id = "6",
                            FirstName = "eier",
                            Handicap = 5,
                            LastName = "kopf",
                            TenantId = ""
                        },
                        new
                        {
                            Id = "7",
                            FirstName = "rudi",
                            Handicap = 30,
                            LastName = "rakete",
                            TenantId = ""
                        },
                        new
                        {
                            Id = "8",
                            FirstName = "homer",
                            Handicap = 55,
                            LastName = "simpson",
                            TenantId = ""
                        });
                });

            modelBuilder.Entity("Handicap.Domain.Models.Game", b =>
                {
                    b.HasOne("Handicap.Domain.Models.MatchDay", null)
                        .WithMany("Games")
                        .HasForeignKey("MatchDayId");

                    b.HasOne("Handicap.Domain.Models.Player", "PlayerOne")
                        .WithMany()
                        .HasForeignKey("PlayerOneId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Handicap.Domain.Models.Player", "PlayerTwo")
                        .WithMany()
                        .HasForeignKey("PlayerTwoId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Handicap.Domain.Models.MatchDayPlayer", b =>
                {
                    b.HasOne("Handicap.Domain.Models.MatchDay", "MatchDay")
                        .WithMany("MatchDayPlayers")
                        .HasForeignKey("MatchDayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Handicap.Domain.Models.Player", "Player")
                        .WithMany("MatchDayPlayers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
