using Paul1000.TaskPlanner.Domain.Models_;

namespace Paul1000.TaskPlanner.DataAccess.Abstractions
{
    public interface IWorkItemsRepository {
        Guid Add(WorkItem workItem);
        WorkItem Get(Guid id);
        WorkItem[] GetAll();
        bool Update(WorkItem workItem);
        bool Remove(Guid id);
        void SaveChanges();
    }
    public class Class1
    {

    }
}
