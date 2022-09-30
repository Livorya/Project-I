using System.Text;

namespace Project_I
{
    internal class ToDoList
    {
        private List<ToDoTask> tasks;

        public ToDoList()
        {   //Constructor that creates an empty list of ToDoTasks
            tasks = new List<ToDoTask>();
        }
        public ToDoList(List<ToDoTask> list)
        {   //Constructor that creates a list of ToDoTasks from a given list
            tasks = list;
        }
        public void AddTask(ToDoTask task)
        {   //Method that adds a task to the list
            tasks.Add(task);
        }
        public void DeleteTask(ToDoTask task)
        {   //Method that delets a task from the list
            tasks.Remove(task);
        }
        public void EditTaskProject(ToDoTask task, string newProject)
        {   //Method that updates the project name with a new one
            if (CheckTaskExist(task))
            {
                tasks[GetIndex(task)].Project = newProject;
            }
        }
        public void EditTaskTitle(ToDoTask task, string newTitle)
        {   //Method that updates the title with a new one
            if (CheckTaskExist(task))
            {
                tasks[GetIndex(task)].Title = newTitle;
            }
        }
        public void EditTaskDueDate(ToDoTask task, DateTime newDueDate)
        {   //Method that updates the due date with a new one
            if (CheckTaskExist(task))
            {
                tasks[GetIndex(task)].DueDate = newDueDate;
            }
        }
        public void MarkTaskDone(ToDoTask task)
        {   //Method that changes the task status to done
            if (CheckTaskExist(task))
            {
                tasks[GetIndex(task)].Status = true;
            }
        }
        public ToDoList SortByProject()
        {   //Method that returns a new list sorted first by project and then by title
            return new ToDoList(tasks.OrderBy(t => t.Project).ThenBy(t => t.Title).ToList());
        }
        public ToDoList SortByTitle()
        {   //Method that returns a new list sorted by title
            return new ToDoList(tasks.OrderBy(t => t.Title).ToList());
        }
        public ToDoList SortByDueDate()
        {   //Method that returns a new list sorted first by due date and then by project and then last by title
            return new ToDoList(tasks.OrderBy(t => t.DueDate).ThenBy(t => t.Project).ThenBy(t => t.Title).ToList());
        }
        public ToDoList SortByStatus()
        {   //Method that returns a new list sorted first by status and then by project and then last by title
            List<ToDoTask> list = tasks.OrderBy(t => t.Status).ThenBy(t => t.Project).ThenBy(t => t.Title).ToList();
            return new ToDoList(list);
        }
        public ToDoTask SearchTaskInList(string taskTitle)
        {   //Method that searches for a task in the list by title and returns a new task if found otherwise null
            return tasks.Find(t => t.Title == taskTitle);
        }
        public ToDoTask SearchTaskInList(int index)
        {   //Method that searches for a task in the list by index and returns a new task if found otherwise null
            if (index >= 0 && index < tasks.Count)
            {
                return tasks[index];
            }
            return null;
        }
        public void PrintTaskListDetails()
        {   //Method that prints the list with a decription at the top with a padded format
            int padding = 15;
            Console.WriteLine();
            Divider();
            Console.WriteLine("Project".PadRight(padding) + "Task".PadRight(padding) + "Due Date".PadRight(padding) + "Status");
            Divider();

            foreach (ToDoTask task in tasks)
            {
                task.PrintDetailsPadded(padding);
            }
            Divider();
        }
        public void PrintTaskListTitlesIndexed()
        {   //Method that prints the list numbered from 1 and with only the title
            foreach (ToDoTask task in tasks)
            {
                int index = tasks.IndexOf(task) + 1;
                Utilities.WriteLineArrowedColoredNumberText(index, ConsoleColor.Cyan, task.Title);
            }
        }
        public void SaveToDoList(string directory, string fileName)
        {   //Method that saves the list to a text file in a easy to read format and gives a confirming message
            //the creation and overwriting of the text file is done by the File.WriteAllText method
            string path = $"{directory}{fileName}";
            StringBuilder sb = new StringBuilder();

            foreach (ToDoTask task in tasks)
            {
                sb.Append($"Project:{task.Project};");
                sb.Append($"Title:{task.Title};");
                sb.Append($"DueDate:{task.DueDate.ToShortDateString()};");
                sb.Append($"Status:{task.Status};");
                sb.Append(Environment.NewLine);
            }

            File.WriteAllText(path, sb.ToString());

            Utilities.WriteArrowedText("ToDoList was successfully saved", ConsoleColor.Gray, true);
        }
        public void LoadToDoList(string directory, string fileName)
        {   //Method that loads the list from a text file and gives a confirming message only if it exist 
            //by splitting the read string several times the information needed is extracted
            //and new tasks are created and added to the list
            string path = $"{directory}{fileName}";
            try
            {
                if (File.Exists(path))
                {
                    tasks.Clear();  //Makes sure the list is empty before filling it

                    string[] tasksAsString = File.ReadAllLines(path);
                    for (int i = 0; i < tasksAsString.Length; i++)
                    {   //Each task is a line in the file and each parameter is divided by the ';'
                        string[] taskSplits = tasksAsString[i].Split(';');
                        //Each parameter has a desctiptive name a ':' and the value
                        //by using the IndexOf method and adding 1 the value is reached
                        string project = taskSplits[0].Substring(taskSplits[0].IndexOf(':') + 1); 
                        string title = taskSplits[1].Substring(taskSplits[1].IndexOf(':') + 1);
                        DateTime dueDate = DateTime.Parse(taskSplits[2].Substring(taskSplits[2].IndexOf(':') + 1));
                        bool status = bool.Parse(taskSplits[3].Substring(taskSplits[3].IndexOf(':') + 1));

                        ToDoTask task = new ToDoTask(project, title, dueDate, status);
                        tasks.Add(task);
                    }
                    Utilities.WriteArrowedText("ToDoList was successfully loaded", ConsoleColor.Gray, true);

                    //In order to shift between colored word and not colored several different calls are needed
                    Utilities.WriteArrowedText("You have ");
                    Utilities.WriteColoredText(tasks.Count.ToString(), ConsoleColor.Cyan);
                    Console.Write(" ToDoTasks and ");
                    Utilities.WriteColoredText(CountDone().ToString(), ConsoleColor.Cyan);
                    Console.Write(" tasks are done");
                    Utilities.WriteColoredText("!", ConsoleColor.Cyan, true);
                }
            }
            catch (IndexOutOfRangeException iex)
            {
                Utilities.WriteArrowedText("Something went wrong parsing the file, please check the data!", ConsoleColor.Red, true);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(iex.Message);
            }
            catch (FileNotFoundException fnfex)
            {
                Utilities.WriteArrowedText("The file couldn't be found!", ConsoleColor.Red, true);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(fnfex.Message);
            }
            catch (Exception ex)
            {
                Utilities.WriteArrowedText("Something went wrong while loading the file!", ConsoleColor.Red, true);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Utilities.ResetTextColor();
            }
        }
        private int CountDone()
        {   //Method that counts how many tasks are done in the list
            return tasks.Count(task => task.Status == true);
        }
        private int GetIndex(ToDoTask task)
        {   //Method that returns the index of a task in the list
            return tasks.FindIndex(t => t.Title == task.Title);
        }
        private bool CheckTaskExist(ToDoTask task)
        {   //Method that checks if a given task exist in the list and return a bool value
            if (task != null)
            {
                int index = GetIndex(task);
                if (index != -1)
                {
                    return true;
                }
                else { return false; }
            }
            else { return false; }
        }
        private void Divider()
        {   //Method that wirtes a dividing line
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("----------------------------------------------------");
            Utilities.ResetTextColor();
        }
    }
}
