using System;
using System.Collections.Generic;
using System.Text;

namespace rOS.Core.Api;

public interface IAddress
{
    string Country { get; }
    string Region { get; }
    string City { get; }
    string Street { get; }
    string Floor { get; }
    string House { get; }
}
