using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comunalka.Models;

public class Counter
{
    public int Id { get; set; }
    public string Number { get; set; } = null!;
    public virtual Tariff Tariff { get; set; } = null!;
}
