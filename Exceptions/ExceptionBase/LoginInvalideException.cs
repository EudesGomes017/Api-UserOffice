namespace Exceptions.ExceptionBase
{
    public class LoginInvalideException : SystemTaskException
    {
        public LoginInvalideException() : base(ResourceMenssagensErro.USER_FAIL_LOGIN)
        {

        }
    }
}
