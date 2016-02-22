namespace PatternsInAutomation.Tests.Advanced.Specifications
{
	public abstract class Specification<TEntity> : ISpecification<TEntity>
	{
		public abstract bool IsSatisfiedBy(TEntity entity);

		public ISpecification<TEntity> And(ISpecification<TEntity> other)
		{
			return new AndSpecification<TEntity>(this, other);
		}

		public ISpecification<TEntity> Or(ISpecification<TEntity> other)
		{
			return new OrSpecification<TEntity>(this, other);
		}

		public ISpecification<TEntity> Not()
		{
			return new NotSpecification<TEntity>(this);
		}
	}
}
