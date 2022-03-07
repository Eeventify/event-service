namespace DTO_Layer
{
    public class EventDTO
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public List<int> Interests { get; set; }
        public List<int> Members { get; set; }
        public string Title { get; set; }
        public bool LocationBased { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int HostID { get; set; }
        public int MaxPeople { get; set; }
        public int MinPeople { get; set; }
        public DateTime StartEvent { get; set; }
        public bool HasStarted { get; set; }
    }
}