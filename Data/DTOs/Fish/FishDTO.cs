using System.ComponentModel.DataAnnotations;

namespace L1_Zvejyba.Data.DTOs.Fish
{
    public record FishDTO(int id, string Name, string Description, int Season, int TimeFrom, int TimeTo);
}
