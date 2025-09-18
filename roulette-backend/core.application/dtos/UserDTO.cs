using core.domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.application.dtos
{
    public class UserDTO
    {
        public int ID { get; set; }
        public string Name { get; set; } = default!;
        public double Balance{ get; set; }

    }
}
