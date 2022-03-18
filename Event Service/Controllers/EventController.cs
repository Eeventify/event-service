using Abstraction_Layer;
using DTO_Layer;
using Microsoft.AspNetCore.Mvc;

namespace Event_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : Controller
    {
        private IEventCollection eventCollection;
        private IEventCreation eventCreation;
        public EventController(IEventCollection? _eventCollection = null, IEventCreation? _eventCreation = null)
        {
            eventCollection = _eventCollection;
            eventCreation = _eventCreation;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Event))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetEventByID")]
        public IActionResult GetEvent(int Id)
        {
            EventDTO? eventDTO = eventCollection.GetEvent(Id);

            if (eventDTO == null)
            {
                return BadRequest("A Event with this ID does not exist");
            }
            return Ok(new Event(eventDTO));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Event>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetAllEvents")]
        public IActionResult GetAllEvents()
        {
            List<EventDTO>? Events = new List<EventDTO>();
            Events = eventCollection.GetAllEvents();

            return Ok(Events);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("DeleteEvent")]
        public IActionResult DeleteEvent(int Id)
        {
            EventDTO? eventDTO = eventCollection.GetEvent(Id);
            if (eventDTO == null)
            {
                return BadRequest("A Event with this ID does not exist");
            }
            eventCollection.DeleteEvent(Id);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Event))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //List<int> interests, List<int> members, 
        public IActionResult AddEvent(string description,string title, bool locationbased, double latitude, double longitude, int hostid, int maxpeople, int minpeople, DateTime startevent, bool hasstarted)
        {
            bool state = eventCreation.AddEvent(new EventDTO() { 
            Description = description,
            //Interests = interests,
            //Members = members,
            Title = title,
            LocationBased = locationbased,
            Latitude = latitude,
            Longitude = longitude,
            HostID = hostid,
            MaxPeople = maxpeople,
            MinPeople = minpeople,
            StartEvent = startevent,
            HasStarted = hasstarted
            });
            throw new NotImplementedException();
            if (state)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Invalid Input");
            }
        }
    }
}
