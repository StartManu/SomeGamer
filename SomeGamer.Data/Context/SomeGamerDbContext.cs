using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SomeGamer.Core.Entity;
using SomeGamer.Data.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SomeGamer.Data.Context
{
    public class SomeGamerDbContext : IdentityDbContext
    {
        public SomeGamerDbContext(DbContextOptions<SomeGamerDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Classe Login:
            modelBuilder.Entity<Login>(entity => entity.Property(e => e.Email)
            .IsRequired().HasMaxLength(255).IsUnicode(false));
            modelBuilder.Entity<Login>(entity => entity.Property(e => e.Password)
            .IsRequired().HasMaxLength(25)
            .IsUnicode(false)); 

            modelBuilder.Entity<Login>().HasKey(k => new { k.PersonId });
            modelBuilder.Entity<Login>().HasOne(l => l.Person).WithOne(p => p.Login)
                .HasForeignKey<Login>(k => k.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            //Classe Friends:
            modelBuilder.Entity<Friend>().HasKey(p => new { p.PersonId1, p.PersonId2 });

            modelBuilder.Entity<Friend>().HasOne(a => a.Person1).WithMany(am => am.Friends)
                .HasForeignKey(a => a.PersonId1)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friend>().HasOne(b => b.Person2).WithMany(f => f.FriendSolicited)
                .HasForeignKey(a => a.PersonId2)
                .OnDelete(DeleteBehavior.Restrict);

            //Classe Image:
            modelBuilder.Entity<Image>().HasKey(k => new { k.PostId, k.ProfileId });

            modelBuilder.Entity<Image>().HasOne(i => i.Post).WithOne(p => p.Image)
               .HasForeignKey<Image>(k => k.PostId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Image>().HasOne(p => p.Profile).WithOne(imagem => imagem.ImageProfile)
               .HasForeignKey<Image>(k => k.ProfileId)
               .OnDelete(DeleteBehavior.Restrict);

            //Classe Perfil com Person:
            modelBuilder.Entity<Profile>().HasKey(k => new { k.PersonId });
            modelBuilder.Entity<Profile>().HasOne(p => p.Person).WithOne(pessoa => pessoa.Profile)
                .HasForeignKey<Profile>(k => k.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            //Classe Like com Person, Posts e Perfils
            modelBuilder.Entity<Like>().HasKey(k => new { k.PersonId, k.PostId, k.ProfileId });

            modelBuilder.Entity<Like>().HasOne(l => l.Person).WithOne(p => p.Like)
                .HasForeignKey<Like>(k => k.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Like>().HasOne(p => p.Post).WithMany(po => po.Likes)
                .HasForeignKey(k => k.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Like>().HasOne(p => p.Profile).WithMany(po => po.Likes)
               .HasForeignKey(k => k.ProfileId)
               .OnDelete(DeleteBehavior.Restrict);

            //Classe Posts com Person:
            modelBuilder.Entity<Post>().HasKey(k => new { k.PersonId });

            modelBuilder.Entity<Post>().HasOne(p => p.Person).WithMany(pe => pe.Posts)
                .HasForeignKey(k => k.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            //Classe comentarios com Person, Posts e Profile:
            modelBuilder.Entity<Comment>().HasKey(k => new { k.PersonId, k.ProfileId, k.PostId });

            modelBuilder.Entity<Comment>().HasOne(c => c.Person).WithOne(p => p.Comment)
                .HasForeignKey<Comment>(k => k.PersonId)

                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Comment>().HasOne(p => p.Post).WithMany(c => c.Comments)
                .HasForeignKey(k => k.PostId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Comment>().HasOne(f => f.Profile).WithMany(c => c.Comments)
                .HasForeignKey(k => k.ProfileId)
                .OnDelete(DeleteBehavior.Restrict);

        }
        public DbSet<User> Usuarios { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Profile> Profiles { get; set; }
    }
}
