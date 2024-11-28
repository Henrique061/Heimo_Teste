using UnityEngine;
using UnityEngine.UIElements;

public class EssentialUIController : MonoBehaviour
{
    #region INSPECTOR VARS
    [Header("UI DOCUMENT")]
    [Tooltip("The root UI document of the menu itself")]
    [SerializeField] UIDocument rootDocument;
    #endregion

    #region GETTERS
    public UIDocument RootDocument
    {
        get { return rootDocument; }
    }
    #endregion
}
