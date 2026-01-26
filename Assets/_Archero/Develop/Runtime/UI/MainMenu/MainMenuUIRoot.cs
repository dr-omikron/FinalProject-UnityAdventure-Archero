using UnityEngine;

namespace _Archero.Develop.Runtime.UI.MainMenu
{
    public class MainMenuUIRoot : MonoBehaviour
    {
        [field:SerializeField] public Transform HUDLayer { get; private set; }
        [field:SerializeField] public Transform PupUpsLayer { get; private set; }
        [field:SerializeField] public Transform VFXUnderPupUpsLayer { get; private set; }
        [field:SerializeField] public Transform VFXOverPupUpsLayer { get; private set; }
    }
}
