namespace TarefasMvc.Exceptions;

public class TodoNotMatchException : Exception
{
    public TodoNotMatchException(string message = "Todo informado não corresponde ao registrado") : base(message)
    {
    }
}