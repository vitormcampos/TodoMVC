namespace TarefasMvc.Exceptions;

public class TodoNotFoundException : Exception
{
    public TodoNotFoundException(string message = "Registro não encontrado") : base(message)
    {
    }
}