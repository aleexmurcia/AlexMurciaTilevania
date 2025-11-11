using UnityEngine;

public class EszenaIraunkorra : MonoBehaviour
{
    void Awake()
    {
        int numEszenaIraunkor = FindObjectsByType<EszenaIraunkorra>(FindObjectsSortMode.None).Length;

        if (numEszenaIraunkor > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void ResetEszenaIraunkorra()
    {
        Destroy(gameObject);
    }
}
