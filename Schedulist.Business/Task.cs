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
        private List<Task> tasks;
        public Task()
        {
            tasks = new List<Task>();
        }
        public void CreateNewTask()
        {
            var finishCreatingTask = false;

            while (!finishCreatingTask)
            {
                int i = 100;
                int taskId = i++;
                Console.WriteLine("You are creating new Task, please provide following data");
                Console.WriteLine("Task Name");
                string taskName = Console.ReadLine();
                Console.WriteLine("Task Description");
                string taskDescription = Console.ReadLine();
                Console.WriteLine("Start date and time of task using format DD/MM/YYYY HH:MM");
                string taskStartDateTime = Console.ReadLine();
                DateTime parsedStartDate;
                bool isValidStartDate = DateTime.TryParse(taskStartDateTime, out parsedStartDate);
                if (isValidStartDate)
                {
                    Console.Write("");
                }
                else
                    Console.WriteLine($"Incorrect date/time format, unable to parse date: {taskStartDateTime}");

                Console.WriteLine("End date and time of task using format DD/MM/YYYY HH:MM");
                string taskEndDateTime = Console.ReadLine();
                DateTime parsedEndDateTime;
                bool isValidEndDate = DateTime.TryParse(taskEndDateTime, out parsedEndDateTime);
                if (isValidEndDate)
                {
                    Console.Write("");
                }
                else
                    Console.WriteLine($"Incorrect date/time format, unable to parse date: {taskEndDateTime}");

                tasks.Add(new Task(taskId, taskName, taskDescription, parsedStartDate, parsedEndDateTime));
                Console.WriteLine("You created new task as following:");
                Console.WriteLine(
                    $"Task ID:             |{taskId}    \nTask Name:           |{taskName}    \nTask Description:    |{taskDescription}    \nStart Date and Time: | {parsedStartDate} \nEnd Date and Time:   | {parsedEndDateTime}");

                Console.Write(
                    "Press 'x' and Enter to close the app, or press any other key and Enter to continue creating tasks: ");
                if (Console.ReadLine() == "x") finishCreatingTask = true;
                else if (Console.ReadLine() == "y") //read menu
                    Console.WriteLine("\n");
            }

            {
                Task task = new Task(); //to be added to menu

                Console.WriteLine("Creating task");
                task.CreateNewTask();

            }
        }

    }
}
