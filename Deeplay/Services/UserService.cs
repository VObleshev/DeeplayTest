using DataLayer.Context;
using DataLayer.Models;
using Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    public class UserService : IUser
    {
        private readonly StaffAccountingContext _dbContext;

        public UserService(StaffAccountingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User Get(string login)
        {
            return _dbContext?.Users?.FirstOrDefault(u => u.Login == login);
        }
    }
}
