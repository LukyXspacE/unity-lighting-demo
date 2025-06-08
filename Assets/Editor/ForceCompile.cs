using UnityEditor;

public static class ForceCompile
{
    [MenuItem("Tools/Force Compile Scripts")]
    public static void ForceCompileScripts()
    {
        UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation();
        UnityEngine.Debug.Log("Forced script compilation triggered.");
    }
}