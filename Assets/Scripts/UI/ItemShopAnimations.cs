using UnityEngine;
using UnityEngine.UIElements;
using System;

[RequireComponent(typeof(EssentialUIController))]
public class ItemShopAnimations : MonoBehaviour
{
    #region INSPECTOR VARS
    [Header("ITEMS")]
    [Tooltip("The number of items present in the shop")]
    [SerializeField] private int numberOfItems;

    [Space(10)][Header("ANIMATIONS SPEED")]
    [Tooltip("The speed of the rotation value of the glow in items background")]
    [SerializeField] private float glowRotationSpeed;
    #endregion

    #region VARS
    // elements names
    private readonly string itemPrefix = "ShopItem_";
    private readonly string glowImageName = "img_itemGlow";
    private readonly string hourIconName = "img_hourIcon";

    // classes names
    private readonly string hourRotationClass = "iconRotation-left";

    // references
    private EssentialUIController essentialUIController;
    private VisualElement rootElement;
    #endregion

    #region UNITY EVENTS
    private void Start()
    {
        essentialUIController = GetComponent<EssentialUIController>();
        rootElement = essentialUIController.RootDocument.rootVisualElement;

        InitializeAnimations();
    }
    #endregion

    #region METHODS
    /// <summary>Initialize all parameters for all animations in the shop item</summary>
    private void InitializeAnimations()
    {
        // prevent null condition
        if (numberOfItems <= 0)
        {
            Debug.LogWarning("No number of items assigned in ItemShopAnimations.");
            return;
        }

        for (int i = 0; i < numberOfItems; i++)
        {
            // item
            var item = GetShopItemElement(i);
            if (item == null) continue;

            // glow image
            var glowImage = VisualElementHelper.GetVisualElement<VisualElement>(item, glowImageName);
            StartCoroutine(UIAnimationsHelper.StartRotationAnimationLoop(glowImage, glowRotationSpeed));

            // hour icon
            var hourIcon = VisualElementHelper.GetVisualElement<VisualElement>(item, hourIconName);
            StartCoroutine(UIAnimationsHelper.StartAnimationByClasses(hourIcon, hourRotationClass));
        }

    }

    #region AUX METHODS
    /// <summary>Get the item container for the specific item index</summary>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private VisualElement GetShopItemElement(int index)
    {
        var item = VisualElementHelper.GetVisualElement<VisualElement>(rootElement, $"{itemPrefix}{index}");

        // throw error if cannot find item shop
        if (item == null)
            throw new Exception($"Cannot find shop item with the index {index}");

        return item;
    }
    #endregion
    #endregion
}
