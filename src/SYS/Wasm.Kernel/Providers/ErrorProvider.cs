using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasm.Kernel.Api;
using Wasm.Kernel.Models;

namespace Wasm.Kernel.Providers;

public class ErrorProvider : IErrorProvider
{

    private readonly IMetaDataProvider m_meta_data;

    public ErrorProvider(IMetaDataProvider metaData)
    {
        m_meta_data = metaData;
    }

    private Dictionary<string, IError[]> m_err_set = new();

    public bool HasErrors => m_err_set.Count > 0;



    private string KeyOf(string name) => string.IsNullOrWhiteSpace(name) ? "*" : char.ToLowerInvariant(name[0]) + name[1..];

    public IError? GetError(string propName)
    {
        if (m_err_set.TryGetValue(KeyOf(propName), out IError[]? err))
        {
            return err?.FirstOrDefault();
        }

        return default;
    }

    public IError[]? GetErrors()
    {
        return m_err_set?.Values.SelectMany(x => x).ToArray();
    }

    private IError[] ToArray(IEnumerable<IError> err)
    {
        return err.Select(x =>
        {
            ErrorModel model = new(x);

            if (x.ErrorCode != null)
            {
                model.Message = m_meta_data.GetMessage(x.ErrorCode);
            }

            return model;
        })
        .ToArray();
    }

    public async Task ThrowErrorsAsync(IExecuteActionResult actionResult)
    {

        IError[] errors = Array.Empty<IError>();

        if (actionResult.Content != null)
        {
            errors = await actionResult.Content.GetErrorsAsync() ?? Array.Empty<IError>();
        }

        if (errors.Length < 1)
        {
            errors = new IError[] { new ErrorModel(actionResult.Reason) };
        }

        m_err_set = errors.Select(x => new ErrorModel(x) as IError)
                              .GroupBy(x => x.PropertyName ?? "*")
                              .ToDictionary(x => KeyOf(x.Key), x => ToArray(x));


    }

    public void ThrowException(Exception err)
    {

        m_err_set.Clear();

        ErrorModel error = new(err.Message);
        m_err_set[error.PropertyName] = new[] { error };


    }

    public bool IsError(string propertyName) => m_err_set.ContainsKey(KeyOf(propertyName));

    public void Success()
    {
        m_err_set.Clear();
    }

}
