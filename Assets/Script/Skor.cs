using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Skor : MonoBehaviour
{
    TextMeshProUGUI txtSkor;
    int personalSkor = 0;
    public int PersonalSkor { get { return personalSkor; } }
    int skor = 100;
    int salah = 0;
    public int Salah { get { return salah; } }

    public void AddSkor()
    {
        txtSkor = GetComponent<TextMeshProUGUI>();
        personalSkor += skor;
        txtSkor.text = "+" + skor.ToString();
    }
    
    public void AddSalah()
    {
        salah++;
    }
}
