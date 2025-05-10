using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Zadanie1;

public partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e) {
        ListBox Cities = this.Cities;
        TextBox City = this.City;
        
        string city = City.Text;
        Cities.Items.Add(city);
        
        DisplayPrice();
    }

    private void Country_OnSelectionChanged(object? sender, SelectionChangedEventArgs e) {

        string[] paths = {
            "avares://zadanie1/Assets/australia.jpg",
            "avares://zadanie1/Assets/chorwacja.jpg",
            "avares://zadanie1/Assets/cypr.jpg",
            "avares://zadanie1/Assets/dominikana.jpg",
            "avares://zadanie1/Assets/dubaj.jpg",
            "avares://zadanie1/Assets/egipt.jpg",
            "avares://zadanie1/Assets/francja.jpg",
            "avares://zadanie1/Assets/grecja.jpg",
            "avares://zadanie1/Assets/hiszpania.jpeg",
            "avares://zadanie1/Assets/indie.jpg",
            "avares://zadanie1/Assets/bali.jpg",
            "avares://zadanie1/Assets/kenia.jpg",
            "avares://zadanie1/Assets/kuba.jpg",
            "avares://zadanie1/Assets/malediwy.jpg",
            "avares://zadanie1/Assets/malta.jpg",
            "avares://zadanie1/Assets/maroko.jpg",
            "avares://zadanie1/Assets/nowazelandia.jpg",
            "avares://zadanie1/Assets/portugalia.jpg",
            "avares://zadanie1/Assets/hawaje.jpeg",
            "avares://zadanie1/Assets/tajlandia.jpg",
            "avares://zadanie1/Assets/turcja.jpg",
            "avares://zadanie1/Assets/usa.jpg",
            "avares://zadanie1/Assets/wielkabrytania.jpg",
            "avares://zadanie1/Assets/wietnam.jpg",
            "avares://zadanie1/Assets/wlochy.jpg"
        };
        Image imgbox = ImgCountry;

        using var stream = AssetLoader.Open(new Uri(paths[Country.SelectedIndex]));
        imgbox.Source = new Bitmap(stream);
        
        DisplayPrice();
    }

    private void Button_OnClick_v2(object? sender, RoutedEventArgs e) {
        ResultWindow window = new(
            (Country.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Nie wybrano",
            string.Join(", ", CheckboxPanel.Children.OfType<CheckBox>().Where(cb => cb.IsChecked == true).Select(cb => cb.Content.ToString())),
            RadioGroup1.Children.OfType<RadioButton>().FirstOrDefault(rb => rb.IsChecked == true)?.Content?.ToString(),
            string.Join(", ", Cities.SelectedItems.Cast<string>().ToArray())
            );
        window.Show();
        Close();
    }
    
    private int CalculatePrice() {
        int[] prices = {
            7500, 2800, 2600, 6200, 6800, 3200, 4100, 3900, 3700, 4500, 5100, 4700, 4900, 8200, 3300, 3400, 8900, 4000, 7600, 5000, 3600, 7000, 4200, 4600, 4300
        };
        int price = prices[Country.SelectedIndex];
        
        if (AttractionsMusea.IsChecked == true) {
            price += 200;
        }
        if (AttractionsNationalParks.IsChecked == true) {
            price += 150;
        }
        if (AttractionsMonuments.IsChecked == true) {
            price += 180;
        }
        if (AttractionsRestaurants.IsChecked == true) {
            price += 120;
        }
        if (AttractionsGalleries.IsChecked == true) {
            price += 160;
        }
        if (AttractionsFestivals.IsChecked == true) {
            price += 300;
        }
            
        if (Plane.IsChecked == true) {
            price += 500;
        }
        else if (Car.IsChecked == true) {
            price += 300;
        }
        else if (Train.IsChecked == true) {
            price += 200;
        }
        else if (Ship.IsChecked == true) {
            price += 450;
        }
        
        int[] cityExtraPrices = {
            500, 300, 250, 450, 400, 350, 380, 360, 340, 370, 390, 410, 430, 600, 320, 310, 650, 330, 550, 370, 300, 580, 360, 340, 350
        };
        price += (Cities.SelectedItems?.Count ?? 0) * cityExtraPrices[Country.SelectedIndex];
        
        return price;
    }

    private void DisplayPrice() {
        PriceBlock.Text = $"Cena za Tydzień: {CalculatePrice().ToString()}";
    }

    private void UpdatePrice(object? sender, RoutedEventArgs e) {
        DisplayPrice();
    }
}