using UnityEngine;

public class SystemTab : MenuTab
{
    new public void Update()
    {
        base.Update();
        if(UIManager.state == "menu")
        {
            if(UIManager.currentTab == "system")
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
