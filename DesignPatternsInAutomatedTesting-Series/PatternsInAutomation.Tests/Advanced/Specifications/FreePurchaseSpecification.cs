using PatternsInAutomatedTests.Advanced.Specifications.Data;

namespace PatternsInAutomatedTests.Advanced.Specifications
{
    public class FreePurchaseSpecification : Specification<PurchaseTestInput>
    {
        public override bool IsSatisfiedBy(PurchaseTestInput entity)
        {
            return entity.TotalPrice == 0;
        }
    }
}