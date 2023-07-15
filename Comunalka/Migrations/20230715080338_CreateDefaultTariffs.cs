using Microsoft.EntityFrameworkCore.Migrations;
using Comunalka.Models;
using System.Linq;

#nullable disable

namespace Comunalka.Migrations
{
    /// <inheritdoc />
    public partial class CreateDefaultTariffs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            using (var context = new ComunalkaContext())
            {
                var el = context.Resources.FirstOrDefault(x => x.Title == "Електрика");
                if (el != null)
                {
                    el.Tariffs.Add(new Tariff { Price = 2.64M });
                }
                var water = context.Resources.FirstOrDefault(x => x.Title == "Вода");
                if (water != null)
                {
                    water.Tariffs.Add(new Tariff { Price = 25.55M });
                }
                var gas = context.Resources.FirstOrDefault(x => x.Title == "Газ");
                if (gas != null)
                {
                    gas.Tariffs.Add(new Tariff { Price = 7.99M });
                }
                context.SaveChanges();
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
