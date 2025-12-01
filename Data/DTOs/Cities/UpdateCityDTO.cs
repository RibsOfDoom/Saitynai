using System.ComponentModel.DataAnnotations;
namespace L1_Zvejyba.Data.DTOs.Cities
{
    public record UpdateCityDTO(int Id, [Required] string Description);
}
