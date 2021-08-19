using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace calc
{
    public partial class MainPage : ContentPage
    {

        ISoundEffect soundEffect = DependencyService.Get<ISoundEffect>();

        public MainPage()
        {
            InitializeComponent();
        }

        // コントロールサイズ調整
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            grdMain.WidthRequest = width;
            grdMain.HeightRequest = height;
            
        }

        private void btnPuls_Clicked(object sender, EventArgs e)
        {
            using (soundEffect as IDisposable)
            {
                soundEffect.HitSound();
            }

            Navigation.PushAsync(new QuestionPage());
        }

        private void btnMinus_Clicked(object sender, EventArgs e)
        {
            using (soundEffect as IDisposable)
            {
                soundEffect.HitSound();
            }

            Navigation.PushAsync(new MinusPage());
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
