using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using rOS.Security.Api;
using rOS.Security.Api.Storages;
using rOS.Security.Api.Tokens;

namespace rOS.Sts.InMemory
{


    public class SessionState : Dictionary<string, object>
    {

        public readonly DateTime ExpiredOn;



        public SessionState(DateTime expired)
        {
            ExpiredOn = expired;
        }


        public SessionState AppendData(IDictionary<string, object> data)
        {
            Clear();
            foreach (var (key, value) in data)
            {
                Add(key, value);
            }

            return this;
        }
    }


    public class SessionStorage   : Dictionary<Guid , SessionState>, ISessionStorage
    {



        public Task<IDictionary<string, object>> GetDataAsync(ISecurityToken securityToken)
        {

            IDictionary<string, object> res = new SessionState(DateTime.MinValue);

           

            if (TryGetValue(securityToken.Sid, out SessionState? state))
            {
                res= state;
            }


            return Task.FromResult(res);

        }

        public Task PutDataAsync(ISecurityToken securityToken, IDictionary<string, object> data)
        {

            this[securityToken.Sid] = new SessionState(securityToken.ExpiredOn).AppendData(data);
            return Task.CompletedTask;
        }

        public Task ForceClearExpired()
        {

            Guid[] expired = this.Where(x => x.Value.ExpiredOn <= DateTime.Now).Select(x => x.Key)
                                 .ToArray();

            if (expired.Length > 0)
            {
                foreach (Guid id in expired)
                {
                    Remove(id);
                }
            }

            return Task.CompletedTask;
        }

        public Task DeleteDataAsync(ISecurityToken securityToken)
        {
            Remove(securityToken.Sid);
            return Task.CompletedTask;
         }
    }
}
