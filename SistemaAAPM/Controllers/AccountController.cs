using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SistemaAAPM.ViewModels;
using SistemaAAPM.Models;
using System.Threading.Tasks;

namespace SistemaAAPM.Controllers
{
    // Padroniza a URL base para ficar amigável em português: ex. /conta/login
    [Route("conta")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // -----------------------------------------------------------
        // LOGIN - EXIBIR TELA (Acessado via GET: /conta/login)
        // -----------------------------------------------------------
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        // -----------------------------------------------------------
        // LOGIN - PROCESSAR (Acessado via POST do formulário)
        // -----------------------------------------------------------
        [HttpPost("login")]
        [ValidateAntiForgeryToken] // Proteção de segurança do .NET
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var result = await _signInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, false, false);

            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(loginVM.ReturnUrl);
            }

            // Se falhar (User nulo ou senha errada)
            ModelState.AddModelError("", "Falha ao realizar o login! Verifique seu usuário e senha.");
            return View(loginVM);
        }

        // -----------------------------------------------------------
        // REGISTRO - EXIBIR TELA (Acessado via GET: /conta/registrar)
        // -----------------------------------------------------------
        [HttpGet("registrar")]
        public IActionResult Register()
        {
            return View();
        }

        // -----------------------------------------------------------
        // REGISTRO - PROCESSAR (Acessado via POST do formulário)
        // -----------------------------------------------------------
        [HttpPost("registrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel registroVM)
        {
            // CORREÇÃO AQUI: removido o "!" para criar apenas se o formulário for VÁLIDO
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = registroVM.UserName };
                var result = await _userManager.CreateAsync(user, registroVM.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    // Caso o Identity recuse (ex: senha muito fraca, usuário já existe)
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("Registro", error.Description);
                    }
                }
            }
            return View(registroVM);
        }

        // -----------------------------------------------------------
        // LOGOUT - PROCESSAR (Acessado via POST para segurança)
        // -----------------------------------------------------------
        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}