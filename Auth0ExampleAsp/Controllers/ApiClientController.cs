using System.Configuration;
using System.Web.Mvc;
using Auth0ExampleAsp.ViewModels;
using Newtonsoft.Json;
using RestSharp;

namespace Auth0ExampleAsp.Controllers
{
    public class ApiClientController : Controller
    {
        
        private readonly string _auth0ApiUrl = ConfigurationManager.AppSettings["auth0:ApiUrl"];
        private Token _jwt;

        public ApiClientController()
        {
            var auth0Domain = ConfigurationManager.AppSettings["auth0:Domain"];
            var auth0ApiClientId = ConfigurationManager.AppSettings["auth0:ApiClientId"];
            var auth0ApiClientSecret = ConfigurationManager.AppSettings["auth0:ApiClientSecret"];
            var auth0ApiGrantType = ConfigurationManager.AppSettings["auth0:ApiGrantType"];

            var tokenRequestClientId = $"\"client_id\":\"{auth0ApiClientId}\"";
            var tokenRequestClientSecret = $"\"client_secret\":\"{auth0ApiClientSecret}\"";
            var tokenRequestGrantType = $"\"grant_type\":\"{auth0ApiGrantType}\"";
            var tokenRequestAudience = $"\"audience\":\"{_auth0ApiUrl}\"";

            var tokenClient = new RestClient($"https://{auth0Domain}/oauth/token");
            var tokenRequest = new RestRequest(Method.POST);
            var tokenRequestParam =
                $"{{{tokenRequestClientId}," +
                $"{tokenRequestClientSecret}," +
                $"{tokenRequestGrantType}," +
                $"{tokenRequestAudience}}}";
            tokenRequest.AddHeader("content-type", "application/json");
            tokenRequest.AddParameter( "application/json", tokenRequestParam, ParameterType.RequestBody);
            var tokenResponse = tokenClient.Execute(tokenRequest);
            _jwt = JsonConvert.DeserializeObject<Token>(tokenResponse.Content);
        }

        public ActionResult ApiClient()
        {
            var code = Request.QueryString["code"];
                
            return View(new ApiClientViewModel
            {
                AccessToken = _jwt.access_token,
                TokenType = _jwt.token_type,
                ExpiresIn = _jwt.expires_in,
                Scope = _jwt.scope
            });
        }
        public PartialViewResult Public()
        {
            var apiClient = new RestClient($"{_auth0ApiUrl}/public");
            var apiRequest = new RestRequest(Method.GET);
            apiRequest.AddHeader("authorization", $"Bearer {_jwt.access_token}");
            IRestResponse apiResponse = apiClient.Execute(apiRequest);
            ApiResponseContent apiResponseContent = 
                JsonConvert.DeserializeObject<ApiResponseContent>(apiResponse.Content);
            ViewBag.Message = apiResponseContent.Message;
            return PartialView();
        }

        public PartialViewResult Private()
        {
            var apiClient = new RestClient($"{_auth0ApiUrl}/private");
            var apiRequest = new RestRequest(Method.GET);
            apiRequest.AddHeader("authorization", $"Bearer {_jwt.access_token}");
            IRestResponse apiResponse = apiClient.Execute(apiRequest);
            ApiResponseContent apiResponseContent = 
                JsonConvert.DeserializeObject<ApiResponseContent>(apiResponse.Content);
            ViewBag.Message = apiResponseContent.Message;
            return PartialView();
        }

        public PartialViewResult PrivateScoped()
        {
            var apiClient = new RestClient($"{_auth0ApiUrl}/private-scoped");
            var apiRequest = new RestRequest(Method.GET);
            apiRequest.AddHeader("authorization", $"Bearer {_jwt.access_token}");
            IRestResponse apiResponse = apiClient.Execute(apiRequest);
            ApiResponseContent apiResponseContent = 
                JsonConvert.DeserializeObject<ApiResponseContent>(apiResponse.Content);
            ViewBag.Message = apiResponseContent.Message;
            return PartialView();
        }

        public PartialViewResult PrivateScopedOut()
        {
            var apiClient = new RestClient($"{_auth0ApiUrl}/private-scopedout");
            var apiRequest = new RestRequest(Method.GET);
            apiRequest.AddHeader("authorization", $"Bearer {_jwt.access_token}");
            IRestResponse apiResponse = apiClient.Execute(apiRequest);
            ApiResponseContent apiResponseContent = 
                JsonConvert.DeserializeObject<ApiResponseContent>(apiResponse.Content);
            ViewBag.Message = apiResponseContent.Message;
            return PartialView();
        }
    }

    public class ApiResponseContent
    {
        public string Message { get; set; }
    }
    
    public class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string scope { get; set; }
    }
}