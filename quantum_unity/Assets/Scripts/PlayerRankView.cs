using TMPro;
using UnityEngine;

public class PlayerRankView : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    public static int Rank { get; private set; } = 0;
    
    private void Awake()
    {
        inputField.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(string value)
    {
        if (int.TryParse(value, out var intValue))
        {
            Rank = intValue;
        }
    }
}
