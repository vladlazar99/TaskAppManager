
using TaskManagementApp.ViewModel;

namespace TaskManagementApp.View
{

    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();
            //var taskViewModel = new TaskViewModel();
            //Set the BindingContext to a new instance of TaskViewModel  TaskViewModel taskViewModel
            BindingContext = new TaskViewModel();
        }


    }
}