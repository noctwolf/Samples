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

            var gb = new GrammarBuilder("设置背景");
            gb.Append(new SemanticResultKey("Color", new Choices(new string[] { "红色", "绿色", "蓝色" })));
            grammar = new Grammar(gb) { Name = "设置背景颜色" };
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
                        switch (e1.Result.Semantics["Color"].Value)
                        {
                            case "红色":
                                Background = Brushes.Red;
                                break;
                            case "绿色":
                                Background = Brushes.Green;
                                break;
                            case "蓝色":
                                Background = Brushes.Blue;
                                break;
                        }
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
