using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;
using System.Diagnostics;

namespace EXPERIMENTAL.Aritificial_Intelligence
{
    public partial class SpeechRecognition : Form
    {
        SpeechSynthesizer ss = new SpeechSynthesizer(); // Akses fungsional
        PromptBuilder pb = new PromptBuilder(); // Ruang control AI
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine(); // Proses pengenalan suara
        Choices clist = new Choices(); // Untuk grammer
        public SpeechRecognition()
        {
            InitializeComponent();
        }
        //string path = @"C:\Users\Akhdan Rasiq\Music";
        private void SpeechRecognition_Load(object sender, EventArgs e)
        {
            //string[] files = System.IO.Directory.GetFiles(path, "*.mp3", SearchOption.AllDirectories);
            //for (int i = 0; i < files.Length; i++)
            //{
            //    textBox2.Text += files[i]+Environment.NewLine;
            //}
        }
        
        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            clist.Add(new string[] { "play some music", "who is develope you", "hello", "how are you", "what is the current time", "play some music", "open chrome", "what is your name", "thank you", "close", "bye bye" });
            Grammar gr = new Grammar(new GrammarBuilder(clist));
            try
            {
                sre.RequestRecognizerUpdate();
                sre.LoadGrammar(gr);
                sre.SpeechRecognized += Sre_SpeechRecognized;
                sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void Sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //var text = e.Result.Text;
            switch(e.Result.Text.ToString())
            {
                case "play some music":
                    ss.SpeakAsync("Playing Music...");
                    Process.Start(@"C:\Users\Akhdan Rasiq\Music\[Electro][new] - Nitro Fun - Final Boss [Monstercat Release].mp3");
                    break;

                case "who is develope you":
                    ss.SpeakAsync("iam develope by you, akhdan");
                    break;

                case "hello":
                    ss.SpeakAsync("Hello Akhdan");
                    break;

                case "how are you":
                    ss.SpeakAsync("I'am fine Akhdan");
                    break;

                case "what is the current time":
                    ss.SpeakAsync("Current time is "+DateTime.Now.ToLongTimeString());
                    break;

                case "what is your name":
                    ss.SpeakAsync("My name is Envy, i'am your personal assistent");
                    break;

                case "thank you":
                    ss.SpeakAsync("You're Welcome Akhdan");
                    break;

                case "open chrome":
                    Process.Start("chrome", "http://www.google.co.id");
                    break;

                case "bye bye":
                    ss.SpeakAsync("bye bye Akhdan");
                    break;

                case "close":
                    Application.Exit();
                    break;
            }
            
            //if (text.StartsWith("call me"))
            //{
            //    //ss.SpeakAsync("hai "+text.Replace("call me", string.Empty).Trim());
            //}
            textBox1.Text += e.Result.Text.ToString() + Environment.NewLine;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            sre.RecognizeAsyncStop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }
    }
}
