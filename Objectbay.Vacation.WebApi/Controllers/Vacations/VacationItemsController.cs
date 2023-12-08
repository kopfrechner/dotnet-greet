using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Objectbay.Vacation.WebApi.DbModels;

namespace Objectbay.Vacation.WebApi.Controllers.Vacations;

[ApiController]
[Route("[controller]")]
public class VacationItemsController(ApplicationDbContext db) : ControllerBase {
    [HttpPost]
    public async Task<IActionResult> CreateVacationItem(
        [FromBody] CreateOrUpdateVacationItem vacationItemRequest) {
        var vacationItem = new VacationItem {
            Name = vacationItemRequest.Name,
            Category = vacationItemRequest.Category
        };

        await db.AddAsync(vacationItem);
        await db.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetVacationItem),
            new { id = vacationItem.Id }, null
        );
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetVacationItem(
        [FromRoute] Guid id) {
        var vacationItem = await db.VacationItems.FindAsync(id);
        if (vacationItem == null) {
            return BadRequest($"Id {id} does not exist.");
        }

        return Ok(vacationItem);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllVacationItem(
        [FromQuery] VacationItemCategory? category = null,
        [FromQuery] string? itemName = null) {
        var vacationsQuery = db.VacationItems.AsQueryable();

        var filterCategories = category != null;
        if (filterCategories) {
            vacationsQuery = vacationsQuery
                .Where(it => it.Category == category);
        }

        var filterItemName = itemName != null;
        if (filterItemName) {
            vacationsQuery = vacationsQuery
                .Where(it => EF.Functions.Like(it.Name, $"%{itemName!}%"));
        }

        return Ok(await vacationsQuery.ToListAsync());
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVacationItem(
        [FromRoute] Guid id,
        [FromBody] CreateOrUpdateVacationItem vacationItemRequest) {
        var vacationItemToUpdate = await db.VacationItems.FindAsync(id);
        if (vacationItemToUpdate == null) {
            return BadRequest($"Id {id} does not exist.");
        }

        vacationItemToUpdate.Name = vacationItemRequest.Name;
        vacationItemToUpdate.Category = vacationItemRequest.Category;

        await db.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVacationItem(
        [FromRoute] Guid id) {
        db.Remove(db.VacationItems.Single(x => x.Id == id));
        await db.SaveChangesAsync();

        return NoContent();
    }
}