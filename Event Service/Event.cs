using DTO_Layer;

namespace Event_Service
{
    public class Event
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

        public Event(int id, string description, List<int> interests, List<int> members, string title, bool locationbased, double latitude, double longitude, int hostid, int maxpeople, int minpeople, DateTime startevent, bool hasstarted)
        {
            ID = id;
            Description = description;
            Interests = interests;
            Members = members;
            Title = title;
            LocationBased = locationbased;
            Latitude = latitude;
            Longitude = longitude;
            HostID = hostid;
            MaxPeople = maxpeople;
            MinPeople = minpeople;
            StartEvent = startevent;
            HasStarted = hasstarted;
        }

        public Event(EventDTO eventDTO)
        {
            ID = eventDTO.ID;
            Description = eventDTO.Description;
            Interests = eventDTO.Interests;
            Members = eventDTO.Members;
            Title = eventDTO.Title;
            LocationBased = eventDTO.LocationBased;
            Latitude = eventDTO.Latitude;
            Longitude = eventDTO.Longitude;
            HostID= eventDTO.HostID;
            MaxPeople = eventDTO.MaxPeople;
            MinPeople = eventDTO.MinPeople;
            StartEvent = eventDTO.StartEvent;
            HasStarted = eventDTO.HasStarted;
        }
    }
}
