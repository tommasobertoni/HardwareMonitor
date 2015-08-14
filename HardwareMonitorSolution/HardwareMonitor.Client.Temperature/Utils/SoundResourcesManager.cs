using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;

namespace HardwareMonitor.Client.Temperature.Utils
{
    public class SoundResourcesManager
    {
        public static readonly SoundResourcesManager INSTANCE = new SoundResourcesManager();

        private SortedSet<ResourcePlayer> _resources;
        private List<ResourcePlayer> _resourcesList;

        private ResourcePlayer _ss;
        public ResourcePlayer SelectedSound {
            get
            {
                return _ss;
            }

            set
            {
                _ss = value;
                _ss?.Load();
            }
        }

        private TemperatureUISettingsHandler _settings;

        private SoundResourcesManager()
        {
            _settings = new TemperatureUISettingsHandler();
            _resources = new SortedSet<ResourcePlayer>(new ResourceNameComparer());

            ResourceSet resourceSet = Properties.Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
            foreach (DictionaryEntry entry in resourceSet)
            {
                if (entry.Value is UnmanagedMemoryStream)
                {
                    _resources.Add(new ResourcePlayer()
                    {
                        ResourceName = entry.Key.ToString(),
                        Stream = entry.Value as Stream
                    });
                }
            }

            if (_resources.Count > 0)
            {
                var resource = GetByResourceName(_settings.SoundResourceName);
                if (resource == null) resource = GetResourcesList()[0];
                SelectedSound = resource;
            }
        }

        public ResourcePlayer GetByResourceName(string resourceName)
        {
            return _resources.Where(res => res.ResourceName == resourceName).FirstOrDefault();
        }

        public List<ResourcePlayer> GetResourcesList()
        {
            if (_resourcesList == null) _resourcesList = _resources.ToList();
            return _resourcesList;
        }
    }

    class ResourceNameComparer : IComparer<ResourcePlayer>
    {
        public int Compare(ResourcePlayer x, ResourcePlayer y)
        {
            return x.ResourceName.CompareTo(y.ResourceName);
        }
    }
}
