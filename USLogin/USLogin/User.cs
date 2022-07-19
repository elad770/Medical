using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace USLogin
{
    
    
    public  class User: INotifyPropertyChanged
    {
        public  string UserName { get; set; }
        public  string Password { get; set; }
        public  string Id { get; set; }
        public bool IsVisable { get; set; }
        public ObservableCollection<string> Massages { get; set; }
       // public  string StrT { get; set; }

        
        public User()
        {
           Massages  = new ObservableCollection<string>() {null,null,null};
           UserName = Password = Id = "";

        }
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

    }
}
