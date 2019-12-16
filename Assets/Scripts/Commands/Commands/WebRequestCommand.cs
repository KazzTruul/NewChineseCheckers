using System;
using System.Collections;
using UnityEngine.Networking;

public class WebRequestCommand : CoroutineCommand
{
    private readonly string _url;
    private readonly string _method;

    private string _result;

    public string Result => _result;

    public bool Done { get; private set; } = false;

    public WebRequestCommand(string url, string method)
    {
        _url = url;
        _method = method;
    }

    public override IEnumerator Execute()
    {
        var x = new UnityWebRequest(_url, _method);

        yield return x.SendWebRequest();

        if (x.isNetworkError)
        {
            throw new Exception("Error in database request!");
        }

        var result = x.downloadHandler.data;

        _result = x.downloadHandler.data.ToString();

        x.Dispose();

        Done = true;
    }
}