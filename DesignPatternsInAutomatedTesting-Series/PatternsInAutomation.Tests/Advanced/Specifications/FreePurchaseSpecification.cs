using PatternsInAutomation.Tests.Advanced.Specifications.Data;

namespace PatternsInAutomation.Tests.Advanced.Specifications
{
    public class FreePurchaseSpecification : Specification<PurchaseTestInput>
    {
        public override bool IsSatisfiedBy(PurchaseTestInput entity)
        {
            return entity.TotalPrice == 0;
        }
    }
}