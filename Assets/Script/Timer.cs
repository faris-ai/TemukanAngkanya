using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Text txtTimer;
    float time;
    public float speed = 1;
    float totalDetik;
    public int TotalDetik { get { return (int)totalDetik; } }

    float menit;
    public int Menit { get { return (int)menit; } }

    float detik;
    public int Detik { get { return (int)detik; } }

    void Start()
    {
        txtTimer = GetComponent<Text>();
    }

    void Update()
    {
        time += Time.deltaTime * speed;
        detik = (int)(time % 60);
        menit = Mathf.Floor((time % 3600)/60);
        totalDetik = (int)time;
        txtTimer.text = menit.ToString("00") + ":" + detik.ToString("00"); 
    }
}
