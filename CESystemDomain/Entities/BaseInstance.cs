using System.ComponentModel.DataAnnotations.Schema;
using CESystemDomainExtensibility.Entities;

namespace CESystemDomain.Entities
{
    public class BaseInstance<TIdType>: IBaseInstance<TIdType>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TIdType Id { get; set; }
    }
}
