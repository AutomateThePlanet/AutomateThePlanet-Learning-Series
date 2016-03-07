using PatternsInAutomatedTests.Advanced.Unity.Base;
using PatternsInAutomatedTests.Advanced.Unity.Enums;

namespace PatternsInAutomatedTests.Advanced.Unity.BingMainPage.HardCore
{
    public interface IBingMainPage<M, V>
    : IPage<M, V>
        where M : BasePageElementMap, new()
        where V : BasePageValidator<M>, new() 
    {
        void Search(string textToType);
        void ClickImages();
        void SetSize(Sizes size);
        void SetColor(Colors color);
        void SetTypes(Types type);
        void SetLayout(Layouts layout);
        void SetPeople(People people);
        void SetDate(Dates date);
        void SetLicense(Licenses license);
    }
}
