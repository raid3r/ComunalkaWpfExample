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
    
    public decimal Price { get; set; }
}
