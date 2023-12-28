using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Restaurant.Models
{
    public partial class RestaurantContext : DbContext
    {
        public RestaurantContext()
        {
        }

        public RestaurantContext(DbContextOptions<RestaurantContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dish> Dishes { get; set; } = null!;
        public virtual DbSet<DishCategory> DishCategories { get; set; } = null!;
        public virtual DbSet<DishIngredient> DishIngredients { get; set; } = null!;
        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>(entity =>
            {
                entity.Property(e => e.DishName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.DishCategoryNavigation)
                    .WithMany(p => p.Dishes)
                    .HasForeignKey(d => d.DishCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dishes_DishCategories");
            });

            modelBuilder.Entity<DishCategory>(entity =>
            {
                entity.Property(e => e.DishCategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DishIngredient>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.Ingredients)
                    .WithMany()
                    .HasForeignKey(d => d.IngredientsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DishIngredients_Ingredients");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.HasKey(e => e.IngredientsId)
                    .HasName("PK__Ingredie__EA22D1B0AA419CB5");

                entity.Property(e => e.IngredientsName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
