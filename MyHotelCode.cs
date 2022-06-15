using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDBEF
{
    public partial class DemoHotel
    {
        
        public override string ToString()
        {
            return $"{Hotel_No}, {Name}, {Address}";
        }
    }

    public partial class DemoRoom
    {
        public override string ToString()
        {
            return $"{Hotel_No}, {Price}, {Types}";
        }
    }

    public partial class DemoGuest
    {
        public override string ToString()
        {
            return $"{Guest_No}, {Name}, {Address}";
        }
    }

    public partial class DemoBooking
    {
        public override string ToString()
        {
            return $"{Booking_id}, {Guest_No}, {Hotel_No}, {Room_No}, {Date_From}, {Date_To} ";
        }
    }
}
