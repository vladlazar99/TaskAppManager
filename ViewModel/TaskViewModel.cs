

// ViewModels/TaskViewModel.cs
using System.Collections.ObjectModel;
using System.ComponentModel;
using TaskManagementApp.Models;
using TaskManagementApp.Services;

namespace TaskManagementApp.ViewModel
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class TaskViewModel : TaskItem
    {
        private string _filePath;

        public ObservableCollection<TaskItem> Tasks { get; private set; } = [];

        private string _newTaskDescription;
        public string NewTaskDescription
        {
            get => _newTaskDescription;
            set
            {
                _newTaskDescription = value;
                OnPropertyChanged(nameof(NewTaskDescription));
            }
        }


        private bool _isFlyoutVisible;
        public bool IsFlyoutVisible
        {
            get => _isFlyoutVisible;
            set
            {
                _isFlyoutVisible = value;
                OnPropertyChanged(nameof(IsFlyoutVisible));
            }
        }


        public Command CreateTaskCommand { get; }
        public Command SaveTaskCommand { get; }
        public Command EditTaskCommand { get; }
        public Command DeleteTaskCommand { get; }
        public Command CheckChangedCommand { get; }
        public Command TogglePopupCommand { get; }
        public Command OpenFlyoutCommand { get; }
        public Command LoadItemsCommand { get; }

        public TaskViewModel()
        {
            CreateTaskCommand = new Command(async () => await OnCreateTask());
            LoadItemsCommand = new Command(async () => await LoadTasksFromDatabase());
            SaveTaskCommand = new Command<TaskItem>(OnSaveTask);
            EditTaskCommand = new Command<TaskItem>(OnEditTask);
            DeleteTaskCommand = new Command<TaskItem>(OnDeleteTask);
            CheckChangedCommand = new Command<TaskItem>(OnCheckChanged);
            TogglePopupCommand = new Command<TaskItem>(OpenFlyout);




            Task.Run(LoadTasksFromDatabase);
            // OnCreateTask();
        }

        private async Task LoadTasksFromDatabase()
        {
            _ = await TaskItemDatabase.Instance;
            var tasks = await TaskItemDatabase.GetItemsAsync();
            //foreach (var task in tasks.OrderByDescending(t => t.IsDone))
            //{
            //    Tasks.Add(task);
            //}
            //  tasks.Clear();
            // if (tasks != null)
            {
                foreach (var task in tasks)
                {
                    // if (task != null)
                    Tasks.Add(task);
                }
            }

        }


        private async Task OnCreateTask()
        {
            _ = await TaskItemDatabase.Instance;
            var taskItem = new TaskItem();
            // await TaskItemDatabase.SaveItemAsync(taskItem);
            //int count = await TaskItemDatabase.GetItemCountAsync();
            //Console.WriteLine(count.ToString() + " <<<< Count DB elements");
            Tasks.Insert(0, taskItem);
        }

        private async void OnSaveTask(TaskItem taskItem)
        {
            _ = await TaskItemDatabase.Instance;
            taskItem.IsReadOnly = true;
            taskItem.Description = NewTaskDescription;
            await TaskItemDatabase.SaveItemAsync(taskItem);
        }

        private void OnEditTask(TaskItem taskItem)
        {
            taskItem.IsReadOnly = false;
        }

        private async void OnDeleteTask(TaskItem taskItem)
        {
            _ = await TaskItemDatabase.Instance;

            await TaskItemDatabase.DeleteItemAsync(taskItem);
            Tasks.Remove(taskItem);
        }

        private void OnCheckChanged(TaskItem taskItem)
        {
            Tasks.Remove(taskItem);
            if (taskItem.IsDone)
                Tasks.Add(taskItem);
            else
                Tasks.Insert(0, taskItem);
        }



        private void OpenFlyout(TaskItem taskItem)
        {

            IsFlyoutVisible = !IsFlyoutVisible;
        }






        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
