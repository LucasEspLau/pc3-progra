using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pc3_progra.Integration;
using pc3_progra.Integration.Entity;

namespace pc3_progra.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly ListarUsuariosApiIntegration _listUsers;
        private readonly ListarUsuarioApiIntegration _unUser;


        public UsuariosController(ILogger<UsuariosController> logger,
        ListarUsuariosApiIntegration listUsers,
        ListarUsuarioApiIntegration unUser)
        {
            _logger = logger;
            _listUsers = listUsers;
            _unUser = unUser;
        }
        [HttpGet]
        public async Task<IActionResult> Perfil(int Id)
        {
            Usuario user = await _unUser.GetUser(Id);
            return View(user);
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            List<Usuario> users = await _listUsers.GetAllUser();
            return View(users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}