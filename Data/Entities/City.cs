using L1_Zvejyba.Data.Auth.Model;
using System.ComponentModel.DataAnnotations;

namespace L1_Zvejyba.Data.Entities
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public required string UserId { get; set; }

        public User SiteUser { get; set; }

        public string lastModifiedBy { get; set; }
    }
}
