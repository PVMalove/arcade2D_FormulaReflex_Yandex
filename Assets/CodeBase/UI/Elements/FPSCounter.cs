using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] private Text fpsText;
        private float deltaTime;

        private void Update()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            fpsText.text = $"FPS: {fps:0}";
        }
    }
}