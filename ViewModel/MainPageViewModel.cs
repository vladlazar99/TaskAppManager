using System.Collections.ObjectModel;
using System.ComponentModel;
using TaskManagementApp.Models;
using TaskManagementApp.Services;
using TaskManagementApp.View;

namespace TaskManagementApp.ViewModel
{
    class MainPageViewModel
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
        public Command LoadItemsCommand { get; }
        public Command OnEditCommand { get; }

        public MainPageViewModel()
        {
            CreateTaskCommand = new Command(async () => await OnCreateTask());
            LoadItemsCommand = new Command(async () => await LoadTasksFromDatabase());
            OnEditCommand = new Command<TaskItem>(OnEditClicked);





            Task.Run(LoadTasksFromDatabase);
            // OnCreateTask();
        }

        private async void OnEditClicked(TaskItem item)
        {
            if (item == null)
            {
                return;
            }

            // Navigate using Shell with the provided TaskItem ID
            string route = $"{nameof(TaskPage)}?{nameof(TaskViewModel.Id)}={item.Id}";
            try
            {
                await Shell.Current.GoToAsync(route);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Navigation error: {ex.Message}");
            }
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
            await TaskItemDatabase.SaveItemAsync(taskItem);
            //int count = await TaskItemDatabase.GetItemCountAsync();
            //Console.WriteLine(count.ToString() + " <<<< Count DB elements");
            Tasks.Insert(0, taskItem);
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
