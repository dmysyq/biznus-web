using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using biznus_web.Models;
using biznus_web.Models.DTOs;

namespace biznus_web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiTranslationsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<ApiTranslationsController> _logger;

        public ApiTranslationsController(
            ApplicationDbContext db,
            ILogger<ApiTranslationsController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetTranslations(
            [FromQuery] string? scope = null,
            [FromQuery] string? culture = null,
            [FromQuery] string? key = null)
        {
            _logger.LogInformation("API: GetTranslations - Scope: {Scope}, Culture: {Culture}, Key: {Key}", 
                scope ?? "All", culture ?? "All", key ?? "All");

            var query = _db.Translations.AsQueryable();

            if (!string.IsNullOrEmpty(scope))
            {
                query = query.Where(t => t.Scope == scope);
            }

            if (!string.IsNullOrEmpty(culture))
            {
                query = query.Where(t => t.Culture == culture);
            }

            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(t => t.Key.Contains(key));
            }

            var translations = await query
                .OrderBy(t => t.Scope)
                .ThenBy(t => t.Key)
                .ThenBy(t => t.Culture)
                .ToListAsync();

            var result = translations.Select(t => new TranslationDto
            {
                Id = t.Id,
                Key = t.Key,
                Culture = t.Culture,
                Value = t.Value,
                Scope = t.Scope,
                UpdatedAt = t.UpdatedAt
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTranslation(int id)
        {
            _logger.LogInformation("API: GetTranslation - ID: {TranslationId}", id);

            var translation = await _db.Translations.FindAsync(id);
            if (translation == null)
            {
                return NotFound(new { message = "Translation not found" });
            }

            return Ok(new TranslationDto
            {
                Id = translation.Id,
                Key = translation.Key,
                Culture = translation.Culture,
                Value = translation.Value,
                Scope = translation.Scope,
                UpdatedAt = translation.UpdatedAt
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTranslation([FromBody] CreateTranslationRequest request)
        {
            _logger.LogInformation("API: CreateTranslation - Key: {Key}, Culture: {Culture}", 
                request.Key, request.Culture);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var translation = new Translation
            {
                Key = request.Key,
                Culture = request.Culture,
                Value = request.Value,
                Scope = request.Scope,
                UpdatedAt = DateTime.UtcNow
            };

            _db.Translations.Add(translation);
            await _db.SaveChangesAsync();

            _logger.LogInformation("API: Translation created - ID: {TranslationId}", translation.Id);

            return CreatedAtAction(nameof(GetTranslation), new { id = translation.Id }, new TranslationDto
            {
                Id = translation.Id,
                Key = translation.Key,
                Culture = translation.Culture,
                Value = translation.Value,
                Scope = translation.Scope,
                UpdatedAt = translation.UpdatedAt
            });
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTranslation(int id, [FromBody] UpdateTranslationRequest request)
        {
            _logger.LogInformation("API: UpdateTranslation - ID: {TranslationId}", id);

            var translation = await _db.Translations.FindAsync(id);
            if (translation == null)
            {
                return NotFound(new { message = "Translation not found" });
            }

            translation.Value = request.Value;
            translation.Scope = request.Scope ?? translation.Scope;
            translation.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            _logger.LogInformation("API: Translation updated - ID: {TranslationId}", id);

            return Ok(new TranslationDto
            {
                Id = translation.Id,
                Key = translation.Key,
                Culture = translation.Culture,
                Value = translation.Value,
                Scope = translation.Scope,
                UpdatedAt = translation.UpdatedAt
            });
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTranslation(int id)
        {
            _logger.LogInformation("API: DeleteTranslation - ID: {TranslationId}", id);

            var translation = await _db.Translations.FindAsync(id);
            if (translation == null)
            {
                return NotFound(new { message = "Translation not found" });
            }

            _db.Translations.Remove(translation);
            await _db.SaveChangesAsync();

            _logger.LogInformation("API: Translation deleted - ID: {TranslationId}", id);

            return NoContent();
        }
    }
}

