using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marathon_Simulatie_Systeem
{
    /// <summary>
    /// De hardloopbaan
    /// </summary>
    public class Baan
    {
        // Lijst met hardlopers
        private List<Hardloper> hardlopers;
        // Scherm waar elementen opstaan
        private MainWindow window;
        // De hardloper die als eerst binnenkomt.
        private Hardloper firstHardloper;
        /// <summary>
        /// Baan aanmaken
        /// </summary>
        /// <param name="window">Het scherm die de baan doorstuurt</param>
        public Baan(MainWindow window)
        {
            this.window = window;
            hardlopers = new List<Hardloper>();
        }
        /// <summary>
        /// Kijk of opgegeven hardloper verder is dan de positie van de sensor.
        /// </summary>
        /// <param name="sender">De hardloper</param>
        /// <param name="positie">De positie van de hardloper</param>
        /// <returns>Stuurt terug of de hardloper nou wel of niet moet stoppen</returns>
        public bool controleerPositie(Hardloper sender, Double positie)
        {
            // Functie om de tijd om te zetten naar aantal secondes
            Func<long, double> afronden = tijd => (double)(tijd/1000000)/10;
            // Sensor 1
            if (sender.sensor == 0 && window.imgSensor1.Margin.Left < positie)
	        {
                sender.sensor++;
                // Voeg tijd aan de lijst toe
                window.sensor1Tijden.Items.Add("Hardloper: " + sender.id.ToString() + " - " + afronden(sender.setTime()).ToString() + " seconde");
	        }
            // Sensor 2
            if (sender.sensor == 1 && window.imgSensor2.Margin.Left < positie)
            {
                sender.sensor++;
                // Voeg tijd aan de lijst toe
                window.sensor2Tijden.Items.Add("Hardloper: " + sender.id.ToString() + " - " + afronden(sender.setTime()).ToString() + " secondes");
            }
            // Sensor 3
            if (sender.sensor == 2 && window.imgSensor3.Margin.Left < positie)
            {
                sender.sensor++;
                // Voeg tijd aan de lijst toe
                window.sensor3Tijden.Items.Add("Hardloper: " + sender.id.ToString() + " - " + afronden(sender.setTime()).ToString() + " secondes");
            }
            // Sensor 4
            if (sender.sensor == 3 && window.imgSensor4.Margin.Left < positie)
            {
                if (firstHardloper == null) // Kijk of de hardloper als eerste is
                {
                    firstHardloper = sender;
                    sender.Gewonnen();
                }
                else
                {
                    sender.Verloren();
                }
                sender.sensor++;
                window.sensor4Tijden.Items.Add("Hardloper: " + sender.id.ToString() + " - " + afronden(sender.setTime()).ToString() + " secondes");
            }
            // Kijken of de hardloper moet stoppen (70pixels na de laatste sensor)
            if (sender.sensor == 4 && window.imgSensor4.Margin.Left + 70 < positie)
            {
                sender.sensor++;
                return (false);
            }
            return (sender.sensor < 5); // Als sensor onder de 5 is doorgaan en anders stoppen
        }
        /// <summary>
        /// Start elke hardloper
        /// </summary>
        public void Start()
        {
            foreach (Hardloper hardloper in hardlopers)
            {
                hardloper.Start();
            }
        }
        /// <summary>
        /// Voeg een hardloper toe
        /// </summary>
        /// <param name="hardloper">De hardloper</param>
        public void Add(Hardloper hardloper)
        {
            this.hardlopers.Add(hardloper);
        }
    }
}
