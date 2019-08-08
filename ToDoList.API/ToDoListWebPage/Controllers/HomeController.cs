using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoListWebPage.Data;
using ToDoListWebPage.Models;

namespace ToDoListWebPage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private IAppRepository _appRepository;
        public HomeController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }


        [HttpGet]
        public ActionResult GetLists(int userId)
        {
            var lists = _appRepository.GetListsByUserId(userId);
            return Ok(lists);
        }


        [HttpPost]
        public ActionResult addPendings([FromBody] Pending pending)
        {

            var user = _appRepository.GetUserById(pending.UserId);

            if (user == null)
            {
                return BadRequest("Could not find user");
            }
            else
            {
                _appRepository.Add(pending);
                _appRepository.saveAll();
                return Ok(pending);
            }

        }

        [HttpPut]
        public ActionResult updateLists(string listname,string comefrom,[FromBody] GeciciEleman geciciEleman)
        {
            var user = _appRepository.GetUserById(geciciEleman.UserId);

            if (user == null)
            {
                return BadRequest("Could not find user");
            }
            else
            {
                if (listname == "cdk-drop-list-0")
                {
                    if (listname==comefrom)
                    {
                        var pending = _appRepository.GetPendingListById(geciciEleman.Id);
                        Pending _pending = new Pending { todo = pending.todo, UserId = pending.UserId };
                        _appRepository.Add<Pending>(_pending); //Pending'e ekle.
                        _appRepository.Delete<Pending>(pending); //Pending'ten sil.
                        _appRepository.saveAll();

                    }
                    else if(comefrom == "cdk-drop-list-1")
                    {
                        var inprogress = _appRepository.GetinProgressListById(geciciEleman.Id);
                        Pending pending = new Pending { todo = inprogress.todo, UserId = inprogress.UserId };
                        _appRepository.Add<Pending>(pending); //inProgress'e  ekle.
                        _appRepository.Delete<inProgress>(inprogress);
                        _appRepository.saveAll();

                    }

                }
                else if (listname == "cdk-drop-list-1")
                {
                    if (comefrom == "cdk-drop-list-0")
                    {
                        var pending = _appRepository.GetPendingListById(geciciEleman.Id);
                        inProgress inprogress = new inProgress { todo = pending.todo, UserId = pending.UserId };
                        _appRepository.Add<inProgress>(inprogress); //inProgress'e  ekle.
                        _appRepository.Delete<Pending>(pending); //Pendings'ten sil.
                        _appRepository.saveAll();

                    }
                    else if (comefrom == "cdk-drop-list-2")
                    {
                        var done = _appRepository.GetDoneListById(geciciEleman.Id);
                        inProgress inprogress = new inProgress { todo = done.todo, UserId = done.UserId };
                        _appRepository.Add<inProgress>(inprogress); //inProgress'e  ekle.
                        _appRepository.Delete<Done>(done); //Done'dan sil.
                        _appRepository.saveAll();
                    }
                    else if(comefrom==listname)
                    {
                        var inprogress = _appRepository.GetinProgressListById(geciciEleman.Id);
                        inProgress _inprogress = new inProgress { todo = inprogress.todo, UserId = inprogress.UserId };
                        _appRepository.Add<inProgress>(_inprogress); //inProgress'e  ekle.
                        _appRepository.Delete<inProgress>(inprogress); //Pendings'ten sil.
                        _appRepository.saveAll();

                    }
                }
                else if (listname == "cdk-drop-list-2")
                {   
                    if(comefrom==listname)
                    {
                        var done2 = _appRepository.GetDoneListById(geciciEleman.Id);
                        Done _done = new Done { todo = done2.todo, UserId = done2.UserId };
                        _appRepository.Add<Done>(_done); //done'a  ekle.
                        _appRepository.Delete<Done>(done2);
                        _appRepository.saveAll();

                    }
                    else
                    {
                        var inprogress = _appRepository.GetinProgressListById(geciciEleman.Id);
                        Done done = new Done { todo = inprogress.todo, UserId = inprogress.UserId };
                        _appRepository.Add<Done>(done); //done'a  ekle.
                        _appRepository.Delete<inProgress>(inprogress);
                        _appRepository.saveAll();

                    }
                }
                else
                {
                    return BadRequest();
                }
                return Ok();
            }


        }

        [HttpDelete]
        public ActionResult delete(int id,string listname)
        {

            if(listname=="pending")
            {
                if (_appRepository.GetPendingListById(id) != null)
                {
                    _appRepository.Delete<Pending>(_appRepository.GetPendingListById(id));
                    _appRepository.saveAll();
                    return Ok();

                }
                else
                    return BadRequest("İşlem başarısız !");
            }
            else if (listname == "inProgress")
            {
                if (_appRepository.GetinProgressListById(id) != null)
                {
                    _appRepository.Delete<inProgress>(_appRepository.GetinProgressListById(id));
                    _appRepository.saveAll();
                    return Ok();

                }
                else
                    return BadRequest("İşlem başarısız !");
            }
            else if (listname == "done")
            {
                if (_appRepository.GetDoneListById(id) != null)
                {
                    _appRepository.Delete<Done>(_appRepository.GetDoneListById(id));
                    _appRepository.saveAll();
                    return Ok();

                }
                else
                    return BadRequest("İşlem başarısız !");
            }
            return Ok();
        }
    }
}