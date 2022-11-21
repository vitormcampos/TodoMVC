namespace TarefasMvc.ViewModels;

public class CreateTodoViewModel
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Title { get; set; } = String.Empty;
    public bool IsCompleted { get; set; }
}