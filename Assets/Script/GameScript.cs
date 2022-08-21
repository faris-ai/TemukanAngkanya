using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    public Angka angka;
    public Skor skor;
    public Timer timer;

    private static List<Pertanyaan> pertanyaanList = new List<Pertanyaan>();
    private Pertanyaan currentPertanyaan;

    private int scene;

    [SerializeField]
    private ManagerAngka managerAngka;
    [SerializeField]
    private Text txtPertanyaan;
    [SerializeField]
    private GameObject imgBenar;
    [SerializeField]
    private GameObject imgSalah;
    [SerializeField]
    private GameObject txtSkor;
    [SerializeField]
    private Text txtSkorTotal;
    [SerializeField]
    private Text txtLevel;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject resultCanvas;
    [SerializeField]
    private Text skorTxt;
    [SerializeField]
    private Text waktuTxt;
    [SerializeField]
    private Text salahTxt;

    private bool IsFinished { get => (pertanyaanList.Count > allSoal - jumlahSoal) ? false : true; set => IsFinished = value; }
    private const float waktuJedaPertanyaan = 1.5f;
    [Range(1, 15)]
    public int jumlahSoal;
    private int allSoal;

    [Range(1, 5)]
    public int jumlahBabak;
    private int babak = 1;

    void Start()
    {
        scene = SceneManager.GetActiveScene().buildIndex;

        SetListPertanyaan();
        allSoal = pertanyaanList.Count;
        jumlahSoal = (scene == 1) ? pertanyaanList.Count : 2;
        jumlahBabak = (scene == 1) ? 1 : 3;
        txtSkorTotal.text = skor.PersonalSkor.ToString();
        StartCoroutine(JudulLevelSoal());
    }

    void SetListPertanyaan()
    {
        List<string> angka = new List<string>{"satu", "dua", "tiga", "empat", "lima", "enam", "tujuh", "delapan", "sembilan", "sepuluh" };
        List<string> warna = new List<string>{ "merah", "hijau", "biru", "kuning" };
        List<string> bentuk = new List<string>{ "kotak", "lingkaran", "segitiga" };
        int randomAngka, randomWarna, randomBentuk;
        int tempAngka = 0;
        if (pertanyaanList == null || pertanyaanList.Count == 0)
        {
            if(scene == 1)
            {
                foreach (string a in angka)
                {
                    Pertanyaan p = new Pertanyaan();
                    p.angka = a;
                    p.Warna = "";
                    p.Bentuk = "";
                    pertanyaanList.Add(p);
                }
                this.angka.AddAllNumber();
            }
            else if (scene == 2)
            {
                for (int i = 0; i < 12; i++)
                {
                    Pertanyaan p = new Pertanyaan();
                    do {
                        randomAngka = Random.Range(0, angka.Count);
                        if (i == 0 || i == 2)
                            tempAngka = randomAngka;
                        randomWarna = Random.Range(0, warna.Count);
                        //Pertanyaan q = new Pertanyaan();
                        //Pertanyaan r = new Pertanyaan();
                        //Pertanyaan s = new Pertanyaan();
                        p.angka = /*q.angka = r.angka = s.angka =*/(i == 1 || i == 3) ? angka[tempAngka] : angka[randomAngka];
                        p.Warna = warna[randomWarna];
                        p.Bentuk = "";
                    } while (!checkIfDouble (pertanyaanList, p));
                    pertanyaanList.Add(p);
                    //q.Warna = warna[1];
                    //pertanyaanList.Add(q);
                    //r.Warna = warna[2];
                    //pertanyaanList.Add(r);
                    //s.Warna = warna[3];
                    //pertanyaanList.Add(s);
                    this.angka.AddListLevel2((i == 1 || i == 3) ? angka[tempAngka] : angka[randomAngka], warna[randomWarna]);
                    //angka.Remove(angka[random]);
                }
            }
            else if (scene == 3)
            {

                for (int i = 0; i < 10; i++)
                {
                    Pertanyaan p = new Pertanyaan();
                    do
                    {
                        randomAngka = Random.Range(0, angka.Count);
                        if (i == 0 || i == 2)
                            tempAngka = randomAngka;
                        randomWarna = Random.Range(0, warna.Count);
                        randomBentuk = Random.Range(0, bentuk.Count);
                        p.angka = (i == 1 || i == 3) ? angka[tempAngka] : angka[randomAngka];
                        p.Warna = warna[randomWarna];
                        p.Bentuk = bentuk[randomBentuk];
                    } while (!checkIfDouble(pertanyaanList, p));
                    pertanyaanList.Add(p);
                    this.angka.AddListLevel3((i == 1 || i == 3) ? angka[tempAngka] : angka[randomAngka], warna[randomWarna], bentuk[randomBentuk]);
                }

            }
        }
    }

    public bool checkIfDouble(List<Pertanyaan> pertanyaans, Pertanyaan pert)
    {
        if (pertanyaanList == null || pertanyaanList.Count == 0)
        {
            return true;            
        }
        else
        {
            foreach (Pertanyaan current in pertanyaans)
            {
                if (pert.angka == current.angka && pert.Warna == current.Warna && pert.Bentuk == current.Bentuk)
                    return false;
            }
            return true;
        }
    }

    void SetPertanyaan()
    {
        int random = Random.Range(0, pertanyaanList.Count);
        currentPertanyaan = pertanyaanList[random];
        if (scene == 1)
        {
            txtPertanyaan.text = "Temukan angka " + currentPertanyaan.angka + "!";
        } else if (scene == 2)
        {
            txtPertanyaan.text = "Temukan angka " + currentPertanyaan.angka + " warna " + currentPertanyaan.Warna + "!";

        } else 
        {
            txtPertanyaan.text = "Temukan angka " + currentPertanyaan.angka + " warna " + currentPertanyaan.Warna + " di dalam " + currentPertanyaan.Bentuk + "!";

        }
        StartCoroutine(SetAudioPertanyaan());
        managerAngka.intiateAngka(angka.jawabanAngka, angka.jawabanBentuk);
        enadisBtn(angka.jawabanAngka, true);
    }

    public IEnumerator KePeratanyaanSelanjutnya()
    {
        pertanyaanList.Remove(currentPertanyaan);
        if (!IsFinished) 
        { 
            yield return new WaitForSeconds(waktuJedaPertanyaan);
            SetPertanyaan(); 
        } else if (babak < jumlahBabak)
        {
            yield return new WaitForSeconds(waktuJedaPertanyaan);
            babak++;
            pertanyaanList = new List<Pertanyaan>();
            foreach (GameObject a in angka.jawabanAngka)
            {
                Destroy(a);
            }
            angka.jawabanAngka = new List<GameObject>(); 
            foreach (GameObject b in angka.jawabanBentuk)
            {
                Destroy(b);
            }
            angka.jawabanBentuk = new List<GameObject>();
            SetListPertanyaan();
            SetPertanyaan();
        }
        else
        {
            yield return new WaitForSeconds(waktuJedaPertanyaan);
            TampilHasil();
            pertanyaanList = new List<Pertanyaan>();
        }
    }

     IEnumerator SetAudioPertanyaan()
    {
        FindObjectOfType<AudioManager>().Play("temukan");
        yield return new WaitForSeconds(FindObjectOfType<AudioManager>().GetDuration("temukan")-0.2f);
        FindObjectOfType<AudioManager>().Play(currentPertanyaan.angka);
        if (currentPertanyaan.Warna != "")
        {
            yield return new WaitForSeconds(FindObjectOfType<AudioManager>().GetDuration(currentPertanyaan.angka) + 0.1f);
            FindObjectOfType<AudioManager>().Play("warna");
            yield return new WaitForSeconds(FindObjectOfType<AudioManager>().GetDuration("warna") + 0.1f);
            FindObjectOfType<AudioManager>().Play(currentPertanyaan.Warna);
        }
        if (currentPertanyaan.Bentuk != "")
        {
            yield return new WaitForSeconds(FindObjectOfType<AudioManager>().GetDuration(currentPertanyaan.Warna) + 0.1f);
            FindObjectOfType<AudioManager>().Play("didalam");
            yield return new WaitForSeconds(FindObjectOfType<AudioManager>().GetDuration("didalam") + 0.1f);
            FindObjectOfType<AudioManager>().Play(currentPertanyaan.Bentuk);
        }
    }

    public void enadisBtn(List<GameObject> list, bool faltrue)
    {
        foreach (GameObject ang in list)
        {
            ang.GetComponent<Button>().enabled = faltrue;
        }
    }

    public Pertanyaan GetPertanyaan()
    {
        return currentPertanyaan;
    }

    IEnumerator JudulLevelSoal()
    {
        FindObjectOfType<AudioManager>().Play("level");
        string no ="satu";
        if (scene == 1)
        {
            txtLevel.text += "1";
            no = "satu";
        }
        else if (scene == 2)
        {
            txtLevel.text += "2";
            no = "dua";
        }
        else if (scene == 3)
        {
            txtLevel.text += "3";
            no = "tiga";
        }
        yield return new WaitForSeconds(FindObjectOfType<AudioManager>().GetDuration("level"));
        FindObjectOfType<AudioManager>().Play(no);
        yield return new WaitForSeconds(FindObjectOfType<AudioManager>().GetDuration(no) + 1f);
        SetPertanyaan();
    }

        public GameObject GetObj(string obj)
    {
        switch (obj)
        {
            case "benar":
                return imgBenar;
            case "salah":
                return imgSalah;
            case "skor":
                return txtSkor;
            case "resultCanvas":
                return resultCanvas;
            default:
                return null;
        }
            
    }
    public Text GetTxt(string txt)
    {
        switch (txt)
        {
            case "skorTotal":
                return txtSkorTotal;
            default:
                return null;
        }

    }
    public Animator GetAnim
    {
        get { return animator; }
    }

    public void TampilHasil()
    {
        resultCanvas.SetActive(true);
        skorTxt.text = skor.PersonalSkor.ToString();
        waktuTxt.text = timer.Menit.ToString("00") + ":" + timer.Detik.ToString("00");
        salahTxt.text = skor.Salah.ToString();
        if(scene == 3)
        {
            GameObject levelbtn = GameObject.Find("LevelButton");
            levelbtn.SetActive(false);
        }
    }

    public void MainLagi()
    {
        FindObjectOfType<AudioManager>().Play("tombol");
        SceneManager.LoadScene(scene);
    }
    public void LanjutLevel()
    {
        FindObjectOfType<AudioManager>().Play("tombol");
        SceneManager.LoadScene(scene+1);
    }

    public void KeMenu()
    {
        FindObjectOfType<AudioManager>().Play("tombol");
        SceneManager.LoadScene(0);
    }
}