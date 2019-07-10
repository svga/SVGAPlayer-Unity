using System.Collections;
using System.IO;
using Svga;
using UnityEngine;

public class Example : MonoBehaviour
{
    public SvgaPlayer Player;

    void Awake()
    {
        string path =
#if UNITY_ANDROID && !UNITY_EDITOR
        Application.streamingAssetsPath + "/image.svga";
#elif UNITY_IPHONE && !UNITY_EDITOR
        "file://" + Application.streamingAssetsPath + "/image.svga";
#elif UNITY_STANDLONE_WIN||UNITY_EDITOR
            "file://" + Application.streamingAssetsPath + "/image.svga";
#else
        string.Empty;
#endif
        StartCoroutine(ReadData(path));
    }

    IEnumerator ReadData(string path)
    {
        WWW www = new WWW(path);
        yield return www;
        while (www.isDone == false)
        {
            yield return new WaitForEndOfFrame();
        }

        var data = www.bytes;

        using (Stream stream = new MemoryStream(data))
        {
            Player.LoadSvgaFileData(stream);
        }

        yield return new WaitForEndOfFrame();
    }

    public void Play()
    {
        Player.Play(0, () => Debug.Log("Play complete."));
    }

    public void Pause()
    {
        Player.Pause();
    }
}