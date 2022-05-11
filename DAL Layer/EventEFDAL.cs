using Microsoft.EntityFrameworkCore;

using Abstraction_Layer;
using DTO_Layer;
using DAL_Layer.Model;

namespace DAL_Layer
{
    public class EventEFDAL : IEventCollection, IEventCreation, IEventMembers
    {
        public readonly EventContext _context;
        public EventEFDAL(EventContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool AddEvent(EventDTO eventDTO)
        {
            _context.Events.Add(new Event(eventDTO));
            return _context.SaveChanges() > 0;
        }

        public void DeleteEvent(int Id)
        {
            Event? _event = _context.Events.FirstOrDefault(x => x.ID == Id);

            if (_event == null)
                return;

            _context.Events.Remove(_event);
            _context.SaveChanges();
        }

        public void UpdateEvent(EventDTO NewEvent)
        {
            Event? OldEvent = _context.Events.FirstOrDefault(x => x.ID == NewEvent.ID);

            if (OldEvent == null)
                return;

            //EventDTO OldEvent = GetEvent(NewEvent.ID);
            if (NewEvent.Title != "")
            {
                OldEvent.Title = NewEvent.Title;
            }
            if (NewEvent.Description != "")
            {
                OldEvent.Description = NewEvent.Description;
            }
            if (NewEvent.LocationBased != null)
            {
                OldEvent.LocationBased = (bool)NewEvent.LocationBased;
            }
            if (NewEvent.Longitude != -1000)
            {
                OldEvent.Longitude = NewEvent.Longitude;  
            }
            if (NewEvent.Latitude != -1000)
            {
                OldEvent.Latitude = NewEvent.Latitude;
            }
            if (NewEvent.StartEvent != default(DateTime))
            {
                OldEvent.StartEvent = NewEvent.StartEvent;
            }
            if (NewEvent.HasStarted != null)
            {
                OldEvent.HasStarted = (bool)NewEvent.HasStarted;
            }
            if (NewEvent.MaxPeople != null)
            {
                OldEvent.MaxPeople = NewEvent.MaxPeople;
            }
            if (NewEvent.MinPeople != null)
            {
                OldEvent.MinPeople = NewEvent.MinPeople;
            }
            if (NewEvent.Interests.Any() && !OldEvent.AreInterestsEqual(NewEvent.Interests))
            {
                HashSet<EventInterest> newList = new();
                foreach (int i in NewEvent.Interests)
                {
                    newList.Add(new EventInterest(i, NewEvent.ID));
                }
                OldEvent.Interests = newList;
            }
            _context.SaveChanges();
        }

        public List<EventDTO>? GetAllEvents()
        {
            List<Event> events = _context.Events.Include(x => x.Members).Include(x => x.Interests).ToList<Event>();

            List<EventDTO> eventDTOs = new();
            foreach(Event _event in events)
            {
                eventDTOs.Add(_event.ToDTO());
            }

            // Reverse list, newest events first
            eventDTOs.Reverse();

            return eventDTOs;
        }

        public List<EventDTO>? GetEventsLocation(double latitude, double longitude, double radius)
        {
            List<Event> events = _context.Events.Include(x => x.Members).Include(x => x.Interests).ToList<Event>();
            List<EventDTO> eventDTOs = new();
            foreach (Event _event in events)
            {
                if (!_event.LocationBased || _event.Latitude == null | _event.Longitude == null)
                {
                    continue;
                }

                double eventLat = _event.Latitude.Value;
                double eventLon = _event.Longitude.Value;

                double distance = GetDistanceFromLatLonInKm(latitude, longitude, eventLat, eventLon);

                if (distance <= radius)
                {
                    eventDTOs.Add(_event.ToDTO());
                }
            }

            // Reverse list, newest events first
            eventDTOs.Reverse();

            return eventDTOs;
        }

        public EventDTO? GetEvent(int Id)
        {
            Event _event = _context.Events.Include(x => x.Members).Include(x => x.Interests).FirstOrDefault(x => x.ID == Id);

            if (_event == null)
                return null;

            return _event.ToDTO();
        }

        public List<EventDTO> GetEventsByInterest(int interestId)
        {
            List<int> eventIDs = _context.EventInterests
                .Where(e => e.InterestID == interestId)
                .Select(e => e.EventID).ToList();
            List<EventDTO> eventDTOs = new();
            foreach (int id in eventIDs)
            {
                eventDTOs.Add(GetEvent(id));
            }

            // Reverse list, newest events first
            eventDTOs.Reverse();

            return eventDTOs;
        }

        public bool AttendEvent(int eventID, int userID)
        {
            Event _event = _context.Events.FirstOrDefault(x => x.ID == eventID);

            if (_event == null)
                return false;

            if (_event.Members.FirstOrDefault(x => x.MemberID == userID) != null)
                return false;

            _event.Members.Add(new EventMember(userID, _event.ID) );
            return _context.SaveChanges() > 0;
        }

        public bool UnattendEvent(int eventID, int userID)
        {
            Event _event = _context.Events.Include(x => x.Members).FirstOrDefault(x => x.ID == eventID);
            if (_event == null)
                return false;

            EventMember _member = _event.Members.FirstOrDefault(x => x.MemberID == userID);
            if (_member == null)
                return false;

            _event.Members.Remove(_member);
            return _context.SaveChanges() > 0;
        }

        private double GetDistanceFromLatLonInKm(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371; // Radius of the earth in km
            var dLat = Deg2Rad(lat2 - lat1);  // deg2rad below
            var dLon = Deg2Rad(lon2 - lon1);
            var a =
              Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) *
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
              ;
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; // Distance in km
            return d;
        }

        private double Deg2Rad(double deg)
        {
            return deg * (Math.PI / 180);
        }
    }
}
