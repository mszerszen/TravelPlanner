using Avalonia.Controls;

namespace Zadanie1
{
    public partial class ResultWindow : Window
    {
        public ResultWindow(string country, string attractions, string transport, string cities, string date)
        {
            InitializeComponent();

            Summary.Text = $"Kraj: {country}\n" +
                           $"Atrakcje: {attractions}\n" +
                           $"Transport: {transport}\n" +
                           $"Miasta: {cities}\n" +
                           $"Data: {date}";
        }
    }
}