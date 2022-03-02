using LibraryDiagnosis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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

namespace MedicalIndices3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int countChildren;
        string path = @"DiseasesHebrew.txt";
        List<TextBox> ls;
        Patient pa;

        public MainWindow()
        {
            InitializeComponent();

            ls = new List<TextBox>();
            countChildren = this.GridRoot.Children.Count;
            //Login login = new Login();
            // login.ShowDialog();

            // string UserAns = login.GetUser();

            // if (UserAns != null)
            // {

            //SystemWindow sys = new SystemWindow(UserAns);
            //sys.Show();

            List<ViewModelComboAge> lc = new List<ViewModelComboAge>();

            for (int i = 0; i < 100; i++)
            {
                lc.Add(new ViewModelComboAge() { Value = i });
            }

            commboxAge.ItemsSource = lc;
            commboxAge.SelectedValuePath = "Content";
            pa = new Patient();
            this.DataContext = pa;
            //ImageBrush brush = new ImageBrush() { Stretch = Stretch.UniformToFill };
            //brush.ImageSource = new BitmapImage(new Uri(@"Main.jpeg", UriKind.RelativeOrAbsolute));
            //GridRoot.Background = brush;
            AddToolTip();
            this.Icon = new BitmapImage(new Uri(@"../../Images/Dapino-Medical-Medical-report.ico", UriKind.Relative));
            // tbUser.Text = UserAns;
            //  }
            // else
            // {
            //     this.Close();
            // }
        }

        private void AddToolTip()
        {
            //Add ToolTip to all TextBox of test
            List<TextBox> ltb = this.GridRoot.Children.OfType<TextBox>().ToList();
            PropertyInfo[] properties = pa.GetType().GetProperties();
            for (int i = 2; i < ltb.Count; i++)
            {
                DescriptionAttribute da = properties[i].GetCustomAttributes(typeof(DescriptionAttribute), false).Cast<DescriptionAttribute>().SingleOrDefault();
                ltb[i].ToolTip = new ToolTip() { Content = da.Description, Style = this.FindResource(typeof(ToolTip)) as Style, FontSize = 10 };
                Canvas.SetTop(ltb[i].ToolTip as UIElement, 10);
                ltb[i].TextChanged += TxtAuto_TextChanged;
            }

        }

        private void ShowErrors(int r, int c, string massage, Thickness margin)
        {
            //Create lables of erros
            Label labelError = new Label() { Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF2929")), Margin = margin };
            labelError.Style = (Style)FindResource("resTextLabel");
            Grid.SetColumn(labelError, c);
            Grid.SetRow(labelError, r);
            Grid.SetColumnSpan(labelError, 2);
            labelError.Content = massage;
            this.GridRoot.Children.Add(labelError);
        }




        private void TxtAuto_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextBox txt = (sender as TextBox);

            if (txt.Text != "")
            {
                if (ls.Count != 0)
                {
                    ls.Remove(txt);
                }
                try
                {
                    if (txt.Text.Contains("-"))
                    {
                        ls.Add(txt);
                        return;
                    }

                    double.Parse(txt.Text);
                }
                catch
                {
                    ls.Add(txt);
                }
            }

        }

        private void CBoxName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.borderPre != null)
            {
                if (cBoxGender.SelectedIndex == 0)
                {
                    borderPre.Visibility = Visibility.Hidden;
                    pregnancyNo.IsChecked = true;
                }
                else
                {
                    borderPre.Visibility = Visibility.Visible;
                }
            }
        }

        private void ButEdit_Click(object sender, RoutedEventArgs e)
        {
            ClearMassage();
            if (!IsTextBoxEmpty())
            {
                int r, c;
                bool flag = false;
                this.lab1Error.Visibility = Visibility.Hidden;


                if (ls.Count != 0)
                {
                    flag = true;
                    for (int i = 0; i < ls.Count; i++)
                    {

                        c = Grid.GetColumn(ls[i]);
                        r = Grid.GetRow(ls[i]) + 1;
                        if (ls[i].Text[0] == '-' && !ls[i].Text.Any(char.IsLetter))
                        {
                            ShowErrors(r, c, "The field must be a positive value", ls[i].Margin);
                        }
                        else
                        {
                            ShowErrors(r, c, "The field must be a number", ls[i].Margin);
                        }
                    }
                }


                if (!Regex.IsMatch(tName.Text.Replace(" ", ""), "^[a-zA-Zא-ת]+$"))
                {
                    flag = true;
                    ShowErrors(Grid.GetRow(this.tName), Grid.GetColumn(this.tName), "The field must contain only letters", new Thickness(tName.Margin.Left, 30, 0, 0));
                }

                r = Grid.GetRow(this.tId);
                c = Grid.GetColumn(this.tId);
                if (this.tId.Text.Length != 9 && this.tId.Text != "")
                {
                    flag = true;
                    ShowErrors(r, c, "The field Must be 9 in length", new Thickness(tId.Margin.Left, 30, 0, 0));
                }
                else if (!Regex.IsMatch(tId.Text, @"^\d+$"))
                {
                    flag = true;
                    ShowErrors(r, c, "The field must be a number", new Thickness(tId.Margin.Left, 30, 0, 0));
                }

                if (flag)
                {
                    return;
                }

                AddDataPatient();
                Diagnostic dia = new Diagnostic(pa, path);
                List<string[]> diagnostic = dia.AllTests();
                DiagnosticWindow win = new DiagnosticWindow(pa, diagnostic);
                this.Visibility = Visibility.Hidden;
                win.ShowDialog();
                if (win.IsSave)
                {
                    pa.SaveToXML(diagnostic);
                    ClearAll();
                }
                this.Visibility = Visibility.Visible;
            }
        }

        private void ClearMassage()
        {
            //Clear all massage error
            while (this.GridRoot.Children.Count != countChildren)
            {
                this.GridRoot.Children.RemoveAt(this.GridRoot.Children.Count - 1);
            }
        }

        private void ClearAll()
        {
            //Clear and resets all control in screen
            List<TextBox> ltb = this.GridRoot.Children.OfType<TextBox>().ToList();
            var temp = ltb.Where(tb => tb.Name != "tName" && tb.Name != "tId").ToList();
            temp.ForEach(tb => tb.TextChanged -= TxtAuto_TextChanged);
            ltb.ForEach(i => i.Clear());
            temp.ForEach(tb => tb.TextChanged += TxtAuto_TextChanged);
            reSmokingNo.IsChecked = reHeatNo.IsChecked = reDiarrheayNo.IsChecked = pregnancyNo.IsChecked = true;
            commboxRegion.SelectedIndex = commboxAge.SelectedIndex = cBoxGender.SelectedIndex = 0;
            ClearMassage();
            this.DataContext = pa = new Patient();
        }
        private void AddDataPatient()
        {

            bool? isPre = null;
            if (gridRePre.IsVisible)
            {
                isPre = pregnancyYes.IsChecked;
            }
            string selectRegion = ((ComboBoxItem)commboxRegion.SelectedItem).Content.ToString();
            bool gen = ((ComboBoxItem)cBoxGender.SelectedItem).Content.ToString() == "Male";
            pa.Age = commboxAge.SelectedIndex;
            pa.Gender = gen;
            pa.IsPregnant = isPre;
            pa.IsSmoking = (bool)reSmokingYes.IsChecked;
            pa.IsDiarrhea = (bool)reDiarrheayYes.IsChecked;
            pa.IsHeat = (bool)reHeatYes.IsChecked;
            pa.Region = (Region)Enum.Parse(typeof(Region), selectRegion, true);

        }

        public bool IsTextBoxEmpty()
        {
            foreach (TextBox tv in this.GridRoot.Children.OfType<TextBox>())
            {

                if (tv.Text == "")
                {
                    lab1Error.Visibility = Visibility.Visible;
                    return true;
                }

            }
            return false;
        }

        private void ButClear_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
            lab1Error.Visibility = Visibility.Hidden;
        }
    }

    public class ViewModelComboAge
    {
        public int Value { get; set; }
    }
}