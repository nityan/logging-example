﻿/*
 * Copyright 2016-2018 Mohawk College of Applied Arts and Technology
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you 
 * may not use this file except in compliance with the License. You may 
 * obtain a copy of the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations under 
 * the License.
 * 
 * User: nitya
 * Date: 2018-11-5
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoggingExample.Data.Migrations
{
	[DbContext(typeof(ApplicationDbContext))]
	partial class ApplicationDbContextModelSnapshot : ModelSnapshot
	{
		protected override void BuildModel(ModelBuilder modelBuilder)
		{
			modelBuilder
				.HasAnnotation("ProductVersion", "1.0.0-rc3")
				.HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

			modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
				{
					b.Property<string>("Id");

					b.Property<string>("ConcurrencyStamp")
						.IsConcurrencyToken();

					b.Property<string>("Name")
						.HasAnnotation("MaxLength", 256);

					b.Property<string>("NormalizedName")
						.HasAnnotation("MaxLength", 256);

					b.HasKey("Id");

					b.HasIndex("NormalizedName")
						.HasName("RoleNameIndex");

					b.ToTable("AspNetRoles");
				});

			modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd();

					b.Property<string>("ClaimType");

					b.Property<string>("ClaimValue");

					b.Property<string>("RoleId")
						.IsRequired();

					b.HasKey("Id");

					b.HasIndex("RoleId");

					b.ToTable("AspNetRoleClaims");
				});

			modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd();

					b.Property<string>("ClaimType");

					b.Property<string>("ClaimValue");

					b.Property<string>("UserId")
						.IsRequired();

					b.HasKey("Id");

					b.HasIndex("UserId");

					b.ToTable("AspNetUserClaims");
				});

			modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
				{
					b.Property<string>("LoginProvider");

					b.Property<string>("ProviderKey");

					b.Property<string>("ProviderDisplayName");

					b.Property<string>("UserId")
						.IsRequired();

					b.HasKey("LoginProvider", "ProviderKey");

					b.HasIndex("UserId");

					b.ToTable("AspNetUserLogins");
				});

			modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
				{
					b.Property<string>("UserId");

					b.Property<string>("RoleId");

					b.HasKey("UserId", "RoleId");

					b.HasIndex("RoleId");

					b.HasIndex("UserId");

					b.ToTable("AspNetUserRoles");
				});

			modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
				{
					b.Property<string>("UserId");

					b.Property<string>("LoginProvider");

					b.Property<string>("Name");

					b.Property<string>("Value");

					b.HasKey("UserId", "LoginProvider", "Name");

					b.ToTable("AspNetUserTokens");
				});

			modelBuilder.Entity("LoggingExample.Models.ApplicationUser", b =>
				{
					b.Property<string>("Id");

					b.Property<int>("AccessFailedCount");

					b.Property<string>("ConcurrencyStamp")
						.IsConcurrencyToken();

					b.Property<string>("Email")
						.HasAnnotation("MaxLength", 256);

					b.Property<bool>("EmailConfirmed");

					b.Property<bool>("LockoutEnabled");

					b.Property<DateTimeOffset?>("LockoutEnd");

					b.Property<string>("NormalizedEmail")
						.HasAnnotation("MaxLength", 256);

					b.Property<string>("NormalizedUserName")
						.HasAnnotation("MaxLength", 256);

					b.Property<string>("PasswordHash");

					b.Property<string>("PhoneNumber");

					b.Property<bool>("PhoneNumberConfirmed");

					b.Property<string>("SecurityStamp");

					b.Property<bool>("TwoFactorEnabled");

					b.Property<string>("UserName")
						.HasAnnotation("MaxLength", 256);

					b.HasKey("Id");

					b.HasIndex("NormalizedEmail")
						.HasName("EmailIndex");

					b.HasIndex("NormalizedUserName")
						.IsUnique()
						.HasName("UserNameIndex");

					b.ToTable("AspNetUsers");
				});

			modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
				{
					b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
						.WithMany("Claims")
						.HasForeignKey("RoleId")
						.OnDelete(DeleteBehavior.Cascade);
				});

			modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
				{
					b.HasOne("LoggingExample.Models.ApplicationUser")
						.WithMany("Claims")
						.HasForeignKey("UserId")
						.OnDelete(DeleteBehavior.Cascade);
				});

			modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
				{
					b.HasOne("LoggingExample.Models.ApplicationUser")
						.WithMany("Logins")
						.HasForeignKey("UserId")
						.OnDelete(DeleteBehavior.Cascade);
				});

			modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
				{
					b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
						.WithMany("Users")
						.HasForeignKey("RoleId")
						.OnDelete(DeleteBehavior.Cascade);

					b.HasOne("LoggingExample.Models.ApplicationUser")
						.WithMany("Roles")
						.HasForeignKey("UserId")
						.OnDelete(DeleteBehavior.Cascade);
				});
		}
	}
}
