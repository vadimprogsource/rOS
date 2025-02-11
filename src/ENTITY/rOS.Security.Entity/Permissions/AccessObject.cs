using rOS.Core.Entity;
using rOS.Security.Api;
using rOS.Security.Api.Permissions;
using System;
using System.Collections.Generic;
using System.Text;

namespace rOS.Security.Entity.Permissions
{
    public abstract class AccessObject : BusinessEntity, IAccessObject
    {
        public abstract bool IsAllow { get; set; }
        public abstract bool IsDeny { get; set; }
        public abstract string UriPath { get; set; }

        public abstract string TypePath { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is IAccessObject ao) return IsAllow == ao.IsAllow && IsDeny == ao.IsDeny && UriPath == ao.UriPath;
            return false;
        }

        

        public override int GetHashCode()=> IsAllow.GetHashCode() ^ UriPath.GetHashCode();
        

    }
}
