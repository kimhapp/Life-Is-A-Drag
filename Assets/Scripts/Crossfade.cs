using UnityEngine;

public class Crossfade : MonoBehaviour
{
    [SerializeField] PlayerController player;

    void BeginCrossfade()
    {
        player.cannotControl();
    }

    void EndCrossfade()
    {
        player.canControl();
    }

    void TeleportCrossfade()
    {
        if (player.interactable is Teleporter teleporter)
        {
            teleporter.Teleport();
        }
    }
}
