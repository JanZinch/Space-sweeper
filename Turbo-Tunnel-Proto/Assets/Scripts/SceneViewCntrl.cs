using UnityEditor;
using UnityEngine;

public class SceneViewCntrl : MonoBehaviour
{
    [MenuItem("SceneView/Go to player")]
    static void GoToPlayer() {

        EditorApplication.ExecuteMenuItem("GameObject/Align View to Selected");
    }
}
