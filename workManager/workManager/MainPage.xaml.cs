using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace workManager
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Worker_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IsimpleWorkerCall>().hh();
        }
    }

    public interface IsimpleWorkerCall
    {
        void hh();
    }
}
