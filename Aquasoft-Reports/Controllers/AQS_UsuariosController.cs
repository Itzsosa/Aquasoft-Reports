using Aquasoft_Reports.Data;
using Aquasoft_Reports.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Aquasoft_Reports.Controllers
{
    public class AQS_UsuariosController : Controller
    {
        private readonly Contexto _contexto;

        public AQS_UsuariosController(Contexto contexto)
        {
            _contexto = contexto;

        }
      
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Login(AQS_Usuario user)
        {

            var temp = this.ValidarUsuario(user);

            if (temp != null)
            {
                var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, temp.NombreUsuario),
                        new Claim(ClaimTypes.Email, temp.Correo),
                        new Claim(ClaimTypes.Role, temp.Rol),
                    };

                //Se crea la instancia para la entidad del usuario y el tipo de autenticación
                var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");

                var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });

                //Se realiza la autenticación dentro del contexto http
                HttpContext.SignInAsync(userPrincipal);


                return RedirectToAction("Index", "Home");

            }

            TempData["Mensaje"] = "Error, el usuario o password incorrecto";
            return View(user);
        }

        private AQS_Usuario ValidarUsuario(AQS_Usuario temp)
        {
            AQS_Usuario autorizado = null;

            //por medio del ORM buscamos en nuestra base de datos un usuario con el email correspondiente
            var user = _contexto.AQS_Usuarios.FirstOrDefault(U => U.Correo == temp.Correo);

            //user contiene datos
            if (user != null)
            {
                //Valida el password
                if (user.Contrasena.Equals(temp.Contrasena))
                {
                    //Se almacena el usuario autorizado
                    autorizado = user;
                }
            }
            return autorizado;
        }

        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult CrearCuenta()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearCuenta([Bind] AQS_Usuario user)
        {
            if (user != null)
            {
                //Primero que todo validar si ya hay una cuenta con el correo
                var correoExiste = _contexto.AQS_Usuarios.FirstOrDefault(u => u.Correo == user.Correo);
                if (correoExiste != null)
                {
                    // El correo ya está registrado
                    TempData["MensajeError"] = "Existe una cuenta con el correo " + user.Correo;
                    return View();
                }

                user.Estado = 'A';
                user.Rol = "User";



                _contexto.AQS_Usuarios.Add(user);

                try
                {
                    _contexto.SaveChanges();

                    TempData["MensajeCreado"] = "Usuario creado correctamente. Puede iniciar sesión";
                }
                catch (Exception ex)
                {


                }
                return RedirectToAction("Login", "AQS_Usuarios");
            }
            else
            {
                return View();
            }
        }//cierre método

    }
}
