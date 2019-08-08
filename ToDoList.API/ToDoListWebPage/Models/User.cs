using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListWebPage.Models
{
    public class User
    {
        public User()
        {
            inProgress = new List<inProgress>();
            Pendings = new List<Pending>();
            Done = new List<Done>();
        }

        public int Id { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string username { get; set; }

        public List<inProgress> inProgress;
        public List<Pending> Pendings;
        public List<Done> Done;

    }
}
