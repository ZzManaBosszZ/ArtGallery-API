﻿// <auto-generated />
using System;
using ArtGallery.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ArtGallery.Migrations
{
    [DbContext(typeof(ArtGalleryApiContext))]
<<<<<<<< HEAD:ArtGallery/ArtGallery/Migrations/20240514091823_InitialCreate.Designer.cs
    [Migration("20240514091823_InitialCreate")]
    partial class InitialCreate
========
    [Migration("20240517042053_aa")]
    partial class aa
>>>>>>>> main:ArtGallery/ArtGallery/Migrations/20240517042053_aa.Designer.cs
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ArtGallery.Entities.ArtWork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ArtWorkImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CertificateOfAuthenticity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Condition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("FavoriteCount")
                        .HasColumnType("int");

                    b.Property<string>("Frame")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Materials")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Medium")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Rarity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Series")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Signature")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ArtWork");
                });

            modelBuilder.Entity("ArtGallery.Entities.ArtWorkSchoolOfArt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArtWorkId")
                        .HasColumnType("int");

                    b.Property<int>("SchoolOfArtId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArtWorkId");

                    b.HasIndex("SchoolOfArtId");

                    b.ToTable("ArtWorkSchoolOfArt");
                });

            modelBuilder.Entity("ArtGallery.Entities.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Biography")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FollowCount")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Artist");
                });

            modelBuilder.Entity("ArtGallery.Entities.ArtistArtWork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArtWorkId")
                        .HasColumnType("int");

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArtWorkId");

                    b.HasIndex("ArtistId");

                    b.ToTable("ArtistArtWork");
                });

            modelBuilder.Entity("ArtGallery.Entities.ArtistRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Biography")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameArtist")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusRequest")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ArtistRequests");
                });

            modelBuilder.Entity("ArtGallery.Entities.ArtistSchoolOfArt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<int>("SchoolOfArtId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("SchoolOfArtId");

                    b.ToTable("ArtistSchoolOfArt");
                });

            modelBuilder.Entity("ArtGallery.Entities.Favorite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArtWorkId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArtWorkId");

                    b.HasIndex("UserId");

                    b.ToTable("Favorite");
                });

            modelBuilder.Entity("ArtGallery.Entities.Follow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("UserId");

                    b.ToTable("Follow");
                });

            modelBuilder.Entity("ArtGallery.Entities.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ArtWorkId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("IsPaid")
                        .HasColumnType("int");

                    b.Property<string>("OfferCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("OfferPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArtWorkId");

                    b.HasIndex("UserId");

                    b.ToTable("Offer");
                });

            modelBuilder.Entity("ArtGallery.Entities.OfferArtWork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArtWorkId")
                        .HasColumnType("int");

                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ArtWorkId");

                    b.HasIndex("OfferId");

                    b.ToTable("OfferArtWork");
                });

            modelBuilder.Entity("ArtGallery.Entities.SchoolOfArt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("SchoolOfArt");
                });

            modelBuilder.Entity("ArtGallery.Entities.Specialists", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Biography")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Specialists");
                });

            modelBuilder.Entity("ArtGallery.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResetTokenExpiry")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ArtGallery.Entities.UserArtist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<int?>("OfferId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("OfferId");

                    b.HasIndex("UserId");

                    b.ToTable("UserArtist");
                });

            modelBuilder.Entity("ArtGallery.Entities.ArtWorkSchoolOfArt", b =>
                {
                    b.HasOne("ArtGallery.Entities.ArtWork", "ArtWork")
                        .WithMany("ArtWorkSchoolOfArts")
                        .HasForeignKey("ArtWorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArtGallery.Entities.SchoolOfArt", "SchoolOfArt")
                        .WithMany()
                        .HasForeignKey("SchoolOfArtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArtWork");

                    b.Navigation("SchoolOfArt");
                });

            modelBuilder.Entity("ArtGallery.Entities.ArtistArtWork", b =>
                {
                    b.HasOne("ArtGallery.Entities.ArtWork", "ArtWork")
                        .WithMany("ArtistArtWorks")
                        .HasForeignKey("ArtWorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArtGallery.Entities.Artist", "Artist")
                        .WithMany("ArtistArtWorks")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArtWork");

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("ArtGallery.Entities.ArtistRequest", b =>
                {
                    b.HasOne("ArtGallery.Entities.User", "User")
                        .WithMany("ArtistRequests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ArtGallery.Entities.ArtistSchoolOfArt", b =>
                {
                    b.HasOne("ArtGallery.Entities.Artist", "Artist")
                        .WithMany("ArtistSchoolOfArts")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArtGallery.Entities.SchoolOfArt", "SchoolOfArt")
                        .WithMany()
                        .HasForeignKey("SchoolOfArtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");

                    b.Navigation("SchoolOfArt");
                });

            modelBuilder.Entity("ArtGallery.Entities.Favorite", b =>
                {
                    b.HasOne("ArtGallery.Entities.ArtWork", "ArtWork")
                        .WithMany("Favorites")
                        .HasForeignKey("ArtWorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArtGallery.Entities.User", "User")
                        .WithMany("Favorites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArtWork");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ArtGallery.Entities.Follow", b =>
                {
                    b.HasOne("ArtGallery.Entities.Artist", "Artist")
                        .WithMany("Follow")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArtGallery.Entities.User", "User")
                        .WithMany("Follow")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ArtGallery.Entities.Offer", b =>
                {
                    b.HasOne("ArtGallery.Entities.ArtWork", null)
                        .WithMany("Offers")
                        .HasForeignKey("ArtWorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArtGallery.Entities.User", "User")
                        .WithMany("Offers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ArtGallery.Entities.OfferArtWork", b =>
                {
                    b.HasOne("ArtGallery.Entities.ArtWork", "ArtWork")
                        .WithMany("OfferArtWork")
                        .HasForeignKey("ArtWorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArtGallery.Entities.Offer", "Offer")
                        .WithMany("OfferArtWorks")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArtWork");

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("ArtGallery.Entities.UserArtist", b =>
                {
                    b.HasOne("ArtGallery.Entities.Artist", "Artist")
                        .WithMany("UserArtists")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArtGallery.Entities.Offer", null)
                        .WithMany("UserArtists")
                        .HasForeignKey("OfferId");

                    b.HasOne("ArtGallery.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ArtGallery.Entities.ArtWork", b =>
                {
                    b.Navigation("ArtWorkSchoolOfArts");

                    b.Navigation("ArtistArtWorks");

                    b.Navigation("Favorites");

                    b.Navigation("OfferArtWork");

                    b.Navigation("Offers");
                });

            modelBuilder.Entity("ArtGallery.Entities.Artist", b =>
                {
                    b.Navigation("ArtistArtWorks");

                    b.Navigation("ArtistSchoolOfArts");

                    b.Navigation("Follow");

                    b.Navigation("UserArtists");
                });

            modelBuilder.Entity("ArtGallery.Entities.Offer", b =>
                {
                    b.Navigation("OfferArtWorks");

                    b.Navigation("UserArtists");
                });

            modelBuilder.Entity("ArtGallery.Entities.User", b =>
                {
                    b.Navigation("ArtistRequests");

                    b.Navigation("Favorites");

                    b.Navigation("Follow");

                    b.Navigation("Offers");
                });
#pragma warning restore 612, 618
        }
    }
}
