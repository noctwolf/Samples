using System;
using System.Globalization;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Media;

namespace 语音合成及语音识别
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new SpeechSynthesizer().SpeakAsync("语音合成演示!");//异步
        }

        SpeechRecognitionEngine recognizer;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            recognizer = new SpeechRecognitionEngine(new CultureInfo("zh-CN"));

            Grammar grammar = new Grammar(new GrammarBuilder("线程")) { Name = "命令" };//没有这个，通常识别为“县城”
            recognizer.LoadGrammar(grammar);

            GrammarBuilder gb = new GrammarBuilder("设置背景");

            gb.Append(new SemanticResultKey("Color", new Choices(
                new SemanticResultValue("红色", Brushes.Red.Color.ToString()),
                new SemanticResultValue("绿色", Brushes.Green.Color.ToString()),
                new SemanticResultValue("蓝色", Brushes.Blue.Color.ToString())
                )));
            grammar = new Grammar(gb) { Name = "设置背景颜色" };
            recognizer.LoadGrammar(grammar);

            gb = new GrammarBuilder();
            Choices choices = new Choices(Enumerable.Range(1, 9).Select(n => n.ToString()).ToArray());
            gb.Append(new SemanticResultKey("num1", choices));
            gb.Append("加");
            gb.Append(new SemanticResultKey("num2", choices));
            gb.Append("等于");
            grammar = new Grammar(gb) { Name = "加法运算" };
            recognizer.LoadGrammar(grammar);

            grammar = new Grammar(new GrammarBuilder("停止识别")) { Name = "Cancel" };
            recognizer.LoadGrammar(grammar);

            recognizer.LoadGrammar(new DictationGrammar());

            recognizer.SpeechRecognized += (sender1, e1) =>
            {
                string s = e1.Result.Text;
                switch (e1.Result.Grammar.Name)
                {
                    case "Cancel":
                        recognizer.RecognizeAsyncCancel();
                        break;
                    case "命令":
                        s = $"识别到命令，{s}";
                        break;
                    case "设置背景颜色":
                        s = $"识别到命令，{s}";
                        Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(e1.Result.Semantics["Color"].Value.ToString()));
                        break;
                    case "加法运算":
                        var i1 = int.Parse(e1.Result.Semantics["num1"].Value.ToString());
                        var i2 = int.Parse(e1.Result.Semantics["num2"].Value.ToString());
                        s = $"识别到命令，{s}{i1 + i2}";
                        break;
                }
                listBox.Items.Insert(0, s);
                new SpeechSynthesizer().SpeakAsync(s);
            };

            recognizer.SetInputToDefaultAudioDevice();
            recognizer.RecognizeAsync(RecognizeMode.Multiple);
        }
    }
}
