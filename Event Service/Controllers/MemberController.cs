using Abstraction_Layer;
using DTO_Layer;
using Microsoft.AspNetCore.Mvc;

using EventContext = DAL_Layer.EventContext;
namespace Event_Service.Controllers
{
    [ApiController]
    [Route("[controller]/{eventID}")]
    public class MemberController : Controller
    {
        private IEventMembers _eventMembers;
        public MemberController(EventContext context, IEventMembers? eventMembers = null)
        {
            _eventMembers = eventMembers;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("Attend")]
        public IActionResult? AttendEvent(int eventID, int userID)
        {
            if (_eventMembers.AttendEvent(eventID, userID))
                return Ok("Event Attended");
            else
                return BadRequest("Event not found or coult not be attended");
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("Unattend")]
        public IActionResult? UnattendEvent(int eventID, int userID)
        {
            if (_eventMembers.UnattendEvent(eventID, userID))
                return Ok("Event Unattended");
            else
                return BadRequest("Event not found or coult not be unattended");
        }
    }
}
