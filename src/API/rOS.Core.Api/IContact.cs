using System;
using System.Collections.Generic;
using System.Text;

namespace rOS.Core.Api;

public interface IContact : ITitled
{
    string Cellular { get; }
    string Phone { get; }
    string Email { get; }
}
