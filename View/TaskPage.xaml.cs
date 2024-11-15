using TaskManagementApp.ViewModel;

namespace TaskManagementApp.View;

public partial class TaskPage : ContentPage
{
    public TaskPage(TaskViewModel taskViewModel)
    {
        InitializeComponent();
        BindingContext = taskViewModel;
    }
}