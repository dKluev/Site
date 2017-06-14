namespace Specialist.Entities.Catalog.Interface
{
    public interface IEntityCommonInfo
    {

        string Name { get; }

        string Description { get; }

        string UrlName { get; }

        int WebSortOrder { get; }
    }
}