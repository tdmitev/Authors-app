namespace SportScore2.Api.Exception;

public class NotFoundException : System.Exception
{
    public NotFoundException(string message) : base(message) { }
}