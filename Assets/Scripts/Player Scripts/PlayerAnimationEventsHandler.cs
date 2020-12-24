using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventsHandler : MonoBehaviour
{
    #region Properties
    [Header("Component Reference")]
    [SerializeField] private PlayerController playerController = null;
    #endregion

    #region Getter And Setter
    public Transform TreeToDestroy { get; set; }
    #endregion

    #region Anim Event Functions
    private void AnimEvent_CutTreeEnd()
    {
        if (TreeToDestroy)
        {
            playerController.FillWoodenLog();
            playerController.EnableMovementFunction(true);
            Destroy(TreeToDestroy.gameObject);
            TreeToDestroy = null;
        }
    }
    #endregion
}
