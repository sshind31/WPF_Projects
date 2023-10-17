using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifthApp
{
    public class Student
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public bool IsIntrovert 
        { get; set; }
        public Gender Gender1 { get; set; }
        public static ObservableCollection<Student> GetList()
        {
            var lissst = new ObservableCollection<Student>() {
                    new Student(){ Id = 1, Name="Bill",IsIntrovert=true,Gender1=Gender.Male},
                    new Student(){ Id = 2, Name="Steve",IsIntrovert=false,Gender1=Gender.Male},
                    new Student(){ Id = 3, Name="Ram",IsIntrovert=true,Gender1=Gender.Male},
                    new Student(){ Id = 4, Name="Abdul",IsIntrovert=false,Gender1=Gender.Male} };
            return lissst;
        }
    }
    public enum Gender
    {
        Male,
        Female,
        Unknown
    }
}
