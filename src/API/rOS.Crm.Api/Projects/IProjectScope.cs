using System;
using rOS.Core.Api;
using rOS.Core.Api.Business;
using rOS.Crm.Api.Customers;
using rOS.Crm.Api.Objects;

namespace rOS.Crm.Api.Projects;

public interface IProjectScope :IBusinessEntity, IDocument,INamed ,ICoded,IDescription
{
   IProgram Program { get; }

   IObjectType ObjectType { get; }

    ProcessState State { get; }

    ICustomerCase Customer { get; }

    IServicedObject ServicedObject { get; }
    string Equipment { get; }
    string Scheme { get; }

    string LastBuyDescription { get; }
    string SaleDate { get; }
    string SaleDescription { get; }
    string ServiceTypeDescription { get; }
    string ServantDescription { get; }

    string Contact { get; }
    string Phone { get; }

    TimeSpan ServiceMaintancePerion { get; }

    int CountOfTasksInProcess { get; set; }
}

