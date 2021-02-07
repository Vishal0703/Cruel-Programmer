//place this script in the Editor folder within Assets.
using UnityEditor;
using System.Linq;



//to be used on the command line:
//$ Unity -quit -batchmode -executeMethod WebGLBuilder.WebGLBuild

class WebGLBuilder
{
    //[MenuItem("Build/Build WebGL")]
    static void WebGLBuild()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        string[] scenes = new string[EditorBuildSettings.scenes.Count()];
        int i = 0;
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                scenes[i] = scene.path;
                i++;
            }
        }
        buildPlayerOptions.scenes = scenes;
        buildPlayerOptions.locationPathName = "./bin-webgl";
        buildPlayerOptions.target = BuildTarget.WebGL;
        buildPlayerOptions.options = BuildOptions.None;
        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
}