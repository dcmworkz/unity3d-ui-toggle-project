using UnityEditor;
using UnityEngine;
using Lairinus.UI;
using UnityEngine.UI;

namespace Lairinus.UI.EditorScripts
{
    [CustomEditor(typeof(UIToggle))]
    public class UITransitionerEditor : Editor
    {
        private SerializedProperty _disabled = null;
        private SerializedProperty _isOn = null;
        private SerializedProperty _toggleOnColor = null;
        private SerializedProperty _toggleOffColor = null;
        private SerializedProperty _toggleHoveredColor = null;
        private SerializedProperty _toggleOnSprite = null;
        private SerializedProperty _toggleOffSprite = null;
        private SerializedProperty _toggleHoveredSprite = null;
        private SerializedProperty _allowHoverEvents = null;
        private Graphic _targetGraphic = null;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            RenderInspector();
            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            _disabled = serializedObject.FindProperty(EditorStringContainer.propDisabled);
            _isOn = serializedObject.FindProperty(EditorStringContainer.propIsOn);
            _toggleOnColor = serializedObject.FindProperty(EditorStringContainer.propToggleOnColor);
            _toggleOffColor = serializedObject.FindProperty(EditorStringContainer.propToggleOffColor);
            _toggleHoveredColor = serializedObject.FindProperty(EditorStringContainer.propToggleHoveredColor);
            _toggleOnSprite = serializedObject.FindProperty(EditorStringContainer.propToggleOnSprite);
            _toggleOffSprite = serializedObject.FindProperty(EditorStringContainer.propToggleOffSprite);
            _toggleHoveredSprite = serializedObject.FindProperty(EditorStringContainer.propToggleHoveredSprite);
            _allowHoverEvents = serializedObject.FindProperty(EditorStringContainer.propAllowHoverEvents);
            UIToggle tar = (UIToggle)target;
            _targetGraphic = tar.gameObject.GetComponent<Graphic>();
        }

        private void RenderInspector()
        {
            EditorGUILayout.PropertyField(_isOn);

            EditorGUILayout.PropertyField(_toggleOffColor);
            EditorGUILayout.PropertyField(_toggleOnColor); ;

            if (_targetGraphic is Image)
            {
                EditorGUILayout.PropertyField(_toggleOnSprite);
                EditorGUILayout.PropertyField(_toggleOffSprite);
            }

            EditorGUILayout.PropertyField(_allowHoverEvents);
            if (_allowHoverEvents.boolValue)
            {
                EditorGUILayout.PropertyField(_toggleHoveredColor);
                EditorGUILayout.PropertyField(_toggleHoveredSprite);
            }

            // Show Toggle Events
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_disabled);
        }

        public class EditorStringContainer
        {
            public const string propDisabled = "_disabled";
            public const string propToggleOnColor = "_toggleIsOnColor";
            public const string propToggleOffColor = "_toggleIsOffColor";
            public const string propToggleHoveredColor = "_toggleHoveredColor";
            public const string propToggleOnSprite = "_toggleIsOnSprite";
            public const string propToggleOffSprite = "_toggleIsOffSprite";
            public const string propToggleHoveredSprite = "_toggleHoveredSprite";
            public const string propAllowHoverEvents = "_allowHoverEvents";
            public const string propIsOn = "_isOn";
        }
    }
}