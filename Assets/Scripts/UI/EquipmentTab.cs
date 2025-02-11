using UnityEngine;

public class EquipmentTab : MenuTab
{
    new public void Update()
    {
        if(UIManager.state == "menu")
        {
            if(UIManager.currentTab == "equipment")
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
