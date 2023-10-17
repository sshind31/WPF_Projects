using FourthApp.Command;
using FourthApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FourthApp.ViewModel
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private Employee emp;
        private ObservableCollection<Employee> employees;
        public EmployeeViewModel()
        {
            Emp = new Employee();
            Employees = new ObservableCollection<Employee>();
        }
        public Employee Emp
        {
            get { return emp; }
            set 
            {
                emp = value;
                //OnChange("Emp");
            }
        }
        public ObservableCollection<Employee> Employees
        {
            get { return employees; }
            set 
            { 
                employees = value;
                OnChange("Employees");
            }
        }

        //this is for collection updation
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnChange(string prop)
        {
            PropertyChangedEventHandler change = PropertyChanged;
            if (change != null)
            {
                change(this,new PropertyChangedEventArgs(prop));
            }
        }
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
