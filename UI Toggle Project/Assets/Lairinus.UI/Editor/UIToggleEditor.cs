using UnityEditor;
using UnityEngine;
using Lairinus.UI;
using UnityEngine.UI;

namespace Lairinus.UI.EditorScripts
{
    [CustomEditor(typeof(UIToggle))]
    public class UITransitionerEditor : Editor
    {
        private SerializedProperty _checkboxIsTrueGraphic = null;
        private SerializedProperty _disabled = null;
        private SerializedProperty _elementHoveredColor = null;
        private SerializedProperty _elementHoveredSprite = null;
        private SerializedProperty _elementNormalColor = null;
        private SerializedProperty _elementNormalSprite = null;
        private bool _eventsFoldout = false;
        private SerializedProperty _isOn = null;
        private SerializedProperty _onToggleChangedHandler = null;
        private SerializedProperty _onToggleOffHandler = null;
        private SerializedProperty _onToggleOnHandler = null;
        private Graphic _thisGraphic = null;
        private SerializedProperty _toggleIsFalseGraphic = null;
        private SerializedProperty _toggleIsTrueGraphic = null;
        private SerializedProperty _toggleType = null;
        private SerializedProperty _useHoverEvents = null;
        private UIToggle _thisObject = null;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            RenderInspector();
            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            _checkboxIsTrueGraphic = serializedObject.FindProperty(EditorStringContainer.checkboxIsTrueGraphic);
            _disabled = serializedObject.FindProperty(EditorStringContainer.disabled);
            _elementHoveredColor = serializedObject.FindProperty(EditorStringContainer.elementHoveredColor);
            _elementNormalColor = serializedObject.FindProperty(EditorStringContainer.elementNormalColor);
            _elementNormalSprite = serializedObject.FindProperty(EditorStringContainer.elementNormalSprite);
            _elementHoveredSprite = serializedObject.FindProperty(EditorStringContainer.elementHoveredSprite);
            _isOn = serializedObject.FindProperty(EditorStringContainer.isOn);
            _onToggleChangedHandler = serializedObject.FindProperty(EditorStringContainer.onToggleChangedHandler);
            _onToggleOffHandler = serializedObject.FindProperty(EditorStringContainer.onToggleOffHandler);
            _onToggleOnHandler = serializedObject.FindProperty(EditorStringContainer.onToggleOnHandler);
            _toggleIsFalseGraphic = serializedObject.FindProperty(EditorStringContainer.toggleIsFalseGraphic);
            _toggleIsTrueGraphic = serializedObject.FindProperty(EditorStringContainer.toggleIsTrueGraphic);
            _toggleType = serializedObject.FindProperty(EditorStringContainer.toggleType);
            _useHoverEvents = serializedObject.FindProperty(EditorStringContainer.useHoverEvents);
            _thisObject = (UIToggle)target;
            _thisGraphic = _thisObject.gameObject.GetComponent<Graphic>();
        }

        private void RenderInspector()
        {
            EditorGUILayout.LabelField("General");
            EditorGUILayout.PropertyField(_isOn);
            EditorGUILayout.PropertyField(_disabled);
            EditorGUILayout.PropertyField(_useHoverEvents);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.PropertyField(_toggleType);

            // Checkbox
            if (_toggleType.enumValueIndex == 0)
            {
                EditorGUILayout.PropertyField(_checkboxIsTrueGraphic);
            }
            // Switch
            else if (_toggleType.enumValueIndex == 1)
            {
                EditorGUILayout.PropertyField(_toggleIsFalseGraphic);
                EditorGUILayout.PropertyField(_toggleIsTrueGraphic);
            }

            // Show Hover options
            EditorGUILayout.Space();
            if (_useHoverEvents.boolValue)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                EditorGUILayout.LabelField("Hover Settings");

                EditorGUILayout.PropertyField(_elementNormalColor);
                if (_thisGraphic is Image)
                    EditorGUILayout.PropertyField(_elementNormalSprite);

                EditorGUILayout.PropertyField(_elementHoveredColor);
                if (_thisGraphic is Image)
                    EditorGUILayout.PropertyField(_elementHoveredSprite);
            }

            // Show Events
            EditorGUILayout.Space();
            _eventsFoldout = EditorGUILayout.Foldout(_eventsFoldout, "Event Callbacks");
            if (_eventsFoldout)
            {
                EditorGUILayout.PropertyField(_onToggleOffHandler);
                EditorGUILayout.PropertyField(_onToggleOnHandler);
                EditorGUILayout.PropertyField(_onToggleChangedHandler);
            }

            if (_thisObject != null)
                _thisObject.ApplyToggleStyles();
        }

        public class EditorStringContainer
        {
            public const string checkboxIsTrueGraphic = "_checkboxIsTrueGraphic";
            public const string disabled = "_disabled";
            public const string elementHoveredColor = "_elementHoveredColor";
            public const string elementHoveredSprite = "_elementHoveredSprite";
            public const string elementNormalColor = "_elementNormalColor";
            public const string elementNormalSprite = "_elementNormalSprite";
            public const string isOn = "_isOn";
            public const string onToggleChangedHandler = "_onToggleChangedHandler";
            public const string onToggleOffHandler = "_onToggleOffHandler";
            public const string onToggleOnHandler = "_onToggleOnHandler";
            public const string toggleIsFalseGraphic = "_toggleIsFalseGraphic";
            public const string toggleIsTrueGraphic = "_toggleIsTrueGraphic";
            public const string toggleType = "_toggleType";
            public const string useHoverEvents = "_useHoverEvents";
        }
    }
}