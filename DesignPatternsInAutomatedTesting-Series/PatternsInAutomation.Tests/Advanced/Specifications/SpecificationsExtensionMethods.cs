namespace PatternsInAutomation.Tests.Advanced.Specifications
{
    public static class SpecificationsExtensionMethods
    {
        public static ISpecification<TEntity> And<TEntity>(this ISpecification<TEntity> leftSpecification, ISpecification<TEntity> rightSpecification)
        {
            return new AndSpecification<TEntity>(leftSpecification, rightSpecification);
        }

        public static ISpecification<TEntity> Or<TEntity>(this ISpecification<TEntity> leftSpecification, ISpecification<TEntity> rightSpecification)
        {
            return new OrSpecification<TEntity>(leftSpecification, rightSpecification);
        }

        public static ISpecification<TEntity> Not<TEntity>(this ISpecification<TEntity> specification)
        {
            return new NotSpecification<TEntity>(specification);
        }
    }
}
