using SecondApp.Command;
using SecondApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SecondApp.ViewModel
{
    public class EmployeeViewModel //: INotifyPropertyChanged
    {
        public EmployeeViewModel()
        {
            Emp = new Employee();
            Employees = new ObservableCollection<Employee>();
        }
        public Employee Emp { get; set; }
        public ObservableCollection<Employee> Employees { get; set; }

        //This used for only when we want to reflect change of inputboxes in the object
        //public event PropertyChangedEventHandler PropertyChanged;

        /*public void OnChange(string prop)
        {
            PropertyChangedEventHandler change = PropertyChanged;
            if (change != null)
            {
                change(this,new PropertyChangedEventArgs(prop));
            }
        }*/



        //=======================================================================================================================
        //To Operate submit button
        private ICommand submitCommand;
        public ICommand SubmitCommand
        {
            get 
            {
                if (submitCommand == null)
                {
                    submitCommand = new RelayCommand(SubmitExecute, CanSubmitExecute, false);
                }
                return submitCommand;
            }
        }
        
        private void SubmitExecute(object parameter)
        {
            Employees.Add(Emp);
        }

        private bool CanSubmitExecute(object parameter)
        {
            if (string.IsNullOrEmpty(Emp.Name) || string.IsNullOrEmpty(Emp.MobileNo) || string.IsNullOrEmpty(Emp.EmailId))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
