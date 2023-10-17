using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdApp.Model
{
    public class Employee: INotifyPropertyChanged
    {
        private string name;
        private string emailId;
        private string mobileNo;

        public string Name 
        {
            get 
            {
                return name;
            }
            set 
            {
                name = value;
                //OnChange("Name");
            } 
        }
        public string EmailId
        {
            get
            {
                return emailId;
            }
            set
            {
                emailId = value;
                //OnChange("EmailId");
            }
        }
        public string MobileNo
        {
            get
            {
                return mobileNo;
            }
            set
            {
                mobileNo = value;
                //OnChange("MobileNo");
            }
        }

        //I don't know why this is required
        public event PropertyChangedEventHandler PropertyChanged;

        /*public void OnChange(string prop)
        {
            PropertyChangedEventHandler change = PropertyChanged;
            if (change != null)
            {
                change(this, new PropertyChangedEventArgs(prop));
            }
        }*/
    }
}
