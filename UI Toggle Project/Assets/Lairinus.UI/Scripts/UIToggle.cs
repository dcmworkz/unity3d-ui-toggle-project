using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Lairinus.UI
{
    /// <summary>
    /// a UIToggle is a custom element that surpasses the functionality of a regular toggle. It adds many necessary features that otherwise would not exist.
    /// </summary>
    [RequireComponent(typeof(Graphic))]
    public class UIToggle : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private bool _disabled = false;
        [SerializeField] private bool _isOn = false;
        [SerializeField] private Color _toggleIsOnColor = Color.white;
        [SerializeField] private Color _toggleIsOffColor = Color.white;
        [SerializeField] private Color _toggleHoveredColor = Color.white;
        [SerializeField] private Sprite _toggleIsOnSprite = null;
        [SerializeField] private Sprite _toggleIsOffSprite = null;
        [SerializeField] private Sprite _toggleHoveredSprite = null;
        [SerializeField] private UnityEvent _onToggleFalseHandler = new UnityEvent();
        [SerializeField] private UnityEvent _onToggleHandler = new UnityEvent();
        [SerializeField] private UnityEvent _onToggleTrueHandler = new UnityEvent();
        [SerializeField] private bool _allowHoverEvents = false;
        private bool _isInitialized = false;
        public bool disabled { get { return _disabled; } set { _disabled = value; } }

        public bool isOn { get { return _isOn; } }
        public UnityEvent onToggleFalseHandler { get { return _onToggleFalseHandler; } }
        public UnityEvent onToggleHandler { get { return _onToggleHandler; } }
        public UnityEvent onToggleTrueHandler { get { return _onToggleTrueHandler; } }
        private Graphic _thisGraphic = null;
        private Image _thisImage = null;
        private Text _thisText = null;
        private bool _isHovered = false;

        public void OnPointerClick(PointerEventData eventData)
        {
            SetToggledState(!_isOn);
        }

        public void SetToggledState(bool toggledState)
        {
            SetToggledStateInternal(toggledState);
        }

        private void Awake()
        {
            TryInitializeComponents();
            SetToggledState(_isOn);
        }

        private void TryInitializeComponents()
        {
            if (_isInitialized)
                return;

            _isInitialized = true;
            _thisGraphic = GetComponent<Graphic>();
            if (_thisGraphic is Image)
                _thisImage = GetComponent<Image>();
            else if (_thisGraphic is Text)
                _thisText = GetComponent<Text>();
        }

        private void SetToggledStateInternal(bool toggledState)
        {
            TryInitializeComponents();
            _isOn = toggledState;
            if (!_isHovered)
            {
                if (_isOn)
                {
                    _thisGraphic.color = _toggleIsOnColor;
                    SetImageSpriteInternal(_toggleIsOnSprite);
                }
                else
                {
                    _thisGraphic.color = _toggleIsOffColor;
                    SetImageSpriteInternal(_toggleIsOffSprite);
                }
            }
            else
            {
                if (_isHovered && _allowHoverEvents)
                {
                    _thisGraphic.color = _toggleHoveredColor;
                    SetImageSpriteInternal(_toggleHoveredSprite);
                }
            }

            // Call the events
            if (_isOn)
                _onToggleTrueHandler.Invoke();
            else
                _onToggleFalseHandler.Invoke();
            _onToggleHandler.Invoke();
        }

        private void SetImageSpriteInternal(Sprite sprite)
        {
            if (_thisImage != null)
            {
                _thisImage.sprite = sprite;
            }
        }

        public void OnPointerEnter(PointerEventData data)
        {
            if (_allowHoverEvents)
            {
                _isHovered = true;
                SetToggledStateInternal(_isOn);
            }
        }

        public void OnPointerExit(PointerEventData data)
        {
            _isHovered = false;
            SetToggledStateInternal(_isOn);
        }

        public class UIToggleStyles
        {
            public Image image = null;
            public Color toggleOffColor = Color.white;
            public Color toggleOnColor = Color.white;
            public Sprite toggleOffSprite = null;
            public Sprite toggleOnSprite = null;
        }
    }
}