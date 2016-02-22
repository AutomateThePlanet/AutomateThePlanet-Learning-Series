using System;

namespace PatternsInAutomation.Tests.Advanced.Specifications
{
    public class ExpressionSpecification<TEntity> : Specification<TEntity>
    {
        private readonly Func<TEntity, bool> expression;

        public ExpressionSpecification(Func<TEntity, bool> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException();
            }
            this.expression = expression;
        }

        public override bool IsSatisfiedBy(TEntity entity)
        {
            return this.expression(entity);
        }
    }
}
