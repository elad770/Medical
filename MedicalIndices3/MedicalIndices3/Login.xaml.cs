using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using System.Windows.Shapes;

namespace MedicalIndices3
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private string path = @"users.txt";
        private string user_name;
        private string[] all_users;

        public Login()
        {
            InitializeComponent();
            all_users = File.ReadAllLines(path);
            this.Icon = new BitmapImage(new Uri(@"../../Images/Dapino-Medical-Medical-report.ico", UriKind.Relative));
            ImageBrush brush = new ImageBrush() { Stretch = Stretch.UniformToFill };
            brush.ImageSource = new BitmapImage(new Uri(@"../../Images/SignIn1.jpeg", UriKind.RelativeOrAbsolute));
            GridMain.Background = brush;
            // countChildern = this.st1.Children.Count;
        }

        public string GetUser()
        {
            return user_name;
        }


        private void ClearLabelError(object obj)
        {
            Label errorDelete = (obj as Label);
            if (errorDelete != null)
            {
                this.gridScoend.Children.Remove(errorDelete);
                // WinMain.Height -= 28;
            }
        }


        private void CreateLableError(int r, int c, string massage)
        {
            //Creates an error message, according to location and specific message
            Label labelError = new Label() { Foreground = Brushes.Red, FontSize = 14 };
            labelError.Content = massage;
            Grid.SetRow(labelError, r);
            Grid.SetColumn(labelError, c);
            //Grid.SetColumnSpan(labelError, 2);
            this.gridScoend.Children.Insert(0, labelError);
            // WinMain.Height += 28;
        }

        private bool CheckLength(string txt, int min, int max, int row, string massage = "")
        {
            if (txt.Trim().Length > max || txt.Trim().Length < min)
            {
                if (massage == "")
                    CreateLableError(row, 1, "The length must be between " + min + " to " + max + " characters");
                else
                    CreateLableError(row, 1, massage);
                return false;
            }
            return true;
        }

        private bool CheckLegal(string txt, bool mode, int r)
        {
            string massage = "";
            txt = txt.ToLower().Trim();

            if (mode)
            {
                if (txt.Count(Char.IsDigit) >= 3)
                {
                    CreateLableError(r, 1, "You can enter up to two digits");
                    return false;
                }
                massage = "Characters must contain English letters only";
                mode = !IsExistsSpecialchar(txt);
            }
            else
            {
                mode = IsExistsSpecialchar(txt) && txt.Any(char.IsLower) && txt.Any(char.IsDigit);
                massage = "The password must contain at least one letter," + Environment.NewLine + "one digit and one special character";
            }

            if (!mode)
            {
                CreateLableError(r, 1, massage);
                return false;
            }

            return true;
        }

        private bool IsExistsSpecialchar(string txt)
        {
            int i = 0;
            string token = "0123456789";
            txt = txt.ToUpper();
            while (i < txt.Length && (txt[i] >= 'A' && txt[i] <= 'Z' || token.Contains(txt[i])))
            {
                i++;
            }
            if (i == txt.Length)
            {
                return false;
            }
            return true;
        }


        private string ValidtionUser()
        {

            //Clear all massage error
            for (int i = 0; i < 3; i++)
            {
                ClearLabelError(this.gridScoend.Children[0]);
            }

            //Valdtion length and characters that must be rules for userName,txtPassword and txtId
            bool b1 = !CheckLength(txtUsername.Text, 6, 8, 4) || !CheckLegal(txtUsername.Text, true, 4);
            bool b2 = !CheckLength(txtPassword.Password, 8, 10, 7) || !CheckLegal(txtPassword.Password, false, 7);
            bool b3 = !CheckLength(txtId.Text, 9, 9, 10, "The length of the ID must be 9 digits");

            //another check In the id field whether 
            //the entered content is a number without additional characters
            if (!b3 && !Regex.IsMatch(txtId.Text.Trim(), @"^\d+$"))
            {
                CreateLableError(10, 1, "ID must contain only digits");
                b3 = true;
            }

            //In case all fields have been entered valid values
            if (!b1 && !b2 && !b3)
            {
                foreach (var user in all_users)
                {
                    string[] u = user.Split();
                    if (u[0] == txtUsername.Text.Trim() && u[1] == txtPassword.Password.Trim() && u[2] == txtId.Text)
                    {
                        return u[0];
                    }
                }
                //Displays a special message in case a particular field does not match 
                //the contents of a text file
                massageError.Visibility = Visibility.Visible;
            }
            return null;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {

            massageError.Visibility = Visibility.Hidden;
            user_name = ValidtionUser();
            if (user_name != null)
            {
                this.Close();
            }
            else
            {
                user_name = null;
            }
        }
    }
}