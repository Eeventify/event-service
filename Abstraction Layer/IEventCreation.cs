using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DTO_Layer;

namespace Abstraction_Layer
{
    public interface IEventCreation
    {
        public bool AddEvent(EventDTO eventDTO);
        public void UpdateEvent(EventDTO eventDTO);
    }
}
