using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comunalka.Models;

public class CommunalResource
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Tariff> Tariffs { get; set; } = new HashSet<Tariff>();
}
