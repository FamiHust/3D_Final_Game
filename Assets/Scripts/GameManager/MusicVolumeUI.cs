using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeUI : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;

    private void Start()
    {
        if (DoNotDestroy.instance != null)
        {
            float currentVolume = DoNotDestroy.instance.GetVolume();
            musicSlider.value = currentVolume;

            musicSlider.onValueChanged.AddListener((value) =>
            {
                DoNotDestroy.instance.SetVolume(value);
            });
        }
    }
}
