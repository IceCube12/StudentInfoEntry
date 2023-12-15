using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ComponentModel;
using StudentInfoEntry.Models;
using System.Collections.ObjectModel;
using StudentInfoEntry.Commands;

namespace StudentInfoEntry.ViewModels
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged_Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }
        #endregion



        StudentService ObjStudentService;
        public StudentViewModel()
        {
            ObjStudentService = new StudentService();
            LoadData();
            CurrentStudent = new Student();
            saveCommand = new RelayCommand(Save);
            searchCommand = new RelayCommand(Search);
            updateCommand = new RelayCommand(Update);
            deleteCommand = new RelayCommand(Delete);
        }

        #region DisplayOperation
        private ObservableCollection<Student> studentsList;

        public ObservableCollection<Student> StudentsList
        {
            get { return studentsList; }
            set { studentsList = value; OnPropertyChanged("StudentsList"); }
        }
        private void LoadData()
        {
            StudentsList = new ObservableCollection<Student>(ObjStudentService.GetAll());
        }
        #endregion

        private Student currentStudent;

        public Student CurrentStudent
        {
            get { return currentStudent; }
            set { currentStudent = value; OnPropertyChanged("CurrentStudent"); }
        }

        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }


        #region SaveOperation
        private RelayCommand saveCommand;

        public RelayCommand SaveCommand
        {
            get { return saveCommand; }

        }

        public void Save()
        {
            try
            {
                var IsSaved = ObjStudentService.Add(CurrentStudent);
                LoadData();
                if (IsSaved)
                    Message = "Student Saved";
                else
                    Message = "Save operation failed";
            }
            catch (Exception ex)
            {
                Message = ex.Message;

            }

        }
        #endregion

        #region SearchOperation
        private RelayCommand searchCommand;

        public RelayCommand SearchCommand
        {
            get { return searchCommand; }
        }
        public void Search()
        {
            try
            {
                var ObjStudent = ObjStudentService.Search(CurrentStudent.Id);
                if (ObjStudent != null)
                {
                    CurrentStudent.Name = ObjStudent.Name;
                    CurrentStudent.Course = ObjStudent.Course;
                    CurrentStudent.Gradelvl = ObjStudent.Gradelvl;
                    Message = "Student Found";
                }
                else
                {
                    Message = "Student Not Found";
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region UpdateOperation
        private RelayCommand updateCommand;

        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }
        public void Update()
        {
            try
            {
                var IsUpdated = ObjStudentService.Update(currentStudent);
                if (IsUpdated)
                {
                    Message = "Student Updated";
                    LoadData();
                }
                else
                {
                    Message = "Update Operation Failed";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion

        #region DeleteOperation
        private RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }
        public void Delete()
        {
            try
            {
                var IsDelete = ObjStudentService.Delete(CurrentStudent.Id);
                if (IsDelete)
                {
                    Message = "Student Deleted";
                    LoadData();
                }
                else
                {
                    Message = "Delete Operation FAILED";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
        #endregion


        

    }
}
