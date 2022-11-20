using Microsoft.AspNetCore.Mvc;
using TarefasMvc.Exceptions;
using TarefasMvc.Services;
using TarefasMvc.ViewModels;

namespace TarefasMvc.Controllers;

public class TodoController : Controller
{
    private readonly TodoService _service;

    public TodoController(TodoService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        var viewModel = _service.ListAll();

        ViewData["Title"] = "Lista de tarefas";
        return View(viewModel);
    }

    public IActionResult Delete(int id)
    {
        try
        {
            _service.Delete(id);
        }
        catch (TodoNotFoundException e)
        {
            return NotFound(e.Message);
        }

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

        try
        {
            var viewModel = _service.FindById(id);
            
            ViewData["Title"] = $"Editar tarefa";
            return View(viewModel);
        }
        catch (TodoNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("/[controller]/Edit/{id?}")]
    public IActionResult Edit(int id, CreateTodoViewModel data)
    {
        if (id == 0)
        {
            _service.Create(data);
        }
        else
        {
            try
            {
                _service.Update(id, data);
            }
            catch (TodoNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        
        return RedirectToAction(nameof(Index));
    }

    public IActionResult ToComplete(int id)
    {
        try
        {
            _service.ToComplete(id);
        }
        catch (TodoNotFoundException e)
        {
            return NotFound(e.Message);
        }
        return RedirectToAction(nameof(Index));
    }
}