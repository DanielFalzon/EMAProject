﻿// <auto-generated />
using EMAProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EMAProject.Migrations
{
    [DbContext(typeof(ClinicContext))]
    [Migration("20200622091526_ForeignKeys2")]
    partial class ForeignKeys2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("EMAProject.Models.WebView", b =>
                {
                    b.Property<int>("WebViewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ViewName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WebViewID");

                    b.ToTable("WebView");
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
#pragma warning restore 612, 618
        }
    }
}
