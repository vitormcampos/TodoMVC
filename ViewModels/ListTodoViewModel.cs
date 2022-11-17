using TarefasMvc.Models;

namespace TarefasMvc.ViewModels;

public class ListTodoViewModel
{
    public ICollection<Todo> Todos { get; set; } = new List<Todo>();
}