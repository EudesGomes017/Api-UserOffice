namespace Exceptions.ExceptionBase;

public class ErroValidatorException : SystemTaskException
{
    public List<string> MesssageError { get; set; }

    public ErroValidatorException(List<string> messsageError) : base(string.Empty)
    { 
        MesssageError = messsageError;
    }

}

