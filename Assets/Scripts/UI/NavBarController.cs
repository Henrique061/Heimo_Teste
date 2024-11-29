using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(EssentialUIController))]
public class NavBarController : MonoBehaviour
{
    #region INSPECTOR VARS
    [Header("BUTTONS")]
    [Tooltip("The audio source that will be reproduced when clicking a button")]
    [SerializeField] private AudioSource clickSound;
    #endregion

    #region VARS
    // elements names
    private readonly string backButtonName = "btn_backButton";
    private readonly string homeButtonName = "btn_homeButton";

    // references
    private EssentialUIController essentialUIController;
    private VisualElement rootElement;
    #endregion

    #region UNITY METHODS
    private void Start()
    {
        essentialUIController = GetComponent<EssentialUIController>();
        rootElement = essentialUIController.RootDocument.rootVisualElement;

        InitializeElements();
    }
    #endregion

    #region METHODS
    /// <summary>Initializes all neccessary elements of navbar</summary>
    private void InitializeElements()
    {
        // elements
        var backButton = VisualElementHelper.GetVisualElement<VisualElement>(rootElement, backButtonName);
        var homeButton = VisualElementHelper.GetVisualElement<VisualElement>(rootElement, homeButtonName);

        // set click events
        backButton.RegisterCallback<ClickEvent>(OnClickButton);
        homeButton.RegisterCallback<ClickEvent>(OnClickButton);
    }
    #endregion

    #region EVENTS
    private void OnClickButton(ClickEvent evt) => clickSound.Play();
    #endregion
}
