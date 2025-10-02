using Paul1000.TaskPlanner.Domain.Models_;

namespace Paul1000.TaskPlanner.Domain.Logic
{
    public class SimpleTaskPlanner
    {
        
        public WorkItem[] CreatePlan(WorkItem[] items)
        {
            var itemsAsList = items.ToList();
            itemsAsList.Sort(CompareWorkItems);
            return itemsAsList.ToArray();
        }

        private static int CompareWorkItems(WorkItem firstItem, WorkItem secondItem)
        {

            int priorityComparison = secondItem.Priority.CompareTo(firstItem.Priority);
            if (priorityComparison != 0)
                return priorityComparison;

            int dateComparison = firstItem.DueDate.CompareTo(secondItem.DueDate);
            if (dateComparison != 0)
                return dateComparison;

            return string.Compare(firstItem.Title, secondItem.Title, StringComparison.OrdinalIgnoreCase);
            
        }
    }
}

