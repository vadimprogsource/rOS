using System;
namespace rOS.Crm.Api.Projects;

public enum ProcessPriority : int { Lowest = 1, Normal = 2, Highest = 3, Asap = 4 }
public enum ProcessState : int { Pending = 1, InQueued = 2, InProcess = 3, Stopped = 4, Canceled = 5, Refused = 6, Completed = 7 }


public interface IProcess
{
    ProcessPriority Priority { get; }
    ProcessState State { get; }
}

