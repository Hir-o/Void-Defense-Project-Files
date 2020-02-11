using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimatorParams
{
    public class AnimatorParam
    {
       //overloaded method that is used for triggers
        public static void SetAnimatorParameter(Animator animator, string parameter,
            AnimatorParameters.AnimatorParameterType parameterType)
        {
            if (parameterType == AnimatorParameters.AnimatorParameterType.Trigger)
                animator.SetTrigger(parameter);
        }
        
        //overloaded method that is used for booleans
        public static void SetAnimatorParameter(Animator animator, string parameter, bool bValue,
            AnimatorParameters.AnimatorParameterType parameterType)
        {
            if (parameterType == AnimatorParameters.AnimatorParameterType.Bool)
                animator.SetBool(parameter, bValue);
        }
        
        //overloaded method that is used for floats
        public static void SetAnimatorParameter(Animator animator, string parameter, float fValue,
            AnimatorParameters.AnimatorParameterType parameterType)
        {
            if (parameterType == AnimatorParameters.AnimatorParameterType.Float)
                animator.SetFloat(parameter, fValue);
        }
        
        //overloaded method that is used for integers
        public static void SetAnimatorParameter(Animator animator, string parameter, int iValue,
            AnimatorParameters.AnimatorParameterType parameterType)
        {
            if (parameterType == AnimatorParameters.AnimatorParameterType.Int)
                animator.SetInteger(parameter, iValue);
        }

        //overloaded method that accepts all values
        public static void SetAnimatorParameter(Animator animator, string parameter, bool bValue, float fValue, int iValue,
            AnimatorParameters.AnimatorParameterType parameterType)
        {
            switch (parameterType)
            {
                case AnimatorParameters.AnimatorParameterType.Trigger:
                    animator.SetTrigger(parameter);
                    break;
                case AnimatorParameters.AnimatorParameterType.Bool:
                    animator.SetBool(parameter, bValue);
                    break;
                case AnimatorParameters.AnimatorParameterType.Float:
                    animator.SetFloat(parameter, fValue);
                    break;
                case AnimatorParameters.AnimatorParameterType.Int:
                    animator.SetInteger(parameter, iValue);
                    break;
            }
        }
    }
}
