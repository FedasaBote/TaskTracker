// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public enum TaskCategory
{
    Personal,
    Work,
    Errands,
    Others
}

public class TaskModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskCategory Category { get; set; }
    public bool IsCompleted { get; set; }
}

public class TaskManager
{
    private List<TaskModel> tasks = new List<TaskModel>();
    private string filePath = Path.Combine(Directory.GetCurrentDirectory(), "tasks.csv");

    public void AddTask(TaskModel task)
    {
        tasks.Add(task);
    }

    public void DisplayTasksByCategory(TaskCategory category)
    {
        var filteredTasks = tasks.Where(t => t.Category == category);
        foreach (var task in filteredTasks)
        {
            Console.WriteLine($"Name: {task.Name}, Description: {task.Description}, Category: {task.Category}, Completed: {task.IsCompleted}");
        }
    }

    public void DisplayAllTasks()
    {
        foreach (var task in tasks)
        {
            Console.WriteLine($"Name: {task.Name}, Description: {task.Description}, Category: {task.Category}, Completed: {task.IsCompleted}");
        }
    }

    public async Task<bool> SaveTasksToFileAsync()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // print the correct the full file path
                Console.WriteLine($"Saving tasks to file: {Path.GetFullPath(filePath)}");
                foreach (var task in tasks)
                {
                    await writer.WriteLineAsync($"{task.Name},{task.Description},{task.Category},{task.IsCompleted}");
                }
            }
            Console.WriteLine("Tasks saved successfully to file.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving tasks to file: {ex.Message}");
            return false;
        }

        return true;
    }
    public async Task<List<TaskModel>> LoadTasksFromFileAsync()
    {
        try
        {
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        string[] parts = line.Split(',');
                        TaskModel task = new TaskModel
                        {
                            Name = parts[0],
                            Description = parts[1],
                            Category = (TaskCategory)Enum.Parse(typeof(TaskCategory), parts[2]),
                            IsCompleted = bool.Parse(parts[3])
                        };
                        tasks.Add(task);
                    }
                }
            }
            else
            {
                Console.WriteLine("Tasks file does not exist.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading tasks from file: {ex.Message}");
        }

        return tasks;
    }
}

class Program
{
    static void Main(string[] args)
    {
        RunAsync().GetAwaiter().GetResult();
    }

    static async Task RunAsync()
    {
        TaskManager taskManager = new TaskManager();

        await taskManager.LoadTasksFromFileAsync();

        TaskModel task1 = new TaskModel { Name = "Task 1", Description = "Description for Task 1", Category = TaskCategory.Personal, IsCompleted = false };
        TaskModel task2 = new TaskModel { Name = "Task 2", Description = "Description for Task 2", Category = TaskCategory.Work, IsCompleted = true };
        TaskModel task3 = new TaskModel { Name = "Task 3", Description = "Description for Task 3", Category = TaskCategory.Errands, IsCompleted = false };

        taskManager.AddTask(task1);
        taskManager.AddTask(task2);
        taskManager.AddTask(task3);

        taskManager.DisplayAllTasks();

        taskManager.DisplayTasksByCategory(TaskCategory.Work);

        await taskManager.SaveTasksToFileAsync();
    }
}
