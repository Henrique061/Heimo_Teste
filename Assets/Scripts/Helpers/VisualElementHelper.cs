using UnityEngine.UIElements;
using System;

public class VisualElementHelper
{
    /// <summary>Get the element inside of a root Document UI, passing its type as generic</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="root"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static T GetVisualElement<T>(VisualElement root, string name) where T : VisualElement
    {
        T element = root.Q<T>(name);

        if (element == null)
            throw new Exception($"Cannot find element of type {typeof(T)}, of name {name}");

        return element;
    }
}
