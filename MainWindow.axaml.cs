using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Linq;

namespace Zadanie1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_OnClick(object? sender, RoutedEventArgs e)
        {
            string city = City.Text;
            if (!string.IsNullOrWhiteSpace(city))
                Cities.Items.Add(city);

            DisplayPrice();
        }

        private void Country_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            string[] paths = {
                "avares://Zadanie1/Assets/australia.jpg",
                "avares://Zadanie1/Assets/chorwacja.jpg",
                "avares://Zadanie1/Assets/cypr.jpg",
                "avares://Zadanie1/Assets/dominikana.jpg",
                "avares://Zadanie1/Assets/dubaj.jpg",
                "avares://Zadanie1/Assets/egipt.jpg",
                "avares://Zadanie1/Assets/francja.jpg",
                "avares://Zadanie1/Assets/grecja.jpg",
                "avares://Zadanie1/Assets/hiszpania.jpeg",
                "avares://Zadanie1/Assets/indie.jpg",
                "avares://Zadanie1/Assets/bali.jpg",
                "avares://Zadanie1/Assets/kenia.jpg",
                "avares://Zadanie1/Assets/kuba.jpg",
                "avares://Zadanie1/Assets/malediwy.jpg",
                "avares://Zadanie1/Assets/malta.jpg",
                "avares://Zadanie1/Assets/maroko.jpg",
                "avares://Zadanie1/Assets/nowazelandia.jpg",
                "avares://Zadanie1/Assets/portugalia.jpg",
                "avares://Zadanie1/Assets/hawaje.jpeg",
                "avares://Zadanie1/Assets/tajlandia.jpg",
                "avares://Zadanie1/Assets/turcja.jpg",
                "avares://Zadanie1/Assets/usa.jpg",
                "avares://Zadanie1/Assets/wielkabrytania.jpg",
                "avares://Zadanie1/Assets/wietnam.jpg",
                "avares://Zadanie1/Assets/wlochy.jpg"
            };

            if (Country.SelectedIndex >= 0)
            {
                using var stream = AssetLoader.Open(new Uri(paths[Country.SelectedIndex]));
                ImgCountry.Source = new Bitmap(stream);
            }

            DisplayPrice();
        }

        private void Button_OnClick_v2(object? sender, RoutedEventArgs e)
        {
            string country = (Country.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Nie wybrano";
            string attractions = string.Join(", ",
                CheckboxPanel.Children.OfType<CheckBox>().Where(cb => cb.IsChecked == true).Select(cb => cb.Content?.ToString()));
            string transport = RadioGroup1.Children.OfType<RadioButton>().FirstOrDefault(rb => rb.IsChecked == true)?.Content?.ToString() ?? "Brak";
            string cities = string.Join(", ", Cities.SelectedItems.Cast<string>());
            string date = TravelDate.SelectedDate?.ToString("dd.MM.yyyy") ?? "Nie wybrano";

            ResultWindow resultWindow = new(country, attractions, transport, cities, date);
            resultWindow.Show();
            Close();
        }

        private void UpdatePrice(object? sender, RoutedEventArgs e)
        {
            DisplayPrice();
        }

        private void DisplayPrice()
        {
            PriceBlock.Text = $"Cena za tydzień: {CalculatePrice()} zł";
        }

        private int CalculatePrice()
        {
            int[] prices = { 7500, 2800, 2600, 6200, 6800, 3200, 4100, 3900, 3700, 4500, 5100, 4700, 4900, 8200, 3300, 3400, 8900, 4000, 7600, 5000, 3600, 7000, 4200, 4600, 4300 };
            int basePrice = Country.SelectedIndex >= 0 ? prices[Country.SelectedIndex] : 0;

            if (AttractionsMusea.IsChecked == true) basePrice += 200;
            if (AttractionsNationalParks.IsChecked == true) basePrice += 150;
            if (AttractionsMonuments.IsChecked == true) basePrice += 180;
            if (AttractionsRestaurants.IsChecked == true) basePrice += 120;
            if (AttractionsGalleries.IsChecked == true) basePrice += 160;
            if (AttractionsFestivals.IsChecked == true) basePrice += 300;

            if (Plane.IsChecked == true) basePrice += 500;
            else if (Car.IsChecked == true) basePrice += 300;
            else if (Train.IsChecked == true) basePrice += 200;
            else if (Ship.IsChecked == true) basePrice += 450;

            int[] cityExtraPrices = { 500, 300, 250, 450, 400, 350, 380, 360, 340, 370, 390, 410, 430, 600, 320, 310, 650, 330, 550, 370, 300, 580, 360, 340, 350 };
            basePrice += (Cities.SelectedItems?.Count ?? 0) * (Country.SelectedIndex >= 0 ? cityExtraPrices[Country.SelectedIndex] : 0);

            return basePrice;
        }
    }
}
