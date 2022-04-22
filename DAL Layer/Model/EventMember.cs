using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Layer.Model
{
    public class EventMember
    {
        // Constructors
        public EventMember()
        {

        }

        public EventMember(int memberID, int eventID)
        {
            MemberID = memberID;
            EventID = eventID;  
        }

        // Primary Key
        public int ID { get; set; }

        // Properties
        public int MemberID { get; set; }

        // Foreign Keys
        public int EventID { get; set; }

        // Navigational Properties
        public Event Event { get; set; }

        // Methods
        public int ToInt()
        {
            return MemberID;
        }
    }
}
