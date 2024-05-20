﻿// <auto-generated />
using System;
using Memorize.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Memorize.Server.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    partial class ApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Memorize.Shared.Models.Card", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("BackSide")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DayCounter")
                        .HasColumnType("int");

                    b.Property<int>("DeckID")
                        .HasColumnType("int");

                    b.Property<string>("FrontSide")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime?>("NextAppear")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("DeckID");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("Memorize.Shared.Models.Deck", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DontRememberFactor")
                        .HasColumnType("int");

                    b.Property<int>("EasyRememberFactor")
                        .HasColumnType("int");

                    b.Property<int?>("FolderID")
                        .HasColumnType("int");

                    b.Property<int>("GoodRememberFactor")
                        .HasColumnType("int");

                    b.Property<int>("HardRememberFactor")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("FolderID");

                    b.HasIndex("UserID");

                    b.ToTable("Decks");
                });

            modelBuilder.Entity("Memorize.Shared.Models.Folder", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Folders");
                });

            modelBuilder.Entity("Memorize.Shared.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Memorize.Shared.Models.Card", b =>
                {
                    b.HasOne("Memorize.Shared.Models.Deck", "Deck")
                        .WithMany("Cards")
                        .HasForeignKey("DeckID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deck");
                });

            modelBuilder.Entity("Memorize.Shared.Models.Deck", b =>
                {
                    b.HasOne("Memorize.Shared.Models.Folder", "Folder")
                        .WithMany("Decks")
                        .HasForeignKey("FolderID");

                    b.HasOne("Memorize.Shared.Models.User", "User")
                        .WithMany("Decks")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Folder");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Memorize.Shared.Models.Folder", b =>
                {
                    b.HasOne("Memorize.Shared.Models.User", "User")
                        .WithMany("Folders")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Memorize.Shared.Models.Deck", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("Memorize.Shared.Models.Folder", b =>
                {
                    b.Navigation("Decks");
                });

            modelBuilder.Entity("Memorize.Shared.Models.User", b =>
                {
                    b.Navigation("Decks");

                    b.Navigation("Folders");
                });
#pragma warning restore 612, 618
        }
    }
}