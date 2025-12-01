using System.ComponentModel.DataAnnotations;
using L1_Zvejyba.Data.Auth.Model;
namespace L1_Zvejyba.Data.DTOs.Cities
{
    public record CreateCityDTO(int Id, string Name, string Description);
}