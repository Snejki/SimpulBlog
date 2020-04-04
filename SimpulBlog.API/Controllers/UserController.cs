using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpulBlog.Infrastructure.Services.Abstract;

namespace SimpulBlog.API.Controllers
{
    public class UserController : Controller
    {

        public async Task<IActionResult> Index()
        {
            return Ok();
        }
    }
}