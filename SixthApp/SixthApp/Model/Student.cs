using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixthApp.Model
{
    class Student
    {
        public string Name;
        public int Id;
        public bool IsCricketLover;
        public static ObservableCollection<Student> GetStudentList()
        {
            return new ObservableCollection<Student>()
            {
                new Student(){Name="KAlyani",Id=1,IsCricketLover=true },
                new Student(){Name="Vivek",Id=2,IsCricketLover=false },
                new Student(){Name="Ninad",Id=3,IsCricketLover=true },
                new Student(){Name="Nikita",Id=4,IsCricketLover=true },
                new Student(){Name="Shridhar",Id=5,IsCricketLover=false }
            };
        }
    }

}
