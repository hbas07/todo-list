using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListWebPage.Models;

namespace ToDoListWebPage.Data
{
    public class AppRepository : IAppRepository
    {
        private DataContext _context;
        public AppRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public List<Done> GetDoneListByUserId(int userId)
        {
            var done = _context.Done.Where(d => d.UserId == userId).ToList();
            return done;
        }
        public Done GetDoneListById(int Id)
        {
            var done = _context.Done.FirstOrDefault(d => d.Id == Id);
            return done;
        }
        public List<inProgress> GetinProgressListByUserId(int userId)
        {
            var inprogress = _context.inProgress.Where(d => d.UserId == userId).ToList();
            return inprogress; ;
        }
        public inProgress GetinProgressListById(int Id)
        {
            var inprogress = _context.inProgress.FirstOrDefault(d => d.Id == Id);
            return inprogress; ;
        }
        public List<Pending> GetPendingListByUserId(int userId)
        {
            var pendings = _context.Pendings.Where(d => d.UserId == userId).ToList();
            return pendings;
        }
        public Pending GetPendingListById(int Id)
        {
            var pendings = _context.Pendings.FirstOrDefault(d => d.Id == Id);
            return pendings;
        }

        public User GetUserById(int userId)
        {
            Lists list = GetListsByUserId(userId);
            var user = _context.Users.FirstOrDefault(d => d.Id == userId);
            return user;
        }

        public Lists GetListsByUserId(int userId)
        {
            var pendings = GetPendingListByUserId(userId);
            var inprogress = GetinProgressListByUserId(userId);
            var done = GetDoneListByUserId(userId);

            Lists lists = new Lists { inProgress = inprogress, Done = done, Pendings = pendings };
            return lists;
        }

        public bool saveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
