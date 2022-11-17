namespace TarefasMvc.ViewModels;

public class CreateTodoViewModel
{
    public DateTime Date { get; set; }
    public string Title { get; set; } = String.Empty;
}