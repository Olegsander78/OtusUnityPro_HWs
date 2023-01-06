using UnityEngine;

namespace CustomParticles
{
    public sealed class SpriteRendererParticle : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public void SetIcon(Sprite icon)
        {
            this.spriteRenderer.sprite = icon;
        }

        private void Reset()
        {
            this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        }
    }
}