using Microsoft.Extensions.Localization;
using System.Reflection;

namespace HospitalAutomation.Services
{
    public class Lang
    {

    }
    public class LanguageService
    {
        private readonly IStringLocalizer _localizer;

        public LanguageService(IStringLocalizerFactory factory)
        {
            var type = typeof(Lang);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("Lang", assemblyName.Name);
        }
        public LocalizedString Getkey(string str)
        {
            return _localizer[str];
        }
    }
}
