using System.Collections;
using UnityEngine;

public class Jawaban : MonoBehaviour
{
    public string nama;
    public string warna;
    public string bentuk;
    public GameScript gameScript;
    public Skor skor;

    public void AngkaDipilih()
    {
        FindObjectOfType<AudioManager>().Play("tombol");
        if (gameScript.GetPertanyaan().angka == nama && gameScript.GetPertanyaan().Warna == warna && gameScript.GetPertanyaan().Bentuk == bentuk)
        {
            FindObjectOfType<AudioManager>().Play("tepat");
            FindObjectOfType<AudioManager>().Play("benefek");
            gameScript.enadisBtn(gameScript.angka.jawabanAngka, false);
            gameScript.GetObj("benar").SetActive(true);
            gameScript.GetAnim.SetTrigger("Benar");
            skor.AddSkor();
            gameScript.GetObj("skor").SetActive(true);
            gameScript.GetTxt("skorTotal").text = skor.PersonalSkor.ToString();
            StartCoroutine(HideObj("benar"));
            StartCoroutine(HideObj("skor"));
            StartCoroutine(gameScript.KePeratanyaanSelanjutnya());

        } else
        {
            StartCoroutine(WaitUntilHide());
            FindObjectOfType<AudioManager>().Play("coba");
            FindObjectOfType<AudioManager>().Play("salefek");
            gameScript.GetObj("salah").SetActive(true);
            gameScript.GetAnim.SetTrigger("Salah");
            StartCoroutine(HideObj("salah"));
            skor.AddSalah();
        }
    }
    IEnumerator HideObj(string nama)
    {
        float state = gameScript.GetAnim.GetCurrentAnimatorStateInfo(0).speed;
        yield return new WaitForSeconds(state);
        gameScript.GetObj(nama).SetActive(false);
    }

    IEnumerator WaitUntilHide()
    {
        gameScript.enadisBtn(gameScript.angka.jawabanAngka, false);
        yield return new WaitForSeconds(1f);
        gameScript.enadisBtn(gameScript.angka.jawabanAngka, true);
    }
}