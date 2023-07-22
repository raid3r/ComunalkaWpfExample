using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comunalka.Models;

public class CounterHistory
{
    public int Id { get; set; }
    public virtual Counter Counter { get; set; } = null!;
    public DateTime Date { get; set; }
    public int Value { get; set; }
}
