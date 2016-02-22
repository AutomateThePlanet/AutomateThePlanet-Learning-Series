namespace PatternsInAutomation.Tests.Advanced.Specifications
{
	public class NotSpecification<TEntity> : Specification<TEntity>
	{
        private readonly ISpecification<TEntity> specification;

		public NotSpecification(ISpecification<TEntity> specification)
		{
			this.specification = specification;
		}

		public override bool IsSatisfiedBy(TEntity entity)
		{
			return !this.specification.IsSatisfiedBy(entity);
		}
	}
}
