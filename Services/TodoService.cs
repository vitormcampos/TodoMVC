using TarefasMvc.Context;
using TarefasMvc.Exceptions;
using TarefasMvc.Models;
using TarefasMvc.ViewModels;

namespace TarefasMvc.Services;

public class TodoService
{
    private AppDbContext _context;

    public TodoService(AppDbContext context)
    {
        _context = context;
    }

    public CreateTodoViewModel FindById(int id)
    {
        var todo = _context.Todos.Find(id);
        if (todo is null)
        {
            throw new TodoNotFoundException();
        }

        var viewModel = new CreateTodoViewModel() { Title = todo.Title, Date = todo.Date};

        return viewModel;
    }
    
    public ListTodoViewModel ListAll()
    {
        var todos = _context.Todos.OrderBy(t => t.Date).ToList();

        var viewModel = new ListTodoViewModel() { Todos = todos };

        return viewModel;
    }

    public void Create(CreateTodoViewModel data)
    {
        var todo = new Todo(data.Date, data.Title);
        _context.Todos.Add(todo);
        _context.SaveChanges();
    }

    public void Update(int id, CreateTodoViewModel data)
    {
        var todo = _context.Todos.Find(id);
        if (todo is null) throw new TodoNotFoundException();

        todo.Title = data.Title;
        todo.Date = data.Date;
        _context.Update(todo);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var todo = _context.Todos.Find(id);
        if (todo is null) throw new TodoNotFoundException();

        _context.Todos.Remove(todo);
        _context.SaveChanges();
    }

    public void ToComplete(int id)
    {
        var todo = _context.Todos.Find(id);
        if (todo is null) throw new TodoNotFoundException();

        todo.IsCompleted = true;

        _context.Todos.Update(todo);
        _context.SaveChanges();
    }
}