using System;
using System.Collections.Generic;
using System.Text;

namespace function_demo.Models
{
    public class InputMessage
    {
        public string id { get; set; }
        public string source { get; set; }
        public string purchasedOn { get; set; }
        public string userId { get; set; }
        public string description { get; set; }
        public string amount { get; set; }
    }
}

