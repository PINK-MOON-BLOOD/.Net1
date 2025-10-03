using Paul1000.TaskPlanner.DataAccess;
using Paul1000.TaskPlanner.DataAccess.Abstractions;
using Paul1000.TaskPlanner.Domain.Logic;
using Paul1000.TaskPlanner.Domain.Models_;
using Enums = Paul1000.TaskPlanner.Domain.Models_.Enums;
using System;
internal static class Program
{
    public static void Main(string[] args)
    {
        var repo = new FileWorkItemsRepository();
        Console.WriteLine("=== Task Planner ===");

        while (true) {
            Console.Write("Title: ");
            var title = Console.ReadLine();

            Console.Write("Description: ");
            var desc = Console.ReadLine();

            Console.Write("Due date (yyyy-MM-dd): ");
            DateTime due;
            if (!DateTime.TryParse(Console.ReadLine(), out due))
                due = DateTime.Now.AddDays(7);

            var item = new WorkItem
            {
                Title = title,
                Description = desc,
                CreationDate = DateTime.Now,
                DueDate = due,
                Priority = Enums.Priority.Medium,
                Complexity = Enums.Complexity.Minutes,
                IsCompleted = false
            };

            var id = repo.Add(item);
            repo.SaveChanges();
            Console.WriteLine($"WorkItem added with Id: {id}");
        }


        static void BuildPlan(FileWorkItemsRepository repo)
        {
            var items = repo.GetAll();
            if (items.Length == 0)
            {
                Console.WriteLine("No tasks available.");
                return;
            }

            Console.WriteLine("=== Work Plan ===");
            foreach (var item in items)
            {
                Console.WriteLine($"{item.Id} | {item.Title} | Due: {item.DueDate:dd.MM.yyyy} | " +
                                  $"Priority: {item.Priority} | Completed: {item.IsCompleted}");
            }
        }

        static void MarkAsCompleted(FileWorkItemsRepository repo)
        {
            Console.Write("Enter WorkItem Id: ");
            if (Guid.TryParse(Console.ReadLine(), out var id))
            {
                var item = repo.Get(id);
                if (item != null)
                {
                    item.IsCompleted = true;
                    repo.Update(item);
                    repo.SaveChanges();
                    Console.WriteLine("Marked as completed.");
                }
                else
                {
                    Console.WriteLine("WorkItem not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid GUID.");
            }
        }

        static void RemoveWorkItem(FileWorkItemsRepository repo)
        {
            Console.Write("Enter WorkItem Id: ");
            if (Guid.TryParse(Console.ReadLine(), out var id))
            {
                if (repo.Remove(id))
                {
                    repo.SaveChanges();
                    Console.WriteLine("WorkItem removed.");
                }
                else
                {
                    Console.WriteLine("WorkItem not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid GUID.");
            }
        }
            //Console.WriteLine("Welcome to Simple Task Planner!");
            //var itemsList = new System.Collections.Generic.List<WorkItem>();

            //while (true)
            //{
            //    Console.WriteLine("\nEnter a new task (or leave Title empty to finish):");
            //    Console.Write("Title: ");
            //    string title = Console.ReadLine();
            //    if (string.IsNullOrWhiteSpace(title))
            //        break;

            //    Console.Write("Description: ");
            //    string description = Console.ReadLine();

            //    Console.Write("Priority (None, Low, Medium, High, Urgent): ");
            //    Priority priority = Enum.Parse<Priority>(Console.ReadLine() ?? "None", true);

            //    Console.Write("Complexity (None, Minutes, Hours, Days, Weeks): ");
            //    Complexity complexity = Enum.Parse<Complexity>(Console.ReadLine() ?? "None", true);

            //    Console.Write("Creation Date (dd.MM.yyyy): ");
            //    DateTime creationDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString("dd.MM.yyyy"));

            //    Console.Write("Due Date (dd.MM.yyyy): ");
            //    DateTime dueDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString("dd.MM.yyyy"));

            //    Console.Write("Is Completed? (true/false): ");
            //    bool isCompleted = bool.Parse(Console.ReadLine() ?? "false");

            //    itemsList.Add(new WorkItem
            //    {
            //        Title = title,
            //        Description = description,
            //        Priority = priority,
            //        Complexity = complexity,
            //        CreationDate = creationDate,
            //        DueDate = dueDate,
            //        IsCompleted = isCompleted
            //    });
            //}

            //var planner = new SimpleTaskPlanner();
            //var sortedItems = planner.CreatePlan(itemsList.ToArray());

            //Console.WriteLine("\n=== Sorted Task List ===");
            //foreach (var item in sortedItems)
            //{
            //    Console.WriteLine(item.ToString());
            //}
        }
}
