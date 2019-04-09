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
    public class FormComponentsController : ControllerBase
    {
        private readonly CompucareWardContext _context;

        public FormComponentsController(CompucareWardContext context)
        {
            _context = context;
        }

        // GET: api/FormComponents
        [HttpGet]
        public IEnumerable<FormComponent> GetFormComponents()
        {
            return _context.FormComponents;
        }

        // GET: api/FormComponents/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFormComponent([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var formComponent = await _context.FormComponents.FindAsync(id);

            if (formComponent == null)
            {
                return NotFound();
            }

            return Ok(formComponent);
        }
    }
}