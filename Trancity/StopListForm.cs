using System;
using System.Windows.Forms;

namespace Trancity
{
    public partial class StopListForm : Form
    {
        public Trip trip;
        public Route route;

        public StopListForm(Route _route, Trip _trip)
        {
            InitializeComponent();
            Localization.ApplyLocalization(this);
            trip = _trip;
            route = _route;
        }

        private void StopListForm_Load(object sender, EventArgs e)
        {
            if (trip.tripStopList == null)
            {
                trip.InitTripStopList(route);
            }
            for (int i = 0; i < trip.tripStopList.Count; i++)
            {
                checkedListBox.Items.Add(trip.tripStopList[i].stop.название);
                checkedListBox.SetItemChecked(i, trip.tripStopList[i].flag);
            }
        }

        private void StopListForm_FormClosed(object sender, FormClosedEventArgs e)
        {   // WTF???
            if (checkedListBox.Items.Count != 0)
            {
                for (int i = 0; i < trip.tripStopList.Count; i++)
                {
                    trip.tripStopList[i].flag = checkedListBox.GetItemChecked(i);
                }
            }
        }

        private void UpdateClick(object sender, EventArgs e)
        {
            checkedListBox.ClearSelected();
            checkedListBox.Items.Clear();
            trip.UpdateTripStopList(route);
            for (int i = 0; i < trip.tripStopList.Count; i++)
            {
                checkedListBox.Items.Add(trip.tripStopList[i].stop.название);
                checkedListBox.SetItemChecked(i, trip.tripStopList[i].flag);
            }
        }


        private void ClearClick(object sender, EventArgs e)
        {
            checkedListBox.ClearSelected();
            checkedListBox.Items.Clear();
            trip.tripStopList.Clear();
        }
    }
}
