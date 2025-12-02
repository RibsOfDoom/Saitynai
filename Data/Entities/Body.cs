using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using L1_Zvejyba.Data.Auth.Model;
namespace L1_Zvejyba.Data.Entities
{
    public class Body
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int cityId { get; set; }
        public City city { get; set; }

        [Required]
        public required string UserId { get; set; }

        public User SiteUser { get; set; }

        public string? lastModifiedBy { get; set; }
    }
}
