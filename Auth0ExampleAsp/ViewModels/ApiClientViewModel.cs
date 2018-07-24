namespace Auth0ExampleAsp.ViewModels
{
    public class ApiClientViewModel
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public string ExpiresIn { get; set; }
        public string Scope { get; set; }
    }
}