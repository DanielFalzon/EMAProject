﻿// <auto-generated />
using System;
using EMAProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EMAProject.Migrations
{
    [DbContext(typeof(ClinicContext))]
    [Migration("20200622121822_ForeignKeyFixes")]
    partial class ForeignKeyFixes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EMAProject.Models.Client", b =>
                {
                    b.Property<int>("ClientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressLine1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Medications")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NiNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ReferredBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoContactNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoFirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoRelationship")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Subscriber")
                        .HasColumnType("bit");

                    b.HasKey("ClientID");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("NiNumber")
                        .IsUnique();

                    b.ToTable("Client");
                });

            modelBuilder.Entity("EMAProject.Models.ClientHealthCareProvider", b =>
                {
                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<int>("HealthCareProviderID")
                        .HasColumnType("int");

                    b.HasKey("ClientID", "HealthCareProviderID");

                    b.HasIndex("HealthCareProviderID");

                    b.ToTable("ClientHealthCareProvider");
                });

            modelBuilder.Entity("EMAProject.Models.ClientIntervention", b =>
                {
                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<int>("InterventionID")
                        .HasColumnType("int");

                    b.HasKey("ClientID", "InterventionID");

                    b.HasIndex("InterventionID");

                    b.ToTable("ClientIntervention");
                });

            modelBuilder.Entity("EMAProject.Models.GdprPolicy", b =>
                {
                    b.Property<int>("GdprPolicyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GdprPolicyID");

                    b.ToTable("GdprPolicy");
                });

            modelBuilder.Entity("EMAProject.Models.GdprPolicyWebView", b =>
                {
                    b.Property<int>("GdprPolicyID")
                        .HasColumnType("int");

                    b.Property<int>("WebViewID")
                        .HasColumnType("int");

                    b.HasKey("GdprPolicyID", "WebViewID");

                    b.HasIndex("WebViewID");

                    b.ToTable("GdprPolicyWebView");
                });

            modelBuilder.Entity("EMAProject.Models.HealthCareProvider", b =>
                {
                    b.Property<int>("HealthCareProviderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HealthCareProviderID");

                    b.ToTable("HealthCareProvider");
                });

            modelBuilder.Entity("EMAProject.Models.Intervention", b =>
                {
                    b.Property<int>("InterventionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Antecedence")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Behaviours")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Consequence")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PostInterventionScore")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<decimal>("PreInterventionScore")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<string>("Treatment")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InterventionID");

                    b.ToTable("Intervention");
                });

            modelBuilder.Entity("EMAProject.Models.Session", b =>
                {
                    b.Property<int>("SessionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CancelledBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InterventionID")
                        .HasColumnType("int");

                    b.Property<bool>("IsAccompanied")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelivered")
                        .HasColumnType("bit");

                    b.Property<int?>("SessionNoteID")
                        .HasColumnType("int");

                    b.Property<DateTime>("SessionTime")
                        .HasColumnType("datetime2");

                    b.HasKey("SessionID");

                    b.HasIndex("InterventionID");

                    b.HasIndex("SessionNoteID");

                    b.ToTable("Session");
                });

            modelBuilder.Entity("EMAProject.Models.SessionNote", b =>
                {
                    b.Property<int>("SessionNoteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("NoteFile")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("SessionNoteID");

                    b.ToTable("SessionNote");
                });

            modelBuilder.Entity("EMAProject.Models.WebView", b =>
                {
                    b.Property<int>("WebViewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ViewName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WebViewID");

                    b.ToTable("WebView");
                });

            modelBuilder.Entity("EMAProject.Models.ClientHealthCareProvider", b =>
                {
                    b.HasOne("EMAProject.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EMAProject.Models.HealthCareProvider", "HealthCareProvider")
                        .WithMany("ClientHealthCareProviders")
                        .HasForeignKey("HealthCareProviderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EMAProject.Models.ClientIntervention", b =>
                {
                    b.HasOne("EMAProject.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EMAProject.Models.Intervention", "Intervention")
                        .WithMany("ClientInterventions")
                        .HasForeignKey("InterventionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EMAProject.Models.GdprPolicyWebView", b =>
                {
                    b.HasOne("EMAProject.Models.GdprPolicy", "GdprPolicy")
                        .WithMany("GdprPolicyWebViews")
                        .HasForeignKey("GdprPolicyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EMAProject.Models.WebView", "WebView")
                        .WithMany("GdprPolicyWebViews")
                        .HasForeignKey("WebViewID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EMAProject.Models.Session", b =>
                {
                    b.HasOne("EMAProject.Models.Intervention", "Intervention")
                        .WithMany("Sessions")
                        .HasForeignKey("InterventionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EMAProject.Models.SessionNote", "SessionNote")
                        .WithMany()
                        .HasForeignKey("SessionNoteID");
                });
#pragma warning restore 612, 618
        }
    }
}
