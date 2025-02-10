using rOS.Core.Api;
using rOS.Core.Api.Employees;

namespace rOS.Crm.Api.Tasks;

public interface ITaskJob : IDocument
{
    IEmployee Manager { get; }
    IEmployee Worker  { get; }

    string JobDescription { get; }
}

