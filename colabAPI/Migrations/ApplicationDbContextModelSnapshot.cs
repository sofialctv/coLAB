﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using colabAPI.Data;

#nullable disable

namespace colabAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ProjetoBolsista", b =>
                {
                    b.Property<int>("BolsistaId")
                        .HasColumnType("integer");

                    b.Property<int>("ProjetoId")
                        .HasColumnType("integer");

                    b.HasKey("BolsistaId", "ProjetoId");

                    b.HasIndex("ProjetoId");

                    b.ToTable("ProjetoBolsista");
                });

            modelBuilder.Entity("colabAPI.Business.Models.Entities.Bolsa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DataFim")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataPrevistaFim")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PesquisadorId")
                        .HasColumnType("integer");

                    b.Property<double>("Valor")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("PesquisadorId")
                        .IsUnique();

                    b.ToTable("Bolsas");
                });

            modelBuilder.Entity("colabAPI.Business.Models.Entities.Financiador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Financiadores");
                });

            modelBuilder.Entity("colabAPI.Business.Models.Entities.Pesquisador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int[]>("Times")
                        .HasColumnType("integer[]");

                    b.HasKey("Id");

                    b.ToTable("Pesquisadores");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("colabAPI.Business.Models.Entities.Projeto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Categoria")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DataFim")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataPrevistaFim")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FinanciadorId")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Orcamento")
                        .HasColumnType("double precision");

                    b.Property<int?>("OrientadorId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FinanciadorId");

                    b.HasIndex("OrientadorId");

                    b.ToTable("Projetos");
                });

            modelBuilder.Entity("colabAPI.Business.Models.Entities.Bolsista", b =>
                {
                    b.HasBaseType("colabAPI.Business.Models.Entities.Pesquisador");

                    b.Property<int?>("OrientadorId")
                        .HasColumnType("integer");

                    b.HasIndex("OrientadorId");

                    b.ToTable("Bolsistas", (string)null);
                });

            modelBuilder.Entity("colabAPI.Business.Models.Entities.Orientador", b =>
                {
                    b.HasBaseType("colabAPI.Business.Models.Entities.Pesquisador");

                    b.ToTable("Orientadores");
                });

            modelBuilder.Entity("ProjetoBolsista", b =>
                {
                    b.HasOne("colabAPI.Business.Models.Entities.Bolsista", null)
                        .WithMany()
                        .HasForeignKey("BolsistaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("colabAPI.Business.Models.Entities.Projeto", null)
                        .WithMany()
                        .HasForeignKey("ProjetoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("colabAPI.Business.Models.Entities.Bolsa", b =>
                {
                    b.HasOne("colabAPI.Business.Models.Entities.Pesquisador", "Pesquisador")
                        .WithOne("Bolsa")
                        .HasForeignKey("colabAPI.Business.Models.Entities.Bolsa", "PesquisadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pesquisador");
                });

            modelBuilder.Entity("colabAPI.Business.Models.Entities.Projeto", b =>
                {
                    b.HasOne("colabAPI.Business.Models.Entities.Financiador", "Financiador")
                        .WithMany("Projetos")
                        .HasForeignKey("FinanciadorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("colabAPI.Business.Models.Entities.Orientador", "Orientador")
                        .WithMany()
                        .HasForeignKey("OrientadorId");

                    b.Navigation("Financiador");

                    b.Navigation("Orientador");
                });

            modelBuilder.Entity("colabAPI.Business.Models.Entities.Bolsista", b =>
                {
                    b.HasOne("colabAPI.Business.Models.Entities.Pesquisador", null)
                        .WithOne()
                        .HasForeignKey("colabAPI.Business.Models.Entities.Bolsista", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("colabAPI.Business.Models.Entities.Orientador", "Orientador")
                        .WithMany()
                        .HasForeignKey("OrientadorId");

                    b.Navigation("Orientador");
                });

            modelBuilder.Entity("colabAPI.Business.Models.Entities.Orientador", b =>
                {
                    b.HasOne("colabAPI.Business.Models.Entities.Pesquisador", null)
                        .WithOne()
                        .HasForeignKey("colabAPI.Business.Models.Entities.Orientador", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("colabAPI.Business.Models.Entities.Financiador", b =>
                {
                    b.Navigation("Projetos");
                });

            modelBuilder.Entity("colabAPI.Business.Models.Entities.Pesquisador", b =>
                {
                    b.Navigation("Bolsa");
                });
#pragma warning restore 612, 618
        }
    }
}
