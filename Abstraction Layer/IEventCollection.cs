using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DTO_Layer;

namespace Abstraction_Layer
{
    public interface IEventCollection
    {
        public EventDTO? GetEvent(int Id);
        public List<EventDTO>? GetAllEvents();
    }
}