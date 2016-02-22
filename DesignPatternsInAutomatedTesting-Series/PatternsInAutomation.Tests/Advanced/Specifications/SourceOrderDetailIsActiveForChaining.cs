namespace PatternsInAutomation.Tests.Advanced.Specifications
{
    ////public class SourceOrderDetailIsActiveForChaining : Specification<DataObjects.InvoiceItem>
    ////{
    ////    public override bool IsSatisfiedBy(DataObjects.InvoiceItem entity)
    ////    {
    ////        Guard.ThrowIf().ArgumentIsNull(entity, "entity");
    ////        Guard.ThrowIf().ArgumentIsNull(entity.OrderDetail, "OrderDetail");

    ////        var orderDetail = entity.OrderDetail;

    ////        return !orderDetail.IsSplitted && !orderDetail.NextOrderDetailId.HasValue && !orderDetail.UnionOrderDetailId.HasValue;
    ////    }
    ////}
}