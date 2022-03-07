using Abstraction_Layer;
using DTO_Layer;
using Microsoft.AspNetCore.Mvc;

namespace Event_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : Controller
    {
        private readonly IEventCollection eventCollection;
        private readonly IEventCreation eventCreation;


        public EventController(IEventCollection? _eventCollection = null, IEventCreation? _eventCreation = null)
        {
            eventCollection = _eventCollection;
            eventCreation = _eventCreation;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Event))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetEvent")]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Event))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("AddEvent")]
        //List<int> interests, List<int> members, 
        public IActionResult AddEvent(string description,string title, bool locationbased, double latitude, double longitude, int hostid, int maxpeople, int minpeople, DateTime startevent, bool hasstarted)
        {
            //if (!ValidateUsername(username))
            //{
            //    return Accepted("Username is already in use");
            //}

            //if (!ValidatePassword(password))
            //{
            //    return Accepted("Password is of incorrect format");
            //}

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
