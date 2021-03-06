﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Painter.EventSetups
{
    class SetColorEventSetup : IEventSetup
    {
        public Canvas Canvas { get; set; }
        private SetColorEventSetup() { }

        public SetColorEventSetup(Canvas c)
        {
            Canvas = c;
        }
        public void SetupEvents()
        {
            foreach (Shape sh in Canvas.Children)
            {
                sh.MouseLeftButtonDown += ChangeColor;
            }
        }

        public void ChangeColor(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.CurrentMode != Mode.SetColor) return;
            var sh = (Shape) sender;
            sh.Fill = new SolidColorBrush(MainWindow.CurrentColor);
        }

        public void OnModeChanged(object sender, RoutedEventArgs e)
        {
            foreach (Shape sh in Canvas.Children)
            {
                sh.MouseLeftButtonDown -= ChangeColor;
            }
        }
    }
}
