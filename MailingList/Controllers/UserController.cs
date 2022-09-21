using MailingList.Data;
using MailingList.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MailingList.Controllers
{
 
    public class UserController : BaseController
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Ctor

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        #endregion

        #region Methods
        [HttpPost]
        public async Task<int> Create([FromForm] User user)
        {
            await _unitOfWork.GetRepository<User>().AddAsync(user);

            return await _unitOfWork.Save();
        }

        //[Authorize]
        [HttpGet]
        public async Task<IEnumerable<User>> Get(string LastName, string Sorting)
        {
            IEnumerable<User> users = await _unitOfWork.GetRepository<User>().GetWhereAsync();
            users = users.OrderBy(x => x.LastName).ThenBy(x => x.FirstName); ;


            if (!string.IsNullOrEmpty(LastName))
                users = users.Where(x => x.LastName == LastName).OrderBy(x=>x.LastName).ThenBy(x=>x.FirstName);

            if(!string.IsNullOrEmpty(Sorting))
            {
                if(Sorting=="Descending")
                {
                    users = users.OrderByDescending(x => x.LastName).ThenBy(x=>x.FirstName);
                }
            }

            return users;
        }


        #endregion
    }
}
