namespace WebApp.Models.Olympics;

public class PersonViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public int? Weight { get; set; }
    public int? Height { get; set; }
    public string Gender { get; set; }
    public int GoldMedals { get; set; }
    public int SilverMedals { get; set; }
    public int BronzeMedals { get; set; }
    public int EventCount { get; set; }
}