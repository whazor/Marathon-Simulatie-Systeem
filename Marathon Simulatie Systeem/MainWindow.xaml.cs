using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Marathon_Simulatie_Systeem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Baan loopbaan;
        /// <summary>
        /// Bij het starten van de class een nieuwe loopbaan aanmaken
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            loopbaan = new Baan(this);
        }
        private double lastHardloperTop = 60;
        private int hardloperId = 1;
        /// <summary>
        /// Als op de knop wordt gedrukt dan moet er een hardloper worden toegevoegd.
        /// </summary>
        /// <param name="sender">Het knopje</param>
        /// <param name="e">Nog wat extra informatie</param>
        private void btnHardloperToevoegen_Click(object sender, RoutedEventArgs e)
        {
            // Maak de image aan
            Image hardloperImg = new Image();
            // Neem bijna alle eigenschappen over van het ontzichtbare plaatje
            hardloperImg.Source = image.Source;
            hardloperImg.Height = image.Height;
            hardloperImg.Width = image.Width;
            hardloperImg.HorizontalAlignment = HorizontalAlignment.Left;
            hardloperImg.VerticalAlignment = VerticalAlignment.Top;
            // Hoe ver het plaatje van de rand afzit.
            hardloperImg.Margin = new Thickness(12, lastHardloperTop, 0, 0);
            // Verhoog de variabel zodat het volgende plaatje iets meer naar onder komt te staan.
            lastHardloperTop += image.Height;
            // Maak een nieuwe hardloper class aan
            Hardloper hardloper = new Hardloper(ref hardloperImg, loopbaan) { id = hardloperId++ };
            // Zet het plaatje op het scherm.
            mainGrid.Children.Add(hardloperImg);
            // Zet de hardloper in de baan
            loopbaan.Add(hardloper);
        }
        /// <summary>
        /// Als er op de knop wordt gedrukt laat dan de hardlopers rennen
        /// </summary>
        /// <param name="sender">Het knopje</param>
        /// <param name="e">Nog wat extra informatie</param>
        private void btnStartRace_Click(object sender, RoutedEventArgs e)
        {
            loopbaan.Start();
            // Verberg de knopjes
            btnHardloperToevoegen.Visibility = Visibility.Hidden;
            btnStartRace.Visibility = Visibility.Hidden;
            // Geef de sensor1 weer
            sensor1Tijden.Visibility = Visibility.Visible;

        }
    }
}
