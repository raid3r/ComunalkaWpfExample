using Microsoft.EntityFrameworkCore.Migrations;
using Comunalka.Models;
using System.Collections.Generic;

#nullable disable

namespace Comunalka.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultResources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            using (var db = new ComunalkaContext())
            {
                db.AddRange(new List<CommunalResource>()
                {
                    new CommunalResource()
                    {
                        Title = "Електрика",
                    },
                    new CommunalResource()
                    {
                        Title = "Вода",
                    },
                    new CommunalResource()
                    {
                        Title = "Газ",
                    },
                });
                db.SaveChanges();
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
