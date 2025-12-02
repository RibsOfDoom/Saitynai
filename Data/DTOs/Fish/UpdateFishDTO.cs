using System.ComponentModel.DataAnnotations;
namespace L1_Zvejyba.Data.DTOs.Fish
{
    public record UpdateFishDTO(string Description, int Season, int TimeFrom, int TimeTo);
}
