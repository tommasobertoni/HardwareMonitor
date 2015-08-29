using System.Media;

namespace HardwareMonitor.Client.Temperature.Utils
{
    public class ResourcePlayer : SoundPlayer
    {
        public string Name { get; private set; }

        private string _resourceName;
        public string ResourceName
        {
            get
            {
                return _resourceName;
            }

            set
            {
                _resourceName = value;
                if (_resourceName != null) BuildName();
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ResourcePlayer)) return false;

            var rp = obj as ResourcePlayer;
            return _resourceName == rp._resourceName;
        }

        public override int GetHashCode()
        {
            return _resourceName.GetHashCode();
        }

        private void BuildName()
        {
            var s = _resourceName.Replace("_", " ");
            Name = char.ToUpper(s[0]) + s.Substring(1);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
