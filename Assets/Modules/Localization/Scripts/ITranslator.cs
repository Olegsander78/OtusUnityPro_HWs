using UnityEngine;

namespace LocalizationModule
{
    public interface ITranslator<out T>
    {
        T GetTranslation(string key, SystemLanguage language);
    }
}