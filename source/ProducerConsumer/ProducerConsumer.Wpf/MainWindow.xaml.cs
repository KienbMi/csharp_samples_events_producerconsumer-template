using ProducerConsumer.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ProducerConsumer.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ILogTask
    {
        private Producer _producer;
        private Consumer _consumer;
        private Queue<Task> _queue;
        private FastClock _fastClock;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Initialized(object source, EventArgs e)
        {
            _fastClock = FastClock.Instance;
        }


        /// <summary>
        /// Producer, Consumer und Queue erzeugen. Observer anmelden und 
        /// Simulation starten
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            TextBlockLog.Text = "";
            _queue = new Queue<Task>();
            
            int min = Convert.ToInt32(TextBoxProducerMinimum.Text);
            int max = Convert.ToInt32(TextBoxProducerMaximum.Text);
            _producer = new Producer(min, max, _fastClock, this, _queue);
            min = Convert.ToInt32(TextBoxConsumerMinimum.Text);
            max = Convert.ToInt32(TextBoxConsumerMaximum.Text);
            _consumer = new Consumer(min, max, _fastClock, _queue);
            CheckBoxIsRunning.IsChecked = true;
            _fastClock.IsRunning = true;
        }

        /// <summary>
        /// Fügt eine Zeile zur Textbox hinzu.
        /// Da Timer in eigenem Thread läuft ist ein Threadwechsel mittels Invoke
        /// notwendig
        /// </summary>
        /// <param name="line"></param>
        void AddLineToTextBox(string line)
        {
            StringBuilder text = new StringBuilder(TextBlockLog.Text);
            text.Append(FastClock.Instance.Time.ToShortTimeString() + "\t");
            text.Append(line + "\n");
            TextBlockLog.Text = text.ToString();
        }

        private void CheckBoxIsRunning_Click(object sender, RoutedEventArgs e)
        {
            _fastClock.IsRunning = CheckBoxIsRunning.IsChecked == true;
        }

        void ILogTask.SendTextLine(object source, string line)
        {
            AddLineToTextBox(line);
        }
    }
}
