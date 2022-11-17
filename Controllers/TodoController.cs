using Microsoft.AspNetCore.Mvc;
using TarefasMvc.Context;
using TarefasMvc.Models;
using TarefasMvc.ViewModels;

namespace TarefasMvc.Controllers;

public class TodoController : Controller
{
    private readonly AppDbContext _context;

    public TodoController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var todos = _context.Todos
                .OrderBy(t => t.Date)
                .ToList();
        var viewModel = new ListTodoViewModel() { Todos = todos };

        ViewData["Title"] = "Lista de tarefas";
        return View(viewModel);
    }

    public IActionResult Delete(int id)
    {
        var todo = _context.Todos.Find(id);
        if (todo is null) return NotFound();

        _context.Todos.Remove(todo);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("/[controller]/Edit/{id?}")]
    public IActionResult Edit(int id)
    {
        if (id == 0)
        {
            ViewData["Title"] = "Cadastrar tarefa";
            return View();
        }

        var todo = _context.Todos.Find(id);
        if (todo is null) return NotFound();

        var viewModel = new CreateTodoViewModel() { Title = todo.Title, Date = todo.Date };

        ViewData["Title"] = $"Editar tarefa: {todo.Id}";
        return View(viewModel);
    }

    [HttpPost("/[controller]/Edit/{id?}")]
    public IActionResult Edit(int id, CreateTodoViewModel data)
    {
        if (id == 0)
        {
            var todo = new Todo(data.Date, data.Title);
            _context.Todos.Add(todo);
        }
        else
        {
            var todo = _context.Todos.Find(id);
            if (todo is null) return NotFound();

            todo.Title = data.Title;
            todo.Date = data.Date;
            _context.Update(todo);
        }

        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult ToComplete(int id)
    {
        var todo = _context.Todos.Find(id);
        if (todo is null) return NotFound();

        todo.IsCompleted = true;

        _context.Todos.Update(todo);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}