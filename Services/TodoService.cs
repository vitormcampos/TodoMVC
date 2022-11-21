using TarefasMvc.Context;
using TarefasMvc.Exceptions;
using TarefasMvc.Models;
using TarefasMvc.Repositories;
using TarefasMvc.ViewModels;

namespace TarefasMvc.Services;

public class TodoService
{
    private TodoRepository _repository;

    public TodoService(TodoRepository repository)
    {
        _repository = repository;
    }

    public CreateTodoViewModel FindById(int id)
    {
        var todo = _repository.FindById(id);
        if (todo is null)
        {
            throw new TodoNotFoundException();
        }

        var viewModel = new CreateTodoViewModel() { Title = todo.Title, Date = todo.Date};

        return viewModel;
    }
    
    public ListTodoViewModel ListAll()
    {
        var todos = _repository.FindAll();

        var viewModel = new ListTodoViewModel() { Todos = todos };

        return viewModel;
    }

    public void Create(CreateTodoViewModel data)
    {
        var todo = new Todo(data.Date, data.Title);
        _repository.Create(todo);
    }

    public void Update(int id, CreateTodoViewModel data)
    {
        if (id != data.Id) throw new TodoNotMatchException();

        var todo = new Todo(data.Id, data.Date, data.Title, data.IsCompleted);
        
        _repository.Update(todo);
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }

    public void ToComplete(int id)
    {
        var todo = _repository.FindById(id);
        if (todo is null) throw new TodoNotFoundException();

        todo.IsCompleted = true;

        _repository.Update(todo);
    }
}