using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comunalka.Models;

public class Tariff
{
    public int Id { get; set; }

    public virtual CommunalResource Resource { get; set; } = null!;

    public virtual ICollection<Counter> Counters { get; set; } = new HashSet<Counter>();

    public decimal Price { get; set; }
}
