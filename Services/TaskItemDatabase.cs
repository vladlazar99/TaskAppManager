using SQLite;
using TaskManagementApp.Models;
namespace TaskManagementApp.Services
{


    public class TaskItemDatabase
    {
        private static SQLiteAsyncConnection Database;
        public const string DatabaseFilename = "TaskSQLite.db3";

        public const SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

        public static readonly AsyncLazy<TaskItemDatabase> Instance = new(async () =>
        {
            var instance = new TaskItemDatabase();
            _ = await Database.CreateTableAsync<TaskItem>();
            return instance;
        });


        public TaskItemDatabase()
        {
            Database = new SQLiteAsyncConnection(DatabasePath, Flags);
        }

        public static Task<List<TaskItem>> GetItemsAsync()
        {
            return Database.Table<TaskItem>().ToListAsync();
        }

        //public Task<List<TaskItem>> GetItemsNotDone

        public static Task<TaskItem> GetItemAsync(int id)
        {
            return Database.Table<TaskItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public static Task<int> SaveItemAsync(TaskItem item)
        {


            if (item.Id > 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public static Task<int> DeleteItemAsync(TaskItem item)
        {
            return Database.DeleteAsync(item);
        }
        public static Task<int> GetItemCountAsync()
        {
            return Database.Table<TaskItem>().CountAsync();
        }

    }
}
