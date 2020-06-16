using EMAProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.InteropServices.ComTypes;

namespace EMAProject.Data
{
    public class ClinicContext : DbContext
    {
        public ClinicContext(DbContextOptions<ClinicContext> options) : base(options)
        {
        }

        public DbSet<GdprPolicy> GdprPolicies {get; set;}
        public DbSet<GdprPolicyWebView> GdprPolicyWebViews {get; set;}
        public DbSet<WebView> WebViews {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<GdprPolicyWebView>()
                .HasKey(c => new { c.GdprPolicyID, c.WebViewID });

            modelBuilder.Entity<GdprPolicy>().ToTable("GdprPolicy");
            modelBuilder.Entity<GdprPolicyWebView>().ToTable("GdprPolicyWebView");
            modelBuilder.Entity<WebView>().ToTable("WebView");
        }
    }
}
