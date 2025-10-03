
using Paul1000.TaskPlanner.DataAccess.Abstractions;
using Paul1000.TaskPlanner.Domain.Models_;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
namespace Paul1000.TaskPlanner.DataAccess
{
    public class FileWorkItemsRepository : IWorkItemsRepository
    {
        private const string FileName = "TaskPlanner.json";
        private readonly Dictionary<Guid, WorkItem> _workItems;

        public FileWorkItemsRepository() {

            if (File.Exists(FileName) && new FileInfo(FileName).Length > 0)
            {
                // читаємо файл і десеріалізуємо в масив WorkItem
                var json = File.ReadAllText(FileName);
                var items = JsonConvert.DeserializeObject<WorkItem[]>(json) ?? Array.Empty<WorkItem>();

                // перетворюємо масив в словник
                _workItems = new Dictionary<Guid, WorkItem>();
                foreach (var item in items)
                {
                    _workItems[item.Id] = item;
                }
            }
            else
            {
                _workItems = new Dictionary<Guid, WorkItem>();
            }
        }

        public Guid Add(WorkItem workItem)
        {
            // створюємо копію
            var copy = workItem.Clone();
            copy.Id = Guid.NewGuid();

            _workItems[copy.Id] = copy;

            return copy.Id;
        }

        public WorkItem Get(Guid id)
        {
            return _workItems.TryGetValue(id, out var item) ? item : null;
        }

        public WorkItem[] GetAll()
        {
            return new List<WorkItem>(_workItems.Values).ToArray();
        }

        public bool Update(WorkItem workItem)
        {
            if (!_workItems.ContainsKey(workItem.Id))
                return false;

            _workItems[workItem.Id] = workItem;
            return true;
        }

        public bool Remove(Guid id)
        {
            return _workItems.Remove(id);
        }

        public void SaveChanges()
        {
            var itemsArray = new List<WorkItem>(_workItems.Values);
            var json = JsonConvert.SerializeObject(itemsArray, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(FileName, json);
        }
    }
}
