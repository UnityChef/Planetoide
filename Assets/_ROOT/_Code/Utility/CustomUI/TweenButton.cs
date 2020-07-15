using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace EcoMundi.Tweens
{
	public class TweenButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
        private Button _buttonComponent;
		private Transform _buttonTransform;
		private Tween _tween;
        [Range(0.9f,1f)]
        public float animationTargetScale = 0.9f;

		void OnEnable()
		{
			if (_buttonTransform != null)
			{
				_buttonTransform.localScale = Vector3.one;
			}
		}

		void OnDisable()
		{
			if (_buttonTransform != null)
			{
				_buttonTransform.localScale = Vector3.one;
			}	
		}

		private void Start()
		{
            _buttonComponent = GetComponent<Button>();
            _buttonTransform = GetComponent<Transform>();
		}

        #region [-----     EVENT SYSTEM INTERFACE METHODS     -----]

        public void OnPointerDown(PointerEventData eventData)
		{
			if(_buttonComponent == null)
			{
				_tween = _buttonTransform.DOScale(animationTargetScale, .05f).Play().SetAutoKill(true);
			}
			else if (_buttonComponent.interactable)
            {
                _tween = _buttonTransform.DOScale(animationTargetScale, .05f).Play().SetAutoKill(true);
            }
		}

		public void OnPointerUp(PointerEventData eventData)
		{
            _tween = _buttonTransform.DOScale(1f, .05f).Play().SetAutoKill(true);
        }

        #endregion
    }
}

