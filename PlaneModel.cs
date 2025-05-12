using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static ProjectAurora.Person;

namespace ProjectAurora
{
    public partial class PlaneModel : Form
    {
        private Timer statusUpdateTimer;
        private Plane currentPlane;
        private List<Passenger> passengers;



        public PlaneModel(Plane plane)
        {
            InitializeComponent();

            this.currentPlane = plane;
            this.passengers = plane.Passengers;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.White;

            this.Load += PlaneModel_Load;
        }

        private void PlaneModel_Load(object sender, EventArgs e)
        {
            if (currentPlane != null)
            {
                UpdateSeats(currentPlane);
            }

            statusUpdateTimer = new Timer();
            statusUpdateTimer.Interval = 500; // кожні 500 мс
            statusUpdateTimer.Tick += StatusUpdateTimer_Tick;
            statusUpdateTimer.Start();

            tlpFirstClass.BringToFront();
            tlpEconomyClass.BringToFront();
        }
        private void StatusUpdateTimer_Tick(object sender, EventArgs e)
        {
            foreach (Control control in tlpFirstClass.Controls)
            {
                if (control is Button btn && btn.Text is string seatNumber &&
                    currentPlane.SeatMap.TryGetValue(seatNumber, out var passenger))
                {
                    btn.BackColor = GetColorByStatus(passenger.Status);
                }
            }

            foreach (Control control in tlpEconomyClass.Controls)
            {
                if (control is Button btn && btn.Text is string seatNumber &&
                    currentPlane.SeatMap.TryGetValue(seatNumber, out var passenger))
                {
                    btn.BackColor = GetColorByStatus(passenger.Status);
                }
            }
        }


        private Color GetColorByStatus(string status)
        {
            if (status == "На борту")
                return Color.Blue;
            else if (status == "Проходить сканування")
                return Color.Orange;
            else if (status == "Порушник")
                return Color.Red;
            else if (status == "У дорозі")
                return Color.LightGreen;
            else
                return Color.Gray;
        }




        private void SetupTableLayout(TableLayoutPanel tlp, int rows, int columns, List<string> seatLabels, Color seatColor)
        {
            if (tlp == null) return;

            tlp.Controls.Clear();
            tlp.RowCount = rows;
            tlp.ColumnCount = columns;

            tlp.RowStyles.Clear();
            tlp.ColumnStyles.Clear();

            for (int i = 0; i < rows; i++)
                tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / rows));

            for (int j = 0; j < columns; j++)
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / columns));

            Font seatFont = new Font("Arial", 10, FontStyle.Bold);


            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    int index = i * columns + j;
                    if (index >= seatLabels.Count) break;

                    string seatLabel = seatLabels[index];

                    Person passenger = currentPlane.SeatMap.TryGetValue(seatLabel, out var found) ? found : null;


                    Button seatButton = new Button
                    {
                        Text = seatLabel,
                        Dock = DockStyle.Fill,
                        Font = seatFont,
                        BackColor = passenger != null ? GetColorByStatus(passenger.Status) : seatColor,
                        Tag = seatLabel 
                    };

                    seatButton.Click += SeatButton_Click;

                    tlp.Controls.Add(seatButton, j, i);
                }
            }
        }


        public void UpdateSeats(Plane plane)
        {
            if (plane != null)
            {
                passengers = plane.Passengers;
                SetupTableLayout(tlpFirstClass, plane.FirstClassRows, plane.FirstClassPerRow, plane.FirstClassSeats, Color.Gold);
                SetupTableLayout(tlpEconomyClass, plane.EconomyRows, plane.EconomyPerRow, plane.EconomySeats, Color.LightGray);
            }
        }
        public void SetPlane(Plane plane)
        {
            currentPlane = plane;
            passengers = plane.Passengers;
            UpdateSeats(currentPlane);
        }


        private void SeatButton_Click(object sender, EventArgs e)
        {
            if (sender is Button clickedButton && clickedButton.Tag is string seatNumber &&
                currentPlane.SeatMap.TryGetValue(seatNumber, out var passenger))
            {
                string message = $"Пасажир: {passenger.Name} {passenger.Surname}\n" +
                                 $"Вік: {passenger.Age}\n" +
                                 $"Місце: {passenger.SeatNumber}\n" +
                                 $"Клас: {(currentPlane.FirstClassSeats.Contains(passenger.SeatNumber) ? "Перший клас" : "Економ")}\n" +
                                 $"Статус: {passenger.Status}";
                MessageBox.Show(message, "Інформація про пасажира", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Місце порожнє.");
            }
        }




        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            // Отримуємо новий розмір PictureBox
            int newWidth = pictureBox1.Width;
            int newHeight = pictureBox1.Height;

            // Пропорції відносно початкових розмірів 
            float firstClassX = 0.28f; 
            float firstClassY = 0.032f; 
            float firstClassWidth = 0.44f;  
            float firstClassHeight = 0.30f; 

            float economyClassX = 0.28f; 
            float economyClassY = 0.35f; 
            float economyClassWidth = 0.44f;  
            float economyClassHeight = 0.63f;

            float btnBackX = 0.83f;
            float btnBackY = 0.02f;

            float btnWidth = 0.15f;
            float btnHeight = 0.07f;
            buttonBack.SetBounds((int)(newWidth * btnBackX), (int)(newHeight * btnBackY), (int)(newWidth * btnWidth), (int)(newHeight * btnHeight));

            // Змінюємо розташування та розміри TableLayoutPanel відповідно до нового розміру PictureBox
            tlpFirstClass.SetBounds(
                (int)(newWidth * firstClassX),
                (int)(newHeight * firstClassY),
                (int)(newWidth * firstClassWidth),
                (int)(newHeight * firstClassHeight)
            );

            tlpEconomyClass.SetBounds(
                (int)(newWidth * economyClassX),
                (int)(newHeight * economyClassY),
                (int)(newWidth * economyClassWidth),
                (int)(newHeight * economyClassHeight)
            );
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}