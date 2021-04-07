using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamToeicOnline_BackEnd_Clients.Models
{
    public class MailClass
    {
        public string FromMailId { get; set; } = "thanhnhan02677@gmail.com";
        public string FromMailPassword { get; set; } = "thanhnhan02677@gmail";
        public List<string> ToMailIds { get; set; } = new List<string>();
        public string Subject { get; set; } = "";
        public string Body { get; set; }="";
        public bool IsBodyHtml { get; set; } = false;
        public List<string> Attachments { get; set; }=new List<string>();

    }
}
