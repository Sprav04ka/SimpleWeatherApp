using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Net;


namespace JustWeather
{
    public partial class MainPage : ContentPage
    {
        const string API = "c92451268034df636279a78a57dae97d";
        
        public MainPage()
        {
            InitializeComponent();
        }

        private async void getWeather_Clicked(object sender, EventArgs e)
        {
            
            if (userInput.Text != null )
            {
                string city = userInput.Text.Trim();
                if (city.Length < 2)
                {
                    await DisplayAlert("Error", "City name used to be bigger", "I get it");
                    return;
                }

                HttpClient client = new HttpClient();
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={API}&units=metric";
                try
                {
                    string response = await client.GetStringAsync(url);
                    var json = JObject.Parse(response);
                    string temp = json["main"]["temp"].ToString();
                    string pressure = json["main"]["pressure"].ToString();
                    string weather = json["weather"][0]["main"].ToString();
                    resultLable.Text = "Weather now: " + weather + "Temperature now: " + temp + "Pressure: " + pressure;
                }
                catch (HttpRequestException ex)
                {
                    if (ex. == HttpStatusCode.NotFound)
                    {
                        await DisplayAlert("Error", "City not found. Please enter a valid city name.", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Error", "Something went wrong. Please try again later.", "OK");
                    }
                }
            }
            else
            {
                await DisplayAlert("Error", "City name used to be bigger", "I get it");
                return;
            }

        }
    }
}
