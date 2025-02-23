using System.Windows;
using System.Net;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using Newtonsoft.Json;
using WeatherApp.JSON;
using System.Windows.Media.Imaging;
using WeatherApp.ViewModel;



namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PlotModel MyModel { get; private set; }
        View view = new View();
        List<Datum> ai = new List<Datum>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ai = await view.GetInfo();
            createModel();
            City.Text = view.cityname;
            DataContext = this;
        }

        public void createModel()
        {
            MyModel = new PlotModel();

            var lineSeries = new LineSeries();
            var weeklyData = new List<Tuple<DateTime, double>>();

            for (int i = 0; i < 7; i++)
            {
                string[] str = ai[i].valid_date.Split('-');
                weeklyData.Add(new Tuple<DateTime, double>(new DateTime(int.Parse(str[0]), int.Parse(str[1]), int.Parse(str[2])), ai[i].temp ));
            }

            // Fill data
            Des.Text = (ai[0].weather.description);
            string ali = ai[0].weather.icon;
            var uriSource = new Uri($"Images\\{ali}.png", UriKind.Relative);
            Weather_Image.Source = new BitmapImage(uriSource);
            Temp.Text = Math.Round(ai[0].temp ) + "°C";

            // Add data points to the series
            foreach (var dataPoint in weeklyData)
            {
                lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(dataPoint.Item1), dataPoint.Item2));
            }

            lineSeries.Color = OxyColor.Parse("#FFFFAD5B");
            lineSeries.Background = OxyColor.Parse("#FFFDF1C4");
            lineSeries.MouseDown += LineSeries_MouseDown;

            MyModel.Series.Add(lineSeries);

            // Create a DateTimeAxis for the X-axis
            var dateAxis = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "yyyy-MM-dd", // Format the date as needed
            };


            var dateAxis1 = new LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = (ai[0].min_temp ) - 3,
                Maximum = (ai[0].max_temp ) + 3,
                Title = "°C",


                // IntervalType = DateTimeIntervalType.Weeks, // Set interval to weeks
            };

            MyModel.Axes.Add(dateAxis);
            MyModel.Axes.Add(dateAxis1);
        }

        private void LineSeries_MouseDown(object? sender, OxyMouseDownEventArgs e)
        {
            if (e.HitTestResult != null && e.HitTestResult.Item != null)
            {
                DataPoint hitPoint = (DataPoint)e.HitTestResult.Item;

                // Convert the X value (which is a double representing the date) back to a DateTime
                DateTime date =  DateTimeAxis.ToDateTime(hitPoint.X);

                // Access the Y value (temperature)
                //double temperature = hitPoint.Y;

                //Find the datum that corresponds to the clicked point

                foreach (Datum datum in ai)
                {
                    if (DateTime.Parse(datum.valid_date).Date == date.Date)
                    {
                        Des.Text = (datum.weather.description);
                        //Icons\pencil.png
                        string ali = datum.weather.icon;
                        var uriSource = new Uri($"Images\\{ali}.png", UriKind.Relative);
                        Weather_Image.Source = new BitmapImage(uriSource);
                        Temp.Text = Math.Round(datum.temp ) + "°C";
                    }
                }
            }
        }
    }
}