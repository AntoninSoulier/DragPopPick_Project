using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class IconeScript : MonoBehaviour
{
    [SerializeField] private MouseManager.ApplicationIcone appType;
    

    private string applicationPath;
    // Start is called before the first frame update
    void Start()
    {
        switch (appType)
        {
            case MouseManager.ApplicationIcone.Excel:
                applicationPath = ListPathApplication.excelPath;
                break;
            case MouseManager.ApplicationIcone.Word:
                applicationPath = ListPathApplication.wordPath;
                break;
            case MouseManager.ApplicationIcone.NodePad:
                applicationPath = ListPathApplication.nodepadPath;
                break;
            case MouseManager.ApplicationIcone.VsCode:
                applicationPath = ListPathApplication.vscodePath;
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Execute(string PathToEx)
    {
        Process process = Process.Start(applicationPath, PathToEx);
        process.WaitForExit();
        process.Close();

    }
}
