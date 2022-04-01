using Microsoft.EntityFrameworkCore;

using Abstraction_Layer;
using DTO_Layer;
using DAL_Layer.Model;

namespace DAL_Layer
{
    public class EventEFDAL : IEventCollection, IEventCreation
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

        public List<EventDTO>? GetAllEvents()
        {
            List<Event> events = _context.Events.ToList<Event>();

            List<EventDTO> eventDTOs = new();
            foreach(Event _event in events)
            {
                eventDTOs.Add(_event.ToDTO());
            }
            return eventDTOs;
        }

        public EventDTO? GetEvent(int Id)
        {
            Event _event = _context.Events.FirstOrDefault(x => x.ID == Id);

            if (_event == null)
                return null;

            return _event.ToDTO();
        }
    }
}
