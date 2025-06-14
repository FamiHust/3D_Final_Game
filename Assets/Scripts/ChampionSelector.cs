using UnityEngine;
using UnityEngine.SceneManagement;

public class ChampionSelector : MonoBehaviour
{
    public static AIType selectedChampion;

    public void SelectSonTinh()
    {
        selectedChampion = AIType.SonTinh;
    }

    public void SelectThuyTinh()
    {
        selectedChampion = AIType.ThuyTinh;
    }

    public void SelectYeuMa()
    {
        selectedChampion = AIType.YeuMa;
    }

    public void SelectLacDieu()
    {
        selectedChampion = AIType.LacDieu;
    }
}
