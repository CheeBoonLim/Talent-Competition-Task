using System;
using System.ComponentModel;
using System.Resources;

namespace Talent.Data.Localization
{
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        private readonly string _resourceName;
        private readonly ResourceManager _resourceManager;

        public LocalizedDisplayNameAttribute(Type resourceType, string resourceName)
        {
            _resourceName = resourceName;
            _resourceManager = new ResourceManager(resourceType);
        }

        public override string DisplayName { get { return _resourceManager.GetString(_resourceName); } }
    }
}
