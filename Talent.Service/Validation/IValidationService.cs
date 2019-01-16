using Talent.Core;

namespace Talent.Service.Validation
{
    public interface IValidationService<TEntity> where TEntity : class
    {
        ValidationResultList<TEntity> Validate(EntityMode mode, TEntity entity);
        ValidationResultList<TEntity> ValidateDelete(TEntity entity);
    }
}
