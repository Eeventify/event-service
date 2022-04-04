using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DTO_Layer;
namespace DAL_Layer.Model
{
    public class Event
    {
        // Constructors
        public Event()
        {
            Interests = new();
            Members = new();
        }

        public Event(EventDTO eventDTO)
        {
            ID = eventDTO.ID;
            Description = eventDTO.Description;            
            Title = eventDTO.Title;
            LocationBased = (bool)eventDTO.LocationBased;
            Latitude = eventDTO.Latitude;
            Longitude = eventDTO.Longitude;
            HostID = eventDTO.HostID;
            MaxPeople = eventDTO.MaxPeople;
            MinPeople = eventDTO.MinPeople;
            StartEvent = eventDTO.StartEvent;
            HasStarted = (bool)eventDTO.HasStarted;

            List<EventInterest> _eventInterests = new();
            foreach(int interest in eventDTO.Interests)
            {
                _eventInterests.Add(new EventInterest(interest, this.ID));
            }
            Interests = _eventInterests;

            List<EventMember> _eventMembers = new();
            foreach (int member in eventDTO.Members)
            {
                _eventMembers.Add(new EventMember(member, this.ID));
            }
            Members = _eventMembers;
        }

        // Primary Key
        public int ID { get; set; }

        // Properties
        public string? Description { get; set; }
        public string Title { get; set; }
        public bool LocationBased { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int HostID { get; set; }
        public int? MaxPeople { get; set; }
        public int? MinPeople { get; set; }
        public DateTime StartEvent { get; set; }
        public bool HasStarted { get; set; }

        // Foreign Keys
        public List<EventInterest> Interests { get; set; }
        public List<EventMember> Members { get; set; }

        // Navigational Properties

        // Methods
        public EventDTO ToDTO()
        {
            List<int> _interests = new();
            foreach (EventInterest interest in Interests)
            {
                _interests.Add(interest.InterestID);
            }

            List<int> _members = new();
            foreach (EventMember member in Members)
            {
                _members.Add(member.MemberID);
            }

            return new EventDTO
            {
                ID = ID,
                Description = Description,
                Interests = _interests,
                Members = _members,
                Title = Title,
                LocationBased = LocationBased,
                Latitude = Latitude,
                Longitude = Longitude,
                HostID = HostID,
                MaxPeople = MaxPeople,
                MinPeople = MinPeople,
                StartEvent = StartEvent,
                HasStarted = HasStarted
            };
        }
    }
}
