using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompucareWard.API.Infrastructure;
using CompucareWard.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace CompucareWard.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class FormSubcomponentsController : ControllerBase
    {
        private readonly CompucareWardContext _context;

        public FormSubcomponentsController(CompucareWardContext context)
        {
            _context = context;
        }
    }
}