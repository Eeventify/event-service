using Abstraction_Layer;
using DTO_Layer;
using Microsoft.AspNetCore.Mvc;

using EventContext = DAL_Layer.EventContext;
namespace Event_Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : Controller
    {
        private IEventCollection eventCollection;
        private IEventCreation eventCreation;
        public EventController(EventContext context, IEventCollection? _eventCollection = null, IEventCreation? _eventCreation = null)
        {
            eventCollection = _eventCollection;
            eventCreation = _eventCreation;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetEventByID")]
        public IActionResult GetEvent(int Id)
        {
            EventDTO? eventDTO = eventCollection.GetEvent(Id);

            if (eventDTO == null)
            {
                return BadRequest("A Event with this ID does not exist");
            }
            return Ok(eventDTO);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EventDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetAllEvents")]
        public IActionResult GetAllEvents()
        {
            List<EventDTO>? Events = new List<EventDTO>();
            Events = eventCollection.GetAllEvents();

            return Ok(Events);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EventDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("UpdateEvent")]
        public IActionResult UpdateEvent(int id, string? title, string? description, bool? locationbased, double? latitude, double? longitude, int? maxpeople, int? minpeople, DateTime? startevent, bool? hasstarted)
        {
            EventDTO? eventDTO = eventCollection.GetEvent(id);
            if (eventDTO == null)
            {
                return BadRequest("A Event with this ID does not exist");
            }

            if (latitude == null)
            {
                latitude = -1000;
            }
            if (longitude == null)
            {
                longitude = -1000;
            }

            eventCreation.UpdateEvent(new EventDTO()
            {
                ID = id,
                Description = description ?? "",
                //Interests = interests,
                //Members = members,
                Title = title ?? "",
                LocationBased = locationbased,
                Latitude = latitude,
                Longitude = longitude,
                HostID = -1,
                MaxPeople = maxpeople,
                MinPeople = minpeople,
                StartEvent = startevent ?? default(DateTime),
                HasStarted = hasstarted
            });
            

            return Ok();
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDTO))]
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
