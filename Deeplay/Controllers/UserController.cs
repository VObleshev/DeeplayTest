using DataLayer.Communication;
using DataLayer.ModelsDTO;
using Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controllers
{
    public class UserController
    {
        private readonly IUser _userService;

        public UserController(IUser userService)
        {
            _userService = userService;
        }

        public IResponsible Login(UserLoginDTO userLoginDTO)
        {
            var user = _userService.Get(userLoginDTO.Login);
            if (user == null)
                return new Response("Логин или пароль указаны неверно", true, false);

            var result = Services.HashService.VerifyHashedPassword(user.Password, userLoginDTO.Password);
            var message = result ? "Авторизация прошла успешно" : "Логин или пароль указаны неверно";

            return new Response(message, true, result);
        }
    }
}
