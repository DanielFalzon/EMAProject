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
            this.ChangeTracker.LazyLoadingEnabled = false;
            
        }

        public DbSet<GdprPolicy> GdprPolicies {get; set;}
        public DbSet<GdprPolicyWebView> GdprPolicyWebViews {get; set;}
        public DbSet<WebView> WebViews {get; set;}
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientHealthCareProvider> ClientHealthCareProviders { get; set; }
        public DbSet<ClientIntervention> ClientInterventions { get; set; }
        public DbSet<Intervention> Interventions { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionNote> SessionNotes { get; set; }
        public DbSet<HealthCareProvider> HealthCareProviders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            //Table constraints
            modelBuilder.Entity<GdprPolicyWebView>()
                .HasKey(c => new { c.GdprPolicyID, c.WebViewID });
            modelBuilder.Entity<ClientHealthCareProvider>()
                .HasKey(c => new { c.ClientID, c.HealthCareProviderID });
            modelBuilder.Entity<ClientIntervention>()
                .HasKey(c => new { c.ClientID, c.InterventionID });

            modelBuilder.Entity<Client>()
                .HasIndex(c => c.NiNumber).IsUnique();
            modelBuilder.Entity<Client>()
                .HasIndex(c => c.Email).IsUnique();
            modelBuilder.Entity<Session>()
                .Property(s => s.CancelledBy)
                .HasConversion<string>();

            modelBuilder.Entity<ClientHealthCareProvider>()
                .HasOne(chcp => chcp.Client)
                .WithMany(chcp => chcp.ClientHealthcareProviders);

            modelBuilder.Entity<ClientHealthCareProvider>()
                .HasOne(chcp => chcp.HealthCareProvider)
                .WithMany(chcp => chcp.ClientHealthCareProviders);


            modelBuilder.Entity<GdprPolicy>().ToTable("GdprPolicy");
            modelBuilder.Entity<GdprPolicyWebView>().ToTable("GdprPolicyWebView");
            modelBuilder.Entity<WebView>().ToTable("WebView");

            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<ClientHealthCareProvider>().ToTable("ClientHealthCareProvider");
            modelBuilder.Entity<ClientIntervention>().ToTable("ClientIntervention");
            modelBuilder.Entity<Intervention>().ToTable("Intervention");
            modelBuilder.Entity<Session>().ToTable("Session");
            modelBuilder.Entity<SessionNote>().ToTable("SessionNote");
            modelBuilder.Entity<HealthCareProvider>().ToTable("HealthCareProvider");
        }
    }
}
