﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Restaurants.Infrastructure.DatabaseContext;

#nullable disable

namespace Restaurants.Infrastructure.Migrations
{
    [DbContext(typeof(RestaurantsDbContext))]
    [Migration("20240418233710_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("Restaurants.Core.Entities.Dish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("Restaurants.Core.Entities.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactEmail")
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("HasDelivery")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("Restaurants.Core.Entities.Dish", b =>
                {
                    b.HasOne("Restaurants.Core.Entities.Restaurant", null)
                        .WithMany("Dishes")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Restaurants.Core.Entities.Restaurant", b =>
                {
                    b.OwnsOne("Restaurants.Core.Entities.Address", "Address", b1 =>
                        {
                            b1.Property<int>("RestaurantId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("City")
                                .HasColumnType("TEXT");

                            b1.Property<string>("PostalCode")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Street")
                                .HasColumnType("TEXT");

                            b1.HasKey("RestaurantId");

                            b1.ToTable("Restaurants");

                            b1.WithOwner()
                                .HasForeignKey("RestaurantId");
                        });

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Restaurants.Core.Entities.Restaurant", b =>
                {
                    b.Navigation("Dishes");
                });
#pragma warning restore 612, 618
        }
    }
}
