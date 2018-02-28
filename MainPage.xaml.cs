using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Windows.Input;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net.Http;
using Windows.UI.Popups;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;
using System.Reflection;

/*
28-02-2018 Michael Kollmeyer
    TransportAPI moved to different server
       - adopted the class, request and deserialization

     */

namespace SmartHome
{
    public sealed partial class MainPage : Page
    {
        double deltaY;
        double ItmY_On1;
        double ItmY_On2;
        double ItmY_On3;
        double ItmY_On4;
        double ItmY_On5;
        double PntY_On;

        string s1, s2, s3;

        int[] y_rain_set = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] y_temp_set = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] y_cond_set = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        DispatcherTimer Timer1 = new DispatcherTimer();
        DispatcherTimer Timer2 = new DispatcherTimer();
        SolidColorBrush ColorRed    = new SolidColorBrush();
        SolidColorBrush ColorBlack  = new SolidColorBrush();
        SolidColorBrush ColorYellow = new SolidColorBrush();
        SolidColorBrush ColorLilac  = new SolidColorBrush();
        SolidColorBrush ColorRain   = new SolidColorBrush();
        SolidColorBrush ColorSnow   = new SolidColorBrush();
        
        

        public Point AsPoint(int x, int y)
        {
            Point tmpPoint = new Point(x, y);
            return tmpPoint;
        }

        private void Timer1_Tick(object sender, object e)
        {
            GetTransport();
        }

        private void Timer2_Tick(object sender, object e)
        {
            GetWeather();
        }
        
        public MainPage()
        {
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.f") + " - InitializeComponent();");
            this.InitializeComponent();
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.f") + " - Polyline Elements: " + TempLine.Points.Count());
            Timer1.Interval = new TimeSpan(0, 0, 1, 0, 0);
            Timer2.Interval = new TimeSpan(0, 1, 0, 0, 0);
            Timer1.Tick += Timer1_Tick;
            Timer2.Tick += Timer2_Tick;
            Timer1.Start();
            Timer2.Start();

            Mask_Canvas.PointerPressed  += TransportDrag;
            Mask_Canvas.PointerMoved    += TransportMove;
            Mask_Canvas.PointerReleased += TransportRelease;
        }
      
        private async void GetWeather()
        {
            ColorSnow.Color = Color.FromArgb(255, 255, 255, 255);
            ColorRain.Color = Color.FromArgb(255, 16, 155, 251);

            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.f") + " - Request Weather Data...");
            WebRequest   W_wrGETURL   = WebRequest.Create("http://api.wunderground.com/api/877361112ea5ac9f/geolookup/conditions/hourly/q/Switzerland/Winterthur.json");
           
            WebResponse  W_response   = await W_wrGETURL.GetResponseAsync();
            Stream       W_dataStream = W_response.GetResponseStream();
            StreamReader W_reader     = new StreamReader(W_dataStream);
            string       W_sResponse  = W_reader.ReadToEnd();
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.f") + " - ...Data received");
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.f") + " - Loading Weather Data Stream");
            WeatherRootObject WeatherObj = JsonConvert.DeserializeObject<WeatherRootObject>(W_sResponse);
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.f") + " - Parsing Weather Data Stream");

            Double[] temp_raw = new Double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            Double[] rain_raw = new Double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            Double vAvg, vScale;
            
            Label_Time1.Text = WeatherObj.hourly_forecast[3].FCTTIME.hour_padded + ":00";
            Label_Time2.Text = WeatherObj.hourly_forecast[6].FCTTIME.hour_padded + ":00";
            Label_Time3.Text = WeatherObj.hourly_forecast[9].FCTTIME.hour_padded + ":00";
            Label_Time4.Text = WeatherObj.hourly_forecast[12].FCTTIME.hour_padded + ":00";
            Label_Time5.Text = WeatherObj.hourly_forecast[15].FCTTIME.hour_padded + ":00";
            Label_Time6.Text = WeatherObj.hourly_forecast[18].FCTTIME.hour_padded + ":00";

            Label_Temp1.Text = String.Format("{0:N0}°C", WeatherObj.hourly_forecast[3].temp.metric);
            Label_Temp2.Text = String.Format("{0:N0}°C", WeatherObj.hourly_forecast[6].temp.metric);
            Label_Temp3.Text = String.Format("{0:N0}°C", WeatherObj.hourly_forecast[9].temp.metric);
            Label_Temp4.Text = String.Format("{0:N0}°C", WeatherObj.hourly_forecast[12].temp.metric);
            Label_Temp5.Text = String.Format("{0:N0}°C", WeatherObj.hourly_forecast[15].temp.metric);
            Label_Temp6.Text = String.Format("{0:N0}°C", WeatherObj.hourly_forecast[18].temp.metric);

            image1.Source = new BitmapImage(new Uri("ms-appx:///Assets/Weather/" + WeatherObj.hourly_forecast[3].fctcode + ".png"));
            image2.Source = new BitmapImage(new Uri("ms-appx:///Assets/Weather/" + WeatherObj.hourly_forecast[6].fctcode + ".png"));
            image3.Source = new BitmapImage(new Uri("ms-appx:///Assets/Weather/" + WeatherObj.hourly_forecast[9].fctcode + ".png"));
            image4.Source = new BitmapImage(new Uri("ms-appx:///Assets/Weather/" + WeatherObj.hourly_forecast[12].fctcode + ".png"));
            image5.Source = new BitmapImage(new Uri("ms-appx:///Assets/Weather/" + WeatherObj.hourly_forecast[15].fctcode + ".png"));
            image6.Source = new BitmapImage(new Uri("ms-appx:///Assets/Weather/" + WeatherObj.hourly_forecast[18].fctcode + ".png"));

            for (byte i = 0; i <= 21; i++)
            {
                temp_raw[i] = Convert.ToInt32(WeatherObj.hourly_forecast[i].temp.metric);
            } 
            

            vAvg = temp_raw.Average();
            vScale = 20 / (Math.Abs(temp_raw.Max()) - Math.Abs(temp_raw.Min()));

            for (byte i = 0; i <= 21; i++)
            {
                double x_old = TempLine.Points.ElementAt(i + 1).X;
                y_temp_set[i] = 120 - Convert.ToInt32(Convert.ToInt32(WeatherObj.hourly_forecast[i].temp.metric) * vScale);
                TempLine.Points.RemoveAt(i + 1); 
                TempLine.Points.Insert(i + 1, AsPoint(Convert.ToInt32(x_old), y_temp_set[i]));
            }

            Label_Temp1.SetValue(Canvas.TopProperty, (y_temp_set[3]  - 25));
            image1.SetValue     (Canvas.TopProperty, (y_temp_set[3]  - 70));
            Label_Temp2.SetValue(Canvas.TopProperty, (y_temp_set[6]  - 25));
            image2.SetValue     (Canvas.TopProperty, (y_temp_set[6]  - 70));
            Label_Temp3.SetValue(Canvas.TopProperty, (y_temp_set[9]  - 25));
            image3.SetValue     (Canvas.TopProperty, (y_temp_set[9]  - 70));
            Label_Temp4.SetValue(Canvas.TopProperty, (y_temp_set[12] - 25));
            image4.SetValue     (Canvas.TopProperty, (y_temp_set[12] - 70));
            Label_Temp5.SetValue(Canvas.TopProperty, (y_temp_set[15] - 25));
            image5.SetValue     (Canvas.TopProperty, (y_temp_set[15] - 70));
            Label_Temp6.SetValue(Canvas.TopProperty, (y_temp_set[18] - 25));
            image6.SetValue     (Canvas.TopProperty, (y_temp_set[18] - 70));

            Rect_Rain11.SetValue(Canvas.HeightProperty, Convert.ToInt32(WeatherObj.hourly_forecast[0].pop));  Rect_Rain11.SetValue(Canvas.TopProperty, 100 - Convert.ToInt32(WeatherObj.hourly_forecast[0].pop));
            Rect_Rain12.SetValue(Canvas.HeightProperty, Convert.ToInt32(WeatherObj.hourly_forecast[1].pop));  Rect_Rain12.SetValue(Canvas.TopProperty, 100 - Convert.ToInt32(WeatherObj.hourly_forecast[1].pop));
            Rect_Rain13.SetValue(Canvas.HeightProperty, Convert.ToInt32(WeatherObj.hourly_forecast[2].pop));  Rect_Rain13.SetValue(Canvas.TopProperty, 100 - Convert.ToInt32(WeatherObj.hourly_forecast[2].pop));
            Rect_Rain21.SetValue(Canvas.HeightProperty, Convert.ToInt32(WeatherObj.hourly_forecast[3].pop));  Rect_Rain21.SetValue(Canvas.TopProperty, 100 - Convert.ToInt32(WeatherObj.hourly_forecast[3].pop));
            Rect_Rain22.SetValue(Canvas.HeightProperty, Convert.ToInt32(WeatherObj.hourly_forecast[4].pop));  Rect_Rain22.SetValue(Canvas.TopProperty, 100 - Convert.ToInt32(WeatherObj.hourly_forecast[4].pop));
            Rect_Rain23.SetValue(Canvas.HeightProperty, Convert.ToInt32(WeatherObj.hourly_forecast[5].pop));  Rect_Rain23.SetValue(Canvas.TopProperty, 100 - Convert.ToInt32(WeatherObj.hourly_forecast[5].pop));
            Rect_Rain31.SetValue(Canvas.HeightProperty, Convert.ToInt32(WeatherObj.hourly_forecast[6].pop));  Rect_Rain31.SetValue(Canvas.TopProperty, 100 - Convert.ToInt32(WeatherObj.hourly_forecast[6].pop));
            Rect_Rain32.SetValue(Canvas.HeightProperty, Convert.ToInt32(WeatherObj.hourly_forecast[7].pop));  Rect_Rain32.SetValue(Canvas.TopProperty, 100 - Convert.ToInt32(WeatherObj.hourly_forecast[7].pop));
            Rect_Rain33.SetValue(Canvas.HeightProperty, Convert.ToInt32(WeatherObj.hourly_forecast[8].pop));  Rect_Rain33.SetValue(Canvas.TopProperty, 100 - Convert.ToInt32(WeatherObj.hourly_forecast[8].pop));
            Rect_Rain41.SetValue(Canvas.HeightProperty, Convert.ToInt32(WeatherObj.hourly_forecast[9].pop));  Rect_Rain41.SetValue(Canvas.TopProperty, 100 - Convert.ToInt32(WeatherObj.hourly_forecast[9].pop));
            Rect_Rain42.SetValue(Canvas.HeightProperty, Convert.ToInt32(WeatherObj.hourly_forecast[10].pop)); Rect_Rain42.SetValue(Canvas.TopProperty, 100 - Convert.ToInt32(WeatherObj.hourly_forecast[10].pop));
            Rect_Rain43.SetValue(Canvas.HeightProperty, Convert.ToInt32(WeatherObj.hourly_forecast[11].pop)); Rect_Rain43.SetValue(Canvas.TopProperty, 100 - Convert.ToInt32(WeatherObj.hourly_forecast[11].pop));
            Rect_Rain51.SetValue(Canvas.HeightProperty, Convert.ToInt32(WeatherObj.hourly_forecast[12].pop)); Rect_Rain51.SetValue(Canvas.TopProperty, 100 - Convert.ToInt32(WeatherObj.hourly_forecast[12].pop));
            Rect_Rain52.SetValue(Canvas.HeightProperty, Convert.ToInt32(WeatherObj.hourly_forecast[13].pop)); Rect_Rain52.SetValue(Canvas.TopProperty, 100 - Convert.ToInt32(WeatherObj.hourly_forecast[13].pop));
            Rect_Rain53.SetValue(Canvas.HeightProperty, Convert.ToInt32(WeatherObj.hourly_forecast[14].pop)); Rect_Rain53.SetValue(Canvas.TopProperty, 100 - Convert.ToInt32(WeatherObj.hourly_forecast[14].pop));
            Rect_Rain61.SetValue(Canvas.HeightProperty, Convert.ToInt32(WeatherObj.hourly_forecast[15].pop)); Rect_Rain61.SetValue(Canvas.TopProperty, 100 - Convert.ToInt32(WeatherObj.hourly_forecast[15].pop));
            Rect_Rain62.SetValue(Canvas.HeightProperty, Convert.ToInt32(WeatherObj.hourly_forecast[16].pop)); Rect_Rain62.SetValue(Canvas.TopProperty, 100 - Convert.ToInt32(WeatherObj.hourly_forecast[16].pop));
            Rect_Rain63.SetValue(Canvas.HeightProperty, Convert.ToInt32(WeatherObj.hourly_forecast[17].pop)); Rect_Rain63.SetValue(Canvas.TopProperty, 100 - Convert.ToInt32(WeatherObj.hourly_forecast[17].pop));
            
            // Current Weather
            image_cur.Source = new BitmapImage(new Uri("ms-appx:///Assets/Weather/big/" + WeatherObj.current_observation.icon+ ".png"));
            Label_CurTemp.Text = WeatherObj.current_observation.temp_c + "°C";
            Label_CurFeel.Text = "Feels like " + WeatherObj.current_observation.feelslike_c + "°C";
            Label_CurPerc.Text = "The weather is " + WeatherObj.current_observation.weather.ToLower() + " with a forecasted percipitation of " + WeatherObj.current_observation.precip_today_metric + "mm. " + "The wind blows from " + WeatherObj.current_observation.wind_dir.ToLower() + " direction with " + WeatherObj.current_observation.wind_kph + "kmh.";

            Int32[] rain_forecast = new Int32[] { Convert.ToInt32(WeatherObj.hourly_forecast[0].pop),
                                                  Convert.ToInt32(WeatherObj.hourly_forecast[1].pop),
                                                  Convert.ToInt32(WeatherObj.hourly_forecast[2].pop),
                                                  Convert.ToInt32(WeatherObj.hourly_forecast[3].pop),
                                                  Convert.ToInt32(WeatherObj.hourly_forecast[4].pop),
                                                  Convert.ToInt32(WeatherObj.hourly_forecast[5].pop),
                                                  Convert.ToInt32(WeatherObj.hourly_forecast[6].pop),
                                                  Convert.ToInt32(WeatherObj.hourly_forecast[7].pop),
                                                  Convert.ToInt32(WeatherObj.hourly_forecast[8].pop),
                                                  Convert.ToInt32(WeatherObj.hourly_forecast[9].pop),
                                                  Convert.ToInt32(WeatherObj.hourly_forecast[10].pop),
                                                  Convert.ToInt32(WeatherObj.hourly_forecast[11].pop)};

            Int32[] wind_forecast = new Int32[] { Convert.ToInt32(WeatherObj.hourly_forecast[0].wspd.metric),
                                                  Convert.ToInt32(WeatherObj.hourly_forecast[1].wspd.metric),
                                                  Convert.ToInt32(WeatherObj.hourly_forecast[2].wspd.metric),
                                                  Convert.ToInt32(WeatherObj.hourly_forecast[3].wspd.metric),
                                                  Convert.ToInt32(WeatherObj.hourly_forecast[4].wspd.metric),
                                                  Convert.ToInt32(WeatherObj.hourly_forecast[5].wspd.metric),
                                                  Convert.ToInt32(WeatherObj.hourly_forecast[6].wspd.metric),
                                                  Convert.ToInt32(WeatherObj.hourly_forecast[7].wspd.metric),
                                                  Convert.ToInt32(WeatherObj.hourly_forecast[8].wspd.metric),
                                                  Convert.ToInt32(WeatherObj.hourly_forecast[9].wspd.metric),
                                                  Convert.ToInt32(WeatherObj.hourly_forecast[10].wspd.metric),
                                                  Convert.ToInt32(WeatherObj.hourly_forecast[11].wspd.metric)};

            if (DateTime.Now.Hour       < 12) { s1 = "Good morning! "; } else { s1 = "Hi there! "; }
            if (rain_forecast.Max()     > 20)
                {
                s2 = "Stay dry, it might rain";
                if (wind_forecast.Average() > 10) { s3 = " and look out for flying cows, it's windy!"; } else { s3 = "."; }
                }
            else
                {
                s2 = "A dry day is forcasted, enjoy";
                if (wind_forecast.Average() > 10) { s3 = ". But look out for flying cows, it's windy!"; } else { s3 = "."; }
                }
            Label_CurPerc.Text = s1 + s2 + s3;
        }
        
        private async void GetTransport()
        {
            DateTime TimeBus2, TimeBus7;
            DateTime TimeNow;
            TimeSpan TimeDif;

            ScheduleObject BusSchedule = new ScheduleObject();

            Byte k, l, m, i;
            
            ColorRed.Color    = Color.FromArgb(255, 255,   0,   0);
            ColorBlack.Color  = Color.FromArgb(255, 255, 255, 255);
            ColorYellow.Color = Color.FromArgb(255, 200, 200,   0);
            ColorLilac.Color  = Color.FromArgb(255,   0, 120, 137);

            // Fill a string with API Transport Data
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.f") + " - Request Transport(2) Data...");            
            WebRequest T_wrGETURL = WebRequest.Create("https://timetable.search.ch/api/route.json?from=8573675&to=8506899&num=5&via=8573674");
            WebResponse T_response = await T_wrGETURL.GetResponseAsync();
            Stream T_dataStream = T_response.GetResponseStream();
            StreamReader T_reader = new StreamReader(T_dataStream);
            string T_sResponse2 = T_reader.ReadToEnd();
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.f") + " - ...Data received");

            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.f") + " - Request Transport(7) Data...");
            // Fill a string with API Transport Data
            T_wrGETURL = WebRequest.Create("https://timetable.search.ch/api/route.json?from=8590942&to=8506899&num=5&via=8590969");
            T_response = await T_wrGETURL.GetResponseAsync();
            T_dataStream = T_response.GetResponseStream();
            T_reader = new StreamReader(T_dataStream);
            string T_sResponse7 = T_reader.ReadToEnd();
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.f") + " - ...Data received");

            // Parse JSON Data
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.f") + " - Load Transport(2) Data Stream...");
            TransportRootObject TransportObj2 = JsonConvert.DeserializeObject<TransportRootObject>(T_sResponse2);
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.f") + " - ...OK");
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.f") + " - Load Transport(7) Data Stream...");
            TransportRootObject TransportObj7 = JsonConvert.DeserializeObject<TransportRootObject>(T_sResponse7);
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.f") + " - ...OK");

            List<ScheduleObject> ScheduleBus = new List<ScheduleObject>();
            k = 0;
            m = 0;
            for (l=0; l<=4; l++)
            {
                DateTime.TryParse(TransportObj2.connections[k].departure, out TimeBus2);
                DateTime.TryParse(TransportObj7.connections[m].departure, out TimeBus7);
                if (TimeBus2<=TimeBus7)
                {
                    ScheduleBus.Add(new ScheduleObject() { Number = TransportObj2.connections[k].legs[0].line, Time = TimeBus2 });
                    k++;
                }
                else
                {
                    ScheduleBus.Add(new ScheduleObject() { Number = TransportObj7.connections[k].legs[0].line, Time = TimeBus7 });
                    m++;
                }
                
                Debug.WriteLine("Bus" + ScheduleBus[l].Number + " at " + ScheduleBus[l].Time);
            }

            TimeNow = DateTime.Now;
            TimeDif = ScheduleBus[0].Time - TimeNow;

            if (TimeDif.Minutes < 7)
            {
                TimeDif = ScheduleBus[0].Time - TimeNow;
                if (ScheduleBus[0].Number == "2")
                   Bus1_rect.Fill = ColorRed;
                else if (ScheduleBus[0].Number == "7")
                   Bus1_rect.Fill = ColorLilac;
                else 
                   Bus1_rect.Fill = ColorYellow;
                Bus1_num.Text = ScheduleBus[0].Number;
                Bus1_time.Text = ScheduleBus[0].Time.ToString("HH:mm");
                Bus1_due.Text = "in " + TimeDif.Minutes.ToString() + "min";
                i = 0;
            }
            else
            {
                Bus1_rect.Fill = ColorBlack;
                Bus1_num.Text = "";
                Bus1_time.Text = "";
                Bus1_due.Text = "";
                i = 1;
            }

            TimeDif = ScheduleBus[1-i].Time - TimeNow;
            if (ScheduleBus[1-i].Number == "2")
                Bus2_rect.Fill = ColorRed;
            else if (ScheduleBus[1-i].Number == "7")
                Bus2_rect.Fill = ColorLilac;
            else
                Bus2_rect.Fill = ColorYellow;
            Bus2_num.Text = ScheduleBus[1-i].Number;
            Bus2_time.Text = ScheduleBus[1-i].Time.ToString("HH:mm");
            Bus2_due.Text = "in " + TimeDif.Minutes.ToString() + "min";

            TimeDif = ScheduleBus[2-i].Time - TimeNow;
            if (ScheduleBus[2 - i].Number == "2")
                Bus3_rect.Fill = ColorRed;
            else if (ScheduleBus[2 - i].Number == "7")
                Bus3_rect.Fill = ColorLilac;
            else
                Bus3_rect.Fill = ColorYellow;
            Bus3_num.Text = ScheduleBus[2-i].Number;
            Bus3_time.Text = ScheduleBus[2-i].Time.ToString("HH:mm");
            Bus3_due.Text = "in " + TimeDif.Minutes.ToString() + "min";

            TimeDif = ScheduleBus[3-i].Time - TimeNow;
            if (ScheduleBus[3 - i].Number == "2")
                Bus4_rect.Fill = ColorRed;
            else if (ScheduleBus[3 - i].Number == "7")
                Bus4_rect.Fill = ColorLilac;
            else
                Bus4_rect.Fill = ColorYellow;
            Bus4_num.Text = ScheduleBus[3-i].Number;
            Bus4_time.Text = ScheduleBus[3-i].Time.ToString("HH:mm");
            Bus4_due.Text = "in " + TimeDif.Minutes.ToString() + "min";

            TimeDif = ScheduleBus[4-i].Time - TimeNow;
            if (ScheduleBus[4 - i].Number == "2")
                Bus5_rect.Fill = ColorRed;
            else if (ScheduleBus[4 - i].Number == "7")
                Bus5_rect.Fill = ColorLilac;
            else
                Bus5_rect.Fill = ColorYellow;
            Bus5_num.Text = ScheduleBus[4-i].Number;
            Bus5_time.Text = ScheduleBus[4-i].Time.ToString("HH:mm");
            Bus5_due.Text = "in " + TimeDif.Minutes.ToString() + "min";
            
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.f") + " - Finished GetTransport()");
        }

        private void TransportDrag(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Input.PointerPoint ptrPt = e.GetCurrentPoint(Mask_Canvas);
            PntY_On  = ptrPt.Position.Y;
            ItmY_On1 = Convert.ToDouble(Bus1_Canvas.GetValue(Canvas.TopProperty));
            ItmY_On2 = Convert.ToDouble(Bus2_Canvas.GetValue(Canvas.TopProperty));
            ItmY_On3 = Convert.ToDouble(Bus3_Canvas.GetValue(Canvas.TopProperty));
            ItmY_On4 = Convert.ToDouble(Bus4_Canvas.GetValue(Canvas.TopProperty));
            ItmY_On5 = Convert.ToDouble(Bus5_Canvas.GetValue(Canvas.TopProperty));
        }

        private void TransportMove(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Input.PointerPoint ptrPt = e.GetCurrentPoint(Mask_Canvas);
            if (ptrPt.Properties.IsLeftButtonPressed)
            {
                deltaY = PntY_On - ptrPt.Position.Y;
                Bus1_Canvas.SetValue(Canvas.TopProperty, ItmY_On1 - deltaY);
                Bus2_Canvas.SetValue(Canvas.TopProperty, ItmY_On2 - deltaY);
                Bus3_Canvas.SetValue(Canvas.TopProperty, ItmY_On3 - deltaY);
                Bus4_Canvas.SetValue(Canvas.TopProperty, ItmY_On4 - deltaY);
                Bus5_Canvas.SetValue(Canvas.TopProperty, ItmY_On5 - deltaY);

                if (Convert.ToInt32(Bus1_Canvas.GetValue(Canvas.TopProperty)) > 0)
                {
                    Bus1_scale.ScaleX = 1 - (Math.Abs(70 - Convert.ToDouble(Bus1_Canvas.GetValue(Canvas.TopProperty))) / 160);
                    Bus1_scale.ScaleY = 1 - (Math.Abs(70 - Convert.ToDouble(Bus1_Canvas.GetValue(Canvas.TopProperty))) / 160);
                    if (Bus1_scale.ScaleX < 0.63) { Bus1_scale.ScaleX = 0.63; };
                    if (Bus1_scale.ScaleY < 0.63) { Bus1_scale.ScaleY = 0.63; };
                }
                if (Convert.ToInt32(Bus2_Canvas.GetValue(Canvas.TopProperty)) > 0)
                {
                    Bus2_scale.ScaleX = 1 - (Math.Abs(70 - Convert.ToDouble(Bus2_Canvas.GetValue(Canvas.TopProperty))) / 160);
                    Bus2_scale.ScaleY = 1 - (Math.Abs(70 - Convert.ToDouble(Bus2_Canvas.GetValue(Canvas.TopProperty))) / 160);
                    if (Bus2_scale.ScaleX < 0.63) { Bus2_scale.ScaleX = 0.63; };
                    if (Bus2_scale.ScaleY < 0.63) { Bus2_scale.ScaleY = 0.63; };
                }

                if (Convert.ToInt32(Bus3_Canvas.GetValue(Canvas.TopProperty)) > 0)
                {
                    Bus3_scale.ScaleX = 1 - (Math.Abs(70 - Convert.ToDouble(Bus3_Canvas.GetValue(Canvas.TopProperty))) / 160);
                    Bus3_scale.ScaleY = 1 - (Math.Abs(70 - Convert.ToDouble(Bus3_Canvas.GetValue(Canvas.TopProperty))) / 160);
                    if (Bus3_scale.ScaleX < 0.63) { Bus3_scale.ScaleX = 0.63; };
                    if (Bus3_scale.ScaleY < 0.63) { Bus3_scale.ScaleY = 0.63; };
                }
                if (Convert.ToInt32(Bus4_Canvas.GetValue(Canvas.TopProperty)) > 0)
                {
                    Bus4_scale.ScaleX = 1 - (Math.Abs(70 - Convert.ToDouble(Bus4_Canvas.GetValue(Canvas.TopProperty))) / 160);
                    Bus4_scale.ScaleY = 1 - (Math.Abs(70 - Convert.ToDouble(Bus4_Canvas.GetValue(Canvas.TopProperty))) / 160);
                    if (Bus4_scale.ScaleX < 0.63) { Bus4_scale.ScaleX = 0.63; };
                    if (Bus4_scale.ScaleY < 0.63) { Bus4_scale.ScaleY = 0.63; };
                }
                if (Convert.ToInt32(Bus5_Canvas.GetValue(Canvas.TopProperty)) > 0)
                {
                    Bus5_scale.ScaleX = 1 - (Math.Abs(70 - Convert.ToDouble(Bus5_Canvas.GetValue(Canvas.TopProperty))) / 160);
                    Bus5_scale.ScaleY = 1 - (Math.Abs(70 - Convert.ToDouble(Bus5_Canvas.GetValue(Canvas.TopProperty))) / 160);
                    if (Bus5_scale.ScaleX < 0.63) { Bus5_scale.ScaleX = 0.63; };
                    if (Bus5_scale.ScaleY < 0.63) { Bus5_scale.ScaleY = 0.63; };
                }

            }
        }

        private void TransportRelease(object sender, PointerRoutedEventArgs e)
        {
            Bus1_Canvas.SetValue(Canvas.TopProperty,  10);
            Bus2_Canvas.SetValue(Canvas.TopProperty,  70);
            Bus3_Canvas.SetValue(Canvas.TopProperty, 130);
            Bus4_Canvas.SetValue(Canvas.TopProperty, 190);
            Bus5_Canvas.SetValue(Canvas.TopProperty, 250);

            Bus1_scale.ScaleX = 0.63;
            Bus1_scale.ScaleY = 0.63;
            Bus2_scale.ScaleX = 1;
            Bus2_scale.ScaleY = 1;
            Bus3_scale.ScaleX = 0.63;
            Bus3_scale.ScaleY = 0.63;
            Bus4_scale.ScaleX = 0.63;
            Bus4_scale.ScaleY = 0.63;
            Bus5_scale.ScaleX = 0.63;
            Bus5_scale.ScaleY = 0.63;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("debug");
            GetWeather();
            GetTransport();
        }
    }
}
