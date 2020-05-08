# UI-Cropping-Image
A simply UI cropping image library for WPF that use adroner layer. Some screenshots:

Interface of sample app

<img src="https://github.com/dmitryshelamov/UI-Cropping-Image/blob/master/cropped-demo.png" width="400">

Cropping result image

<img src="https://github.com/dmitryshelamov/UI-Cropping-Image/blob/master/cropped-result.png" width="400">

Cropping result image info

<img src="https://github.com/dmitryshelamov/UI-Cropping-Image/blob/master/cropped-result-info.png" width="400">

# Main Workflow:
1. Load image
2. Select cropping area
3. Crop and save image

# Main features:
 * Can draw/redraw cropping rectangle
 * Can move cropping rectangle
 * Can resize cropping rectangle
 * Shadow area outside of cropping rectangle
 * Show current size of cropping rectangle
 
 
 # Basic Setup
 XAML: 
 
 In order to properly handle image size, set `SizeToContent ="WidthAndHeight"` to window.
 There is a custom user control CropToolControl.xaml, that holds all necessary elements to handle crop operation. 
 Just add this `<croppingImageLibrary:CropToolControl Name="CropTool"></croppingImageLibrary:CropToolControl>`
 
 Here is a example in CroppingWindow.xaml
 ```
<Window x:Class="CroppingImageLibrary.SampleApp.CroppingWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:croppingImageLibrary="clr-namespace:CroppingImageLibrary;assembly=CroppingImageLibrary"
        mc:Ignorable="d"
        Title="CroppingWindow" ResizeMode="NoResize" WindowStartupLocation="Manual" SizeToContent ="WidthAndHeight">
    <croppingImageLibrary:CropToolControl Name="CropTool"></croppingImageLibrary:CropToolControl>
</Window>
 ```
Code Behind:     
    You need to pass image to user control, here is simple way describe in CroppingWindow.xaml.cs    
 ```
    public partial class CroppingWindow : Window
    {
        public CroppingWindow()
        {
            InitializeComponent();
        }

        public CroppingWindow(BitmapImage bitmapImage)
        {
            InitializeComponent();
            //  pass data to custom user control
            CropTool.SetImage(bitmapImage);
        }
    }
 ```
This library return only crop area, not cropped image itself. You need to perform crop operation manually. To get crop area, call CropTool.CropService.GetCroppedArea()
