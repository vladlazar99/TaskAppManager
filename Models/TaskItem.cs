using SQLite;
using System.ComponentModel;

namespace TaskManagementApp.Models
{
    public class TaskItem : INotifyPropertyChanged
    {
        private string _description;
        private bool _isReadOnly;
        private bool _isDone;
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        //public string Description
        //{
        //    get => _description;
        //    set { _description = value; OnPropertyChanged(nameof(Description)); }
        //}
        [Column("description")]
        public string? Description { get; set; }

        //public bool IsReadOnly
        //{
        //    get => _isReadOnly;
        //    set { _isReadOnly = value; OnPropertyChanged(nameof(IsReadOnly)); }
        //}

        [Column("IsReadOnly")]
        public bool IsReadOnly { get; set; } = false;

        [Column("isDone")]
        public bool IsDone { get; set; } = false;

        //public bool IsDone
        //{
        //    get => _isDone;
        //    set { _isDone = value; OnPropertyChanged(nameof(IsDone)); }
        //}

        public TaskItem()
        {
            // Description = "";
            IsReadOnly = false;
            IsDone = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }



    //public class TaskItem
    //{
    //    [PrimaryKey, AutoIncrement]
    //    public int Id { get; set; }
    //    [Column("Description")]
    //    public string Description { get; set; } = string.Empty;
    //    [Column("IsDone")]
    //    public bool IsDone { get; set; } = false;
    //    [Column("IsReadOnly")]
    //    public bool IsReadOnly { get; set; } = false;



    //}
}
