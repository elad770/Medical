using LibraryDiagnosis;
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
using System.Windows.Shapes;

namespace MedicalIndices3._2
{
    /// <summary>
    /// Interaction logic for DiagnosticWindow.xaml
    /// </summary>
    public partial class DiagnosticWindow : Window
    {
        //Patient Patient;
        public bool IsSave;
        public DiagnosticWindow(Patient pa, List<string[]> diagnostic)
        {
            InitializeComponent();
            //ImageBrush brush = new ImageBrush() { Stretch = Stretch.UniformToFill };
            //brush.ImageSource = new BitmapImage(new Uri(@"Main.jpeg", UriKind.RelativeOrAbsolute));
            //GridRoot.Background = brush;
            this.Icon = new BitmapImage(new Uri(@"../../Images/Dapino-Medical-Medical-report.ico", UriKind.Relative));
            DataContext = pa;
            tbGen.Text = pa.Gender ? "זכר" : "נקבה";

            int i = 0, j = 4;
            if (diagnostic != null)
            {
                Border[] bo = new Border[diagnostic.Count];
                totalResult.Text = "סה''כ  " + diagnostic.Count + " תוצאות";
                string fullTxet = ":המחלות שאובחנו למטופל";
                bool flag = true;
                foreach (var item in diagnostic)
                {

                    //Create all border 
                    bo[i] = new Border();
                    bo[i].Name = "border" + i;
                    bo[i].Style = (Style)FindResource("BorderStyle");
                    bo[i].Margin = new Thickness(20, 0, 20, 0);
                    bo[i].Child = new Grid();

                    // if(i==countMax[1]|| i == 0&&countMax[0]<2)
                    if (int.Parse(item[2]) == 1)
                    {
                        fullTxet = ":הבדיקות/טיפולים מומלצים למטופל";
                        flag = true;
                    }

                    if (flag)
                    {
                        GridRoot.Children.Add(CreateLabel(this.GridRoot, fullTxet, FontWeights.Bold, 40, j++));
                        flag = false;

                    }

                    ((Grid)bo[i].Child).Children.Add(CreateLabel(((Grid)bo[i].Child), " שם המחלה/בעיה -" + " " + item[0] + ", " + "להלן טיפול מומלץ", FontWeights.Normal, 40));
                    ((Grid)bo[i].Child).RowDefinitions.Add(new RowDefinition() { Height = new GridLength(90) });
                    ((Grid)bo[i].Child).Children.Add(CreateTextBox(((Grid)bo[i].Child), item[1]));

                    GridRoot.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(100) });

                    Grid.SetRow(bo[i], j);
                    GridRoot.Children.Add(bo[i]);
                    j += 2;
                    GridRoot.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10) });
                    i++;
                }

            }
            else
            {
                totalResult.Text = "המטופל בריא ללא מחלות ובעיות";
            }


            GridRoot.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40) });
            Button b1 = new Button();
            b1.Content = "שמירת פרטים";
            b1.Style = (Style)FindResource("btn-primary");
            b1.HorizontalAlignment = HorizontalAlignment.Center;
            b1.Click += new RoutedEventHandler(button_Click);
            Grid.SetRow(b1, j + 1);
            GridRoot.Children.Add(b1);
        }

        private TextBox CreateTextBox(Grid grid, string massage)
        {
            TextBox tb = new TextBox();
            tb.FlowDirection = FlowDirection.RightToLeft;
            tb.IsEnabled = false;
            tb.Text = massage;
            tb.Style = (Style)FindResource("textboxPasswordboxStyles");
            tb.Background = Brushes.White;
            tb.Margin = new Thickness(20, 0, 20, 50);
            Grid.SetRow(tb, 1);
            return tb;
        }


        private Label CreateLabel(Grid grid, string massage, FontWeight font, int someAdd, int r = 0)
        {
            Label la = new Label() { HorizontalAlignment = HorizontalAlignment.Right, FontWeight = font, Margin = new Thickness(0, 0, 20, 0) };
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(someAdd) });
            la.Style = (Style)FindResource("resTextLabel");
            la.Content = massage;
            Grid.SetRow(la, r);
            return la;
        }

        protected void button_Click(object sender, EventArgs e)
        {
            IsSave = true;
            this.Close();

        }

    }
}