using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogStackHandler : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private int stackSize = 0;
    [SerializeField] private List<MeshRenderer> woodLogMeshRenderers = new List<MeshRenderer>();

    private int stackIndex = 0;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        CapacityReached = false;
    }
    #endregion

    #region Getter And Setter
    public bool CapacityReached { get; set; }
    #endregion

    #region Public Functions
    public bool IsStackFull()
    {
        if(stackIndex < stackSize)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void FillLog()
    {
        woodLogMeshRenderers[stackIndex].enabled = true;
        stackIndex++;
    }
    #endregion
}
