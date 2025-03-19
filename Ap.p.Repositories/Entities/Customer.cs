using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Entities
{
    public class Customer : BaseEntity<int>
    {

        public string Name { get; set; } = default!;

        public string Phone { get; set; } = default!;

        public string Email { get; set; } = default!;
       

    }
}
