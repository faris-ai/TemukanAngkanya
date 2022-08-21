using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ManagerAngka : MonoBehaviour
{
    Vector2 realCenter;
    Vector2 realSize;

    public void resetAngka(List<GameObject> angka)
    {
        foreach (GameObject a in angka)
        {
            a.transform.position = new Vector2(-100, -100);
        }
    }

    public void intiateAngka(List<GameObject> angka, List<GameObject> bentuk)
    {
        resetAngka(angka);
        realSize = new Vector2(Screen.width - Screen.width / 10, Screen.height - Screen.height/ 5 - Screen.width / 10);
        realCenter = new Vector2(Screen.width / 2, (Screen.height - Screen.height / 5) / 2);
        for(int a = 0; a < angka.Count; a++)
        {
            Vector3 pos, recPos;
            TextMeshProUGUI txtMP = angka[a].GetComponent<TextMeshProUGUI>();
            txtMP.fontSize = Random.Range(120, 160);
            Jawaban jawaban = angka[a].GetComponent<Jawaban>();
            switch (jawaban.warna)
            {
                case "merah":
                    txtMP.color = Color.red;
                    break;
                case "hijau":
                    txtMP.color = Color.green;
                    break;
                case "biru":
                    txtMP.color = Color.blue;
                    break;
                case "kuning":
                    txtMP.color = Color.yellow;
                    break;
                default:
                    break;
            }
            do
            {
                pos = realCenter + new Vector2(Random.Range(-realSize.x / 2, realSize.x / 2), Random.Range(-realSize.y / 2, realSize.y / 2));
                recPos = pos + new Vector3(0,20);

            } while (!checkIfPosEmpty(pos));
            angka[a].transform.position = pos;
            if (SceneManager.GetActiveScene().buildIndex == 3)
                bentuk[a].transform.position = (jawaban.bentuk != "segitiga") ? pos : recPos;
        }
    }
    public bool checkIfPosEmpty(Vector3 targetPos)
    {
        GameObject[] allAngkaTampil = GameObject.FindGameObjectsWithTag("Angka");
        foreach (GameObject current in allAngkaTampil)
        {
            if ((current.transform.position - targetPos).magnitude < 100)
                return false;
        }
        return true;
    }
}

