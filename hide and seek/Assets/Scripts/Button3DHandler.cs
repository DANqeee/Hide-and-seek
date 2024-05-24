using UnityEngine;

public class Button3DHandler : MonoBehaviour
{
    public ChangeTagAndTeleport changeTagAndTeleport;
    public string tagToSet;

    private void OnMouseDown()
    {
        changeTagAndTeleport.SetTagAndTeleport(tagToSet);
    }
}