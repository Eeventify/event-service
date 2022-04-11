﻿using Microsoft.EntityFrameworkCore;

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
            _context.SaveChanges();
        }

        //als hier een lijst met alle huidige interests van het event wordt doorgestuurd kan hij hem in een keer updaten
        public void UpdateInterests(HashSet<int> interestIds)
        {

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