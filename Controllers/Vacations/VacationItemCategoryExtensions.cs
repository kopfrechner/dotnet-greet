namespace dotnet_greet.Controllers.Vacations;

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