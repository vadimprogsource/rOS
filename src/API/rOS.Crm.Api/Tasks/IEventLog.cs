using System;
using rOS.Core.Api;

namespace rOS.Crm.Api.Tasks;

public interface IEventLog : INamed , IEntity
{
    DateTime EventTime { get; set; }
    TimeSpan TotalTime { get; set; }
    string   Description { get; set; }
}

