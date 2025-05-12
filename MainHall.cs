    using System;
    using System.Collections.Generic;
using System.Drawing;
using System.Linq;
    using System.Text;
    using System.Windows.Forms;
using System.Diagnostics;
using static ProjectAurora.Person;

namespace ProjectAurora
{
    public partial class MainHall : Form
        {
            private Plane currentPlane;
            private List<Person.Passenger> activePassengers = new List<Person.Passenger>();
            private AirportManager airportManager;
            private Label timeLabel = new Label();
            private Timer clockTimer = new Timer();
            private DateTime simulatedTime = DateTime.Now;
            private double timeMultiplier = 10;
            private Timer passengerSpawnTimer = new Timer();
            private Timer movementTimer = new Timer();


            private List<Person.Passenger> currentPassengers = new List<Person.Passenger>();
            private List<Button> passengerButtons = new List<Button>();
            private Queue<Person.Passenger> passengerQueue = new Queue<Person.Passenger>();
            private Point scannerPoint = new Point(1180, 520);
            private Point midExitPoint = new Point(950, 75); 
            private Point exitPoint = new Point(485, 50); 
            private Point policeRoomPoint = new Point(1400, 360); 


        public MainHall()
        {
            InitializeComponent();

            this.Load += Form1_Load;

            this.WindowState = FormWindowState.Maximized;
                airportManager = new AirportManager(simulatedTime);

                timeLabel.Font = new Font("Arial", 14, FontStyle.Bold);
                timeLabel.ForeColor = Color.White;
                timeLabel.BackColor = Color.Black;
                timeLabel.AutoSize = true;
                timeLabel.Location = new Point(20, 20);
                timeLabel.TextAlign = ContentAlignment.MiddleRight;
                this.Controls.Add(timeLabel);
                timeLabel.BringToFront();

                clockTimer.Interval = 1000 / (int)timeMultiplier;
                clockTimer.Tick += ClockTimer_Tick;
                clockTimer.Start();

                passengerSpawnTimer.Interval = 3620;
                passengerSpawnTimer.Start();
                passengerSpawnTimer.Tick += PassengerSpawnTimer_Tick;

                movementTimer.Interval = 50;
                movementTimer.Tick += MovementTimer_Tick;
                movementTimer.Start();

        }
        private void BringAllToFront()
        {
            foreach (var btn in passengerButtons)
            {
                btn.BringToFront();
            }

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button && ctrl != buttonExit && ctrl != buttonInfo && ctrl != buttonToPlane)
                {
                    ctrl.BringToFront();
                }
            }

            timeLabel.BringToFront();
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
            {
                simulatedTime = simulatedTime.AddSeconds(1 * timeMultiplier);
                timeLabel.Text = "📅 Час: " + simulatedTime.ToString("HH:mm:ss");
                UpdateFlights();
        }

        private void UpdateFlights()
        {
            DateTime now = simulatedTime;
            List<Plane> upcomingPlanes = airportManager.GetIncomingPlanes().ToList();

            foreach (var plane in upcomingPlanes.ToList())
            {
                if (plane.DepartureTime <= now)
                {
                    Console.WriteLine($"🔄 Літак {plane.PlaneNumber} має вилетіти!");

                    foreach (var btn in passengerButtons)
                    {
                        this.Controls.Remove(btn);
                    }
                    passengerButtons.Clear();
                    currentPassengers.Clear();
                    passengerQueue.Clear();
                    activePassengers.Clear();

                    airportManager.DepartPlane();

                    GeneratePassengersAndButtons();

                    passengerSpawnTimer.Start();

                    break;
                }
            }

            if (infoWindow != null && !infoWindow.IsDisposed)
            {
                UpdateFlightInfo();
            }
        }


        private Form infoWindow = null;
            private Label infoLabel = null;

            private void btnInformation_Click(object sender, EventArgs e)
            {
                if (infoWindow == null || infoWindow.IsDisposed)
                {
                    infoWindow = new Form
                    {
                        Text = "Інформація про рейси",
                        Size = new Size(400, 300),
                        StartPosition = FormStartPosition.CenterScreen
                    };
                    infoWindow.FormClosing += (s, args) => infoWindow = null;

                    infoLabel = new Label
                    {
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Font = new Font("Arial", 10),
                        AutoSize = false,
                        Padding = new Padding(10)
                    };

                    infoWindow.Controls.Add(infoLabel);
                    UpdateFlightInfo();
                    infoWindow.Show();
                }
                else
                {
                    infoWindow.BringToFront();
                }
            }

            private void UpdateFlightInfo()
            {
                if (infoWindow == null) return;

                StringBuilder info = new StringBuilder();

                info.AppendLine("📌 Наступні рейси:");
                foreach (var plane in airportManager.GetIncomingPlanes())
                {
                    info.AppendLine($"✈ {plane.PlaneNumber}: {plane.TotalSeats} місць");
                    info.AppendLine($"   Прибуття: {plane.ArrivalTime.ToShortTimeString()}");
                    info.AppendLine($"   Відправлення: {plane.DepartureTime.ToShortTimeString()}");
                }

                info.AppendLine("\n📌 Минулі рейси:");
                foreach (var plane in airportManager.GetPastPlanes())
                {
                    info.AppendLine($"✈ {plane.PlaneNumber}: {plane.TotalSeats} місць");
                    info.AppendLine($"   Прибуло: {plane.ArrivalTime.ToShortTimeString()}");
                    info.AppendLine($"   Відправлено: {plane.DepartureTime.ToShortTimeString()}");
                }

                infoLabel.Text = info.ToString();
                infoWindow.ClientSize = new Size(infoLabel.PreferredWidth + 20, infoLabel.PreferredHeight + 20);
            }


        private void Form1_Load(object sender, EventArgs e)
        {
            HallPic = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Properties.Resources.map_png
            };
            this.Controls.Add(HallPic);
            HallPic.SendToBack();
            GeneratePassengersAndButtons();
            AddPoliceOfficer();
        }
        private void AddPoliceOfficer()
        {
            var firstName = PersonGenerator.GetRandomFirstName();
            var lastName = PersonGenerator.GetRandomLastName();

            Point startPosition = new Point(10, 10);
            Point targetPosition = new Point(10, 10);

            Police officer = new Police(firstName, lastName, startPosition, targetPosition, scannerPoint, exitPoint, policeRoomPoint);

            Button officerButton = CreatePoliceOfficerButton(officer);

            officerButton.Size = new Size(45, 45);
            officerButton.Location = new Point(1250, 450);

            officerButton.Tag = officer;

            officerButton.Click += (s, e) =>
            {
                var clickedOfficer = (Police)((Button)s).Tag;
                MessageBox.Show($"👮‍♂️ Офіцер {clickedOfficer.Name} {clickedOfficer.Surname}\nВік: {clickedOfficer.Age} років", "Інформація про поліцейського");
            };

            this.Controls.Add(officerButton);
            BringAllToFront();
        }




        private Button CreatePoliceOfficerButton(Police officer)
        {
            Button policeButton = new Button();
            policeButton.Text = $"{officer.Name} {officer.Surname}\n{officer.Age} років";
            policeButton.BackColor = Color.Blue;
            policeButton.ForeColor = Color.White;
            policeButton.Width = 100;
            policeButton.Height = 50;
            policeButton.FlatStyle = FlatStyle.Flat;

            return policeButton;
        }

        private void GeneratePassengersAndButtons()
        {
            foreach (var btn in passengerButtons)
                this.Controls.Remove(btn);
            passengerButtons.Clear();
            currentPassengers.Clear();
            passengerQueue.Clear();

            currentPlane = airportManager.GetIncomingPlanes().FirstOrDefault();
            if (currentPlane == null) return;

            var rnd = new Random();

            if (currentPlane.Passengers == null || currentPlane.Passengers.Count == 0)
            {
                int totalSeats = currentPlane.TotalSeats;
                var allSeats = currentPlane.FirstClassSeats.Concat(currentPlane.EconomySeats).ToList();
                allSeats = allSeats.OrderBy(x => rnd.Next()).ToList();

                for (int i = 0; i < totalSeats && i < allSeats.Count; i++)
                {
                    Point entryPoint = new Point(750, 730);
                    Point targetPosition = scannerPoint;

                    var passenger = Person.PersonGenerator.CreateRandomPassenger(
                        currentPlane.PlaneNumber,
                        allSeats[i],
                        i + 1,
                        entryPoint,
                        targetPosition,
                        scannerPoint,
                        exitPoint,
                        policeRoomPoint,
                        policeRoomPoint
                    );

                    currentPlane.Passengers.Add(passenger);
                }
            }

            var shuffledPassengers = currentPlane.Passengers.OrderBy(p => rnd.Next()).ToList();

            foreach (var passenger in shuffledPassengers)
            {
                passengerQueue.Enqueue(passenger);
                activePassengers.Add(passenger);
            }

        }


        private void PassengerSpawnTimer_Tick(object sender, EventArgs e)
        {
            if (passengerQueue.Count > 0)
            {
                var passenger = passengerQueue.Dequeue();
                currentPassengers.Add(passenger);

                Point entryPoint = new Point(750, 730);

                passenger.Position = entryPoint;
                passenger.TargetPosition = scannerPoint;

                Button btn = new Button
                {
                    Size = new Size(40, 40),
                    BackColor = passenger.Color,
                    Location = entryPoint,
                    Tag = passenger,
                    Text = passenger.Name[0] + "." + passenger.Surname
                };


                btn.Click += (s, args) =>
                {
                    var p = (Person.Passenger)((Button)s).Tag;
                    MessageBox.Show(p.GetInfo(), "👤 Інформація про пасажира");
                };

                this.Controls.Add(btn);
                passengerButtons.Add(btn);
                btn.BringToFront();
            }
            else
            {
                passengerSpawnTimer.Stop();
            }
        }

        private void MovementTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < passengerButtons.Count; i++)
            {
                Button btn = passengerButtons[i];
                var passenger = (Person.Passenger)btn.Tag;

                Point current = btn.Location;
                Point target = passenger.TargetPosition;

                int dx = target.X - current.X;
                int dy = target.Y - current.Y;
                double distance = Math.Sqrt(dx * dx + dy * dy);

                if (!Utils.IsNear(current, target))
                {
                    passenger.Status = "У дорозі";
                    double step = 5;
                    int moveX = (int)(step * dx / distance);
                    int moveY = (int)(step * dy / distance);

                    btn.Location = new Point(current.X + moveX, current.Y + moveY);
                }
                else
                {
                    if (!passenger.IsWaiting && !passenger.IsChecked)
                    {
                        passenger.ArrivedAtScannerTime = DateTime.Now;
                        passenger.IsWaiting = true;
                        passenger.Status = "Проходить сканування";
                    }
                    else if (passenger.IsWaiting && !passenger.IsChecked)
                    {
                        TimeSpan waitingTime = DateTime.Now - passenger.ArrivedAtScannerTime;
                        if (waitingTime.TotalSeconds >= 2.45)
                        {
                            passenger.IsChecked = true;
                            passenger.IsWaiting = false;
                         
                            if (PersonGenerator.forbiddenLuggage.Contains(passenger.Luggage))
                            {
                                if (passenger.Status != "Порушник")
                                {
                                    passenger.TargetPosition = policeRoomPoint;
                                    btn.BackColor = Color.Red;
                                    passenger.Status = "Порушник";
                                }
                            }

                            else
                            {
                                passenger.TargetPosition = midExitPoint;
                                btn.BackColor = Color.Green;
                                passenger.Status = "У дорозі";
                            }
                        }
                    }
                    else
                    {
                        if (Utils.IsNear(btn.Location, policeRoomPoint))
                        {
                            passenger.Status = "Порушник";

                            if (currentPlane != null && currentPlane.SeatMap != null &&
                            !string.IsNullOrEmpty(passenger.SeatNumber) &&
                            !currentPlane.SeatMap.ContainsKey(passenger.SeatNumber))
                            {
                                currentPlane.SeatMap[passenger.SeatNumber] = passenger;
                            }

                            this.Controls.Remove(btn);
                            passengerButtons.RemoveAt(i);
                            i--;

                            continue;
                        }



                        else
                        {
                            if (!passenger.HasPassedIntermediatePoint)
                            {
                                if (Utils.IsNear(btn.Location, midExitPoint))
                                {
                                    passenger.HasPassedIntermediatePoint = true;
                                    passenger.TargetPosition = exitPoint;
                                }
                            }
                            else
                            {
                                if (Utils.IsNear(btn.Location, exitPoint))
                                {
                                    passenger.Status = "На борту";
                                    this.Controls.Remove(btn);
                                    passengerButtons.RemoveAt(i);
                                    i--;
                                }
                            }
                        }
                    }


                }
            }
        }



        private void buttonToPlane_Click(object sender, EventArgs e)
        {
            List<Plane> planes = airportManager.GetIncomingPlanes();

            if (planes.Count > 0)
            {
                Plane firstPlane = planes[0];
                List<Passenger> planePassengers = firstPlane.Passengers;
                PlaneModel planeForm = new PlaneModel(firstPlane);
                planeForm.Show();
            }
            else
            {
                MessageBox.Show("Немає доступних літаків у черзі.");
            }
         }

            private void HallPic_Resize(object sender, EventArgs e)
            {
                int newWidth = HallPic.Width;
                int newHeight = HallPic.Height;

                float btnWidth = 0.15f;
                float btnHeight = 0.07f;

                float btnToPlaneX = 0.05f;
                float btnToPlaneY = 0.05f;
                float btnInfoX = 0.8f;
                float btnInfoY = 0.05f;
                float btnExitX = 0.8f;
                float btnExitY = 0.88f;

                buttonToPlane.SetBounds((int)(newWidth * btnToPlaneX), (int)(newHeight * btnToPlaneY), (int)(newWidth * btnWidth), (int)(newHeight * btnHeight));
                buttonInfo.SetBounds((int)(newWidth * btnInfoX), (int)(newHeight * btnInfoY), (int)(newWidth * btnWidth), (int)(newHeight * btnHeight));
                  buttonExit.SetBounds((int)(newWidth * btnExitX), (int)(newHeight * btnExitY), (int)(newWidth * btnWidth), (int)(newHeight * btnHeight));
            }

            private void buttonExit_Click(object sender, EventArgs e)
            {
                this.Close();
            }

            private void buttonInfo_Click(object sender, EventArgs e)
            {
                btnInformation_Click(sender, e);
            }
        }
}