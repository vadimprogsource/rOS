using rOS.Core.Api;
using rOS.Core.Api.Employees;

namespace rOS.Crm.Api.Tasks;

public interface ITaskJob : IDocument
{
    IEmployee Manager { get; }
    IEmployee Worker  { get; }

    string JobDescription { get; }

    bool IsInProcess { get; }
    bool IsClosed { get; }
    bool IsReviewRequired { get; }

    IContact Contact { get; }

    ITaskJobType JobType { get; }

    DateTime ScheduledAt { get; }

    DateTime StartTime { get; }
    DateTime EndTime { get; }

    TimeSpan Estimates { get; }
    TimeSpan Efforts { get; }
    string Objective { get; }
    string Description { get; }
    string Reason { get; }

    IEnumerable<IEventLog> EventsLog { get; }

    ITaskJobResult TaskResult { get; }
    string ResultLog { get; }

    ITaskJobReview JobReview { get; }
    string ReviewLog { get; }
}

