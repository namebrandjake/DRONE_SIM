using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

[InitializeOnLoad]
public static class PackageInstaller
{
    static readonly string[] Packages =
    {
        "com.unity.cinemachine",
        "com.unity.addressables",
        "com.unity.probuilder",
        "com.unity.render-pipelines.universal"
    };

    const string ActiveKey = "PackageInstaller_Active";
    const string IndexKey = "PackageInstaller_Index";

    static AddRequest currentRequest;

    static PackageInstaller()
    {
        if (SessionState.GetBool(ActiveKey, false))
        {
            EditorApplication.update += Tick;
        }
    }

    public static void Run()
    {
        SessionState.SetBool(ActiveKey, true);
        SessionState.SetInt(IndexKey, 0);
        EditorApplication.update += Tick;
    }

    static void Tick()
    {
        if (currentRequest == null)
        {
            int index = SessionState.GetInt(IndexKey, 0);
            if (index >= Packages.Length)
            {
                EditorApplication.update -= Tick;
                SessionState.SetBool(ActiveKey, false);
                Debug.Log("PackageInstaller: all packages installed successfully.");
                EditorApplication.Exit(0);
                return;
            }

            string pkg = Packages[index];
            Debug.Log("PackageInstaller: adding " + pkg);
            currentRequest = Client.Add(pkg);
            return;
        }

        if (currentRequest.IsCompleted)
        {
            if (currentRequest.Status == StatusCode.Failure)
            {
                Debug.LogError("PackageInstaller: failed to add package - " + currentRequest.Error.message);
                EditorApplication.update -= Tick;
                SessionState.SetBool(ActiveKey, false);
                EditorApplication.Exit(1);
                return;
            }

            Debug.Log("PackageInstaller: installed " + currentRequest.Result.name + "@" + currentRequest.Result.version);
            int index = SessionState.GetInt(IndexKey, 0);
            SessionState.SetInt(IndexKey, index + 1);
            currentRequest = null;
        }
    }
}
