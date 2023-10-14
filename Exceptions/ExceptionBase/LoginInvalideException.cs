namespace Exceptions.ExceptionBase
{
    public class LoginInvalideException : SistemaTaskException
    {
        public LoginInvalideException() : base(ResourceMenssagensErro.USER_FAIL_LOGIN)
        {

        }
    }
}
