using UnityEngine;
using UnityEngine.UIElements;
using System;

[RequireComponent(typeof(EssentialUIController))]
public class ToolbarAnimations : MonoBehaviour
{
    #region VARS
    // element names
    private readonly string freeIconContainerName = "FreeContainer";

    // class names
    private readonly string freeIconClassName = "iconScale-grow";

    // references
    private EssentialUIController essentialUIController;
    private VisualElement rootElement;
    #endregion

    #region UNITY METHODS
    private void Start()
    {
        essentialUIController = GetComponent<EssentialUIController>();
        rootElement = essentialUIController.RootDocument.rootVisualElement;

        InitializeAnimations();
    }
    #endregion

    #region METHODS
    private void InitializeAnimations()
    {
        var freeContainer = VisualElementHelper.GetVisualElement<VisualElement>(rootElement, freeIconContainerName);
        StartCoroutine(UIAnimationsHelper.StartAnimationByClasses(freeContainer, freeIconClassName));
    }
    #endregion
}
