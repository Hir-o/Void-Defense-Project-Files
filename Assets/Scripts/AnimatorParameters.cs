using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorParameters
{
    // Animator parameter types
    public enum AnimatorParameterType
    {
        Trigger,
        Bool,
        Float,
        Int
    }

    public static AnimatorParameterType ParameterTypeTrigger = AnimatorParameterType.Trigger;
    public static AnimatorParameterType ParameterTypeBool = AnimatorParameterType.Bool;
    public static AnimatorParameterType ParameterTypeFloat = AnimatorParameterType.Float;
    public static AnimatorParameterType ParameterTypeInt = AnimatorParameterType.Int;

    // Enemy animator parameters
    public static readonly string ENEMY_DEACTIVATE = "deactivate";
    public static readonly string ENEMY_DEACTIVATE_ALTERNATE = "deactivate_alternate";
    
    // Skill panel parameters
    public static readonly string SKILL_PANEL_OPEN = "open";
    public static readonly string SKILL_PANEL_CLOSE = "close";
    
    // Popup resource text parameters
    public static readonly string RESOURCES_SHOW_POPUP = "showPopup";
    
    // Fade In parameter
    public static readonly string FADE = "fade";
    
    // Turret Spawn/Despawn parameter
    public static readonly string SPAWN = "spawn";
    public static readonly string DESPAWN = "despawn";
}