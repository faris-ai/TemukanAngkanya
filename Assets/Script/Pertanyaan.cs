[System.Serializable]
public class Pertanyaan
{
    public string angka;
    private string warna;
    private string bentuk;
    public string Warna { get => warna; set => warna = value; }
    public string Bentuk { get => bentuk; set => bentuk = value; }
}
