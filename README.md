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
     
    <Grid x:Name="RootGrid" MouseLeftButtonDown="RootGrid_OnMouseLeftButtonDown">
        <Canvas x:Name="CanvasPanel"
                    Grid.Column="1">
            <Border Height="{Binding ElementName=CanvasPanel, Path=ActualHeight}" Width="{Binding ElementName=CanvasPanel, Path=ActualWidth}" Background="LightBlue">
                <Image x:Name = "SourceImage"
                           Stretch="Fill"/>
            </Border>
        </Canvas>
    </Grid>
    
Code Behind: 
    
        public CroppingAdorner CroppingAdorner;

        private void RootGrid_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CroppingAdorner.CaptureMouse();
            CroppingAdorner.MouseLeftButtonDownEventHandler(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(CanvasPanel);
            CroppingAdorner = new CroppingAdorner(CanvasPanel);
            adornerLayer.Add(CroppingAdorner);
        }
