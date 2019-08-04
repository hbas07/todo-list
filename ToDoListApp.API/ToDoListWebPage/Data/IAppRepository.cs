using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListWebPage.Models;

namespace ToDoListWebPage.Data
{
    public interface IAppRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool saveAll();

        List<Pending> GetPendingListByUserId(int userId);
        Pending GetPendingListById(int Id);
        List<Done> GetDoneListByUserId(int userId);
        Done GetDoneListById(int Id);
        List<inProgress> GetinProgressListByUserId(int userId);
        inProgress GetinProgressListById(int Id);
        User GetUserById(int userId);
        Lists GetListsByUserId(int userId);
    }
}
