using System;

namespace core_rest_api
{
    public class HighScore
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public DateTime Date { get; set; }
        
        public int Score { get; set; }
    }
}
