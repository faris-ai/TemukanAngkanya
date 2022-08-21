using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Angka : MonoBehaviour
{
    [SerializeField]
    public GameObject[] angkaprefab;
    [SerializeField]
    public GameObject[] bentukprefab;

    public List<GameObject> jawabanAngka;
    public List<GameObject> jawabanBentuk;
    public void AddAllNumber()
    {
        jawabanAngka = angkaprefab.ToList();
    }
    public void AddListLevel2(string angka, string warna/*, List<string> warna*/)
    {
        foreach (GameObject a1 in angkaprefab)
        {
            Jawaban jawab = a1.GetComponent<Jawaban>();
            if (jawab.nama == angka)
            {
                GameObject a = Instantiate(a1, a1.transform.position, Quaternion.identity);
                a.transform.SetParent(GameObject.FindGameObjectWithTag("Respawn").transform, false);
                a.GetComponent<Jawaban>().warna = warna/*[0]*/;
                //GameObject b = Instantiate(a1, a1.transform.position, Quaternion.identity);
                //b.transform.SetParent(GameObject.FindGameObjectWithTag("Respawn").transform, false);
                //b.GetComponent<Jawaban>().warna = warna[1];
                //GameObject c = Instantiate(a1, a1.transform.position, Quaternion.identity);
                //c.transform.SetParent(GameObject.FindGameObjectWithTag("Respawn").transform, false);
                //c.GetComponent<Jawaban>().warna = warna[2]; 
                //GameObject d = Instantiate(a1, a1.transform.position, Quaternion.identity);
                //d.transform.SetParent(GameObject.FindGameObjectWithTag("Respawn").transform, false);
                //d.GetComponent<Jawaban>().warna = warna[3];
                jawabanAngka.Add(a);
                //jawabanAngka.Add(b);
                //jawabanAngka.Add(c);
                //jawabanAngka.Add(d); 
            }
        }
    }

    public void AddListLevel3(string angka, string warna, string bentuk)
    {
        foreach (GameObject a1 in angkaprefab)
        {
            Jawaban jawab = a1.GetComponent<Jawaban>();
            if (jawab.nama == angka)
            {
                GameObject a = Instantiate(a1, a1.transform.position, Quaternion.identity);
                a.transform.SetParent(GameObject.FindGameObjectWithTag("Respawn").transform, false);
                a.GetComponent<Jawaban>().warna = warna;
                a.GetComponent<Jawaban>().bentuk = bentuk;
                jawabanAngka.Add(a);
                foreach(GameObject b1 in bentukprefab)
                {
                    Bentuk jawabBentuk = b1.GetComponent<Bentuk>();
                    if(jawabBentuk.namaBentuk == bentuk)
                    {
                        GameObject b = Instantiate(b1, b1.transform.position, Quaternion.identity);
                        b.transform.SetParent(GameObject.FindGameObjectWithTag("Bentuk").transform, false);
                        jawabanBentuk.Add(b);
                    }
                }
            }
        }
    }
}
