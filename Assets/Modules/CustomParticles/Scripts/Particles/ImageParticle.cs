using UnityEngine;
using UnityEngine.UI;

namespace CustomParticles
{
    public sealed class ImageParticle : MonoBehaviour
    {
        [SerializeField]
        private Image iconImage;

        public void SetIcon(Sprite icon)
        {
            this.iconImage.sprite = icon;
        }
        
        private void Reset()
        {
            this.iconImage = this.GetComponent<Image>();
        }
    }
}