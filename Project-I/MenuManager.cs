
namespace Project_I
{
    internal static class MenuManager
    {
        private static bool returnStartMenu;
        public static MenuState StartMenu()
        {   //Method that allows the user to choose what to do and then returns a valid menu state
            Utilities.WriteLineColoredTitle("Start Menu", ConsoleColor.Green);
            while (true)
            {
                Utilities.WriteLineArrowedColoredNumberText(1, ConsoleColor.Green, "Show Task List");
                Utilities.WriteLineArrowedColoredNumberText(2, ConsoleColor.Green, "Add New Task");
                Utilities.WriteLineArrowedColoredNumberText(3, ConsoleColor.Green, "Edit Task");
                Utilities.WriteLineArrowedColoredNumberText(0, ConsoleColor.Red, "Save & Quit");

                Utilities.WritePickOption();
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        return MenuState.ShowListMenu;
                    case "2":
                        return MenuState.AddTaskMenu;
                    case "3":
                        return MenuState.EditTaskMenu;
                    case "0":
                        return MenuState.NoMenu;
                    default:
                        ShowErrorMessage();
                        break;
                }
            }
        }
        public static void SortListMenu(ToDoList tasks)
        {   //Method that allows the user to choose a sort method and then prints the list in the chosen sorted order
            //then after it has been printed it will return to the start menu
            Utilities.WriteLineColoredTitle("Sort List Menu", ConsoleColor.Magenta);
            string input;
            do
            {
                Utilities.WriteLineArrowedColoredNumberText(1, ConsoleColor.Magenta, "Sort by Project");
                Utilities.WriteLineArrowedColoredNumberText(2, ConsoleColor.Magenta, "Sort by Title");
                Utilities.WriteLineArrowedColoredNumberText(3, ConsoleColor.Magenta, "Sort by Due Date");
                Utilities.WriteLineArrowedColoredNumberText(4, ConsoleColor.Magenta, "Sort by Status");
                Utilities.WriteLineArrowedColoredNumberText(0, ConsoleColor.Green, "Return to Start Menu");

                Utilities.WritePickOption();
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        tasks.SortByProject().PrintTaskListDetails();
                        input = "0";
                        break;
                    case "2":
                        tasks.SortByTitle().PrintTaskListDetails();
                        input = "0";
                        break;
                    case "3":
                        tasks.SortByDueDate().PrintTaskListDetails();
                        input = "0";
                        break;
                    case "4":
                        tasks.SortByStatus().PrintTaskListDetails();
                        input = "0";
                        break;
                    case "0":
                        break;
                    default:
                        ShowErrorMessage();
                        break;
                }
            } while (input != "0");
        }
        
        #region AddTaskMenu
        public static void AddTaskMenu(ToDoList tasks)
        {   //Method that asks the user to enter a new task by calling methods for each entry
            //and the creats a new task in the list
            Utilities.WriteLineColoredTitle("Add Task Menu", ConsoleColor.Yellow);
            returnStartMenu = false;
            while (true)
            {
                //Writing the return message with some added colors
                Utilities.WriteArrowedText("Enter ");
                Utilities.WriteColoredNumber(0, ConsoleColor.Red);
                Console.Write(" to return to ");
                Utilities.WriteColoredText("Start Menu", ConsoleColor.Yellow, true);

                string project = EnterProject();
                if (returnStartMenu) { break; }
                string title = EnterTitle(tasks);
                if (returnStartMenu) { break; }
                DateTime dueDate = EnterDueDate();
                if (returnStartMenu) { break; }  //Checks each entry if the user wants to return to start menu

                tasks.AddTask(new ToDoTask(project, title, dueDate));
                Utilities.WriteArrowedText("Task was successfully added!", ConsoleColor.Green, true);
                break;
            }
        }
        private static string EnterProject()
        {   //Method that asks the user for a project name and makes sure a name has been given then returns it
            while (true)
            {
                Utilities.WriteArrowedText("Enter a Project: ");
                string input = Console.ReadLine();
                string project = input.Trim();  //A project name sould be trimmed
                if (project == "0")
                {
                    returnStartMenu = true;
                    return "";
                }
                if (project == "" || project == null)
                {
                    ShowErrorMessage();
                }
                else
                {
                    return project;
                }
            }
        }
        private static string EnterTitle(ToDoList tasks)
        {   //Method that asks the user for a title and makes sure a title has been given 
            //it also makes sure that the title is uniqe and then returns it
            while (true)
            {
                Utilities.WriteArrowedText("Enter a Title: ");
                string input = Console.ReadLine();
                string title = input.Trim();  //A title should be trimmed
                if (title == "0")
                {
                    returnStartMenu = true;
                    return "";
                }
                if (title == "" || title == null)
                {
                    ShowErrorMessage();
                }
                else
                {
                    ToDoTask tempTask = tasks.SearchTaskInList(title);
                    if (tempTask != null && title == tempTask.Title)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(">> Title needs to be unique, please try again");
                        Utilities.ResetTextColor();
                        continue;
                    }
                    return title;
                }
            }
        }
        private static DateTime EnterDueDate()
        {   //Method that asks the user for a due date and makes sure the date is in a correct format and then returns it
            while (true)
            {
                Utilities.WriteArrowedText("Enter a Due Date: ");
                string input = Console.ReadLine();
                if (input == "0")
                {
                    returnStartMenu = true;
                    return DateTime.Now;
                }
                if (DateTime.TryParse(input, out DateTime date))
                {
                    return date;
                }
                else
                {
                    ShowErrorMessage();
                }
            }
        }
        #endregion
        #region EditTaskMenu
        public static void EditTaskStartMenu(ToDoList tasks)
        {   //Method that prints the tasks list titles and a return to start option and then asks the user to choose
            //a valid choice will send the user to a diffrent edit task menu where the selected task can be edited
            Utilities.WriteLineColoredTitle("Edit Task Menu", ConsoleColor.Cyan);
            string input;
            ToDoTask task = null;
            while (true)
            {
                tasks.PrintTaskListTitlesIndexed();
                Utilities.WriteLineArrowedColoredNumberText(0, ConsoleColor.Red, "Return to Start Menu");

                Utilities.WriteArrowedText("Pick a Task to Edit: ");
                input = Console.ReadLine();

                if (input == "0")
                {
                    break;
                }
                task = CheckInputToTaskList(tasks, input);
                if (task != null)
                {
                    break;
                }
            }
            if (input != "0")
            {   //Only progress if exited the choosing loop and 'return to start menu' has not been choosen
                Utilities.WriteArrowedText("Task has been choosen: ", ConsoleColor.Green, true);
                Utilities.WriteArrows();
                task.PrintDetails();
                
                EditTaskMenu(tasks, task);
            }
        }
        public static void EditTaskMenu(ToDoList tasks, ToDoTask task)
        {   //Method that asks the user what edit they want to do to the choosen task and preform the edit
            //the user will stay in the edit menu unless the 'return to start menu' or 'delete task' has been choosen
            Utilities.WriteLineColoredTitle("Edit Task Menu", ConsoleColor.Cyan);
            string input;
            do
            {
                Utilities.WriteLineArrowedColoredNumberText(1, ConsoleColor.Cyan, "Update Project");
                Utilities.WriteLineArrowedColoredNumberText(2, ConsoleColor.Cyan, "Update Title");
                Utilities.WriteLineArrowedColoredNumberText(3, ConsoleColor.Cyan, "Update Due Date");
                Utilities.WriteLineArrowedColoredNumberText(4, ConsoleColor.Cyan, "Mark as Done");
                Utilities.WriteLineArrowedColoredNumberText(5, ConsoleColor.Cyan, "Remove Task");
                Utilities.WriteLineArrowedColoredNumberText(0, ConsoleColor.Red, "Return to Start Menu");

                Utilities.WritePickOption();
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        string newProject = EnterProject();
                        tasks.EditTaskProject(task, newProject);
                        ShowUpdatedTaskMessage(task);
                        break;
                    case "2":
                        string newTitle = EnterTitle(tasks);
                        tasks.EditTaskTitle(task, newTitle);
                        ShowUpdatedTaskMessage(task);
                        break;
                    case "3":
                        DateTime newDueDate = EnterDueDate();
                        tasks.EditTaskDueDate(task, newDueDate);
                        ShowUpdatedTaskMessage(task);
                        break;
                    case "4":
                        tasks.MarkTaskDone(task);
                        ShowUpdatedTaskMessage(task);
                        break;
                    case "5":
                        tasks.DeleteTask(task);
                        Utilities.WriteArrowedText("Task has been Removed", ConsoleColor.Red, true);
                        input = "0";
                        break;
                    case "0":
                        break;
                    default:
                        ShowErrorMessage();
                        break;
                }
            } while (input != "0");
        }
        private static ToDoTask CheckInputToTaskList(ToDoList tasks, string input)
        {   //Method that checks if the given input is valid as either a index or a title
            //and returns the valid choice as a ToDoTask to be edited

            if (int.TryParse(input, out int index)) //Testing if the input is a number
            {
                index--;  //The printed index stats at 1 while the list index start at 0
                if (tasks.SearchTaskInList(index) != null)  //Testing if the input is a valid index
                {
                    return tasks.SearchTaskInList(index);
                }
                else
                {
                    ShowErrorMessage();
                }
            }
            else if (tasks.SearchTaskInList(input) != null)  //Tasting if the input is a valid task title
            {
                return tasks.SearchTaskInList(input);
            }
            else
            {
                ShowErrorMessage();
            }
            return null;
        }
        private static void ShowUpdatedTaskMessage(ToDoTask task)
        {   //Method that writes a update message and prints the new details of the choosen task
            Utilities.WriteArrowedText("Task has been updated: ", ConsoleColor.Green, true);
            Utilities.WriteArrows();
            task.PrintDetails();
            Console.WriteLine();
        }
        #endregion
        private static void ShowErrorMessage()
        {   //Method that writes an error message
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(">> Invalid entry, please try again");
            Utilities.ResetTextColor();
        }
    }
}
