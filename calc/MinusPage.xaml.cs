using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace calc
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MinusPage : ContentPage
    {
        int cntQuiz = 0;
        static int maxQuiz = 5;
        int rightQuiz = 0;
        string strAnswer = "";

        ISoundEffect soundEffect = DependencyService.Get<ISoundEffect>();

        public MinusPage()
        {
            InitializeComponent();

            // 問題表示
            setQuestion();
        }

        // コントロールサイズ調整
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            grdMain.WidthRequest = width - 20;
            grdMain.HeightRequest = height - 20;
            imgResult.WidthRequest = width - 20;
            imgResult.HeightRequest = height - 20;
            lblQuestion1.FontSize = width / 6;
            lblSymbol.FontSize = width / 6;
            lblQuestion2.FontSize = width / 6;
            lblAns1.FontSize = width / 6;
            lblAns2.FontSize = width / 6;
            lblAns3.FontSize = width / 6;
            lblAns4.FontSize = width / 6;
        }

        // 問題出題
        private void setQuestion()
        {
            imgResult.Source = null;
            // 問題作成
            string[] strQuestion = createQuestion();
            // 問題文セット
            lblQuestion1.Text = strQuestion[0];
            lblQuestion2.Text = strQuestion[1];

            List<string> lst = new List<string>();

            // 正解を選択肢に加える
            lst.Add(strQuestion[2]);
            // 正解セット
            strAnswer = strQuestion[2];

            Random rnd = new Random();

            // 他の選択肢をセット
            while (lst.Count < 4)
            {
                var value = strQuestion[rnd.Next(3, strQuestion.Length)];

                while (lst.Contains(value))
                {
                    value = strQuestion[rnd.Next(3, strQuestion.Length)];
                }

                lst.Add(value);
            }

            // 選択肢の並び替え
            var sortLst = lst.OrderBy(i => Guid.NewGuid()).ToArray();

            lblAns1.Text = sortLst[0];
            lblAns2.Text = sortLst[1];
            lblAns3.Text = sortLst[2];
            lblAns4.Text = sortLst[3];

            // 画像を選択
            int num = rnd.Next(1, 5);

            // 画像表示切替
            dispImages("Q1", int.Parse(strQuestion[0]), string.Concat("img",num.ToString(),"_1.png"));
            dispImages("Q2", int.Parse(strQuestion[1]), string.Concat("img", num.ToString(), "_2.png"));
            dispImages("A1", int.Parse(sortLst[0]), string.Concat("img", num.ToString(), "_1.png"));
            dispImages("A2", int.Parse(sortLst[1]), string.Concat("img", num.ToString(), "_1.png"));
            dispImages("A3", int.Parse(sortLst[2]), string.Concat("img", num.ToString(), "_1.png"));
            dispImages("A4", int.Parse(sortLst[3]), string.Concat("img", num.ToString(), "_1.png"));
            
        }

        // 問題作成
        private string[] createQuestion()
        {
            List<string> lstRet = new List<string>();

            Random rnd = new Random();

            int p1 = 0, p2 = 0, ans = 0, max = 0, min = 0;
            p1 = rnd.Next(1, 10);
            p2 = rnd.Next(1, p1);
            ans = p1 - p2;
            max = 10;
            min = 0;
            
            // 返却リスト1つ目、2つ目は問題文
            lstRet.Add(p1.ToString());
            lstRet.Add(p2.ToString());
            // 返却リスト3つ目は答え
            lstRet.Add(ans.ToString());

            // 選択肢作成
            for (int i = 1; i <= 10; i++)
            {
                if (checkLimit(ans + i, max, min)) lstRet.Add((ans + i).ToString());
                if (checkLimit(ans - i, max, min)) lstRet.Add((ans - i).ToString());
            }

            return lstRet.ToArray();
        }

        private bool checkLimit(int num, int max, int min)
        {
            if (num > max) return false;
            if (num < min) return false;
            return true;
        }

        // 画像表示切替
        private void dispImages(string str, int num, string strImage)
        {
            int cnt = 0;
            int cntMax = 9;

            Random rnd = new Random();
            int rnum = rnd.Next(1, 4);

            // 表示
            while (cnt < num )
            {
                Image img = NameScopeExtensions.FindByName<Image>(this, string.Concat("img" , str, "_", (cnt + 1).ToString()));
                img.Source = strImage;
                img.IsVisible = true;
                cnt = cnt + 1;
            }

            // 非表示
            for (int i = cnt; i <= cntMax; i++)
            {
                Image img = NameScopeExtensions.FindByName<Image>(this, string.Concat("img", str, "_", (i + 1).ToString()));
                img.Source = strImage; 
                img.IsVisible = false;
            }

        }

        private async void lblAns_Clicked(object sender, EventArgs e)
        {
            grdMain.IsEnabled = false;

            Button btn = (Button)sender;
            if (strAnswer == btn.Text)
            {
                imgResult.Source = "maru.png";
                using (soundEffect as IDisposable)
                {
                    soundEffect.CorrectSound();
                }
                rightQuiz++;
            }
            else
            {
                imgResult.Source = "batu.png";
                using (soundEffect as IDisposable)
                {
                    soundEffect.IncorrectSound();
                }

            }

            imgResult.IsVisible = true;

            cntQuiz++;

            await Task.Delay(1000);

            imgResult.IsVisible = false;

            if (cntQuiz >= maxQuiz)
            {
                Navigation.PushAsync(new ResultPage(rightQuiz, "minus"));
            }
            else
            {
                setQuestion();
            }

            grdMain.IsEnabled = true;
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

    }
}