using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comunalka.Models;

public class ComunalkaContext: DbContext
{

    public virtual DbSet<CommunalResource> Resources { get; set; } = null!;
    public virtual DbSet<Tariff> Tariffs { get; set; } = null!;
    public virtual DbSet<Counter> Counters { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=SILVERSTONE\\SQLEXPRESS;Initial Catalog=Comunalka;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Connect Timeout=60;Encrypt=False;Trust Server Certificate=False");
}


/*
 * ViewModel для головного вікна
 * підключити його до вікна як DataContext
 * 
 * 
 */ 