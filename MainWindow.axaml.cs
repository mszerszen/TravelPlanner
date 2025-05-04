using Avalonia.Controls;
using Avalonia.Interactivity;

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
    }
}