﻿// <auto-generated />
using System;
using DataAccess.Context.IdentityContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Context.IdentityContext.Migrations
{
    [DbContext(typeof(AppIdentityDbContext))]
    [Migration("20240725093420_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ApplicationCore.Entities.UserEntites.Concrete.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("d5c4d023-7327-4228-8a62-285b1bae7bf8"),
                            CreatedDate = new DateTime(2024, 7, 25, 12, 34, 19, 116, DateTimeKind.Local).AddTicks(548),
                            Name = "admin",
                            NormalizedName = "ADMIN",
                            Status = 1
                        },
                        new
                        {
                            Id = new Guid("68e427b9-851a-497e-a8ea-eb1d77e90a6d"),
                            CreatedDate = new DateTime(2024, 7, 25, 12, 34, 19, 116, DateTimeKind.Local).AddTicks(607),
                            Name = "customerManager",
                            NormalizedName = "CUSTOMERMANAGER",
                            Status = 1
                        },
                        new
                        {
                            Id = new Guid("c260905a-c63e-4ccb-8246-46808ee55975"),
                            CreatedDate = new DateTime(2024, 7, 25, 12, 34, 19, 116, DateTimeKind.Local).AddTicks(610),
                            Name = "teacher",
                            NormalizedName = "TEACHER",
                            Status = 1
                        },
                        new
                        {
                            Id = new Guid("105a56ee-862b-4bdc-9dc2-6041128b0fb7"),
                            CreatedDate = new DateTime(2024, 7, 25, 12, 34, 19, 116, DateTimeKind.Local).AddTicks(612),
                            Name = "student",
                            NormalizedName = "STUDENT",
                            Status = 1
                        });
                });

            modelBuilder.Entity("ApplicationCore.Entities.UserEntites.Concrete.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("LoginCount")
                        .HasColumnType("integer");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("9242f3c1-d0a9-47bd-aab1-44228447ca81"),
                            AccessFailedCount = 0,
                            BirthDate = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "897e4312-1f0c-4fbb-a373-b0e2872c43c3",
                            CreatedDate = new DateTime(2024, 7, 25, 12, 34, 18, 695, DateTimeKind.Local).AddTicks(647),
                            Email = "admin@bilgeadam.com",
                            EmailConfirmed = false,
                            FirstName = "Yönetici",
                            LastName = "Admin",
                            LockoutEnabled = false,
                            LoginCount = 0,
                            NormalizedEmail = "ADMIN@BILGEADAM.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAIAAYagAAAAEJINekQpJXlsdh0XLuxZLMpG6KbrXHglH0mR4n06G/YN8M4l8evPEJPUqkEh3jnBlw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "a02352cf-ad06-4b2e-b08a-a841898f1b87",
                            Status = 1,
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        },
                        new
                        {
                            Id = new Guid("994ca4eb-a2ba-4fd6-9604-ab51c7852eb1"),
                            AccessFailedCount = 0,
                            BirthDate = new DateTime(1994, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "4ab88e6d-afbc-4b02-99f9-e8d3f9fc961c",
                            CreatedDate = new DateTime(2024, 7, 25, 12, 34, 18, 777, DateTimeKind.Local).AddTicks(1650),
                            Email = "pelin.ozerserdar@bilgeadamakademi.com",
                            EmailConfirmed = false,
                            FirstName = "Pelin",
                            LastName = "Özer Serdar",
                            LockoutEnabled = false,
                            LoginCount = 0,
                            NormalizedEmail = "PELIN.OZERSERDAR@BILGEADAMAKADEMI.COM",
                            NormalizedUserName = "PELIN.OZERSERDAR",
                            PasswordHash = "AQAAAAIAAYagAAAAEDrjgUJ8m9iz/nFbT7EI87NjPZ0XmRlrE4H+igAsUZHIw88D497f89KQ3dZC5KlsDQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "15867fd5-6fe7-4191-bbe8-3bdc3bc500a0",
                            Status = 1,
                            TwoFactorEnabled = false,
                            UserName = "pelin.ozerserdar"
                        },
                        new
                        {
                            Id = new Guid("8e2d2709-73cd-4447-b2a6-b4bee1ace17c"),
                            AccessFailedCount = 0,
                            BirthDate = new DateTime(1996, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "ed1b9220-cebb-4b9a-a1be-9006d670f4dc",
                            CreatedDate = new DateTime(2024, 7, 25, 12, 34, 18, 863, DateTimeKind.Local).AddTicks(4710),
                            Email = "snabkr7010@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Sina Emre",
                            LastName = "Bekar",
                            LockoutEnabled = false,
                            LoginCount = 0,
                            NormalizedEmail = "SNABKR7010@GMAIL.COM",
                            NormalizedUserName = "SINAEMRE.BEKAR",
                            PasswordHash = "AQAAAAIAAYagAAAAECUs/JoIs9sA+iF8vPCPW0vlhp/K2kdC2r0VIj4EMVzol3m81+RXk5tCOVuprcXabA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "3522a420-732e-411b-a02b-4ca9e1396a33",
                            Status = 1,
                            TwoFactorEnabled = false,
                            UserName = "sinaemre.bekar"
                        },
                        new
                        {
                            Id = new Guid("c79ef618-bff0-4721-b31d-edd5668ba1e3"),
                            AccessFailedCount = 0,
                            BirthDate = new DateTime(1993, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "41254720-ea4d-415d-a7b0-c1782d91b997",
                            CreatedDate = new DateTime(2024, 7, 25, 12, 34, 18, 947, DateTimeKind.Local).AddTicks(9901),
                            Email = "dicle.goya@bilgeadamakademi.com",
                            EmailConfirmed = false,
                            FirstName = "Dicle",
                            LastName = "Göya",
                            LockoutEnabled = false,
                            LoginCount = 0,
                            NormalizedEmail = "DICLE.GOYA@BILGEADAMAKADEMI.COM",
                            NormalizedUserName = "DICLE.GOYA",
                            PasswordHash = "AQAAAAIAAYagAAAAEKdcB+d/GYni2AiYmXoIzARO9A5XHaqcU3DNxgJAt0uFY8oEz1AMk2DKLQsWUnAD6Q==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "d5352d12-26ef-4ee8-a0cc-6c1203dbe575",
                            Status = 1,
                            TwoFactorEnabled = false,
                            UserName = "dicle.goya"
                        },
                        new
                        {
                            Id = new Guid("935965f5-f54e-46a4-b1fb-323b877a640b"),
                            AccessFailedCount = 0,
                            BirthDate = new DateTime(1997, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ConcurrencyStamp = "c38e6704-c9b2-4978-b0d3-f91f509a3277",
                            CreatedDate = new DateTime(2024, 7, 25, 12, 34, 19, 31, DateTimeKind.Local).AddTicks(2969),
                            Email = "aysenur.cihanger@bilgeadamakademi.com",
                            EmailConfirmed = false,
                            FirstName = "Ayşe Nur",
                            LastName = "Cihanger",
                            LockoutEnabled = false,
                            LoginCount = 0,
                            NormalizedEmail = "AYSENUR.CIHANGER@BILGEADAMAKADEMI.COM",
                            NormalizedUserName = "AYSENUR.CIHANGER",
                            PasswordHash = "AQAAAAIAAYagAAAAEFYgqjhQCaU4FkW7l8ngxdCCSK6bg/XL/xqOQdpT7QyAEPhdmAiODGTS32Au8409bw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e0a30830-735c-4387-86e2-d6b9eb220f2e",
                            Status = 1,
                            TwoFactorEnabled = false,
                            UserName = "aysenur.cihanger"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("9242f3c1-d0a9-47bd-aab1-44228447ca81"),
                            RoleId = new Guid("d5c4d023-7327-4228-8a62-285b1bae7bf8")
                        },
                        new
                        {
                            UserId = new Guid("994ca4eb-a2ba-4fd6-9604-ab51c7852eb1"),
                            RoleId = new Guid("68e427b9-851a-497e-a8ea-eb1d77e90a6d")
                        },
                        new
                        {
                            UserId = new Guid("8e2d2709-73cd-4447-b2a6-b4bee1ace17c"),
                            RoleId = new Guid("c260905a-c63e-4ccb-8246-46808ee55975")
                        },
                        new
                        {
                            UserId = new Guid("c79ef618-bff0-4721-b31d-edd5668ba1e3"),
                            RoleId = new Guid("105a56ee-862b-4bdc-9dc2-6041128b0fb7")
                        },
                        new
                        {
                            UserId = new Guid("935965f5-f54e-46a4-b1fb-323b877a640b"),
                            RoleId = new Guid("105a56ee-862b-4bdc-9dc2-6041128b0fb7")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("ApplicationCore.Entities.UserEntites.Concrete.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("ApplicationCore.Entities.UserEntites.Concrete.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("ApplicationCore.Entities.UserEntites.Concrete.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("ApplicationCore.Entities.UserEntites.Concrete.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationCore.Entities.UserEntites.Concrete.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("ApplicationCore.Entities.UserEntites.Concrete.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
