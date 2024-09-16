using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FusionFuryGame
{
    public class FloatingText : MonoBehaviour
    {
        public TMP_Text textMesh;
        private Color defaultColor;
        private Sequence floatingSequence;
        private Vector3 originalScale;

        private Camera mainCamera;
        private void Awake()
        {
            originalScale = textMesh.transform.localScale;
            defaultColor = textMesh.color;
            mainCamera = Camera.main;  // Get a reference to the main camera
        }

        public void Initialize(string text, Color color, Transform target)
        {
            textMesh.text = text;
            textMesh.color = color;

            // Position the floating text above the target
            Vector3 targetPosition = target.position + Vector3.up * 2; // Adjust offset as needed
            transform.position = targetPosition;

            FaceCamera();  // Make sure the text faces the camera

            // Reset any previous animation
            if (floatingSequence != null)
            {
                floatingSequence.Kill();
            }

            // Create the floating animation sequence
            floatingSequence = DOTween.Sequence();
            floatingSequence.Append(transform.DOMoveY(targetPosition.y + Random.Range(0.5f, 1), 0.7f).SetRelative()); // Move up
            //floatingSequence.Join(transform.DOMoveX(targetPosition.x + Random.Range(1 , 2), 0.7f).SetRelative()); // Move up
            floatingSequence.Join(transform.DOScale(0.5f, 0.5f)); // Scale
            floatingSequence.Join(textMesh.DOFade(0, 1f)); // Fade out
            floatingSequence.Join(transform.DOScale(0, 0.5f)); // Scale
            floatingSequence.OnComplete(ReturnToPool);
        }

        public void ShowFloatingText(string message, Color color, Transform enemyTransform, float duration = 1f)
        {
            textMesh.text = message;
            textMesh.color = color;

            // Randomize the size and position
            float randomSize = Random.Range(0.8f, 1.2f);
            transform.position = enemyTransform.position;
            textMesh.transform.localScale = originalScale * randomSize;


            // Bouncing effect
            textMesh.transform.DOMoveY(transform.position.y + Random.Range(25, 100), duration ).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutBounce);
            textMesh.transform.DOScale(Vector3.zero, duration);
            // Fade out and return to pool
            textMesh.DOFade(0, duration).OnComplete(() => ReturnToPool());
        }

        private void FaceCamera()
        {
            // Rotate the text to face the camera
            Vector3 direction = transform.position - mainCamera.transform.position;
            direction.y = 0; // Keep the text upright by nullifying the Y rotation
            transform.rotation = Quaternion.LookRotation(direction);
        }

        private void ReturnToPool()
        {
            textMesh.transform.localScale = originalScale;
            textMesh.color = defaultColor;
            textMesh.alpha = 1;
            PoolManager.Instance.ReturnToPool( gameObject, PooledObjectNames.FloatingText.ToString());
        }
    }
}