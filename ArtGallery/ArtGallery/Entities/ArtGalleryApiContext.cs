using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ArtGallery.Entities;
namespace ArtGallery.Entities
{
    public partial class ArtGalleryApiContext : DbContext 
    {
        public ArtGalleryApiContext(DbContextOptions<ArtGalleryApiContext> options)
           : base(options)
        {
        }
        public DbSet<ArtGallery.Entities.Artist> Artist { get; set; } = default!;
        public DbSet<ArtGallery.Entities.ArtWork> ArtWork { get; set; } = default!;
        public DbSet<ArtGallery.Entities.ArtWorkMovement> ArtWorkMovement { get; set; } = default!;
        public DbSet<ArtGallery.Entities.Auction> Auction { get; set; } = default!;
        public DbSet<ArtGallery.Entities.Favorite> Favorite { get; set; } = default!;
        public DbSet<ArtGallery.Entities.Specialists> Specialists { get; set; } = default!;
        public DbSet<ArtGallery.Entities.User> Users { get; set; }

     
    }
}
