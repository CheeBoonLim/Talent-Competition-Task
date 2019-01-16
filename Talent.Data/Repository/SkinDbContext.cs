using Talent.Data.Entities;
using System.Data.Entity;

namespace Talent.Data
{
    public class TalentDbContext : TalentEntities, IDbContext
    {
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
