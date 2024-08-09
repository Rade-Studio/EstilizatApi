﻿// <auto-generated />
using System;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240809182623_add_all_scope_to_shops_modules")]
    partial class add_all_scope_to_shops_modules
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.DbEntities.ServiceCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateUTC")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("LastUpdateUTC")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("UsedCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ServiceCategories", (string)null);
                });

            modelBuilder.Entity("Models.DbEntities.Shop", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("AverageRating")
                        .HasPrecision(2, 1)
                        .HasColumnType("decimal(2,1)");

                    b.Property<string>("City")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Country")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("CreateUTC")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsVerified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("LastUpdateUTC")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("ProfileImage")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ServiceDescription")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("TotalReviews")
                        .HasColumnType("int");

                    b.Property<string>("Website")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Shops", (string)null);
                });

            modelBuilder.Entity("Models.DbEntities.ShopService", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateUTC")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("Discount")
                        .HasPrecision(2, 1)
                        .HasColumnType("decimal(2,1)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdateUTC")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<decimal>("Price")
                        .HasPrecision(2, 1)
                        .HasColumnType("decimal(2,1)");

                    b.Property<Guid>("ServiceCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ShopId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ServiceCategoryId");

                    b.HasIndex("ShopId");

                    b.ToTable("ShopServices", (string)null);
                });

            modelBuilder.Entity("Models.DbEntities.ShopSetting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateUTC")
                        .HasColumnType("datetime2");

                    b.Property<string>("Key")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("LastUpdateUTC")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ShopId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("ShopId");

                    b.ToTable("ShopSettings", (string)null);
                });

            modelBuilder.Entity("Models.DbEntities.Shop", b =>
                {
                    b.OwnsMany("Models.DbEntities.JsonEntities.GalleryShop", "Gallery", b1 =>
                        {
                            b1.Property<Guid>("ShopId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<string>("Description")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Image")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ShopId", "Id");

                            b1.ToTable("Shops");

                            b1.ToJson("Gallery");

                            b1.WithOwner()
                                .HasForeignKey("ShopId");
                        });

                    b.OwnsMany("Models.DbEntities.JsonEntities.OpeningHour", "OpeningHours", b1 =>
                        {
                            b1.Property<Guid>("ShopId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<string>("Close")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Day")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Open")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ShopId", "Id");

                            b1.ToTable("Shops");

                            b1.ToJson("OpeningHours");

                            b1.WithOwner()
                                .HasForeignKey("ShopId");
                        });

                    b.OwnsMany("Models.DbEntities.JsonEntities.SocialMedia", "SocialMedia", b1 =>
                        {
                            b1.Property<Guid>("ShopId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Url")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ShopId", "Id");

                            b1.ToTable("Shops");

                            b1.ToJson("SocialMedia");

                            b1.WithOwner()
                                .HasForeignKey("ShopId");
                        });

                    b.Navigation("Gallery");

                    b.Navigation("OpeningHours");

                    b.Navigation("SocialMedia");
                });

            modelBuilder.Entity("Models.DbEntities.ShopService", b =>
                {
                    b.HasOne("Models.DbEntities.ServiceCategory", "ServiceCategory")
                        .WithMany("ShopServices")
                        .HasForeignKey("ServiceCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.DbEntities.Shop", "Shop")
                        .WithMany("ShopServices")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("Models.DbEntities.JsonEntities.ShopService.IncludeProduct", "IncludeProducts", b1 =>
                        {
                            b1.Property<Guid>("ShopServiceId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<string>("Description")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Image")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Price")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Quantity")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Unit")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ShopServiceId", "Id");

                            b1.ToTable("ShopServices");

                            b1.ToJson("IncludeProducts");

                            b1.WithOwner()
                                .HasForeignKey("ShopServiceId");
                        });

                    b.OwnsMany("Models.DbEntities.JsonEntities.ShopService.MaterialNeeded", "Materials", b1 =>
                        {
                            b1.Property<Guid>("ShopServiceId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<string>("Description")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Quantity")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Unit")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ShopServiceId", "Id");

                            b1.ToTable("ShopServices");

                            b1.ToJson("Materials");

                            b1.WithOwner()
                                .HasForeignKey("ShopServiceId");
                        });

                    b.Navigation("IncludeProducts");

                    b.Navigation("Materials");

                    b.Navigation("ServiceCategory");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("Models.DbEntities.ShopSetting", b =>
                {
                    b.HasOne("Models.DbEntities.Shop", "Shop")
                        .WithMany("ShopSettings")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("Models.DbEntities.ServiceCategory", b =>
                {
                    b.Navigation("ShopServices");
                });

            modelBuilder.Entity("Models.DbEntities.Shop", b =>
                {
                    b.Navigation("ShopServices");

                    b.Navigation("ShopSettings");
                });
#pragma warning restore 612, 618
        }
    }
}
