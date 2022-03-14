using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CollabAction : StateLight
{
    public List<StateLight> buttons;

    public UnityEvent action;

    public void CheckAllButtons()
    {
        if (buttons.All(button => button.State))
        {
            action.Invoke();
            ChangeState(true);
            return;
        }
        ChangeState(false);
        
    }
}
