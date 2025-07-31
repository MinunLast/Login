namespace Login.Routes
{
    public class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;


        public static class Register
        {
            public const string RegisterUser = Base + "/" + "register";
        }
        public static class Login
        {
            public const string LoginUser = Base + "/" + "login";
        }
    }
}
