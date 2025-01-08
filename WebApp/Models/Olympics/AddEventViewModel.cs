namespace WebApp.Models.Olympics;

public class AddEventViewModel
{
    public int CompetitorId { get; set; }
    public int EventId { get; set; }
    public int MedalId { get; set; }
    
    public List<Person> Competitors { get; set; } = new List<Person>();
    public List<Event> Events { get; set; } = new List<Event>();
    public List<Medal> Medals { get; set; } = new List<Medal>();
}
