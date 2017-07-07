using Microsoft.EntityFrameworkCore;
using SeriesApi.Model;
using System;
using JetBrains.Annotations;

namespace SeriesApi
{
    public class SeriesContext : DbContext
    {
        /// <summary>
        /// For now to get things working...
        /// </summary>
        public SeriesContext() : this(new DbContextOptionsBuilder<SeriesContext>().UseSqlite("File=data.db").Options)
        {
        }

        public SeriesContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Serie> Series { get; set; }

        public DbSet<SeriesApi.Model.Season> Season { get; set; }

        public DbSet<SeriesApi.Model.Episode> Episode { get; set; }
    }
}