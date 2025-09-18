using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.domain.entities
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; } = default!;
        public string NameNormalized { get; set; } = default!;
        public decimal Balance { get; set; }
    }               
}
