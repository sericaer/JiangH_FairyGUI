using JiangH.API;
using JiangH.Kernels;
using JiangH.Kernels.Mods;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Facade.gameBuilder = GameSessionBuilder.inst;

        Facade.NewEnv();

        ModManager.Init(Path.Combine(Application.streamingAssetsPath, "mods"), new string[] { "native" });

        UIGenerator.Init(ModManager.inst.mods);

        Facade.NewGame();

        SceneManager.LoadScene(nameof(MainScene));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
