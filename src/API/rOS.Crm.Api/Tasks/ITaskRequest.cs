using rOS.Core.Api;
using rOS.Core.Api.Employees;
using rOS.Crm.Api.Customers;
using rOS.Crm.Api.Objects;
using rOS.Crm.Api.Tasks;

namespace rOS.Crm.Api.Tasks;

public interface ITaskRequest : IDocument
{
    ICustomerCase   Customer       { get; }
    ICustomerContact Contact        { get; }
    IServicedObject  ServicedObject { get; }
    IEmployee         Manager        { get; }

    IObjectSystem System { get; }
    ITaskSource   Source { get; }
    
    bool IsTechOnly { get; }
    bool IsPrivate { get; }
    bool UnderApprovalManager { get; }
    bool UnderApprovalCustomer { get; }

    DateTime ReceiveAt { get; }

    DateTime ControlOn  { get; }
    DateTime CloseOn    { get; }

    string Reason { get; }
    string Description { get; }


    IReadOnlyCollection<ITaskJob> Tasks { get; }

}

