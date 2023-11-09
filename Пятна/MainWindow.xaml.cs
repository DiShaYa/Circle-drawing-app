using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApplication2_canvas
{

    public partial class MainWindow : Window
    {
        Random rnd = new Random();

       

        public MainWindow()
        {
            InitializeComponent();
            ToolTipService.SetShowDuration(canvas, 3000);
            ToolTipService.SetInitialShowDelay(canvas, 300);
            ToolTipService.SetBetweenShowDelay(canvas, 1000);
        }

        private void drawELlipse(DrawingVisual visual, double x, double y, double radius)
        {
            using (DrawingContext dc = visual.RenderOpen())
            {
                
                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = Color.FromArgb(255, (byte)rnd.Next(150, 255), (byte)rnd.Next(150, 255), (byte)rnd.Next(150, 255));
                
                Pen p = new Pen(mySolidColorBrush, 2.0f);
                dc.DrawEllipse(mySolidColorBrush, p, new Point(x, y), radius, radius);

            }
        }


        private void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition((IInputElement)sender);
            if (e.ChangedButton == MouseButton.Left)
            {

                var dv = new DrawingVisualObject(rnd.Next(0, 1000), new string(Enumerable.Range(0, 5).Select(x => (char)rnd.Next(65, 90)).ToArray()));
                drawELlipse(dv, pos.X, pos.Y, rnd.Next((int)4.0f, (int)75.0f));
                canvas.AddVisual(dv);

            }
        }

        ToolTip tt = new System.Windows.Controls.ToolTip();
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point pos = e.GetPosition(canvas);
            DrawingVisualObject dvo = canvas.GetVisualObject(pos);

            if (dvo == null) { canvas.ToolTip = null; tt.IsOpen = false; return; };

            tt.Content = $"ID: {dvo.Id}\nName: {dvo.Name}";
            canvas.ToolTip = tt;
            tt.IsOpen = true;

        }
    }
}
