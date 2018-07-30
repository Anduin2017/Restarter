﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Restarter.Data;

namespace Restarter.Migrations
{
    [DbContext(typeof(RestarterDbContext))]
    [Migration("20180730054618_CreateMonitorTemplate")]
    partial class CreateMonitorTemplate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Name");

                    b.Property<string>("OS");

                    b.Property<string>("OutterIP");

                    b.Property<string>("Owner");

                    b.Property<string>("UsageA");

                    b.Property<string>("UsageB");

                    b.Property<string>("VCPU");

                    b.Property<string>("VMArchitect");

                    b.HasKey("Id");

                    b.ToTable("Servers");
                });
#pragma warning restore 612, 618
        }
    }
}
