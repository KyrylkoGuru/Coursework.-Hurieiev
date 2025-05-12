using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ProjectAurora.Person;

namespace ProjectAurora
{
        public class Person
        {
            public string Gender {  get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public int Age { get; set; }
            public string SeatType { get; set; }
            public string SeatNumber { get; set; }
            public string PlaneNumber { get; set; }
            public string Luggage {  get; set; }
            public string Status { get; set; } = "У дорозі";

            public int EnterNumber { get; set; }
            public Point Position { get; set; }
            public Point ScannerPoint { get; set; }
            public Point MidExitPoint { get; set; }
            public Point ExitPoint { get; set; }
            public Point PoliceRoomPoint { get; set; }
            public Point TargetPosition { get; set; }
            public Color Color { get; set; }
            public bool IsWaiting { get; set; } = false;
            public bool IsChecked { get; set; } = false;
            public bool HasPassedIntermediatePoint { get; set; } = false;

        public DateTime ArrivedAtScannerTime { get; set; }
        public Person(string gender, string name, string surname, int age, string seatNumber, string planeNumber, string luggage, int enterNumber, Point position, Point scannerPoint, Point midExitPoint, Point exitPoint, Point policeRoomPoint, Color color)
        {
            Gender = gender;
            Name = name;
            Surname = surname;
            Age = age;
            SeatNumber = seatNumber;
            PlaneNumber = planeNumber;
            Luggage = luggage;
            EnterNumber = enterNumber;
            Position = position;
            ScannerPoint = scannerPoint;
            MidExitPoint = midExitPoint;
            ExitPoint = exitPoint;
            PoliceRoomPoint = policeRoomPoint;
            TargetPosition = scannerPoint;
            Color = color;
        }
        public virtual string GetInfo()
        {
            return $"Ім'я: {Name} {Surname}\n" +
                $"Стать: {Gender}\n" +
                $"Вік: {Age}\n" +
                $"Місце: {SeatNumber}\n" +
                $"Номер борту: {PlaneNumber}\n" +
                $"Багаж: {Luggage}";
        }
        public class Passenger : Person
        {
            public Passenger(string gender, string name, string surname, int age, string seatNumber, string planeNumber,
                             string luggage, int enterNumber, Point position, Point scannerPoint, Point midExitPoint,
                             Point exitPoint, Point policeRoomPoint)
                : base(gender, name, surname, age, seatNumber, planeNumber, luggage, enterNumber,
                       position, scannerPoint, midExitPoint, exitPoint, policeRoomPoint, Color.Green)
            {
                TargetPosition = scannerPoint;
            }

            public bool CheckLuggage(List<string> forbiddenItems)
            {
                if (forbiddenItems.Contains(Luggage))
                {
                    this.Color = Color.Red;
                    return false;
                }
                return true;
            }
        }

        public class BadGuy : Person
        {
            public BadGuy(Passenger passenger)
                : base(passenger.Gender, passenger.Name, passenger.Surname, passenger.Age, passenger.SeatNumber,
                       passenger.PlaneNumber, passenger.Luggage, passenger.EnterNumber, passenger.Position,
                       passenger.ScannerPoint, passenger.MidExitPoint, passenger.ExitPoint, passenger.PoliceRoomPoint, Color.Red)
            {
            }

            public override string GetInfo()
            {
                return base.GetInfo();
            }
        }

        public class Police : Person
        {
            private static Random rnd = new Random();

            public Police(string name, string surname, Point position, Point scannerPoint, Point midExitPoint, Point exitPoint, Point policeRoomPoint)
                : base("Male", name, surname, rnd.Next(20, 61), "N/A", "N/A", "N/A", -1, position, scannerPoint, midExitPoint, exitPoint, policeRoomPoint, Color.Blue)
            {
            }

            public override string GetInfo()
            {
                return base.GetInfo();
            }
        }


        public static class PersonGenerator
        {
            private static Random random = new Random();

            private static List<string> maleFirstNames = new List<string> 
            { 
                "John", "Mike", "Robert", "David", "James", "Antony", "Brian", "Joshua", "Bertholdt", "Rainer", "Gregory", "Fransua",
                "Criss", "Arnold", "Harry", "Wojtek", "Serhii", "Kyrylo", "Mykhailo", "Stephan", "Viktor", "Konstyantin", "Thomas",
                "Jake", "Jack", "Lerny", "Tony"
            };
            private static List<string> femaleFirstNames = new List<string> 
            { 
                "Jane", "Anna", "Emma", "Olivia", "Emily", "Caroline", "Jannet", "Viktoria", "Anastasia", "Sophia", "Elen", "Criss",
                "Valerie", "Linda", "Rose", "Adelina", "Clementine", "Barbara", "Charlotte", "Elizabeth", "Amelia", "Evelyn",
                "Isabella", "Ava", "Evie", "Helen", "Margaret", "Luna", "Lily", "Eleanor"
            };
            private static List<string> lastNames = new List<string> 
            { 
                "Smith", "Johnson", "Williams", "Brown", "Davis", "Miller", "Garcia", "Jones", "Bones", "Morales", "Myers", "Rodriguez",
                "Martinez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Taylor", "Moore", "Martin", "Thompson", "White", "Black",
                "Harris", "Ramirez", "Clark", "Stark", "Robinson", "Lewis", "Young"
            };

            private static List<string> allowedLuggage = new List<string> 
            { 
                "Backpack", "Suitcase", "Handbag", "Telephone", "Water 100ml", "Money and documents", "Medicine", "Warm clothes", "Wet wipes",
                "Food"
            };
            public static List<string> forbiddenLuggage = new List<string> 
            { 
                "Gun", "Knife", "Explosives", "Alcohol", "Drugs", "Gunpowder", "Water 200ml", "Red book plant", "Battery"
            };

            public static Passenger CreateRandomPassenger(
            string planeNumber,
            string seatNumber,
            int enterNumber,
            Point position,
            Point targetPosition,
            Point scannerPoint,
            Point midExitPoint,
            Point exitPoint,
            Point policeRoomPoint)
            {
                bool isMale = random.Next(0, 2) == 0;
                string gender = isMale ? "Male" : "Female";
                string name = isMale ? maleFirstNames[random.Next(maleFirstNames.Count)] : femaleFirstNames[random.Next(femaleFirstNames.Count)];
                string surname = lastNames[random.Next(lastNames.Count)];
                int age = random.Next(18, 70);
                string luggage = random.Next(0, 5) == 0
                    ? forbiddenLuggage[random.Next(forbiddenLuggage.Count)]
                    : allowedLuggage[random.Next(allowedLuggage.Count)];

                return new Passenger(
                    gender,
                    name,
                    surname,
                    age,
                    seatNumber,
                    planeNumber,
                    luggage,
                    enterNumber,
                    position,
                    scannerPoint,
                    midExitPoint,
                    exitPoint,
                    policeRoomPoint
                );
            }


            public static string GetRandomFirstName()
            {
                bool isMale = random.Next(0, 2) == 0;
                return isMale ? maleFirstNames[random.Next(maleFirstNames.Count)] : femaleFirstNames[random.Next(femaleFirstNames.Count)];
            }

            public static string GetRandomLastName()
            {
                return lastNames[random.Next(lastNames.Count)];
            }

        }

    }
        public class Plane
        {
        public List<Passenger> Passengers { get; set; } = new List<Passenger>();
        public Dictionary<string, Passenger> SeatMap { get; set; } = new Dictionary<string, Passenger>();

        public string PlaneNumber { get; set; }
            public int FirstClassRows { get; set; }
            public int EconomyRows { get; set; }
            public int FirstClassPerRow { get; set; }
            public int EconomyPerRow { get; set; }
            public int TotalSeats => (FirstClassRows * FirstClassPerRow) + (EconomyRows * EconomyPerRow);
            public DateTime ArrivalTime { get; set; }
            public DateTime DepartureTime { get; set; }

            public List<string> EconomySeats { get; set; } = new List<string>();
            public List<string> FirstClassSeats { get; set; } = new List<string>();

        public Plane(string planeNumber, int firstClassRows, int economyRows, int firstClassPerRow, int economyPerRow, DateTime arrivalTime, List<Passenger> passengers)

        {
            PlaneNumber = planeNumber;  
                FirstClassRows = firstClassRows;
                EconomyRows = economyRows;
                FirstClassPerRow = firstClassPerRow;
                EconomyPerRow = economyPerRow;
                ArrivalTime = arrivalTime;
                Passengers = passengers ?? new List<Passenger>();
                DepartureTime = arrivalTime.AddMinutes(TotalSeats * 6);

                GenerateSeats();
            }

        private void GenerateSeats()
        {
            char[] rowLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            for (int row = 0; row < FirstClassRows; row++)
            {
                for (int seat = 1; seat <= FirstClassPerRow; seat++)
                {
                    FirstClassSeats.Add($"{rowLetters[row]}{seat}");
                }
            }

            for (int row = FirstClassRows; row < (FirstClassRows + EconomyRows); row++)
            {
                for (int seat = 1; seat <= EconomyPerRow; seat++)
                {
                    EconomySeats.Add($"{rowLetters[row]}{seat}");
                }   
            }
        }
        public Person GetPassengerBySeat(string seatNumber)
        {
            return SeatMap.TryGetValue(seatNumber, out var passenger) ? passenger : null;
        }
    }
    public class AirportManager
    {
        private double timeMultiplier = 9.0;
        private DateTime simulatedTime;
        private List<Plane> incomingPlanes = new List<Plane>();
        private Queue<Plane> pastPlanes = new Queue<Plane>();
        private Random random = new Random();

        public AirportManager(DateTime initialTime)
        {
            simulatedTime = initialTime;
            GeneratePlanes(5);
        }

        public void UpdateTime(DateTime newTime)
        {
            simulatedTime = newTime;
        }

        private void GeneratePlanes(int count)
        {
            DateTime lastDepartureTime = simulatedTime;
            for (int i = 0; i < count; i++)
            {
                Plane newPlane = CreateRandomPlane(lastDepartureTime);
                incomingPlanes.Add(newPlane);
                lastDepartureTime = newPlane.DepartureTime.AddMinutes(1);
            }
        }

        private Plane CreateRandomPlane(DateTime arrivalTime)
        {
            char letter = (char)random.Next('A', 'Z' + 1);
            string planeNumber = letter + random.Next(1000, 9999).ToString();
            int firstClassRows = random.Next(1, 4);
            int economyRows = random.Next(10, 20);
            int firstClassPerRow = random.Next(2, 4);
            int economyPerRow = random.Next(4, 6);

            Plane plane = new Plane(planeNumber, firstClassRows, economyRows, firstClassPerRow, economyPerRow, arrivalTime, new List<Passenger>());

            foreach (string seat in plane.FirstClassSeats.Concat(plane.EconomySeats))
            {
                Point position = new Point(random.Next(50, 200), random.Next(50, 300));
                Point scanner = new Point(300, 200);
                Point midExit = new Point(400, 200);
                Point exit = new Point(500, 200);
                Point police = new Point(100, 100);

            var passenger = PersonGenerator.CreateRandomPassenger
            (
                planeNumber,
                seat,
                enterNumber: 0,
                position: position,
                targetPosition: scanner,
                scannerPoint: scanner,
                midExitPoint: midExit,
                exitPoint: exit,
                policeRoomPoint: police
            );

                plane.Passengers.Add(passenger);
                plane.SeatMap[seat] = passenger;
            }

            return plane;
        }


        public void DepartPlane()
        {
            if (incomingPlanes.Count > 0)
            {
                Plane departingPlane = incomingPlanes[0];
                incomingPlanes.RemoveAt(0);
                pastPlanes.Enqueue(departingPlane);

                if (pastPlanes.Count > 10)
                {
                    pastPlanes.Dequeue();
                }

                Console.WriteLine($"✈ Літак {departingPlane.PlaneNumber} вилетів!");

                AddNewPlane();
            }
        }
        public void AddNewPlane()
        {
            DateTime newArrivalTime = incomingPlanes.Count > 0
                ? incomingPlanes.Last().DepartureTime.AddMinutes(1)
                : simulatedTime;

            Plane newPlane = CreateRandomPlane(newArrivalTime);
            incomingPlanes.Add(newPlane);
            Console.WriteLine($"✅ Додано новий літак {newPlane.PlaneNumber} з {newPlane.TotalSeats} місцями!");
        }

        public void MoveToPastPlanes(Plane plane)
        {
            incomingPlanes.Remove(plane);
            pastPlanes.Enqueue(plane);
        }
        public List<Plane> GetIncomingPlanes()
        {
            return incomingPlanes;
        }

        public List<Plane> GetPastPlanes()
        {
            return pastPlanes.ToList();
        }
    }
}