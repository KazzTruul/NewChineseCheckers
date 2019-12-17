using System;
using System.Text;
using System.Collections;
using UnityEngine.Networking;
using System.Runtime.Serialization.Json;
using System.IO;

public class WebRequestDownloadCommand<T> : CoroutineCommand where T : class
{
    private readonly string _url;
    private readonly string _method;

    private T _result;

    public T Result => _result;

    public bool Done { get; private set; } = false;

    public WebRequestDownloadCommand(string url, string method)
    {
        _url = url;
        _method = method;
    }

    public override IEnumerator Execute()
    {
        var webRequest = new UnityWebRequest(_url, _method);

        webRequest.downloadHandler = new DownloadHandlerBuffer();

        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError)
        {
            throw new Exception("Error in database request!");
        }

        var result = webRequest.downloadHandler.data;

        if(result == null)
        {
            throw new Exception("No result");
        }

        var resultString = Encoding.UTF8.GetString(result);

        var translationSerializer = new DataContractJsonSerializer(typeof(TranslationCatalog),
            new DataContractJsonSerializerSettings
            {
                UseSimpleDictionaryFormat = true
            });

        using (var memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(resultString)))
        {
            _result = translationSerializer.ReadObject(memoryStream) as T;
        }

        webRequest.Dispose();

        Done = true;
    }
}