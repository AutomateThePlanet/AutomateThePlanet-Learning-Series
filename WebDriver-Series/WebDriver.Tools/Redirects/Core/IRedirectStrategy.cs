using System;

namespace WebDriver.Tools.Redirects.Core
{
    public interface IRedirectStrategy : IDisposable
    {
        void Initialize();

        string NavigateToFromUrl(string fromUrl);

        void Dispose();
    }
}