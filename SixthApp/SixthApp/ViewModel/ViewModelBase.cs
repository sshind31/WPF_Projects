using SixthApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixthApp.ViewModel
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public static ObservableCollection<Person> wantedView ;

        public ViewModelBase()
        {
            wantedView= Person.GetListOfPerson(); 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnChange(string prop)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(prop));
        }
    }
}
