using System.IO;
using Svga;
using UnityEngine;

public class Example : MonoBehaviour
{
    public SvgaPlayer Player;

    // Start is called before the first frame update
    void Start()
    {
        var stream = File.Open("Assets/image.svga", FileMode.Open);
        Player.LoadSvgaFileData(stream);
    }

    public void Play()
    {
        Player.Play(2, () => Debug.Log("Play complete."));
    }

    public void Pause()
    {
        Player.Pause();
    }
}