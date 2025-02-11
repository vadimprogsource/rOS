using System.Collections.Generic;
using rOS.Core.Api;
using rOS.Core.Api.Banking;
using rOS.Core.Api.Business;
using rOS.Core.Api.Orgs;
using rOS.Crm.Api.Objects;
using rOS.Crm.Api.Projects;
using rOS.Crm.Api.Tasks;
using rOS.eCommerce.Api.Ordering;
using rOS.eCommerce.Api.Payments;

namespace rOS.Crm.Api.Customers;




public interface ICustomerCase : IBusinessEntity
{
    IBusinessOrg Org { get; }
    DateTime StartOfService { get; }
    DateTime EndOfService { get; }

    ICustomerGroup Group { get; }
    ICustomerCaseStatus State { get; }
    IPromotionType Promotion { get; }



    ICustomerType Type { get; set; }
    ICustomerKind Kind { get; set; }
    ICustomerIndustry Industry { get; set; }
    ICustomerPriority Priority { get; set; }
    ICustomerNetwork Network { get; set; }
    IBussinessType Bussiness { get; set; }


    string NickName { get; }
    string TaxCode { get; }
    string RegCode { get; }
    IBankAccount BankAccount { get; }
    IAddress Address { get; }
    string Url { get; }

    string Phone { get; }
    string Fax { get; }
    string Email { get; }
    string Skype { get; }

    string Cellular { get; }
    string LastBuyDescription { get; }
    string SaleDate { get; }
    string SaleDescription { get; }
    string Equipment { get; }
    string ServiceTypeDescription { get; }
    string ServiceDescription { get; }


    IReadOnlyCollection<ICustomerContact> Contacts { get; }
    IReadOnlyCollection<ICustomerRelation> Relations  { get; }

    IReadOnlyCollection<IServicedObject> ServicedObjects { get; }
    IReadOnlyCollection<IPayment> Payments { get; }
    IReadOnlyCollection<IProjectScope> Projects { get; }
    IReadOnlyCollection<ISalesOrder> Orders { get; }
    IReadOnlyCollection<ITaskJob> Tasks { get; }

    IReadOnlyCollection<ICustomerContactPhone> Phones { get; }

    DateTime BalancedAt { get; }
    decimal CumulativeBalance { get; set; }
}

