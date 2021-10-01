// https://www.patrykgalach.com/2020/01/20/readonly-attribute-in-unity-editor/
/* -------------------------------------------------------------------------- */
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Make this variable non-editable but still show in Inspector
/// </summary>
public class ReadOnlyAttribute : PropertyAttribute { }


#if UNITY_EDITOR
/// <summary>
/// This class contain custom drawer for ReadOnly attribute.
/// </summary>
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    /// <summary>
    /// Unity method for drawing GUI in Editor
    /// </summary>
    /// <param name="position">Position.</param>
    /// <param name="property">Property.</param>
    /// <param name="label">Label.</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Saving previous GUI enabled value
        var previousGUIState = GUI.enabled;
        // Disabling edit for property
        GUI.enabled = false;
        // Drawing Property
        EditorGUI.PropertyField(position, property, label);
        // Setting old GUI enabled value
        GUI.enabled = previousGUIState;
    }
}

#else
public class ReadOnlyDrawer {  }
#endif