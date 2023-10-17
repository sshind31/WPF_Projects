using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixthApp.Model
{
    class Person
    {
        public string Name;
        public int Age;
        public bool IsOld { get => Age > 55; }
        public static ObservableCollection<Person> GetListOfPerson()
        {
            return new ObservableCollection<Person>()
            {
                new Person(){Name="saurbh", Age=25 },
                new Person(){Name="Sachin",Age=55 },
                new Person(){Name="Shyam",Age=65 },
                new Person(){Name="Ram",Age=95 },
                new Person(){Name="Rahul", Age=35}
            };
        }
    }
}
