using System.ComponentModel.DataAnnotations;
namespace L1_Zvejyba.Data.DTOs.Fish
{
    public record CreateFishDTO([Required] string Name, string Description, int Id, int TimeTo, int TimeFrom, int Season);
}
