using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HookIsland.Animations
{
    public class UIAnimations
    {
        //FadeIn
        private readonly float initialAlphaPercentFadeIn = 0;
        private readonly float finalAlphaPercentFadeIn = 1f;

        //FadeOut
        private readonly float initialAlphaPercentFadeOut = 0.9f;
        private readonly float finalAlphaPercentFadeOut = 0;

        public IEnumerator Flash(Image image, float time, Color color)
        {
            if (image)
            {
                image.enabled = true;
                yield return FadeIn(image, time, color);
                yield return FadeOut(image, time, color);
                image.enabled = false;
            }
        }

        public IEnumerator FadeIn(Image image, float time, Color color)
        {
            if (image)
            {
                image.enabled = true;
                yield return Fade(initialAlphaPercentFadeIn, finalAlphaPercentFadeIn, time, color, image, null);
            }
        }

        public IEnumerator FadeIn(CanvasGroup canvasGroup, float time, Color color)
        {
            if (canvasGroup)
            {
                canvasGroup.interactable = false;
                yield return Fade(initialAlphaPercentFadeIn, finalAlphaPercentFadeIn, time, color, null, canvasGroup);
                canvasGroup.interactable = true;
            }
        }        

        public IEnumerator FadeOut(Image image, float time, Color color)
        {
            if (image)
            {
                image.enabled = true;
                yield return Fade(initialAlphaPercentFadeOut, finalAlphaPercentFadeOut, time, color, image, null);
                image.enabled = false;
            }        
        }

        public IEnumerator FadeOut(CanvasGroup canvasGroup, float time, Color color)
        {
            if (canvasGroup)
            {
                canvasGroup.interactable = true;
                yield return Fade(initialAlphaPercentFadeOut, finalAlphaPercentFadeOut, time, color, null, canvasGroup);
                canvasGroup.interactable = false;
            }
        }

        private IEnumerator Fade(float initialAlphaPercent, float finalAlphaPercent, float time, Color color, Image image = null, CanvasGroup canvasGroup = null)
        {
            if (initialAlphaPercent < 0) initialAlphaPercent = 0;
            if (finalAlphaPercent > 1) finalAlphaPercent = 1;
            
            if (image)
            {
                yield return FadeTween(initialAlphaPercent, finalAlphaPercent, time, color, image, null);
            }
            else if (canvasGroup)
            {
                yield return FadeTween(initialAlphaPercent, finalAlphaPercent, time, color, null, canvasGroup);
            }            
        }

        public IEnumerator FadeTween(float initialAlpha, float finalAlpha, float time, Color color, Image image = null, CanvasGroup canvasGroup = null)
        {
            if (image) image.color = color;
            float elapsedTime = 0f;
            SetAlphaInImageOrCanvasGroup(initialAlpha, image, canvasGroup);
            while (elapsedTime < time)
            {
                float timePercent = Mathf.Clamp01(elapsedTime / time);               
                float currentAlpha = Mathf.Lerp(initialAlpha, finalAlpha, timePercent);
                SetAlphaInImageOrCanvasGroup(currentAlpha, image, canvasGroup);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            SetAlphaInImageOrCanvasGroup(finalAlpha, image, canvasGroup);
            yield return null;
        }

        public IEnumerator MoveTween(CanvasGroup canvasGroup, Vector3 initialPosition, Vector3 finalPosition, float time)
        {
            float elapsedTime = 0f;
            canvasGroup.transform.position = initialPosition;
            while (elapsedTime < time)
            {                
                float timePercent = Mathf.Clamp01(elapsedTime / time);
                canvasGroup.transform.position = Vector3.Lerp(initialPosition, finalPosition, timePercent);
                elapsedTime += Time.deltaTime;                
                yield return null;
            }
            canvasGroup.transform.position = finalPosition;
            yield return null;
        }

        public IEnumerator ScoreCountTween(TextMeshProUGUI scoreText, int initialScore, int finalScore, float time)
        {
            int currentScoreCount = initialScore;
            float elapsedTime = 0f;
            scoreText.text = currentScoreCount.ToString();

            if (finalScore == 0) yield break;
            float timeBetweenNumbers = (float)(time / finalScore);

            while (currentScoreCount < finalScore)
            {
                if (elapsedTime > (currentScoreCount * timeBetweenNumbers))
                {
                    currentScoreCount++;
                    scoreText.text = currentScoreCount.ToString();
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            scoreText.text = finalScore.ToString();
            yield return null;
        }

        private void SetAlphaInImageOrCanvasGroup(float alpha, Image image = null, CanvasGroup canvasGroup = null)
        {
            if (image)
            {
                SetImageAlpha(image, alpha);
            }
            else if (canvasGroup)
            {
                canvasGroup.alpha = alpha;
            }
        }

        private void SetImageAlpha(Image image, float alpha)
        {
            Color imageColor = image.color;
            imageColor.a = alpha;
            image.color = imageColor;
        }
    }
}
