/*
 * Created by SharpDevelop.
 * User: sergey
 * Date: 07.09.2015
 * Time: 19:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Collections.Generic;
using System.Windows.Forms;

namespace Engine.WinFormsExt
{
    /// <summary>
    /// WinFormsUt - provides search utils
    /// </summary>
    public class WinFormsUt
    {
        public static List<Control> FindControl(Control ctrl, string target)
        {
            List<Control> list = new List<Control>();
            if (ctrl.Name == target)
            {
                list.Add(ctrl);
            }
            foreach (Control control in ctrl.Controls)
            {
                list.AddRange(FindControl(control, target));
            }
            return list;
        }

        public static List<MenuItem> FindMenuItems(Menu menu, string target)
        {
            List<MenuItem> list = new List<MenuItem>();
            foreach (MenuItem subitem in menu.MenuItems)
            {
                list.AddRange(FindMenuItemsPr(subitem, target));
            }
            return list;
        }

        private static List<MenuItem> FindMenuItemsPr(MenuItem menu, string target)
        {
            List<MenuItem> list = new List<MenuItem>();
            if (menu.Name == target)
            {
                list.Add(menu);
            }
            foreach (MenuItem subitem in menu.MenuItems)
            {
                list.AddRange(FindMenuItemsPr(subitem, target));
            }
            return list;
        }
    }
}
