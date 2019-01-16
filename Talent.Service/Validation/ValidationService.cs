using Talent.Core;

namespace Talent.Service.Validation
{
    public class ValidationService<TEntity> : IValidationService<TEntity> where TEntity : class
    {
        public virtual ValidationResultList<TEntity> Validate(EntityMode mode, TEntity entity)
        {
            return new ValidationResultList<TEntity>();
        }

        public virtual ValidationResultList<TEntity> ValidateDelete(TEntity entity)
        {
            return new ValidationResultList<TEntity>();
        }
    }
}
