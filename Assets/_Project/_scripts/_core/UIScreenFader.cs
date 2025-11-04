using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project._scripts._core
{
    public class UIScreenFader : Singleton<UIScreenFader>
    {
        [SerializeField] private Image fadePanel;
        [SerializeField] private float fadeSpeed = 1.5f;

        [SerializeField] private Color fadePanelColor = Color.black;
        private Color clearColor = Color.clear;

        protected override void Awake()
        {
            base.Awake();
            fadePanel.color = Color.clear;
            gameObject.SetActive(false);
        }

        public void FadeOut(Action onComplete = null)
        {
            StartCoroutine(Fade(clearColor, fadePanelColor, onComplete));
        }

        public void FadeIn(Action onComplete = null)
        {
            StartCoroutine(Fade(fadePanelColor, clearColor, onComplete, true));
        }

        private IEnumerator Fade(Color from, Color to, Action onComplete, bool deactivate = false)
        {
            Debug.Log("Fade");
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * fadeSpeed;
                fadePanel.color = Color.Lerp(from, to, t);
                yield return null;
            }

            onComplete?.Invoke();

            if (deactivate)
                gameObject.SetActive(false);
        }
    }
}
