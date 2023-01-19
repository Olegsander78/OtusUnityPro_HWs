using UnityEngine;

namespace LocalizationModule
{
    public interface IComponent
    {
        void UpdateLanguage(SystemLanguage language);
    }
}