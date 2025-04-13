using UnityEngine;

public class LandGroup : MonoBehaviour
{
    public ZoneElement[] zonesInThisLand;
    public GameObject earthModel;
    public GameObject waterModel;
    public GameObject forestModel;
    public GameObject swampModel;

    public ElementType landElement;
    
    public void SetLandElement(ElementType newElement)
    {
        landElement = newElement;

        foreach (ZoneElement zone in zonesInThisLand)
        {
            if (zone != null)
            {
                zone.elementType = newElement;
            }
        }

        earthModel?.SetActive(false);
        waterModel?.SetActive(false);
        forestModel?.SetActive(false);
        swampModel?.SetActive(false);

        switch (newElement)
        {
            case ElementType.Earth:
                earthModel?.SetActive(true);
                break;
            case ElementType.Water:
                waterModel?.SetActive(true);
                break;
            case ElementType.Forest:
                forestModel?.SetActive(true);
                break;
            case ElementType.Swamp:
                swampModel?.SetActive(true);
                break;
        }
    }
}
