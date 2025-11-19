using System;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace CS106
{
    public partial class SplashPage : Page
    {
        public SplashPage()
        {
            InitializeComponent();
            Loaded += SplashPage_Loaded;
        }

        private void SplashPage_Loaded(object sender, RoutedEventArgs e)
        {
            PlaySound();
            StartFadeAnimation();
        }

        private void PlaySound()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("D:/Users/270040396/Documents/GitHub/CS106-Alex---Mako/Assests/Style Guide/granny-i-see-you.mp3");
                player.Load();
                player.Play();
            }
            catch
            {
                // ignore errors if sound file missing
            }
        }

        private void StartFadeAnimation()
        {
            // Fade IN
            DoubleAnimation fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1.8));
            fadeIn.Completed += (s, e) =>
            {
                // Hold for 1 second then fade OUT
                var hold = new System.Windows.Threading.DispatcherTimer();
                hold.Interval = TimeSpan.FromSeconds(1);
                hold.Tick += (s2, e2) =>
                {
                    hold.Stop();
                    FadeOut();
                };
                hold.Start();
            };

            LayoutRoot.BeginAnimation(UIElement.OpacityProperty, fadeIn);
        }

        private void FadeOut()
        {
            DoubleAnimation fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1.8));
            fadeOut.Completed += (s, e) =>
            {
                NavigationService.Navigate(new Menu());
            };

            LayoutRoot.BeginAnimation(UIElement.OpacityProperty, fadeOut);
        }

    }
}
