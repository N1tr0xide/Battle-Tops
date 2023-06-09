using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public static class Utilities
{
    /// <summary>
    /// Check whether an object is touching the ground, returning true or false
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="ground"></param>
    /// <returns></returns>
    public static bool IsGrounded(Vector3 pos, LayerMask ground)
    {
        return Physics.Raycast(pos, Vector3.down, 0.1f, ground, QueryTriggerInteraction.Ignore);
    }

    /// <summary>
    /// Gets a reference for the Forward camera position
    /// </summary>
    /// <param name="cam"></param>
    public static Vector3 GetCamF(Transform cam)
    {
        Vector3 camF = cam.forward;
        camF.y = 0;
        return camF;
    }
    
    /// <summary>
    /// Gets a reference for the Right camera position
    /// </summary>
    /// <param name="cam"></param>
    /// <returns></returns>
    public static Vector3 GetCamR(Transform cam)
    {
        Vector3 camR = cam.right;
        camR.y = 0;
        return camR;
    }

    /// <summary>
    /// while in play mode, restarts current scene if an object Transform is lower than y = -4
    /// </summary>
    /// <param name="gameObject"></param>
    public static void ResetGame(GameObject gameObject)
    {
        if (gameObject.transform.position.y < -10f)
        {
#if UNITY_EDITOR
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
#endif
        }
    }
}
