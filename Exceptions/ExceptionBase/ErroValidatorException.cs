namespace Exceptions.ExceptionBase;

public class ErroValidatorException : SistemaTaskException
{
    public List<string> MesssageError { get; set; }

    public ErroValidatorException(List<string> messsageError)
    {
        MesssageError = messsageError;
    }
}

