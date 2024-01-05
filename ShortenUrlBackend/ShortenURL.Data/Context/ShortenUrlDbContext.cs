using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShortenURL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortenURL.Data.Context
{
    public class ShortenUrlDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ShortenUrlDbContext(IConfiguration configurataion)
        {
            _configuration = configurataion;
            _connectionString = _configuration.GetConnectionString("default");
        }

        /*public ShortenUrlDbContext(DbContextOptions<ShortenUrlDbContext> options) : base(options)
        {

        }*/

        
        public DbSet<OriginalUrl> OriginalUrl { get; set; }
        public DbSet<NewUrl> NewUrl { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // "ConnectionStrings" is defined in the appsetting.json
            optionsBuilder.UseMySQL(_connectionString);
        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            // "ConnectionStrings" is defined in the appsetting.json
            optionsBuilder.UseMySQL("server=localhost;port=3306;database=ShortenURL;user=root;password=1234");
        }*/

    }
}
