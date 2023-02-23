using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LockObjectPosition : MonoBehaviour
{
    private bool isObjectLocked = false;
    public event Action<bool> OnObjectLockedStateChanged;

    [SerializeField] private Image toggleButtonImage;
    [SerializeField] private TextMeshProUGUI toggleButtonText;

    public void ToggleObjectLockedState()
    {
        isObjectLocked = !isObjectLocked;
        OnObjectLockedStateChanged?.Invoke(isObjectLocked);
        ToggleButtonAppearance();
    }

    private void ToggleButtonAppearance()
    {
        if (isObjectLocked)
        {
            toggleButtonText.text = "Unlock \nObject";
            toggleButtonImage.color = Color.green;
        }
        else
        {
            toggleButtonText.text = "Lock \nObject";
            toggleButtonImage.color = Color.red;
        }
    }
}
