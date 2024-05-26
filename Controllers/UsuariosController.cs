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

        private readonly CrearUsuarioApiIntegration _createUser;

        public UsuariosController(ILogger<UsuariosController> logger,
        ListarUsuariosApiIntegration listUsers,
        ListarUsuarioApiIntegration unUser,
        CrearUsuarioApiIntegration createUser)
        {
            _logger = logger;
            _listUsers = listUsers;
            _unUser = unUser;
            _createUser = createUser;
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


        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(string name, string job)
        {
            try
            {
                // Llamar al método CreateUser de tu integración para crear un nuevo usuario
                var response = await _createUser.CreateUser(name, job);
                
                // Verificar si la creación del usuario fue exitosa
                if (response != null)
                {
                    // Mostrar mensaje de confirmación
                    TempData["SuccessMessage"] = "Usuario "+response.Name+" creado correctamente, con el trabajo "+ response.Job+" y Id "+response.Id;
                }
                else
                {
                    // Manejar el caso en que la creación del usuario no fue exitosa
                    ModelState.AddModelError("", "Error al crear el usuario");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la creación del usuario
                _logger.LogError($"Error al crear el usuario: {ex.Message}");
                ModelState.AddModelError("", "Error al crear el usuario");
            }
            
            // Redireccionar a la acción Index
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}