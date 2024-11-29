using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class UIAnimationsHelper
{
    /// <summary>Handles the animation by toggling classes of the element. Call by Coroutine</summary>
    /// <param name="element"></param>
    /// <param name="className"></param>
    /// <returns></returns>
    public static IEnumerator StartAnimationByClasses(VisualElement element, string className)
    {
        yield return new WaitForSeconds(0.1f);

        element.ToggleInClassList(className);
        element.RegisterCallback<TransitionEndEvent>(evt =>
        {
            element.ToggleInClassList(className);
        });
    }

    /// <summary>Handles the rotation animatoin loop of elements. Call by coroutine</summary>
    /// <param name="element"></param>
    /// <param name="speed"></param>
    /// <returns></returns>
    public static IEnumerator StartRotationAnimationLoop(VisualElement element, float speed)
    {
        yield return new WaitForSeconds(0.1f);

        element.transform.rotation *= Quaternion.Euler(0f, 0f, speed);
        element.RegisterCallback<TransitionEndEvent>(evt =>
        {
            element.transform.rotation *= Quaternion.Euler(0f, 0f, speed);
        });
    }
}
