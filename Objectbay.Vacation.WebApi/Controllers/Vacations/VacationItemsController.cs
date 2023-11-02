using Objectbay.Vacation.WebApi.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Objectbay.Vacation.WebApi.Controllers.Vacations;

[ApiController]
[Route("[controller]")]
public class VacationItemsController : ControllerBase
{
    private readonly ILogger<VacationItemsController> _logger;
    private readonly ApplicationDbContext _db;

    public VacationItemsController(
        ILogger<VacationItemsController> logger,
        ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }


    [HttpPost]
    public async Task<IActionResult> CreateVacationItem([FromBody] CreateVacationItemRequest vacationItemRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var vacationItem = new VacationItem
        {
            Name = vacationItemRequest.Name,
            Category = vacationItemRequest.vacationItemCategory.ToDbEnum()
        };

        await _db.AddAsync(vacationItem);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVacationItem), new { id = vacationItem.Id }, null);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetVacationItem([FromRoute] Guid id)
    {
        var vacationItem = await _db.VacationItems.FindAsync(id);
        if (vacationItem == null)
        {
            return BadRequest($"Id {id} does not exist.");
        }

        return Ok(new VacationItemResponse
        {
            Id = vacationItem.Id,
            Name = vacationItem.Name,
            vacationItemCategory = vacationItem.Category.ToApiEnum()
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllVacationItem(
        [FromQuery] VacationItemCategory? category = null,
        [FromQuery] string? itemName = null)
    {
        var vacationsQuery = _db.VacationItems.AsQueryable();

        var filterCategories = category != null;
        if (filterCategories)
        {
            vacationsQuery = vacationsQuery.Where(it => it.Category == category.ToDbEnum());
        }

        var filterItemName = itemName != null;
        if (filterItemName)
        {
            vacationsQuery = vacationsQuery.Where(it => EF.Functions.Like(it.Name, $"%{itemName!}%"));
        }

        return Ok(await vacationsQuery
            .Select(it => new VacationItemResponse
            {
                Id = it.Id,
                Name = it.Name,
                vacationItemCategory = it.Category.ToApiEnum()
            })
            .ToListAsync());
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVacationItem([FromRoute] Guid id, [FromBody] UpdateVacationItemRequest updateVacationItemRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var vacationItem = await _db.VacationItems.FindAsync(id);
        if (vacationItem == null)
        {
            return BadRequest($"Id {id} does not exist.");
        }

        vacationItem.Name = updateVacationItemRequest.Name;
        vacationItem.Category = updateVacationItemRequest.vacationItemCategory.ToDbEnum();

        await _db.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVacationItem([FromRoute] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }


        _db.Remove(_db.VacationItems.Single(x => x.Id == id));

        await _db.SaveChangesAsync();

        return NoContent();
    }
}

public class CreateVacationItemRequest
{
    public required string Name { get; init; }
    public VacationItemCategory? vacationItemCategory { get; init; }
}

public class UpdateVacationItemRequest
{
    public required string Name { get; init; }
    public VacationItemCategory? vacationItemCategory { get; init; }
}

public class VacationItemResponse
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public VacationItemCategory? vacationItemCategory { get; init; }
}

public enum VacationItemCategory
{
    CLOTHING,
    ACCESSORIES,
    ELECTRONICS,
    SURVIVAL_ESSENTIALS
}
