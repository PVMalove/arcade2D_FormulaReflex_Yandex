using UnityEngine;

namespace CodeBase.UI.Root
{
    public class UIRoot : MonoBehaviour, IUIRoot
    {
        [field: SerializeField] public Transform ContainerScreen { get; private set; }
        [field: SerializeField] public Transform ContainerHUD { get; private set; }
        [field: SerializeField] public Transform ContainerPopup { get; private set; }
    }
}