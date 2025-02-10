using System;
using System.Collections.Generic;
using System.Text;

namespace rOS.Core.Api;

public interface IMetaPromotion
{
    string     MetaTitle       {get;}
    string     MetaDescription {get;}
    IMetaKeyword[] MetaKeywords    {get;}
}
