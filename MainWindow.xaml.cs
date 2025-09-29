using Microsoft.UI.Xaml;
using WindowSize;
using System;

// -----------------------------------------------------------------------------
// WindowSizeLimiter for WinUI 3
// A complete Window Size Limiter implementation for WinUI 3 apps.
//
// Repository: https://github.com/MEHDIMYADI
// Author: Mehdi Dimyadi
// License: MIT
// -----------------------------------------------------------------------------

namespace WindowSizeLimiterWinUI3
{
    public sealed partial class MainWindow : Window
    {
        private readonly WindowSizeLimiter _windowSizeLimiter;

        public MainWindow()
        {
            this.InitializeComponent();

            _windowSizeLimiter = new WindowSizeLimiter(this)
            {
                Enabled = true,
                EnableMaxLimits = false,
                MinWidth = 600,
                MinHeight = 400,
                MaxWidth = 1200,
                MaxHeight = 900
            };

            MinWidthBox.Text = _windowSizeLimiter.MinWidth.ToString();
            MinHeightBox.Text = _windowSizeLimiter.MinHeight.ToString();
            MaxWidthBox.Text = _windowSizeLimiter.MaxWidth.ToString();
            MaxHeightBox.Text = _windowSizeLimiter.MaxHeight.ToString();
            WindowLimit.IsChecked = _windowSizeLimiter.Enabled;
            EnableMaxCheck.IsChecked = false;

            MaxWidthBox.IsEnabled = EnableMaxCheck.IsChecked == true;
            MaxHeightBox.IsEnabled = EnableMaxCheck.IsChecked == true;
            ApplyMaxWidthBox.IsEnabled = EnableMaxCheck.IsChecked == true;
            ApplyMaxHeightBox.IsEnabled = EnableMaxCheck.IsChecked == true;
        }

        private void WindowLimit_Changed(object sender, RoutedEventArgs e)
        {
            bool enabled = WindowLimit.IsChecked == true;
            MinWidthBox.IsEnabled = enabled;
            MinHeightBox.IsEnabled = enabled;
            MaxWidthBox.IsEnabled = enabled;
            MaxHeightBox.IsEnabled = enabled;
            EnableMaxCheck.IsEnabled = enabled;
            ApplyMinWidthBox.IsEnabled = enabled;
            ApplyMinHeightBox.IsEnabled = enabled;

            if (EnableMaxCheck.IsChecked == true)
            {
                ApplyMaxWidthBox.IsEnabled = enabled;
                ApplyMaxHeightBox.IsEnabled = enabled;
            }
            else
            {
                ApplyMaxWidthBox.IsEnabled = false;
                ApplyMaxHeightBox.IsEnabled = false;
            }
            _windowSizeLimiter.Enabled = enabled;
        }

        private void EnableMaxCheck_Changed(object sender, RoutedEventArgs e)
        {
            bool enabled = EnableMaxCheck.IsChecked == true;
            MaxWidthBox.IsEnabled = enabled;
            MaxHeightBox.IsEnabled = enabled;
            _windowSizeLimiter.EnableMaxLimits = enabled;
            ApplyMaxWidthBox.IsEnabled = enabled;
            ApplyMaxHeightBox.IsEnabled = enabled;
        }

        private void ApplyMinWidth_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(MinWidthBox.Text, out int value))
                _windowSizeLimiter.MinWidth = value;
        }

        private void ApplyMinHeight_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(MinHeightBox.Text, out int value))
                _windowSizeLimiter.MinHeight = value;
        }

        private void ApplyMaxWidth_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(MaxWidthBox.Text, out int value))
                _windowSizeLimiter.MaxWidth = value;
        }

        private void ApplyMaxHeight_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(MaxHeightBox.Text, out int value))
                _windowSizeLimiter.MaxHeight = value;
        }

        private async void GitHub_Click(object sender, RoutedEventArgs e)
        {
            var uri = new Uri("https://github.com/MEHDIMYADI");
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
    }
}
