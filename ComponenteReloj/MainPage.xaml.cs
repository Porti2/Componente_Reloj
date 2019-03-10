using ComponenteReloj.Datos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace ComponenteReloj
{
    
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Atributo info
        /// Este atributo crea una instancia de la clase Informacion en la cual 
        /// almacenaremos la informacion que necesitamos en sus respectivos atributos
        /// </summary>
        /// <value>Informacion</value>
        public static Informacion info = new Informacion();

        /// <summary>
        /// Atributo tiempo.
        /// Este atributo nos servira para indicar al componente la hora que deseamos.
        /// </summary>
        /// <value>DispatcherTimer</value>
        public DateTime Time { get; set; } = DateTime.Now;
        //public string[] colores = { "Aqua", "Green", "Red", "Yellow", "Blue", "Brown", "Black" };

        /// <summary>
        /// Atributo continentes
        /// Este atributo es un array de strings que contiene los distintos contientes
        /// que el usuario podra elegir.
        /// </summary>
        /// <value>string[]</value>
        public string[] continentes = { "Africa", "America", "Europe", "Asia", "Pacific", "Oceania" };

        /// <summary>
        /// Atributo Africa
        /// Este atributo es un array de strings que contiene las distintas ciudades
        /// que el usuario podra elegir de Africa.
        /// </summary>
        /// <value>string[]</value>
        public string[] Africa = { "Accra", "Ceuta", "Lagos", "Casablanca", "Algiers" };

        /// <summary>
        /// Atributo America
        /// Este atributo es un array de strings que contiene las distintas ciudades
        /// que el usuario podra elegir de America.
        /// </summary>
        /// <value>string[]</value>
        public string[] America = { "Barbados", "Bogota", "Los_Angeles", "New_York", "Puerto_Rico" };

        /// <summary>
        /// Atributo Europe
        /// Este atributo es un array de strings que contiene las distintas ciudades
        /// que el usuario podra elegir de Europe.
        /// </summary>
        /// <value>string[]</value>
        public string[] Europe = { "Dublin", "Andorra", "Madrid", "Berlin", "Monaco" };

        /// <summary>
        /// Atributo Asia
        /// Este atributo es un array de strings que contiene las distintas ciudades
        /// que el usuario podra elegir de Asia.
        /// </summary>
        /// <value>string[]</value>
        public string[] Asia = { "Amman", "Baku", "Colombo", "Tokyo", "Tomsk" };

        /// <summary>
        /// Atributo Pacific
        /// Este atributo es un array de strings que contiene las distintas ciudades
        /// que el usuario podra elegir de Pacific.
        /// </summary>
        /// <value>string[]</value>
        public string[] Pacific = { "Apia", "Honolulu", "Gambier", "Marquesas", "Wallis" };

        /// <summary>
        /// Atributo Oceania
        /// Este atributo es un array de strings que contiene las distintas ciudades
        /// que el usuario podra elegir de Oceania.
        /// </summary>
        /// <value>string[]</value>
        public string[] Oceania = { "Brisbane", "Currie", "Melbourne", "Sydney", "Lindeman" };

        /// <summary>
        /// Metodo MainPage
        /// Este metodo simplemente inicia el componente
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            continente.ItemsSource = continentes;
        }

        /// <summary>
        /// Metodo Continente_SelectionChanged
        /// Este metodo lo que realiza es un SelectionChanged, es decir, que cuando el usuario
        /// escoga una opcion del desplegable de Continentes este metodo lo recogera y 
        /// dependiendo de este cambiara las ciudades del desplegable de Ciudades.
        /// </summary>
        /// <param name="sender">Objeto con el que se lanza el evento</param>
        /// <param name="e">Argumentos del evento</param>
        private void Continente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (continente.SelectedItem.Equals("Africa"))
            {
                ciudad.ItemsSource = Africa;
                info.continente = "Africa";

            }
            else if (continente.SelectedItem.Equals("America"))
            {
                ciudad.ItemsSource = America;
                info.continente = "America";
            }
            else if (continente.SelectedItem.Equals("Europe"))
            {
                ciudad.ItemsSource = Europe;
                info.continente = "Europe";
            }
            else if (continente.SelectedItem.Equals("Asia"))
            {
                ciudad.ItemsSource = Asia;
                info.continente = "Asia";
            }
            else if (continente.SelectedItem.Equals("Pacific"))
            {
                ciudad.ItemsSource = Pacific;
                info.continente = "Pacific";
            }
            else if (continente.SelectedItem.Equals("Oceania"))
            {
                ciudad.ItemsSource = Oceania;
                info.continente = "Australia";
            }
        }

        /// <summary>
        /// Metodo PedirHora_Click
        /// Este metodo es la accion Click de un boton. En este caso se encarga de recoger los
        /// datos que proporciona la API y de estos escoge la hora, minutos y segundos y los 
        /// almacena en un objeto Informacion <see cref="Informacion"/>.
        /// </summary>
        /// <param name="sender">Objeto con el que se lanza el evento</param>
        /// <param name="e">Argumentos del evento</param>
        private async void PedirHora_Click(object sender, RoutedEventArgs e)
        {
            info.ciudad = ciudad.SelectedItem.ToString();

            RootObject datos = new RootObject();

            datos = await API.getDatos();
            info.formatted = datos.formatted;

            int length = 2;
            info.hor = info.formatted.Substring(11, length);
            info.min = info.formatted.Substring(14, length);
            info.seg = info.formatted.Substring(17, length);

        }

        /// <summary>
        /// Metodo Actual_Click
        /// Este Metodo recoge la hora actual del sistema y la muestra en el componente
        /// </summary>
        /// <param name="sender">Objeto con el que se lanza el evento</param>
        /// <param name="e">Argumentos del evento</param>
        private void Actual_Click(object sender, RoutedEventArgs e)
        {
            Time = DateTime.Now;

            info.seg = Time.Second.ToString();
            info.min = Time.Minute.ToString();
            info.hor = Time.Hour.ToString();
        }

        /// <summary>
        /// Metodo Ciudad_SelectionChanged
        /// Este metodo lo que realiza es un SelectionChanged, es decir, que cuando el usuario
        /// escoga una opcion del desplegable de Ciudades, este metodo lo recogera y lo almacenara
        /// para posteriormente utilizarlo en la API.
        /// </summary>
        /// <param name="sender">Objeto con el que se lanza el evento</param>
        /// <param name="e">Argumentos del evento</param>
        /// 
        /*
        private void Ciudad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            info.ciudad = ciudad.SelectedItem.ToString();
        }
        */

    }
}
