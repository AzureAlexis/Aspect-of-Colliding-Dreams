using UnityEngine;

public class ArsenalTab : MenuTab
{
    new public void Update()
    {
        Debug.Log(newWaypoint);
        base.Update();
        if(UIManager.state == "menu")
        {
            if(UIManager.currentTab == "arsenal")
            {
                this.UpdateActive();
            }
            else
            {
                this.UpdateInactive();
            }
        }
    }
}
