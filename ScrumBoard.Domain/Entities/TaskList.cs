﻿using System.Linq;
using System.Collections.Generic;
using ScrumBoard.Domain.Entities;
using System;
using ScrumBoard.Domain.Interfaces;
using ScrumBoard.Domain.Shared;

namespace ScrumBoard.Domain
{
    public class TaskList : BaseEntity
    {
        public IList<Task> Tasks { get; set; }

        public string Name { get; set; }
        public int Position { get; set; }

        public TaskList() : this("") { }

        public TaskList(string name)
        {
            this.Tasks = new List<Task>();
            this.Name = name;
        }

        public void AddTask(string taskTitle)
        {
            this.AddTask(new Task(taskTitle));
        }

        public void AddTask(Task task)
        {
            task.Position = this.Tasks.Count;
            task.List = this;

            this.Tasks.Add(task);
        }

        public void RemoveTask(string taskTitle)
        {
            this.Tasks.Remove(this.FindTask(taskTitle));

            this.ReOrderTasks();
        }

        public Task FindTask(string taskTitle)
        {
            return Tasks.Where(t => t.Title == taskTitle).FirstOrDefault();
        }

        public void ChangeTaskPosition(Task firstTask, int lastTaskPosition)
        {
            this.Tasks.Remove(firstTask);

            this.Tasks.Insert(lastTaskPosition, firstTask);

            this.ReOrderTasks();
        }

        private void ReOrderTasks()
        {
            for (int i = 0; i < this.Tasks.Count; i++)
            {
                this.Tasks[i].Position = i;
            }
        }

        public Notification Save(IRepository<TaskList> repository)
        {
            try
            {
                repository.Save(this);

                this.State = State.Modify;
            }
            catch (Exception ex)
            {
                this.Notification.AddError(ex);
            }
            return this.Notification;
        }
    }
}
