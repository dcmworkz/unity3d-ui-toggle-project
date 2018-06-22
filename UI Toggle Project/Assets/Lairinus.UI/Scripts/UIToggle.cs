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
        [SerializeField] private GameObject _checkboxIsTrueGraphic = null;

        [SerializeField] private bool _disabled = false;

        [SerializeField] private Color _elementHoveredColor = new Color();

        [SerializeField] private Sprite _elementHoveredSprite = null;

        [SerializeField] private Color _elementNormalColor = new Color();

        [SerializeField] private Sprite _elementNormalSprite = null;

        private bool _isHovered = false;

        private bool _isInitialized = false;

        [SerializeField] private bool _isOn = false;

        [SerializeField] private UnityEvent _onToggleChangedHandler = new UnityEvent();

        [SerializeField] private UnityEvent _onToggleOffHandler = new UnityEvent();

        [SerializeField] private UnityEvent _onToggleOnHandler = new UnityEvent();

        private Graphic _thisGraphic = null;

        private Image _thisImage = null;

        private Text _thisText = null;

        [SerializeField] private GameObject _toggleIsFalseGraphic = null;

        [SerializeField] private GameObject _toggleIsTrueGraphic = null;

        [SerializeField] private ToggleType _toggleType = new ToggleType();

        [SerializeField] private bool _useHoverEvents = false;

        public enum ToggleType
        {
            Checkbox = 1,
            Switch = 2,
            SingleGraphic = 3
        }

        public bool disabled { get { return _disabled; } set { _disabled = value; } }

        public bool isOn { get { return _isOn; } }
        public UnityEvent onToggleFalseHandler { get { return _onToggleOffHandler; } }
        public UnityEvent onToggleHandler { get { return _onToggleChangedHandler; } }
        public UnityEvent onToggleTrueHandler { get { return _onToggleOnHandler; } }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_disabled)
                SetToggledState(!_isOn, false);
        }

        public void OnPointerEnter(PointerEventData data)
        {
            if (_useHoverEvents)
            {
                _isHovered = true;
                SetToggledStateInternal(_isOn, false);
            }
        }

        public void OnPointerExit(PointerEventData data)
        {
            _isHovered = false;
            SetToggledStateInternal(_isOn, false);
        }

        /// <summary>
        /// Sets the state of the toggle through code. The "Force" parameter determines if it should still be set even if the Toggle is disabled
        /// </summary>
        /// <param name="toggledState"></param>
        /// <param name="force"></param>
        public void SetToggledState(bool toggledState, bool force)
        {
            SetToggledStateInternal(toggledState, force);
        }

        private void Awake()
        {
            TryInitializeComponents();
            SetToggledState(_isOn, true);
        }

        private void SetImageSpriteInternal(Sprite sprite)
        {
            if (_thisImage != null)
            {
                _thisImage.sprite = sprite;
            }
        }

        /// <summary>
        /// Internally adjusts the toggle states based on the current value
        /// </summary>
        /// <param name="toggledState"></param>
        /// <param name="force"></param>
        private void SetToggledStateInternal(bool toggledState, bool force)
        {
            _isOn = toggledState;
            TryInitializeComponents();

            if (_disabled && !force)
                return;

            ApplyToggleStylesInternal();

            // Call the events
            _onToggleChangedHandler.Invoke();
            if (_isOn)
                _onToggleOnHandler.Invoke();
            else
                _onToggleOffHandler.Invoke();
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

            ApplyToggleStylesInternal();
        }

        private void ApplyToggleStylesInternal()
        {
            switch (_toggleType)
            {
                // We only hide/show one graphic
                case ToggleType.Checkbox:
                    {
                        if (_isOn)
                        {
                            if (_checkboxIsTrueGraphic != null)
                                _checkboxIsTrueGraphic.gameObject.SetActive(true);
                        }
                        else
                        {
                            if (_checkboxIsTrueGraphic != null)
                                _checkboxIsTrueGraphic.gameObject.SetActive(false);
                        }
                    }
                    break;

                // We hide/show two graphics
                case ToggleType.Switch:
                    {
                        if (_isOn)
                        {
                            if (_toggleIsFalseGraphic != null)
                                _toggleIsFalseGraphic.SetActive(false);

                            if (_toggleIsTrueGraphic != null)
                                _toggleIsTrueGraphic.SetActive(true);
                        }
                        else
                        {
                            if (_toggleIsFalseGraphic != null)
                                _toggleIsFalseGraphic.SetActive(true);

                            if (_toggleIsTrueGraphic != null)
                                _toggleIsTrueGraphic.SetActive(false);
                        }
                    }
                    break;

                // We modify the current Graphic
                case ToggleType.SingleGraphic:
                    {
                    }
                    break;
            }

            // Handle the Hover events
            if (_isHovered && _useHoverEvents)
            {
                _thisGraphic.color = _elementHoveredColor;
                SetImageSpriteInternal(_elementHoveredSprite);
            }
            else if (!_isHovered && _useHoverEvents)
            {
                _thisGraphic.color = _elementNormalColor;
                SetImageSpriteInternal(_elementNormalSprite);
            }
        }
    }
}