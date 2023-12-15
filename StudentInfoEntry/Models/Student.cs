using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
namespace StudentInfoEntry.Models
{
        public class Student : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            private void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

            private int id;
            public int Id
            {
                get { return id; }
                set { id = value; OnPropertyChanged("Id"); }
            }

            private string name;
            public string Name
            {
                get { return name; }
                set { name = value; OnPropertyChanged("Name"); }
            }

            private int gradelvl;
            public int Gradelvl
            {
                get { return gradelvl; }
                set { gradelvl = value; OnPropertyChanged("Gradelvl"); }
            }

            private string course;
            public string Course
            {
                get { return course; }
                set { course = value; OnPropertyChanged("Course"); }
            }

        }
}
