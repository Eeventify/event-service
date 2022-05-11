using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction_Layer
{
    public interface IEventMembers
    {
        public bool AttendEvent(int eventID, int userID);
        public bool UnattendEvent(int eventID, int userID);
    }
}
