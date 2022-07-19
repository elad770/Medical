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
    /// Interaction logic for SystemWindow.xaml
    /// </summary>
    public partial class SystemWindow : Window
    {
        string userId;
        Window[] mainWindows;

        public SystemWindow()
        {
            InitializeComponent();
            //Login login = new Login();
            mainWindows = new Window[] { new LoginWindow(), new MainWindow() };
            mainWindows[0].ShowDialog();
            string UserAns = (mainWindows[0] as LoginWindow).GetUser();
            if (UserAns != null)
            {
                userId = ((mainWindows[0] as LoginWindow)._User.Id);
                CreateTitle(UserAns);
                this.Icon = new BitmapImage(new Uri(@"../../Images/Dapino-Medical-Medical-report.ico", UriKind.Relative));
            }
            else
            {
                this.Close();
            }
        }

        private void CreateTitle(string user)
        {
            int[,] hours = { { 17, 19 }, { 5, 11 }, { 12, 16 } };
            int hour = DateTime.Now.Hour;
            //string[] current = { "ערב", "בוקר", "צהריים" };
            string[] current = { "Evening", "Morning", "Afternoon" };
            string temp = "";
            for (int i = 0; i < hours.GetLength(0); i++)
            {
                if (hours[i, 0] <= hour && hours[i, 1] >= hour)
                {
                    temp = current[i];
                    break;
                }
            }
            userTitle.Content = user + " " + (temp != "" ? temp : "Night") + " Good";
            //temp = user +" "+ (temp != "" ? temp : "לילה") + " טוב";
            //userTitle.Content =  temp.Replace("צהריים טוב", "צהריים טובים");
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {

            switch ((sender as Border).Name)
            {
                case "border1":
                    ShowWindow(1, new MainWindow());
                    break;
                case "border2":
                    ShowWindow(2);
                    break;
                default:
                    break;
            }

        }

        private void ShowWindow(int index, Window instance = null)
        {
            try
            {
                if (!mainWindows[index].IsVisible)
                {
                    mainWindows[index] = instance;
                    mainWindows[index].Show();
                }
                else
                {
                    // mainWindows[index].Activate();
                    mainWindows[index].WindowState = System.Windows.WindowState.Normal;
                }
            }
            catch { }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}