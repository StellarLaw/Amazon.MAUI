using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.MAUI.Models
{
    public class WishList
    {
        public string? Name { get; set; }
        public int Id { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
    }
}
