using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedulist.Business
{
    public class Task
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime TaskStartDateTime { get; set; }
        public DateTime TaskEndDateTime { get; set;}
        public Task(int taskId, string taskName, string taskDescription, DateTime taskStartDateTime, DateTime taskEndDateTime) 
        { 
            TaskId = taskId;
            TaskName = taskName;
            TaskDescription = taskDescription;
            TaskStartDateTime = taskStartDateTime;
            TaskEndDateTime = taskEndDateTime;
        }
        public void CreateNewTask()
        {
            Console.WriteLine("You are creating new Task, please provide following data");
            Console.WriteLine("Task Name");
            string taskName = Console.ReadLine();
            Console.WriteLine("Task Description");
            string taskDescription = Console.ReadLine();
            Console.WriteLine("Start date of task");
            string taskStartDateTime = Console.ReadLine();
            DateTime parsedStartDate;
            bool isValidStartDate = DateTime.TryParse(taskStartDateTime, out parsedStartDate);
            if (isValidStartDate)
            {
                Console.WriteLine(parsedStartDate);
            }
            else
                Console.WriteLine($"Unable to parse date: {taskStartDateTime}");

            Console.WriteLine("End date of task");
            string taskEndDateTime = Console.ReadLine();
            DateTime parsedEndDateTime;
            bool isValidEndDate = DateTime.TryParse(taskEndDateTime, out parsedEndDateTime);
            if (isValidEndDate)
            {
                Console.WriteLine(parsedEndDateTime);
            }
            else
                Console.WriteLine($"Unable to parse date: {taskEndDateTime}");
        }

    }
}
