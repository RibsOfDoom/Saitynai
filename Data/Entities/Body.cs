using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L1_Zvejyba.Data.Entities
{
    public class Body
    {
        [Key]
        public string Name { get; set; }
        public string Description { get; set; }

        public string cityName { get; set; }
        public City city { get; set; }
    }
}
