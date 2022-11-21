using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TarefasMvc.Context;
using TarefasMvc.Exceptions;
using TarefasMvc.Models;

namespace TarefasMvc.Repositories;

public class TodoRepository
{
    private AppDbContext _context;

    public TodoRepository(AppDbContext context)
    {
        _context = context;
    }

    public ICollection<Todo> FindAll()
    {
        return _context.Todos
            .AsNoTracking()
            .ToList();
    }

    public ICollection<Todo> FindAll<TKey>(Expression<Func<Todo, TKey>> orderby)
    {
        return _context.Todos
            .OrderBy(orderby)
            .ToList();
    }

    public Todo? FindById(int id)
    {
        return _context.Todos
            .AsNoTracking()
            .FirstOrDefault(t => t.Id == id);
    }

    public void Delete(int id)
    {
        var todo = _context.Todos.FirstOrDefault(t => t.Id == id);
        if (todo is null) throw new TodoNotFoundException();

        _context.Todos.Remove(todo);
        _context.SaveChanges();
    }

    public void Create(Todo todo)
    {
        _context.Todos.Add(todo);
        _context.SaveChanges();
    }

    public void Update(Todo todo)
    {
        if (todo is null) throw new TodoNotFoundException();
        
        _context.Todos.Update(todo);
        _context.SaveChanges();
    }
}