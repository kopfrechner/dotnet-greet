using Objectbay.Vacation.WebApi.DbModels;

namespace Objectbay.Vacation.WebApi.Controllers.Vacations;

public record CreateOrUpdateVacationItem
{
    public required string Name { get; set; }
    public VacationItemCategory? Category { get; set; }
}