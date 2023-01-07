using System.Collections.Generic;

namespace AsthaIslamicService.Models
{
    public class LoginViewModel
    {
        public string Msisdn { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Otp { get; set; }
        public string Token { get; set; }
        public string RegisterWith { get; set; }
        public bool IsSubscribed { get; set; }
        public string SubscribedPackage { get; set; }
    }
    public class Data
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public string imageUrl { get; set; }
        public string token { get; set; }
        public List<string> roles { get; set; }
    }

    public class LoginResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public int totalRecords { get; set; }
        public Data data { get; set; }
        public object error { get; set; }
    }
}
