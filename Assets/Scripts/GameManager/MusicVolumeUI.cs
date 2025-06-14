using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeUI : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;

    private void Start()
    {
        if (DoNotDestroy.instance != null)
        {
            // Đồng bộ slider với volume hiện tại
            float currentVolume = DoNotDestroy.instance.GetVolume();
            musicSlider.value = currentVolume;

            // Gán sự kiện thay đổi volume
            musicSlider.onValueChanged.AddListener((value) =>
            {
                DoNotDestroy.instance.SetVolume(value);
            });
        }
        else
        {
            Debug.LogWarning("DoNotDestroy instance not found!");
        }
    }
}
