using UnityEngine;
using UnityEngine.UIElements;

public class EssentialUIController : MonoBehaviour
{
    #region INSPECTOR VARS
    [Header("UI DOCUMENT")]
    [Tooltip("The root UI document of the menu itself")]
    [SerializeField] UIDocument rootDocument;

    [Space(10)][Header("SOUNDS")]
    [Tooltip("The sound when clicking a button")]
    [SerializeField] private AudioSource clickSound;
    [Tooltip("The sound when clicking in the plus button of currency")]
    [SerializeField] private AudioSource coinSound;
    #endregion

    #region GETTERS
    public UIDocument RootDocument
    {
        get { return this.rootDocument; }
    }

    public AudioSource ClickSound
    {
        get { return this.clickSound; }
    }

    public AudioSource CoinSound
    {
        get { return this.coinSound; }
    }
    #endregion
}
