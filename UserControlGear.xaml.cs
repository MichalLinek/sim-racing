using System;
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

namespace Gauge
{
    /// <summary>
    /// Interaction logic for UserControlGauge.xaml
    /// </summary>
    public partial class UserControlGear : UserControl
    {
        public UserControlGear()
        {
            InitializeComponent();

            //System.Windows.Shapes.Path myPath = new System.Windows.Shapes.Path();
            //myPath.Stroke = Brushes.Black;
            //myPath.Stretch = Stretch.UniformToFill;
            //myPath.ClipToBounds = false;

            //PathGeometry importGeometry = new PathGeometry();
            //PathFigure importFigure = new PathFigure();
            //importFigure.IsFilled = true;

            //importGeometry.Figures.Add(importFigure);
            //myPath.Data = importGeometry;

            //importFigure.StartPoint = new Point(100, 100);
            //importFigure.Segments.Add(new ArcSegment(new Point(100, 100), new Size(100, 100), 0, true, SweepDirection.Clockwise, true));

            //this.AddChild(myPath);
        }
    }
}
