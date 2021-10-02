using System;
using System.Collections.Generic;

namespace core_rest_api.Configuration
{
    public class AuthResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}