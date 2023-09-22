namespace Exceptions.ExceptionBase;

public class ResponseErroJson
{

    public List<string> Messages { get; set; }
    public ResponseErroJson(string message)
    {
        Messages = new List<string> {
                message
            };
    }

    //ou 

    public ResponseErroJson(List<string> message)
    {
        Messages = message;
    }
}

