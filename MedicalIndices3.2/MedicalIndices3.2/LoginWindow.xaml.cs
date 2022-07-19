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
using USLogin;
namespace MedicalIndices3._2
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
       
        public User _User { set; get; }
       
        private string path = @"users.txt";
        private string user_name;
        private string[] all_users;

        public LoginWindow()
        {
       
            InitializeComponent();
            all_users = File.ReadAllLines(path);
            this.Icon = new BitmapImage(new Uri(@"../../Images/Dapino-Medical-Medical-report.ico", UriKind.Relative));
            ImageBrush brush = new ImageBrush() { Stretch = Stretch.UniformToFill };
            brush.ImageSource = new BitmapImage(new Uri(@"../../Images/SignIn.jpg", UriKind.RelativeOrAbsolute));
            this.Background = brush;
            UserLogin usl = new UserLogin("Resources/AllResource.xaml", _User =new User());
            //string[] namesUI = { "txtUsername", "txtPassword", "txtId", "massageError" };
            //for (int i = 0; i < namesUI.Length; i++)
            //{
            //    Ui[i] = us.FindName(namesUI[i]) as UIElement;
            //}
            GridMain.Children.Add(usl);
            usl.UserControlClicked += UserControlClicked;
        }

        //private void ClearLabelError(/*object obj*/)
        //{
        //    _User = new User();
        //    //Label errorDelete = (obj as Label);
        //    //if (errorDelete != null)
        //    //{
        //    //    errorDelete.Content = "";
        //    //    //(us.Content as Grid).Children.Remove(errorDelete);
        //    //}
        //}

        private void CreateLableError(int r, string massage)
        {

            //Creates an error message, according to location and specific message
             int index = (r * 3) % 4;
             this._User.Massages[index] = massage;
            
            /* usl.InsertMassage(index);*/
            /*Label labelError = new Label() { Foreground = Brushes.Red, FontSize = 14 };
            labelError.Content = massage;
            Grid.SetRow(labelError, r);
            Grid.SetColumn(labelError, c);

            (us.Content as Grid).Children.Insert(0, labelError);*/

        }

        private bool CheckLength(string txt, int min, int max, int row, string massage = "")
        {
            if (txt.Trim().Length > max || txt.Trim().Length < min)
            {
                if (massage == "")
                    CreateLableError(row, "The length must be between " + min + " to " + max + " characters");
                else
                    CreateLableError(row, massage);
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
                    CreateLableError(r, "You can enter up to two digits");
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
                CreateLableError(r, massage);
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
            _User.Massages[0] = _User.Massages[1] = _User.Massages[2] = null;
           
            //Valdtion length and characters that must be rules for userName,txtPassword and txtId
            bool b1 = !CheckLength(_User.UserName, 6, 8, 4) || !CheckLegal(_User.UserName, true, 4);
            bool b2 = !CheckLength(_User.Password, 8, 10, 7) || !CheckLegal(_User.Password, false, 7);
            bool b3 = !CheckLength(_User.Id, 9, 9, 10, "The length of the ID must be 9 digits");

            //another check In the id field whether 
            //the entered content is a number without additional characters
            if (!b3 && !Regex.IsMatch(_User.Id.Trim(), @"^\d+$"))
            {
                CreateLableError(10, "ID must contain only digits");
                b3 = true;
            }

            //In case all fields have been entered valid values
            if (!b1 && !b2 && !b3)
            {
                foreach (var user in all_users)
                {
                    string[] u = user.Split();
                    if (u[0] == _User.UserName.Trim() && u[1] == _User.Password.Trim() && u[2] == _User.Id)
                    {

                        return u[0];
                    }
                }
                //Displays a special message in case a particular field does not match 
                //the contents of a text file
                /*Ui[3].Visibility = Visibility.Visible;*/
                _User.IsVisable = true;
               
            }
            return null;
        }

        public string GetUser()
        {
            return user_name;
        }

        private void UserControlClicked(object sender, EventArgs e)
        {
            
            _User.IsVisable = false;
            user_name = ValidtionUser();
            if (user_name != null)
            {
                this.Close();
            }
            //else
            //{
            //    user_name = null;
            //}
        }
   }
}
