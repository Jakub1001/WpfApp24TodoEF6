using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfApp24TodoEF6
{
    public partial class MainWindow : Window
    {
        readonly MSIDBEntities context = new MSIDBEntities();
        readonly CollectionViewSource taskViewSource;

        public MainWindow()
        {
            InitializeComponent();
            taskViewSource = ((CollectionViewSource)(FindResource("taskViewSource")));   
            DataContext = this;
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource taskViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("taskViewSource")));         
            context.Tasks.Load();
            taskViewSource.Source = context.Tasks.Local;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Task task = new Task()
                {
                    TaskName = taskname.Text,
                    Deadline = deadline.SelectedDate.Value.Date

                };
                context.Tasks.Add(task);
                context.SaveChanges();
                taskDataGrid.UpdateLayout();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message); 
            }

           

        }
        private void Remove_Click(object sender, RoutedEventArgs e)
        {           
            var emp_id = (taskDataGrid.SelectedItem as Task).Id;
            Task emp = (from task in context.Tasks where task.Id == emp_id select task).SingleOrDefault();

            context.Tasks.Remove(emp);
            context.SaveChanges();
            taskViewSource.View.Refresh();
        }
    }
}
