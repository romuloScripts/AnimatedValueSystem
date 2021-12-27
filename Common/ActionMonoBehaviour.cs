using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public interface IActions {
        
    }

    public interface IActionMonoBehaviour: IActions {
        public void InvokeAction();
    }

    public interface IStopActionMonoBehaviour: IActions {
        public void StopAction();
    }

    public static class ActionsExtensions {
        public static List<T> GetActionsOfType<T>(this MonoBehaviour[] monoBehaviours) where T : class, IActions{
            List<T> actionMonoBehaviours = new List<T>();
            if (monoBehaviours != null) {
                foreach (var item in monoBehaviours) {
                    if (item && item is T actionMonoBehaviour) {
                        actionMonoBehaviours.Add(actionMonoBehaviour);
                    }
                }
            }
            return actionMonoBehaviours;
        }
    }
}
