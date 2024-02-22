using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ELanguage Language;

    public static GameManager Instance;

    public void Awake()
    {
        Instance = this;
    }
}
