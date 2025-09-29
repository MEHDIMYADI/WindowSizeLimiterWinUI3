# WindowSizeLimiter for WinUI 3

A complete Window Size management implementation for WinUI 3 applications. Since WinUI 3 doesn’t natively support minimum or maximum window size, this library provides a fully DPI-aware, configurable solution.

## 🎥 Please wait, the animation (GIF) will appear shortly
![WindowSizeLimiter](https://github.com/user-attachments/assets/a369870c-de55-436b-85a6-29ec1a7be8a6)

## ⚠️ Important Note

**WinUI 3 does NOT provide native Min/Max Window Size.** This implementation uses Win32 interop to handle window sizing and allows dynamic updates via your UI.

## Features

- 🔲 Min/Max Window Size (DPI-aware)
- ✅ Enable/disable window size limits at runtime
- ⬆️⬇️ Apply changes dynamically from TextBoxes or UI controls
- 🎛️ Flexible max size enabling
- 🔗 Easy integration with your existing WinUI 3 windows

## Installation

1. Add WindowSizeLimiter.cs to your WinUI 3 project.
2. Ensure you have the required dependencies:
   - Microsoft.UI.Xaml
   - Microsoft.WindowsAppSDK
   - WinRT.Interop (for WindowNative.GetWindowHandle)

## Quick Start

### 1. Initialize in MainWindow

```csharp
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

        // Initialize UI controls with default values
        MinWidthBox.Text = _windowSizeLimiter.MinWidth.ToString();
        MinHeightBox.Text = _windowSizeLimiter.MinHeight.ToString();
        MaxWidthBox.Text = _windowSizeLimiter.MaxWidth.ToString();
        MaxHeightBox.Text = _windowSizeLimiter.MaxHeight.ToString();
    }
}
```

### 2. Handle Enable/Disable Limits

```csharp
private void WindowLimit_Changed(object sender, RoutedEventArgs e)
{
    bool enabled = WindowLimit.IsChecked == true;
    _windowSizeLimiter.Enabled = enabled;

    MinWidthBox.IsEnabled = enabled;
    MinHeightBox.IsEnabled = enabled;
    MaxWidthBox.IsEnabled = enabled && EnableMaxCheck.IsChecked == true;
    MaxHeightBox.IsEnabled = enabled && EnableMaxCheck.IsChecked == true;
}
```

```csharp
private void EnableMaxCheck_Changed(object sender, RoutedEventArgs e)
{
    bool enabled = EnableMaxCheck.IsChecked == true;
    _windowSizeLimiter.EnableMaxLimits = enabled;

    MaxWidthBox.IsEnabled = enabled && WindowLimit.IsChecked == true;
    MaxHeightBox.IsEnabled = enabled && WindowLimit.IsChecked == true;
}
```

### 3. Apply Values Dynamically

```csharp
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
```

## ✅ Supported Platforms
- WinUI 3 Desktop Apps (Project Reunion / Windows App SDK)
- DPI-aware (handles scaling automatically)
- Windows 10 and Windows 11


### 3. Settings Integration

### Properties  

| Property                  | Type   | Description                                 |
|----------------------------|--------|---------------------------------------------|
| `Enabled`            | bool   | Enables or disables window size limits entirely.           |
| `EnableMaxLimits`             | bool | Enables or disables maximum width/height constraints. |
| `MinWidth`              | int | Minimum width of the window in pixels.     |
| `MinHeight`              | int | Minimum height of the window in pixels.     |
| `MaxWidth`              | int | Maximum width of the window in pixels (applied only if EnableMaxLimits is true).     |
| `MaxHeight`              | int | Maximum height of the window in pixels (applied only if EnableMaxLimits is true).     |


#### License
This code is provided as-is. Feel free to modify and use in your projects.

#### Contributing
Contributions are welcome! Please feel free to submit pull requests or open issues for bugs and feature requests.

🔗 [GitHub - MEHDIMYADI](https://github.com/MEHDIMYADI)
