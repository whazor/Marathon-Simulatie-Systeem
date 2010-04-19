using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Marathon_Simulatie_Systeem
{
    /// <summary>
    /// De hardloper
    /// </summary>
    public class Hardloper
    {
        // Het nummertje van de hardloper
        public int id;
        // Een lijst met tijden van de hardloper
        private List<Int64> tijden;
        // Het plaatje van de hardloper
        private Image hardloperImage;
        // De baan waar de hardloper op rent.
        private Baan baan;
        // De starttijd van de hardloper.
        private DateTime startTijd;
        // Welke sensor de hardloper is.
        public byte sensor = 0;
        /// <summary>
        /// De hardloper class
        /// </summary>
        /// <param name="hardloperImage">Het plaatje van de hardloper, deze wordt dus naarvoren geschoven</param>
        /// <param name="baan">De baan waar de hardloper op rent</param>
        public Hardloper(ref Image hardloperImage, Baan baan)
        {
            this.baan = baan;
            this.hardloperImage = hardloperImage;
            tijden = new List<Int64>();
        }
        // De willekeurige getallen generator
        Random randomizer = new Random();
        /// <summary>
        /// Laat de hardloper nog een stap nemen.
        /// </summary>
        public void volgendeStap()
        {
            // Animatie aanmaken
            DoubleAnimation animatie = new DoubleAnimation();
            // Het plaatje van de hardloper
            Thickness nieuweLocatie = hardloperImage.Margin;
            // De nieuwe locatie van de hardloper (willekeurig)
            nieuweLocatie.Left += randomizer.Next(20, 30);
            // De animatie om dat te doen (tijd ook willekeurig)
            ThicknessAnimation resizeAnimation = new ThicknessAnimation(hardloperImage.Margin, nieuweLocatie, TimeSpan.FromMilliseconds(randomizer.Next(200, 300)));
            // Als de animatie klaar is
            resizeAnimation.Completed +=
                (o, t) =>
                {
                    // Kijk of hij nog een keer moet rennen
                    if (baan.controleerPositie(this, nieuweLocatie.Left+hardloperImage.Width))
                    {
                        // Laat hem nog een keer rennen
                        volgendeStap();
                    }
                };
            // Start de animatie
            hardloperImage.BeginAnimation(Rectangle.MarginProperty, resizeAnimation);
        }
        /// <summary>
        /// Als de hardloper heeft gewonnen
        /// </summary>
        public void Gewonnen()
        {
            //hardloperImage.Opacity = 0.5;
        }
        /// <summary>
        /// Als de hardloper heeft verloren
        /// </summary>
        public void Verloren()
        {
            hardloperImage.Opacity = 0.5;
        }
        /// <summary>
        /// Laat de hardloper rennen
        /// </summary>
        public void Start()
        {
            startTijd = DateTime.Now;
            volgendeStap();
        }
        /// <summary>
        /// Voeg tijd toe aan de lijst met tijden van hardloper
        /// </summary>
        /// <returns>De toegevoegde tijd.</returns>
        public Int64 setTime()
        {
            tijden.Add(DateTime.Now.ToBinary() - startTijd.ToBinary());
            return (tijden.Last());
        }
    }
}
