namespace WebApp.Models.Olympics
{
    public class EventViewModel
    {
        public string SportName { get; set; }
        public string EventName { get; set; }
        public string Olympics { get; set; }
        public string Season { get; set; }
        public int? AgeDuringEvent { get; set; }
        public string Medal { get; set; }
    }
}