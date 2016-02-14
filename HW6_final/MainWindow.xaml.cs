using HW6_final.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HW6_final
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string abv;

        JsonAlmanac jsonAlmanacData;
        JsonConditions jsonContactsdata;
        JsonForecast jsonForecastData;

        Requests ar = new Requests();

        public MainWindow()
        {
            InitializeComponent();
            UpdateAwCmbChooseCountry();
            awCmbChooseCountry.SelectedIndex = 43;
            awTbCity.Text = "Moscow";
        }

        private void UpdateAwCmbChooseCountry()
        {
            using (FileStream fs = new FileStream(@"../../ISO_3166.txt", FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(fs);
                while (!sr.EndOfStream)
                {
                    string[] str = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    awCmbChooseCountry.Items.Add(String.Format("{0} - {1}", str[1], str[0]));

                }
                sr.Close();
                fs.Close();
            }
        }

        private async void awBtnSendRequest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((awCmbChooseCountry.SelectedItem != null) && (awTbCity != null))
                {
                    abv = String.Concat(awCmbChooseCountry.SelectedItem.ToString()[0], awCmbChooseCountry.SelectedItem.ToString()[1]);

                    jsonAlmanacData = await ar.AlmanacQuery(abv, awTbCity.Text);
                    jsonContactsdata = await ar.ConditionsQuery(abv, awTbCity.Text);
                    jsonForecastData = await ar.Forecast3Query(abv, awTbCity.Text);

                    await Task.Factory.StartNew(() => UpdateUIAlmanac());
                    await Task.Factory.StartNew(() => UpdateUIConditions());
                    await Task.Factory.StartNew(() => UpdateUIForecast());
                }
                else
                    MessageBox.Show("Choose country and fill city!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                throw new WebException();
            }
        }

        private Task UpdateUIAlmanac()
        {
            return Task.Factory.StartNew(() =>
            {
                if (jsonAlmanacData.almanac != null)
                {
                    Dispatcher.Invoke(() =>
                    {
                        awTb1_1.Text = jsonAlmanacData.almanac.highestTemp.normal.c.ToString();
                        awTb1_2.Text = jsonAlmanacData.almanac.highestTemp.record.c.ToString();
                        awTb1_3.Text = jsonAlmanacData.almanac.highestTemp.year.ToString();

                        awTb2_1.Text = jsonAlmanacData.almanac.lowestTemp.normal.c.ToString();
                        awTb2_2.Text = jsonAlmanacData.almanac.lowestTemp.record.c.ToString();
                        awTb2_3.Text = jsonAlmanacData.almanac.lowestTemp.year.ToString();
                    });
                }
                else
                    MessageBox.Show("Web-service has returned error message! Possibly, city-field is filled incorrectly:(", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            });
        }

        private Task UpdateUIConditions()
        {
            return Task.Factory.StartNew(() =>
            {
                if (jsonContactsdata.conditions != null)
                {
                    Dispatcher.Invoke(() =>
                    {
                        awTb3_1.Text = jsonContactsdata.conditions.temp;
                        awTb3_2.Text = jsonContactsdata.conditions.humidity;
                        awTb3_3.Text = jsonContactsdata.conditions.weather;
                        awTb3_4.Text = jsonContactsdata.conditions.windSpeed + " from the " + jsonContactsdata.conditions.windDirection;
                        awTb3_5.Text = jsonContactsdata.conditions.visibility;
                        awTb3_6.Text = jsonContactsdata.conditions.idStation;
                    });
                }
            });
        }

        private Task UpdateUIForecast()
        {
            ImageDownload downloader = new ImageDownload();

            return Task.Factory.StartNew(async () =>
            {
                await Dispatcher.Invoke(async () =>
                {
                    awGb1.Header = jsonForecastData.forecast.txt_forecast.forecastDay[0].day;
                    awTb4_1.Text = jsonForecastData.forecast.txt_forecast.forecastDay[0].description;
                    awImg2.Source = await downloader.DownloadImageTaskAsync(jsonForecastData.forecast.txt_forecast.forecastDay[0].icon);

                    awGb2.Header = jsonForecastData.forecast.txt_forecast.forecastDay[1].day;
                    awTb4_2.Text = jsonForecastData.forecast.txt_forecast.forecastDay[1].description;
                    awImg1.Source = await downloader.DownloadImageTaskAsync(jsonForecastData.forecast.txt_forecast.forecastDay[0].icon);

                    awGb3.Header = jsonForecastData.forecast.txt_forecast.forecastDay[2].day;
                    awTb4_3.Text = jsonForecastData.forecast.txt_forecast.forecastDay[2].description;
                    awImg3.Source = await downloader.DownloadImageTaskAsync(jsonForecastData.forecast.txt_forecast.forecastDay[0].icon);

                    awGb4.Header = jsonForecastData.forecast.txt_forecast.forecastDay[3].day;
                    awTb4_4.Text = jsonForecastData.forecast.txt_forecast.forecastDay[3].description;
                    awImg4.Source = await downloader.DownloadImageTaskAsync(jsonForecastData.forecast.txt_forecast.forecastDay[0].icon);

                    awGb5.Header = jsonForecastData.forecast.txt_forecast.forecastDay[4].day;
                    awTb4_5.Text = jsonForecastData.forecast.txt_forecast.forecastDay[4].description;
                    awImg5.Source = await downloader.DownloadImageTaskAsync(jsonForecastData.forecast.txt_forecast.forecastDay[0].icon);

                    awGb6.Header = jsonForecastData.forecast.txt_forecast.forecastDay[5].day;
                    awTb4_6.Text = jsonForecastData.forecast.txt_forecast.forecastDay[5].description;
                    awImg6.Source = await downloader.DownloadImageTaskAsync(jsonForecastData.forecast.txt_forecast.forecastDay[0].icon);
                });
            });

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string str1 = "This application shows information about the weather. It allows user to know weather in any city of the world. ";
            string str2 = "For this user should pick a country and then type city name. Basing on API of wunderground.com, this app ";
            string str3 = "shows the data in three blocks: the most remarkable indeces, weather at this moment and forecast for 3 days.";
            MessageBox.Show(str1+str2+str3, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
