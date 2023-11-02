namespace Objectbay.Vacation.WebApi.Controllers.Vacations;

public static class VacationItemCategoryExtensions
{
    public static DbModels.VacationItemCategory? ToDbEnum(this VacationItemCategory? vacationItemCategory)
    {
        return vacationItemCategory switch
        {
            null => null,
            VacationItemCategory.CLOTHING => DbModels.VacationItemCategory.CLOTHING,
            VacationItemCategory.ACCESSORIES => DbModels.VacationItemCategory.ACCESSORIES,
            VacationItemCategory.ELECTRONICS => DbModels.VacationItemCategory.ELECTRONICS,
            VacationItemCategory.SURVIVAL_ESSENTIALS => DbModels.VacationItemCategory.SURVIVAL_ESSENTIALS,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static VacationItemCategory? ToApiEnum(this DbModels.VacationItemCategory? vacationItemCategory)
    {
        return vacationItemCategory switch
        {
            null => null,
            DbModels.VacationItemCategory.CLOTHING => VacationItemCategory.CLOTHING,
            DbModels.VacationItemCategory.ACCESSORIES => VacationItemCategory.ACCESSORIES,
            DbModels.VacationItemCategory.ELECTRONICS => VacationItemCategory.ELECTRONICS,
            DbModels.VacationItemCategory.SURVIVAL_ESSENTIALS => VacationItemCategory.SURVIVAL_ESSENTIALS,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}