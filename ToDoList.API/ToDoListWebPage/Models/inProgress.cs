using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListWebPage.Models
{
    public class inProgress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string todo { get; set; }
        public User user { get; set; }

    }
}