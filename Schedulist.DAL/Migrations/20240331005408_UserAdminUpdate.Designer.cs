﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Schedulist.DAL;

#nullable disable

namespace Schedulist.DAL.Migrations
{
    [DbContext(typeof(SchedulistDbContext))]
    [Migration("20240331005408_UserAdminUpdate")]
    partial class UserAdminUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "c3e92f9e-e8e9-4fe3-b600-ed1b055d25aa",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "bc877f1b-1e44-492c-acce-ba01f7bcd77f",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Schedulist.DAL.Models.CalendarEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("CalendarEventDate")
                        .HasColumnType("date");

                    b.Property<string>("CalendarEventDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeOnly>("CalendarEventEndTime")
                        .HasColumnType("time");

                    b.Property<string>("CalendarEventName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeOnly>("CalendarEventStartTime")
                        .HasColumnType("time");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CalendarEvents");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CalendarEventDate = new DateOnly(2024, 1, 10),
                            CalendarEventDescription = "Ongoing maintenance tasks in the office",
                            CalendarEventEndTime = new TimeOnly(9, 30, 0),
                            CalendarEventName = "Maintenance Work",
                            CalendarEventStartTime = new TimeOnly(8, 30, 0),
                            UserId = "1"
                        },
                        new
                        {
                            Id = 2,
                            CalendarEventDate = new DateOnly(2024, 1, 11),
                            CalendarEventDescription = "Scheduled office cleaning day",
                            CalendarEventEndTime = new TimeOnly(10, 30, 0),
                            CalendarEventName = "Office Cleaning",
                            CalendarEventStartTime = new TimeOnly(9, 30, 0),
                            UserId = "2"
                        },
                        new
                        {
                            Id = 3,
                            CalendarEventDate = new DateOnly(2024, 1, 11),
                            CalendarEventDescription = "Time for a relaxed atmosphere!",
                            CalendarEventEndTime = new TimeOnly(11, 30, 0),
                            CalendarEventName = "Pottering",
                            CalendarEventStartTime = new TimeOnly(10, 30, 0),
                            UserId = "1"
                        },
                        new
                        {
                            Id = 4,
                            CalendarEventDate = new DateOnly(2024, 1, 11),
                            CalendarEventDescription = "Team project meeting to discuss progress, challenges, and plans for project execution",
                            CalendarEventEndTime = new TimeOnly(12, 30, 0),
                            CalendarEventName = "Project Meeting",
                            CalendarEventStartTime = new TimeOnly(11, 30, 0),
                            UserId = "3"
                        },
                        new
                        {
                            Id = 5,
                            CalendarEventDate = new DateOnly(2024, 1, 12),
                            CalendarEventDescription = "Strategic business meeting covering company development, market strategy, and key decisions",
                            CalendarEventEndTime = new TimeOnly(13, 30, 0),
                            CalendarEventName = "Business Meeting",
                            CalendarEventStartTime = new TimeOnly(12, 30, 0),
                            UserId = "2"
                        },
                        new
                        {
                            Id = 6,
                            CalendarEventDate = new DateOnly(2024, 1, 13),
                            CalendarEventDescription = "Educational workshop aimed at enhancing employee's skills",
                            CalendarEventEndTime = new TimeOnly(14, 30, 0),
                            CalendarEventName = "Training Workshop",
                            CalendarEventStartTime = new TimeOnly(13, 30, 0),
                            UserId = "1"
                        });
                });

            modelBuilder.Entity("Schedulist.DAL.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "IT"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Construction"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Human Resources"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Marketing"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Production"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Finance and Accounting"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Customer Service"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Administration"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Procurement"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Sales"
                        });
                });

            modelBuilder.Entity("Schedulist.DAL.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Positions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Software Developer"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Constructor"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Human Resources Manager"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Marketing Manager"
                        },
                        new
                        {
                            Id = 5,
                            Name = "CNC Operator"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Financial Controller"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Customer Service Supporter"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Administrative Assistant"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Procurement Specialist"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Sales Representative"
                        });
                });

            modelBuilder.Entity("Schedulist.DAL.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<int?>("PositionId")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("PositionId");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            AccessFailedCount = 50,
                            ConcurrencyStamp = "23a83bb9-86f9-4bfc-bda8-0e23601e4f51",
                            DepartmentId = 1,
                            Email = "kurstomasza@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            Name = "Tomasz",
                            NormalizedEmail = "KURSTOMASZA@GMAIL.COM",
                            NormalizedUserName = "KURSTOMASZA@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEA14Ca4Qxi1aLPhhX//QlyWzbRxLXzvnr+15a91K8Gm2wm4aJm8WmvYvHNbF5Uy/2Q==",
                            PhoneNumberConfirmed = false,
                            PositionId = 2,
                            SecurityStamp = "b047864c-3535-47b2-bb55-d8ba2ec4383c",
                            Surname = "Tomaszewicz",
                            TwoFactorEnabled = false,
                            UserName = "kurstomasza@gmail.com"
                        },
                        new
                        {
                            Id = "2",
                            AccessFailedCount = 50,
                            ConcurrencyStamp = "f408323a-6f7b-4c2d-97ac-8a0911ac231c",
                            DepartmentId = 2,
                            Email = "kursandrzeja@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            Name = "Andrzej",
                            NormalizedEmail = "KURSANDRZEJA@GMAIL.COM",
                            NormalizedUserName = "KURSANDRZEJA@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEA14Ca4Qxi1aLPhhX//QlyWzbRxLXzvnr+15a91K8Gm2wm4aJm8WmvYvHNbF5Uy/2Q==",
                            PhoneNumberConfirmed = false,
                            PositionId = 3,
                            SecurityStamp = "a55540bb-c4ac-4d78-bfbb-d88cb0bffb11",
                            Surname = "Andrzejewski",
                            TwoFactorEnabled = false,
                            UserName = "kursandrzeja@gmail.com"
                        });
                });

            modelBuilder.Entity("Schedulist.DAL.Models.WorkMode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WorkModes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Office"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Home office"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Sick leave"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Delegation"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Holiday"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Others"
                        });
                });

            modelBuilder.Entity("Schedulist.DAL.Models.WorkModeForUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("DateOfWorkMode")
                        .HasColumnType("date");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("WorkModeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WorkModeId");

                    b.ToTable("WorkModesToUsers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfWorkMode = new DateOnly(2024, 1, 10),
                            UserId = "1",
                            WorkModeId = 1
                        },
                        new
                        {
                            Id = 2,
                            DateOfWorkMode = new DateOnly(2024, 1, 10),
                            UserId = "2",
                            WorkModeId = 1
                        },
                        new
                        {
                            Id = 3,
                            DateOfWorkMode = new DateOnly(2024, 1, 11),
                            UserId = "3",
                            WorkModeId = 1
                        },
                        new
                        {
                            Id = 4,
                            DateOfWorkMode = new DateOnly(2024, 1, 12),
                            UserId = "1",
                            WorkModeId = 1
                        },
                        new
                        {
                            Id = 5,
                            DateOfWorkMode = new DateOnly(2024, 1, 13),
                            UserId = "2",
                            WorkModeId = 1
                        },
                        new
                        {
                            Id = 6,
                            DateOfWorkMode = new DateOnly(2024, 1, 14),
                            UserId = "2",
                            WorkModeId = 1
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Schedulist.DAL.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Schedulist.DAL.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Schedulist.DAL.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Schedulist.DAL.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Schedulist.DAL.Models.CalendarEvent", b =>
                {
                    b.HasOne("Schedulist.DAL.Models.User", "User")
                        .WithMany("CalendarEvents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Schedulist.DAL.Models.User", b =>
                {
                    b.HasOne("Schedulist.DAL.Models.Department", "Department")
                        .WithMany("User")
                        .HasForeignKey("DepartmentId");

                    b.HasOne("Schedulist.DAL.Models.Position", "Position")
                        .WithMany("User")
                        .HasForeignKey("PositionId");

                    b.Navigation("Department");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("Schedulist.DAL.Models.WorkModeForUser", b =>
                {
                    b.HasOne("Schedulist.DAL.Models.User", "User")
                        .WithMany("WorkModesForUser")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Schedulist.DAL.Models.WorkMode", "WorkMode")
                        .WithMany()
                        .HasForeignKey("WorkModeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("WorkMode");
                });

            modelBuilder.Entity("Schedulist.DAL.Models.Department", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("Schedulist.DAL.Models.Position", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("Schedulist.DAL.Models.User", b =>
                {
                    b.Navigation("CalendarEvents");

                    b.Navigation("WorkModesForUser");
                });
#pragma warning restore 612, 618
        }
    }
}
