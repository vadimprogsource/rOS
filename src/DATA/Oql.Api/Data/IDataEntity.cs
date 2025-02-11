using System;
namespace Oql.Api.Data;

public interface IDataEntity
{
    Task OnCreateInstance(IDataContext context);

    Task OnLookup(IDataContext context);

    Task<bool> OnCreate(IDataContext context);
    Task OnCreateComplete(IDataContext context);

    Task<bool> OnUpdate(IDataContext context);
    Task OnUpdateComplete(IDataContext context);

    Task<bool> OnRemove(IDataContext context);
    Task OnRemoveComplete(IDataContext context);
}

