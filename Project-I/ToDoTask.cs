
namespace Project_I
{
    internal class ToDoTask
    {
        public ToDoTask(string project, string title, DateTime dueDate, bool status = false)
        {   //Constructor that creates a task where the status is optional
            Project = project;
            Title = title;
            DueDate = dueDate;
            Status = status;
        }
        public string Project { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool Status { get; set; }
        public void PrintDetailsPadded(int padding)
        {   //Method that prints the details of a task in a given padded format
            string statusText;
            if (Status)
            {
                statusText = "Done";
            }
            else
            {
                statusText = "Pending";
            }
            Console.WriteLine(Project.PadRight(padding) + Title.PadRight(padding) + DueDate.ToShortDateString().PadRight(padding) + statusText);
        }
        public void PrintDetails()
        {   //Method that prints the details of a task with decriptive tags
            string statusText;
            if (Status)
            {
                statusText = "Done";
            }
            else
            {
                statusText = "Pending";
            }
            Console.WriteLine($"Project: {Project} | Title: {Title} | Due Date: {DueDate.ToShortDateString()} | Status: {statusText}");
        }
    }
}
