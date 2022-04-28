using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Layer.Model
{
    public class EventInterest
    {
        // Constructors
        public EventInterest()
        {

        }

        public EventInterest(int interestID, int eventID)
        {
            InterestID = interestID;
            EventID = eventID;
        }

        // Primary Key
        public int ID { get; set; }

        // Properties
        public int InterestID { get; set; }

        // Foreign Keys
        public int EventID { get; set; }

        // Navigational Properties
        public Event Event { get; set; }

        // Methods
        public int ToInt()
        {
            return InterestID;
        }
    }
}
