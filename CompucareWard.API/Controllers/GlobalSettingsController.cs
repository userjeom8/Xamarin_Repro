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
using CompucareWard.API.Services;

namespace CompucareWard.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class GlobalSettingsController : ControllerBase
    {
        private readonly IGlobalSettingsService _globalSettingsService;

        public GlobalSettingsController(IGlobalSettingsService globalSettingsService)
        {
            _globalSettingsService = globalSettingsService;
        }

        // GET: api/GlobalSettings
        //[HttpGet]
        //public IEnumerable<GlobalSetting> GetGlobalSettings()
        //{
        //    return _context.GlobalSettings;
        //}

        // GET: api/GlobalSettings/5
        [HttpGet("news")]
        public async Task<IActionResult> GetNEWSSettings()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var globalSetting = await _globalSettingsService.GetNEWSSettings();

            if (globalSetting == null)
            {
                return NotFound();
            }

            return Ok(globalSetting);
        }

        // PUT: api/GlobalSettings/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutGlobalSetting([FromRoute] string id, [FromBody] GlobalSetting globalSetting)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != globalSetting.GlobalSettingID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(globalSetting).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GlobalSettingExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/GlobalSettings
        //[HttpPost]
        //public async Task<IActionResult> PostGlobalSetting([FromBody] GlobalSetting globalSetting)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.GlobalSettings.Add(globalSetting);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetGlobalSetting", new { id = globalSetting.GlobalSettingID }, globalSetting);
        //}

        //// DELETE: api/GlobalSettings/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteGlobalSetting([FromRoute] string id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var globalSetting = await _context.GlobalSettings.FindAsync(id);
        //    if (globalSetting == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.GlobalSettings.Remove(globalSetting);
        //    await _context.SaveChangesAsync();

        //    return Ok(globalSetting);
        //}

        //private bool GlobalSettingExists(string id)
        //{
        //    return _context.GlobalSettings.Any(e => e.GlobalSettingID == id);
        //}
    }
}