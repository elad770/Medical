using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace USLogin
{
    /// <summary>
    /// Interaction logic for UserLogin.xaml
    /// </summary>
    public partial class UserLogin : UserControl
    {

        User user;

        public UserLogin(string pathResource,User user)
        {
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            string fullPath = System.IO.Path.GetFullPath(pathResource);
            string remPathFolder = "bin\\Debug\\";
            fullPath = fullPath.Remove(fullPath.IndexOf(remPathFolder), remPathFolder.Length);
            resourceDictionary.Source = new Uri(fullPath, UriKind.RelativeOrAbsolute);
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            DataContext = this.user = user;
            InitializeComponent();
        
        }
       

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            this.UserControlClicked?.Invoke(sender, e);
        }
          public event EventHandler UserControlClicked;

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
           user.Password = (sender as PasswordBox).Password;
        }

        //private void CreateLabels()
        //{
        //    la = new Label[3];
        //    string[] namesLabels = { "massageErrorUserName", "massageErrorPassword", "massageErrorId" };
        //    for (int i = 0; i < la.Length; i++)
        //    {
        //        la[i] = new Label()
        //        {
        //            Name = namesLabels[i],
        //            FontSize = 14,
        //            Foreground = Brushes.Red,
        //        };
        //        la[i].SetBinding(Label.ContentProperty, new Binding($"Massages[{i}]")
        //        {
        //            Source = user,
        //            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
        //            Mode = BindingMode.OneWay

        //        });
        //    }

        //}


        //public void ClearMassage(int index)
        //{
        //    gridScoend.Children.Remove(la[index]);
        //}

        //public void InsertMassage(int index)
        //{
        //    gridScoend.Children.Add(la[index]);
        //    Grid.SetRow(la[index],(4*index)+(4- index) );
        //}

    }
}
