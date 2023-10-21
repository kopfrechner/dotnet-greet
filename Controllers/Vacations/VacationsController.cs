using dotnet_greet.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_greet.Controllers.Vacations;

[ApiController]
[Route("[controller]")]
public class VacationsController : ControllerBase
{
    private readonly ILogger<VacationsController> _logger;
    private readonly ApplicationDbContext _db;

    public VacationsController(
        ILogger<VacationsController> logger,
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
            Category = vacationItemRequest.vacationItemCategory.toDbEnum()
        };

        await _db.AddAsync(vacationItem);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVacationItem), new { id = vacationItem.Id }, vacationItem);
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
            vacationItemCategory = vacationItem.Category.toApiEnum()
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllVacationItem(
        [FromQuery] VacationItemCategory? category = null,
        [FromQuery] bool idsOnly = false,
        [FromQuery] string? itemName = null)
    {
        var vacationsQuery = _db.VacationItems.AsQueryable();

        var filterCategories = category != null;
        if (filterCategories)
        {
            vacationsQuery = vacationsQuery.Where(it => it.Category == category.toDbEnum());
        }

        var filterItemName = itemName != null;
        if (filterItemName)
        {
            vacationsQuery = vacationsQuery.Where(it => EF.Functions.Like(it.Name, $"%{itemName!}%"));
        }

        if (idsOnly)
        {
            return Ok(await vacationsQuery
                .Select(it => it.Id)
                .ToListAsync());
        }

        return Ok(await vacationsQuery
            .Select(it => new VacationItemResponse
            {
                Id = it.Id,
                Name = it.Name,
                vacationItemCategory = it.Category.toApiEnum()
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
        vacationItem.Category = updateVacationItemRequest.vacationItemCategory.toDbEnum();

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
    public string Name { get; init; }
    public VacationItemCategory? vacationItemCategory { get; init; }
}

public class UpdateVacationItemRequest
{
    public string Name { get; init; }
    public VacationItemCategory? vacationItemCategory { get; init; }
}

public class VacationItemResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public VacationItemCategory? vacationItemCategory { get; init; }
}

public enum VacationItemCategory
{
    CLOTHING,
    ACCESSIOURS,
    ELECTRONICS
}

public static class VacationItemCategoryExtensions
{
    public static DbModels.VacationItemCategory? toDbEnum(this VacationItemCategory? vacationItemCategory)
    {
        return vacationItemCategory switch
        {
            VacationItemCategory.CLOTHING => DbModels.VacationItemCategory.CLOTHING,
            VacationItemCategory.ACCESSIOURS => DbModels.VacationItemCategory.ACCESSIOURS,
            VacationItemCategory.ELECTRONICS => DbModels.VacationItemCategory.ELECTRONICS,
            null => null,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static VacationItemCategory? toApiEnum(this DbModels.VacationItemCategory? vacationItemCategory)
    {
        return vacationItemCategory switch
        {
            DbModels.VacationItemCategory.CLOTHING => VacationItemCategory.CLOTHING,
            DbModels.VacationItemCategory.ACCESSIOURS => VacationItemCategory.ACCESSIOURS,
            DbModels.VacationItemCategory.ELECTRONICS => VacationItemCategory.ELECTRONICS,
            null => null,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}