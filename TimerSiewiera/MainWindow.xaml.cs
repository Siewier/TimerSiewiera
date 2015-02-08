using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Timers;

namespace TimerSiewiera
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        int godziny = 0;
        int minuty = 0;
        int sekundy = 0;

        public MainWindow()
        {
            InitializeComponent();
            
        }
        private void StartOdliczania()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (dispatcherTimer.IsEnabled == true)
            {
                if (sekundy == 0)
                {
                    if (minuty == 0)
                    {
                        if (godziny == 0)
                        {
                            //koniec odliczania
                            dispatcherTimer.Stop();
                            //MessageBox.Show("Czas minął!", "Alarm!");
                            Alarm KoniecCzasu = new Alarm();
                            KoniecCzasu.Owner = this;
                            KoniecCzasu.Show();

                        }
                        else
                        {
                            godziny = godziny - 1;
                            minuty = 59;
                            Wyswietl();
                        }
                    }
                    else
                    {
                        minuty = minuty - 1;
                        sekundy = 59;
                        Wyswietl();
                    }
                }
                else
                {
                    sekundy = sekundy - 1;
                    Wyswietl();
                }
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            StartOdliczania();
        }

        private void btnTrzydziesci_Click(object sender, RoutedEventArgs e)
        {
            minuty = minuty + 30;
            if (minuty > 59)
            {
                minuty = minuty - 60;
                godziny = godziny + 1;
            }
            Wyswietl();
        }

        private void Wyswietl()
        {
            string textDoLabelki;
            //textDoLabelki = godziny.ToString() + ":" + minuty.ToString() + ":" + sekundy.ToString();
            if (godziny < 10)
            {
                textDoLabelki = "0" + godziny.ToString();
            }
            else
            {
                textDoLabelki = godziny.ToString();
            }
            textDoLabelki = textDoLabelki + ":";
            if (minuty < 10)
            {
                textDoLabelki = textDoLabelki + "0" + minuty.ToString();
            }
            else
            {
                textDoLabelki = textDoLabelki + minuty.ToString();
            }
            textDoLabelki = textDoLabelki + ":";
            if (sekundy < 10)
            {
                textDoLabelki = textDoLabelki + "0" + sekundy.ToString();
            }
            else
            {
                textDoLabelki = textDoLabelki + sekundy.ToString();
            }
            labelCzas.Content = textDoLabelki;
            
        }

        private void btnGodzina_Click(object sender, RoutedEventArgs e)
        {
            godziny = godziny + 1;
            Wyswietl();
        }

        private void btnPausa_Click(object sender, RoutedEventArgs e)
        {
            if (dispatcherTimer.IsEnabled == true)
            {
                dispatcherTimer.Stop(); 
            }
            else
            {
                dispatcherTimer.Start();
            }
            
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            godziny = 0;
            minuty = 0;
            sekundy = 0;
            Wyswietl();
            labelCzas.Refresh();
        } 
    }
    //http://geekswithblogs.net/NewThingsILearned/archive/2008/08/25/refresh--update-wpf-controls.aspx
    public static class ExtensionMethods
    {
        private static Action EmptyDelegate = delegate() { };

        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }
    }
}
