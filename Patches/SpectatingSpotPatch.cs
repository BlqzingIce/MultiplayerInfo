using SiraUtil.Affinity;
using Zenject;

namespace MultiplayerInfo.Patches
{
    internal class SpectatingSpotPatch : IAffinity
    {
        [Inject] PluginConfig _config = null!;

        [AffinityPostfix]
        [AffinityPatch(typeof(MultiplayerSpectatingSpotPickerViewController), nameof(MultiplayerSpectatingSpotPickerViewController.RefreshSpectatingSpotName))]
        private void Postfix(StepValuePicker ____stepValuePicker)
        {
            if (_config.EnableNicknames)
            {
                for (int i = _config.Nicknames.Count - 1; i >= 0; i--)
                {
                    if (_config.Nicknames[i].Name.Equals(____stepValuePicker.text))
                    {
                        ____stepValuePicker.text = _config.Nicknames[i].Nick;
                        break;
                    }
                }
            }
        }
    }
}