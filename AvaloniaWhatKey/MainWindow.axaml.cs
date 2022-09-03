using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace AvaloniaWhatKey
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            AddHandler(KeyDownEvent, OnKeyDown, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        }

        private void OnKeyDown(object? sender, KeyEventArgs e)
        {
            var gesture = 
                (e.KeyModifiers != KeyModifiers.None 
                    ? e.KeyModifiers.ToString().Replace(", ", "+") + "+" 
                    : "")
                + "Key." 
                + e.Key;
            GestureText.Text = gesture;
        }

        private async void CopyButton_OnClick(object? sender, RoutedEventArgs e)
        {
            if (Application.Current?.Clipboard is { } clipboard)
            {
                try
                {
                    await clipboard.SetTextAsync(GestureText.Text);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
    }
}