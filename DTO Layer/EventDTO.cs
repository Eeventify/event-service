namespace DTO_Layer
{
    public class EventDTO
    {
        public EventDTO()
        {
            if (Interests == null)
                Interests = new();

            if (Members == null)
                Members = new();

        }

        public int ID { get; set; }

        public string? Description { get; set; }
        public HashSet<int> Interests { get; set; }
        public HashSet<int> Members { get; set; }
        public string Title { get; set; }
        public bool? LocationBased { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int HostID { get; set; }
        public int? MaxPeople { get; set; }
        public int? MinPeople { get; set; }
        public DateTime StartEvent { get; set; }
        public bool? HasStarted { get; set; }
    }
}