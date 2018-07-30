﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Restarter.Data;

namespace Restarter.Migrations
{
    [DbContext(typeof(RestarterDbContext))]
    partial class RestarterDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Restarter.HealthMonitor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ExpectedContent");

                    b.Property<int>("ExpectedStatusCode");

                    b.Property<string>("Form");

                    b.Property<bool>("IsPostMethod");

                    b.Property<string>("MonitorName");

                    b.Property<string>("RequestPath");

                    b.HasKey("Id");

                    b.ToTable("MonitorTemplates");
                });

            modelBuilder.Entity("Restarter.Models.AuditLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Action");

                    b.Property<DateTime>("EventTime");

                    b.Property<string>("IPAddress");

                    b.Property<string>("Operator");

                    b.HasKey("Id");

                    b.ToTable("AuditLogs");
                });

            modelBuilder.Entity("Restarter.Models.Server", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Architect");

                    b.Property<string>("DiskSize");

                    b.Property<string>("InDomainName");

                    b.Property<string>("InnerIP");

                    b.Property<string>("Memory");

                    b.Property<int?>("MonitorId");

                    b.Property<string>("Name");

                    b.Property<string>("OS");

                    b.Property<string>("OutterIP");

                    b.Property<string>("Owner");

                    b.Property<string>("UsageA");

                    b.Property<string>("UsageB");

                    b.Property<string>("VCPU");

                    b.Property<string>("VMArchitect");

                    b.HasKey("Id");

                    b.HasIndex("MonitorId");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("Restarter.Models.Server", b =>
                {
                    b.HasOne("Restarter.HealthMonitor", "Monitor")
                        .WithMany("Servers")
                        .HasForeignKey("MonitorId");
                });
#pragma warning restore 612, 618
        }
    }
}
