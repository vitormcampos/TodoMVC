namespace TarefasMvc.Models;

public class Todo
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }

    public Todo(int id, DateTime date, string title, bool isCompleted = false)
    {
        Id = id;
        Date = date;
        Title = title;
        IsCompleted = isCompleted;
    }
    
    public Todo(DateTime date, string title, bool isCompleted = false)
    {
        Date = date;
        Title = title;
        IsCompleted = isCompleted;
    }
}