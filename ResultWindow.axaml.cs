using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Zadanie1;

public partial class ResultWindow : Window {
    public ResultWindow(string Country, string Attractions, string CommuteType, string Cities) {
        InitializeComponent();
        
        Summary.Text = $"Kraj: {Country}\n" +
                       $"Atrakcje: {Attractions}\n" +
                       $"Środek Transportu: {CommuteType}\n" +
                       $"Miasta: {Cities}";
    }
}