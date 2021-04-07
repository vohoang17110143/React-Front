using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamToeicOnline_BackEnd_Clients.Models
{
    public class CommonProperties
    {
        public DateTime Create { get; set; } = new DateTime(1900, 1, 1);
        public DateTime Update { get; set; } = new DateTime(1900, 1, 1);
        public string Message { get; set; }

    }
}
