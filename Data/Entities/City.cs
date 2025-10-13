using System.ComponentModel.DataAnnotations;

namespace L1_Zvejyba.Data.Entities
{
    public class City
    {
        [Key]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
