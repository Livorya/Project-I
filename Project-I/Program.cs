
//Project-I - Todo List
//Created by Malin Wirén

using Project_I;

//Global variables
ToDoList tasks = new ToDoList();
string directory = @"C:\Users\Malin\Desktop\Projects\Project-I\Project-I\bin\Debug\net6.0\";
string fileName = "todolist.txt";

//Main-method
Utilities.WriteArrowedText("Welcome to ToDoLy", true);

tasks.LoadToDoList(directory, fileName);

MenuState menuState;
do
{
    menuState = MenuManager.StartMenu();

    switch (menuState)
    {
        case MenuState.ShowListMenu:
            MenuManager.SortListMenu(tasks);
            break;
        case MenuState.AddTaskMenu:
            MenuManager.AddTaskMenu(tasks);
            break;
        case MenuState.EditTaskMenu:
            MenuManager.EditTaskStartMenu(tasks);
            break;
        case MenuState.NoMenu:
            break;
        default:
            Utilities.WriteArrowedText("Something went wrong..", ConsoleColor.Red, true);
            break;
    }
} while (menuState != MenuState.NoMenu); 

tasks.SaveToDoList(directory, fileName);

Console.ResetColor();

