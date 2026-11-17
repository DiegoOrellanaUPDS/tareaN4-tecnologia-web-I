using System;
using ConvertidorTTS.Models;
using Microsoft.EntityFrameworkCore;


namespace ConvertidorTTS.Data;

public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Usuario>()
        .Property(u => u.Estado)
        .HasConversion<string>(); // Guarda el enum como texto en la BD
}
    public DbSet<Usuario> Usuarios {get; set;}
}
