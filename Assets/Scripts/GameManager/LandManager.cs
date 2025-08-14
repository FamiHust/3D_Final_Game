using UnityEngine;

public class LandManager : MonoBehaviour
{
    public LandGroup[] lands = new LandGroup[4];

    void Start()
    {
        lands[0].SetLandElement(ElementType.Earth);
        lands[1].SetLandElement(ElementType.Water);
        lands[2].SetLandElement(ElementType.Forest);
        lands[3].SetLandElement(ElementType.Swamp);
    }
}
