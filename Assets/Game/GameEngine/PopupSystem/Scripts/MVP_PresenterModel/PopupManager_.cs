using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class PopupManager_ : MonoBehaviour, Popup.ICallback
{
    [SerializeField]
    private PopupHolder[] allPopups;

    private readonly Dictionary<PopupName_, Popup> activePopups = new();

    private void Awake()
    {
        foreach (var popupHolder in this.allPopups)
        {
            popupHolder.popup.gameObject.SetActive(false);
        }
    }

    [Title("Methods")]
    [Button]
    public void ShowPopup(PopupName_ name, object args = null)
    {
        if (this.IsPopupActive(name))
        {
            return;
        }

        var popup = this.FindPopup(name);
        popup.gameObject.SetActive(true);
        popup.Show(args: args, callback: this);
        this.activePopups.Add(name, popup);
    }

    [Button]
    public void HidePopup(PopupName_ name)
    {
        if (!this.IsPopupActive(name))
        {
            return;
        }

        var popup = this.activePopups[name];
        popup.Hide();
        popup.gameObject.SetActive(false);
        this.activePopups.Remove(name);
    }

    [Button]
    public bool IsPopupActive(PopupName_ name)
    {
        return this.activePopups.ContainsKey(name);
    }

    void Popup.ICallback.OnClose(Popup popup)
    {
        var name = this.FindName(popup);
        this.HidePopup(name);
    }

    private PopupName_ FindName(Popup popup)
    {
        foreach (var holder in allPopups)
        {
            if (ReferenceEquals(holder.popup, popup))
            {
                return holder.name;
            }
        }

        throw new Exception($"Name of popup {popup.name} is not found!");
    }

    private Popup FindPopup(PopupName_ name)
    {
        foreach (var holder in this.allPopups)
        {
            if (holder.name == name)
            {
                return holder.popup;
            }
        }

        throw new Exception($"Popup with name {name} is not found!");
    }

    [Serializable]
    private struct PopupHolder
    {
        [SerializeField]
        public PopupName_ name;

        [SerializeField]
        public Popup popup;
    }
}