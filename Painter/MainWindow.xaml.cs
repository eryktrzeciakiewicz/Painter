﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Painter.EventSetups;
using Xceed.Wpf.Toolkit;

namespace Painter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Color CurrentColor { get; set; } = Colors.Black;
        public IEventSetup EventSetup { get; set; }
        public static Mode CurrentMode { get; set; } = Mode.None;

        public MainWindow()
        {
            InitializeComponent();
            ColorLabel.Background = new SolidColorBrush(CurrentColor);
        }



        private void CheckButton(object sender, RoutedEventArgs e)
        {
            UncheckButtons(this, e);
            Button b = (Button) sender;
            b.BorderBrush = new SolidColorBrush(Colors.Blue);
            b.BorderThickness = new Thickness(2);

        }

        private void UncheckButtons(object sender, RoutedEventArgs e)
        {
            var buttons = from Button b in SideButtons.Children select b;
            foreach (var b in buttons)
            {
                b.BorderBrush = null;
                b.BorderThickness = new Thickness(0);
            }
        }

        private void SetColorButton_OnClick(object sender, RoutedEventArgs e)
        {
            CheckButton(sender, e);
            Button senderBtn = (Button) sender;
            ColorPicker cp = new ColorPicker();
            cp.IsOpen = true;
            cp.SelectedColor = CurrentColor;
            MainCanvas.Children.Add(cp);
            Canvas.SetLeft(cp, 200);
            cp.SelectedColorChanged += (o, args) =>
            {
                CurrentColor = cp.SelectedColor.Value;
                MainCanvas.Children.Remove(cp);
                ChangeColorButton_OnClick(this, e);
            };
        }

        private void ChangeColorButton_OnClick(object sender, RoutedEventArgs e)
        {
            ColorLabel.Background = new SolidColorBrush(CurrentColor);
        }

        private void RectangleButton_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentMode = Mode.Rectangle;
            CheckButton(sender, e);
            UpdateEventSetup();
        }
        
        private void CircleButton_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentMode = Mode.Circle;
            CheckButton(sender, e);
            UpdateEventSetup();
        }

        private void PolygonButton_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentMode = Mode.Polygon;
            CheckButton(sender, e);
            UpdateEventSetup();
        }
        
        private void AssignEvents()
        {
            EventSetup.SetupEvents();
        }

        private void UpdateEventSetup()
        {
            switch (CurrentMode)
            {
                case Mode.Rectangle:
                    EventSetup = new RectangleEventSetup(MainCanvas);
                    break;

                case Mode.Circle:
                    EventSetup = new CircleEventSetup(MainCanvas);
                    break;

                case Mode.Polygon:
                    EventSetup = new PolygonEventSetup(MainCanvas);
                    break;

            }
            AssignEvents();
        }
    }
}