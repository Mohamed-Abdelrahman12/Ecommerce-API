using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ecom.Application.DTOs
{
    public class EmailDTO
    {
        public EmailDTO(string sendTo, string from, string subject, string content)
        {
            SendTo = sendTo;
            From = from;
            Subject = subject;
            Content = content;
        }

        public string SendTo { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
