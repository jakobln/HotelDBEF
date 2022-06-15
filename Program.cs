using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDBEF
{

    class Program
    {
        static void SelectFromOneClass()
        //A. Select fra een klasse
        {
            using (HotelModel db = new HotelModel())
            {
                //1. List alle oplysninger om alle hoteller.               
                var query1 = from h in db.DemoHotels
                             select h;

                Console.WriteLine("1. All info about hotels:");
                foreach (var item in query1)
                {
                    //Console.WriteLine($"{item.Hotel_No}, {item.Name}, {item.Address}");
                    Console.WriteLine(item);
                }
                Console.WriteLine();

                //2. List alle oplysninger om alle hoteller i Roskilde.
                var query2 = from h in db.DemoHotels
                             where h.Address.Contains("4000") 
                             select h;

                Console.WriteLine("2. All info about all hotels in Roskilde:");
                foreach (var item in query2)
                {
                    Console.WriteLine($"{item.Hotel_No}, {item.Name}, {item.Address}");
                    //Console.WriteLine(item);
                }
                Console.WriteLine();

                //3. List navne og adresser på alle gæster fra Roskilde
                var query3 = from g in db.DemoGuests
                             where g.Address.Contains("4000")
                             select new { g.Name, g.Address };

                Console.WriteLine("3. All info about all guests in Roskilde:");
                foreach (var item in query3)
                {
                    Console.WriteLine($"{item.Name},  {item.Address}");
                }
                Console.WriteLine();

                //4. List navne og adresser på alle gæster fra Roskilde sorteret alfabetisk efter navn.
                var query4 = from g in db.DemoGuests
                             orderby g.Name
                             where g.Address.Contains("4000")
                             select new { g.Name, g.Address };

                Console.WriteLine("4. All info about all guests by name in Roskilde:");
                foreach (var item in query4)
                {
                    Console.WriteLine($"{item.Name},  {item.Address}");
                }
                Console.WriteLine();

                //5. List alle dobbeltværelser med en pris under 200 pr.nat.
                var query5 = from r in db.DemoRooms
                             where r.Price < 200 && r.Types.ToLower() == "d"
                             select new {r.DemoHotel.Name, r.Hotel_No,r.Room_No,r.Types  };

                Console.WriteLine("5. All double rooms below 200 DKK:");
                foreach (var item in query5)
                {
                    Console.WriteLine($"{item.Hotel_No}, {item.Name}, {item.Room_No}, {item.Types}");
                }
                Console.WriteLine();

                //6. List alle dobbeltværelser eller familierum med en pris under 400 pr.nat.
                var query6 = from r in db.DemoRooms
                             where     r.Price < 400 
                                   && (   r.Types.ToLower() == "d" 
                                       || r.Types.ToLower() == "f")
                             select new { r.DemoHotel.Name, r.Hotel_No, r.Room_No, r.Types, r.Price };

                Console.WriteLine("6. All double and family rooms below 400 DKK:");
                foreach (var item in query6)
                {
                    Console.WriteLine($"{item.Hotel_No}, {item.Name}, {item.Room_No}, {item.Types}, {item.Price} DKK");
                }
                Console.WriteLine();

                //7. List alle dobbeltværelser eller familierum med en pris under 400 pr.nat sorteret i stigende orden efter pris.
                var query7 = from r in db.DemoRooms
                             orderby r.Price
                             where r.Price < 400
                                   && (r.Types.ToLower() == "d"
                                       || r.Types.ToLower() == "f")
                             select new { r.DemoHotel.Name, r.Hotel_No, r.Room_No, r.Types, r.Price };

                Console.WriteLine("7. All double and family rooms below 400 DKK by price:");
                foreach (var item in query7)
                {
                    Console.WriteLine($"{item.Hotel_No}, {item.Name}, {item.Room_No}, {item.Types}, {item.Price} DKK");
                }
                Console.WriteLine();

                //8. List alle gæster, som har et navn, der starter med 'G'.
                var query8 = from g in db.DemoGuests
                             where g.Name.ToLower().StartsWith("g")
                             select new { g.Name, g.Address };

                Console.WriteLine("8. All info about all guests starting with 'G':");
                foreach (var item in query8)
                {
                    Console.WriteLine($"{item.Name},  {item.Address}");
                }
                Console.WriteLine();
            }
        }


        static void AggregateFunctions()
        //B. Aggregate-funktioner
        {
            using (HotelModel db = new HotelModel())
            {
                //1. Hvor mange hoteller er der?
                var query1 = from h in db.DemoHotels
                             select h;

                Console.WriteLine($"1. Number of hotels: {query1.Count()}");
                Console.WriteLine();

                //2. Hvor mange hoteller er der i Roskilde?
                var query2 = from h in db.DemoHotels
                             where h.Address.Contains("4000")
                             select h;

                Console.WriteLine($"2. Number of hotels in Roskilde: {query2.Count()}");
                Console.WriteLine();

                //3. Hvad er gennemsnitsprisen på et værelse?
                var query3 = from r in db.DemoRooms
                             select r.Price;

                Console.WriteLine($"3. Average room price: {query3.Average()} DKK");
                Console.WriteLine();

                //4. Hvad er gennemsnitsprisen på et enkeltværelse?
                var query4 = from r in db.DemoRooms
                             where r.Types.ToLower() == "s"
                             select r.Price;

                Console.WriteLine($"4. Average room price single room: {query4.Average()} DKK");
                Console.WriteLine();

                //5. Hvad er gennemsnitsprisen på et dobbeltværelse?
                var query5 = from r in db.DemoRooms
                             where r.Types.ToLower() == "d"
                             select r.Price;

                Console.WriteLine($"5. Average room price double room: {query5.Average()} DKK");
                Console.WriteLine();

                //6. Hvad er gennemsnitsprisen på et værelse på Hotel Scandic?
                var query6 = from r in db.DemoRooms
                             where r.DemoHotel.Name == "Scandic" 
                             select r.Price;

                Console.WriteLine($"6. Average room price at Hotel Scandic: {query6.Average()} DKK");
                Console.WriteLine();

                //7. Hvad er den totale indtægt pr.nat for alle dobbeltværelser?
                var query7 = from r in db.DemoRooms
                             where r.Types.ToLower() == "d"
                             select r.Price;

                Console.WriteLine($"7. Total income on all double rooms per night: {query7.Sum() } DKK");
                Console.WriteLine();

                //8. Hvor mange forskellige gæster har foretaget bookinger i marts måned ?
                DateTime startMarch = new DateTime(2011, 3, 1);
                DateTime endMarch   = new DateTime(2011, 3, 31);
                var query8 = from b in db.DemoBookings
                             where    b.Date_From <=endMarch 
                                   && b.Date_To >= startMarch
                                   && b.Date_To >= b.Date_From
                             select b.Guest_No;
                               
                Console.WriteLine($"8. Number different guests in March: {query8.Distinct().Count() }");
                Console.WriteLine();

                //9. Hvor mange bookinger er der i dag på Scandic hotel ?
                DateTime Today = DateTime.Now.Date;
                var query9 = from b in db.DemoBookings
                             where    b.Date_From <= Today 
                                   && b.Date_To >= Today
                                   && b.DemoRoom.DemoHotel.Name == "Scandic"
                             select b;

                Console.WriteLine($"9. Number of bookings today at Scandic: {query9.Count() }");
                Console.WriteLine();

                //10. Hvor mange bookinger er der i morgen på Scandic hotel ?
                DateTime Tomorrow = DateTime.Now.Date.AddDays(1);
                var query10 = from b in db.DemoBookings
                              where    b.Date_From <= Tomorrow
                                    && b.Date_To >= Tomorrow
                                    && b.DemoRoom.DemoHotel.Name == "Scandic"
                             select b;

                Console.WriteLine($"10. Number bookings tomorrow at Scandic: {query10.Count() }");
                Console.WriteLine();
            }
        }

        static void Grouping()
        //C.Gruppering
        {
            using (HotelModel db = new HotelModel())
            {
                //1. List antal værelser for hvert hotel. 
                var groups1 = from r in db.DemoRooms
                             group r by r.DemoHotel.Name into g
                             select new { Hotel = g.Key, Total = g.Count() };

                Console.WriteLine("1. Number of rooms at each hotel:");
                foreach (var item in groups1)
                {
                    Console.WriteLine($"{item.Hotel}: {item.Total}");
                }
                Console.WriteLine();

                //2. List antal værelser for hvert hotel i Roskilde.
                var groups2 = from r in db.DemoRooms
                              where r.DemoHotel.Address.Contains("4000")
                              group r by r.DemoHotel.Name into g
                              select new { Hotel = g.Key, Total = g.Count() };

                Console.WriteLine("2. Number of rooms at each hotel in Roskilde:");
                foreach (var item in groups2)
                {
                    Console.WriteLine($"{item.Hotel}: {item.Total}");
                }
                Console.WriteLine();
            }
        }

        static void CreateRenderUpdateDeleteHotel()
        {
            //Insert, Update, Sletning: Hotel

            using (HotelModel db = new HotelModel())
            {
                // Hotel database key is NOT generated automatically -> Next key = current max + 1
                int nextHotelKey = db.DemoHotels.Max<DemoHotel>(Hotel => Hotel.Hotel_No) + 1;
                //int nextHotelKey =  44;


                // Create and save a new Hotel
                DemoHotel hotel = new DemoHotel
                {
                    Hotel_No = nextHotelKey,
                    Name = "Myhotel",
                    Address = "Maglegaardsvej 2 4000 Roskilde"
                };
                db.DemoHotels.Add(hotel);
                db.SaveChanges();

                // Display all hotels from the database
                var query = from h in db.DemoHotels
                            orderby h.Name
                            select h;

                Console.WriteLine("All hotels in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine();
                
                //Find the hotel that we just created
                Object[] hotelKeys = new Object[]{ nextHotelKey };
                DemoHotel foundHotel = db.DemoHotels.Find(hotelKeys);
                
                //Update found hotel
                foundHotel.Address += " Update";
                db.SaveChanges();
                foundHotel = db.DemoHotels.Find(hotelKeys);
                Console.WriteLine($"{foundHotel}");

                //Remove Hotel
                db.DemoHotels.Remove(foundHotel);
                db.SaveChanges();
            }
        }

        static void CreateRenderUpdateDeleteRoom()
        {
            //Insert, Update, Sletning: Room

            using (HotelModel db = new HotelModel())
            {
                // Hotel database key is NOT generated automatically -> Next key = current max + 1
                int maxHotelKey = db.DemoHotels.Max<DemoHotel>(Hotel => Hotel.Hotel_No);
                int nextRoomKey = db.DemoRooms.Max<DemoRoom>(Room => Room.Room_No) + 1;
                //int nextHotelKey =  44;


                // Create and save a new Hotel
                DemoRoom room = new DemoRoom
                {
                    Hotel_No = maxHotelKey, Price = 300, Types = "S", Room_No = nextRoomKey
                };
                db.DemoRooms.Add(room);
                db.SaveChanges();

                // Display all hotels from the database
                var query = from r in db.DemoRooms
                            orderby r.Hotel_No
                            select r;

                Console.WriteLine("All rooms in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine();

                //Find the room that we just created
                Object[] roomKeys = new Object[] { nextRoomKey, maxHotelKey };
                DemoRoom foundRoom = db.DemoRooms.Find(roomKeys);

                //Update found hotel
                foundRoom.Types = "D";
                db.SaveChanges();
                foundRoom = db.DemoRooms.Find(roomKeys);
                Console.WriteLine($"{foundRoom}");

                //Remove Hotel
                db.DemoRooms.Remove(foundRoom);
                db.SaveChanges();
            }
        }

        static void CreateRenderUpdateDeleteGuest()
        {
            //Insert, Update, Sletning: Guest

            using (HotelModel db = new HotelModel())
            {
                // Hotel database key is NOT generated automatically -> Next key = current max + 1
                int maxGuestKey = db.DemoGuests.Max<DemoGuest>(Guest => Guest.Guest_No) + 1;

                // Create and save a new Guest
                DemoGuest guest = new DemoGuest
                {
                    Guest_No = maxGuestKey,
                    Name = "NewGuest",
                    Address = "NewGuestAddress",
                };
                db.DemoGuests.Add(guest);
                db.SaveChanges();

                // Display all hotels from the database
                var query = from g in db.DemoGuests
                            orderby g.Guest_No
                            select g;

                Console.WriteLine("All guests in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine();

                //Find the guest that we just created
                Object[] guestKeys = new Object[] { maxGuestKey };
                DemoGuest foundGuest = db.DemoGuests.Find(guestKeys);

                //Update found guest
                foundGuest.Address = " Update";
                db.SaveChanges();
                foundGuest = db.DemoGuests.Find(guestKeys);
                Console.WriteLine($"{foundGuest}");

                //Remove Guest
                db.DemoGuests.Remove(foundGuest);
                db.SaveChanges();
            }
        }

        static void CreateRenderUpdateDeleteBooking()
        {
            //Insert, Update, Sletning: Booking

            using (HotelModel db = new HotelModel())
            {
                // Hotel database key is NOT generated automatically -> Next key = current max + 1
                int maxBookingKey = db.DemoBookings.Max<DemoBooking>(Booking => Booking.Booking_id) + 1;

                // Create and save a new Booking
                DemoBooking booking = new DemoBooking
                {
                    Booking_id = maxBookingKey,
                    Hotel_No = 1,
                    Guest_No = 4,
                    Room_No = 11,
                    Date_From = DateTime.Today.AddYears(-3),
                    Date_To = DateTime.Today.AddDays(2).AddYears(-3)
                };
                db.DemoBookings.Add(booking);
                db.SaveChanges();

                // Display all bookings from the database
                var query = from b in db.DemoBookings
                            orderby b.Booking_id
                            select b;

                Console.WriteLine("All bookings in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine();

                //Find the guest that we just created
                Object[] bookingKeys = new Object[] { maxBookingKey };
                DemoBooking foundBooking = db.DemoBookings.Find(bookingKeys);

                //Update found booking
                foundBooking.Guest_No = 5;
                db.SaveChanges();
                foundBooking = db.DemoBookings.Find(bookingKeys);
                Console.WriteLine($"{foundBooking}");

                //Remove Booking
                db.DemoBookings.Remove(foundBooking);
                db.SaveChanges();
            }
        }

        static void Main(string[] args)
        {
            SelectFromOneClass();
            AggregateFunctions();
            Grouping();
            CreateRenderUpdateDeleteHotel();
            CreateRenderUpdateDeleteRoom();
            CreateRenderUpdateDeleteGuest();
            CreateRenderUpdateDeleteBooking();



            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}