using Paul1000.TaskPlanner.Domain.Logic;
using Paul1000.TaskPlanner.Domain.Models_;
using Paul1000.TaskPlanner.Domain.Models_.Enums;

internal static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Simple Task Planner!");
        var itemsList = new System.Collections.Generic.List<WorkItem>();

        while (true)
        {
            Console.WriteLine("\nEnter a new task (or leave Title empty to finish):");
            Console.Write("Title: ");
            string title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
                break;

            Console.Write("Description: ");
            string description = Console.ReadLine();

            Console.Write("Priority (None, Low, Medium, High, Urgent): ");
            Priority priority = Enum.Parse<Priority>(Console.ReadLine() ?? "None", true);

            Console.Write("Complexity (None, Minutes, Hours, Days, Weeks): ");
            Complexity complexity = Enum.Parse<Complexity>(Console.ReadLine() ?? "None", true);

            Console.Write("Creation Date (dd.MM.yyyy): ");
            DateTime creationDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString("dd.MM.yyyy"));

            Console.Write("Due Date (dd.MM.yyyy): ");
            DateTime dueDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString("dd.MM.yyyy"));

            Console.Write("Is Completed? (true/false): ");
            bool isCompleted = bool.Parse(Console.ReadLine() ?? "false");

            itemsList.Add(new WorkItem
            {
                Title = title,
                Description = description,
                Priority = priority,
                Complexity = complexity,
                CreationDate = creationDate,
                DueDate = dueDate,
                IsCompleted = isCompleted
            });
        }

        var planner = new SimpleTaskPlanner();
        var sortedItems = planner.CreatePlan(itemsList.ToArray());

        Console.WriteLine("\n=== Sorted Task List ===");
        foreach (var item in sortedItems)
        {
            Console.WriteLine(item.ToString());
        }
    }
}
