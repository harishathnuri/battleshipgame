﻿// <auto-generated />
using Battle.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Battle.Repository.Migrations
{
    [DbContext(typeof(BattleAppContext))]
    partial class BattleAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Battle.Domain.Attack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BlockId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BlockId");

                    b.ToTable("Attacks");
                });

            modelBuilder.Entity("Battle.Domain.BattleShip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BoardId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.ToTable("BattleShips");
                });

            modelBuilder.Entity("Battle.Domain.BattleShipBlock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BattleShipId")
                        .HasColumnType("int");

                    b.Property<int>("BlockId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BattleShipId");

                    b.HasIndex("BlockId")
                        .IsUnique();

                    b.ToTable("BattleShipBlocks");
                });

            modelBuilder.Entity("Battle.Domain.Block", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BoardId")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.ToTable("Blocks");
                });

            modelBuilder.Entity("Battle.Domain.Board", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("Battle.Domain.Attack", b =>
                {
                    b.HasOne("Battle.Domain.Block", "Block")
                        .WithMany()
                        .HasForeignKey("BlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Battle.Domain.BattleShip", b =>
                {
                    b.HasOne("Battle.Domain.Board", "Board")
                        .WithMany("BattleShips")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Battle.Domain.BattleShipBlock", b =>
                {
                    b.HasOne("Battle.Domain.BattleShip", null)
                        .WithMany("BattleShipBlocks")
                        .HasForeignKey("BattleShipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Battle.Domain.Block", "Block")
                        .WithOne("BattleShipBlock")
                        .HasForeignKey("Battle.Domain.BattleShipBlock", "BlockId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Battle.Domain.Block", b =>
                {
                    b.HasOne("Battle.Domain.Board", "Board")
                        .WithMany("Blocks")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
