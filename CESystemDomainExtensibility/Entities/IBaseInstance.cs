namespace CESystemDomainExtensibility.Entities
{
    public interface IBaseInstance<TIdType>
    {
        TIdType Id { get; set; }
    }
}
