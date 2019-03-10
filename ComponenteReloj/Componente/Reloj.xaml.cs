using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace ComponenteReloj.Componente
{
    public sealed partial class Reloj : UserControl
    {
        public Reloj()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Clase <see cref="Reloj"/>.
        /// Componente grafico que muestra la hora de la ciudad indicada, conectandose
        /// a una API que te devuelve la hora de dicha ciudad. Se muestra en un reloj
        /// analogico y en uno digital. 
        /// 
        /// El ejemplo siguiente muestra como crear un <see cref="Reloj"/> y añadirle
        /// la hora recogida con la API. 
        /// <code>
        /// Layout(canvas);
        /// tiempo.Interval = TimeSpan.FromSeconds(1);
        /// tiempo.Tick += (object s, object obj) =>
        /// {
        ///     manecillaSegundos(Int32.Parse(MainPage.info.seg));
        ///     manecillaMinutos(Int32.Parse(MainPage.info.min), Int32.Parse(MainPage.info.seg));
        ///     manecillaHoras(Int32.Parse(MainPage.info.hor), Int32.Parse(MainPage.info.min), Int32.Parse(MainPage.info.seg));
        ///     texto.Text = MainPage.info.hor + " : " + MainPage.info.min + " : " + MainPage.info.seg;
        /// };
        /// tiempo.Start();
        /// </code>
        /// </summary>

        // Atributos
        /// <summary>
        /// Atributo tiempo.
        /// Este atributo nos servira para indicar al componente la hora que deseamos.
        /// </summary>
        /// <value>DispatcherTimer</value>
        public static DispatcherTimer tiempo = new DispatcherTimer();

        /// <summary>
        /// Atributos Canvas1 y Canvas2
        /// Haran referencia al canvas donde se dibujara el reloj.
        /// </summary>
        /// <value>Canvas</value>
        public Canvas canvas1 = new Canvas();
        public Canvas canvas2 = new Canvas();

        /// <summary>
        /// Atributo Segundos
        /// Este define la manecilla de los segundos en el reloj.
        /// </summary>
        /// <value>Rectangle</value>
        public Rectangle segundos;

        /// <summary>
        /// Atributo Minutos
        /// Este define la manecilla de los minutos en el reloj.
        /// </summary>
        /// <value>Rectangle</value>
        public Rectangle minutos;

        /// <summary>
        /// Atributo Horas
        /// Este define la manecilla de las horas en el reloj.
        /// </summary>
        /// <value>Rectangle</value>
        public Rectangle horas;

        /// <summary>
        /// Atributo _uiSettings
        /// Este atributo hace referencia a la clase UISettings que nos servira para
        /// obtener el colores con los que se pintaran el reloj.
        /// </summary>
        /// <value>UISettings</value>
        public static UISettings _uiSettings = new UISettings();

        /// <summary>
        /// Atributo foreground
        /// Este atributo contiene el color con el que se pintaran las lineas 
        /// que indican la hora.
        /// </summary>
        /// <value>Brush</value>
        public Brush foreground = new SolidColorBrush(Colors.White);

        /// <summary>
        /// Atributo background
        /// Este atributo contiene el color con el que se pintara el borde del reloj.
        /// </summary>
        /// <value>Brush</value>
        public Brush background = new SolidColorBrush(Colors.DarkCyan);

        /// <summary>
        /// Atributo anchoSegundos
        /// Este atributo define el ancho que tendra la manecilla de los segundos.
        /// </summary>
        /// <value>int</value>
        public int anchoSegundos = 1;

        /// <summary>
        /// Atributo largoSegundos
        /// Este atributo define el largo que tendra la manecilla de los segundos.
        /// </summary>
        /// <value>int</value>
        public int largoSegundos;

        /// <summary>
        /// Atributo anchoMinutos
        /// Este atributo define el ancho que tendra la manecilla de los minutos.
        /// </summary>
        /// <value>int</value>
        public int anchoMinutos = 5;

        /// <summary>
        /// Atributo largoMinutos
        /// Este atributo define el largo que tendra la manecilla de los minutos.
        /// </summary>
        /// <value>int</value>
        public int largoMinutos;

        /// <summary>
        /// Atributo anchoHoras
        /// Este atributo define el ancho que tendra la manecilla de las horas.
        /// </summary>
        /// <value>int</value>
        public int anchoHoras = 8;

        /// <summary>
        /// Atributo largoHoras
        /// Este atributo define el largo que tendra la manecilla de las horas.
        /// </summary>
        /// <value>int</value>
        public int largoHoras;

        /// <summary>
        /// Atributo diametro
        /// Este atributo define el diametro que tendra la Elipse que contendra el reloj.
        /// </summary>
        /// <value>double</value>
        public double diametro;

        /// <summary>
        /// Atributo Time
        /// Este atributo definira la hora actual cuando el usuario quiera obtenerla.
        /// </summary>
        /// <value>DateTime</value>
        public DateTime Time { get; set; } = DateTime.Now;

        /// <summary>
        /// Metodo Manecillas
        /// Este metodo recoge el ancho, largo, coordenada x, coordenada y, y el grosor
        /// para definir las distintas manecillas del reloj.
        /// </summary>
        /// <param name="width">Ancho de la manecilla</param>
        /// <param name="height">Largo de la manecilla</param>
        /// <param name="x">Coordenada X, donde se posiciona la manecilla</param>
        /// <param name="y">Coordenada Y, donde se posiciona la manecilla</param>
        /// <param name="grosor">Grosor que tendra la manecilla.</param>
        /// <returns></returns>
        public Rectangle Manecillas(double width, double height, double x, double y, double grosor)
        {
            Rectangle hand = new Rectangle
            {
                Width = width,
                Height = height,
                Fill = background,
                StrokeThickness = grosor,
                RadiusX = x,
                RadiusY = y
            };

            return hand;
        }

        /// <summary>
        /// Metodo eliminarManecilla
        /// Este metodo recoge la manecilla que se acaba de añadir y la elimina cuando se
        /// añade la siguiente, para que se produzca el efecto de un reloj y no se quede
        /// la manecilla anterior dibujada.
        /// </summary>
        /// <param name="rectangulo">Este parametro es la manecilla que se va a eliminar.</param>
        public void eliminarManecilla(Rectangle rectangulo)
        {
            if (rectangulo != null && canvas2.Children.Contains(rectangulo))
            {
                canvas2.Children.Remove(rectangulo);
            }
        }

        /// <summary>
        /// Metodo anadirManecilla
        /// Este metodo recoge la manecilla que se desea añadir y la añade al canvas.
        /// </summary>
        /// <param name="rectangulo">Este parametro es la manecilla que se va a añadir.</param>
        public void anadirManecilla(Rectangle rectangulo)
        {
            if (!canvas2.Children.Contains(rectangulo))
            {
                canvas2.Children.Add(rectangulo);
            }
        }

        /// <summary>
        /// Metodo TransformGroup
        /// Este metodo recoge el angulo y las coordenadas X e Y, para ir dibujando las 
        /// manecillas a lo largo del reloj en sus distintos angulos. 
        /// </summary>
        /// <param name="angulo"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>transformGroup</returns>
        public TransformGroup TransformGroup(double angulo, double x, double y)
        {
            TransformGroup transformGroup = new TransformGroup();
            TranslateTransform translate = new TranslateTransform
            {
                X = x,
                Y = y
            };
            transformGroup.Children.Add(translate);

            RotateTransform rotateTransform = new RotateTransform
            {
                Angle = angulo
            };
            transformGroup.Children.Add(rotateTransform);

            TranslateTransform secondTranslate = new TranslateTransform
            {
                X = diametro / 2,
                Y = diametro / 2
            };
            transformGroup.Children.Add(secondTranslate);

            return transformGroup;
        }

        /// <summary>
        /// Metodo manecillaSegundos
        /// Este metodo recoge los segundos que le proporciona la API, crea la manecilla
        /// y la posiciona en el canvas, dependiendo de los datos recogidos se posicionara
        /// en una posicion y angulo distintos.
        /// </summary>
        /// <param name="seconds">Este parametro son los segundos que se obtienen de la API.</param>
        public void manecillaSegundos(int seconds)
        {
            eliminarManecilla(segundos);

            segundos = Manecillas(anchoSegundos, largoSegundos, 0, 0, 0);
            segundos.RenderTransform = TransformGroup(seconds * 6, -anchoSegundos / 2, -largoSegundos + 4.25);
            anadirManecilla(segundos);

        }

        /// <summary>
        /// Metodo manecillaMinutos
        /// Este metodo recoge los minutos y segundos que le proporciona la API, 
        /// crea la manecilla y la posiciona en el canvas.
        /// </summary>
        /// <param name="seconds">Este parametro son los segundos que se obtienen de la API.</param>
        /// /// <param name="minutes">Este parametro son los minutos que se obtienen de la API.</param>
        public void manecillaMinutos(int minutes, int seconds)
        {
            eliminarManecilla(minutos);

            minutos = Manecillas(anchoMinutos, largoMinutos, 2, 2, 0.6);
            minutos.RenderTransform = TransformGroup(6 * minutes + seconds / 10, -anchoMinutos / 2, -largoMinutos + 4.25);
            anadirManecilla(minutos);

        }

        /// <summary>
        /// Metodo manecillaHoras
        /// Este metodo recoge la hora que le proporciona la API, crea la manecilla
        /// y la posiciona en el canvas.
        /// </summary>
        /// <param name="hours">Este parametro son las horas que se obtienen de la API.</param>
        /// <param name="minutes">Este parametro son los minutos que se obtienen de la API.</param>
        /// <param name="seconds">Este parametro son los segundos que se obtienen de la API.</param>
        public void manecillaHoras(int hours, int minutes, int seconds)
        {
            eliminarManecilla(horas);

            horas = Manecillas(anchoHoras, largoHoras, 3, 3, 0.6);
            horas.RenderTransform = TransformGroup(30 * hours + minutes / 2 + seconds / 120, -anchoHoras / 2, -largoHoras + 4.25);
            anadirManecilla(horas);

        }

        /// <summary>
        /// Metodo Layout
        /// Este metodo lo primero que hace es limpiar el canvas por si se hubiera
        /// dibujado algo anteriormente.
        /// A continuacion, coge el ancho del canvas para ver que diametro tendra
        /// nuestra elipse, la cual se dibuja a continuacion añadiendole un borde
        /// y el color de este. 
        /// Dentro de este borde van las rayas las cuales indican la hora que es.
        /// Se calculan las rayas para las horas y para los minutos. 
        /// Despues de esto se dibujan dentro del borde de la elipse.
        /// </summary>
        /// <param name="canvas">Este parametro es el canvas donde se va a dibujar
        /// el reloj.</param>
        public void Layout(Canvas canvas)
        {
            canvas.Children.Clear();
            diametro = canvas.Width;
            double inner = diametro - 20;

            Ellipse elipse = new Ellipse
            {
                Height = diametro,
                Width = diametro,
                Stroke = ColorBorde,
                StrokeThickness = 25
            };

            canvas.Children.Add(elipse);
            canvas1.Children.Clear();
            canvas1.Width = inner;
            canvas1.Height = inner;

            for (int i = 0; i < 60; i++)
            {
                Rectangle indicadorHora =
                    new Rectangle
                    {
                        Fill = ColorHora
                    };
                if ((i % 5) == 0)
                {
                    indicadorHora.Width = 3;
                    indicadorHora.Height = 8;
                    indicadorHora.RenderTransform = TransformGroup(i * 6, -(indicadorHora.Width / 2), -(indicadorHora.Height * 2 + 4.5 - elipse.StrokeThickness / 2 - inner / 2 - 6));
                }
                else
                {
                    indicadorHora.Width = 1;
                    indicadorHora.Height = 4;
                    indicadorHora.RenderTransform = TransformGroup(i * 6, -(indicadorHora.Width / 2), -(indicadorHora.Height * 2 + 12.75 - elipse.StrokeThickness / 2 - inner / 2 - 8));
                }
                canvas1.Children.Add(indicadorHora);
            }

            canvas.Children.Add(canvas1);
            canvas2.Width = diametro;
            canvas2.Height = diametro;
            canvas.Children.Add(canvas2);
            largoSegundos = (int)diametro / 2 - 20;
            largoMinutos = (int)diametro / 2 - 40;
            largoHoras = (int)diametro / 2 - 60;
        }

        /// <summary>
        /// Metodo ColorHora
        /// Este metodo simplemente define el color que tendran las rayas que 
        /// indican la hora y los minutos. 
        /// </summary>
        /// <value>Brush</value>
        public Brush ColorHora
        {
            get { return foreground; }
            set
            {
                foreground = value;
                Layout(canvas);
            }
        }

        /// <summary>
        /// Metodo ColorBorde
        /// Este metodo simplemente define el color que tendra el borde.
        /// </summary>
        /// <value>Brush</value>
        public Brush ColorBorde
        {
            get { return background; }
            set
            {
                background = value;
                Layout(canvas);
            }
        }

        /// <summary>
        /// Metodo Canvas_Loaded
        /// Este metodo recoge todos los datos anteriores, la elipse, las manecillas, 
        /// colores, etc. Y las pinta en el canvas colocando las manecillas en la hora
        /// que proporciona la API.
        /// </summary>
        /// <param name="sender">Objeto con el que se lanza el evento</param>
        /// <param name="e">Argumentos del evento</param>
        public void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            Layout(canvas);
            tiempo.Interval = TimeSpan.FromSeconds(1);
            tiempo.Tick += (object s, object obj) =>
            {
                
                manecillaSegundos(Int32.Parse(MainPage.info.seg));
                manecillaMinutos(Int32.Parse(MainPage.info.min), Int32.Parse(MainPage.info.seg));
                manecillaHoras(Int32.Parse(MainPage.info.hor), Int32.Parse(MainPage.info.min), Int32.Parse(MainPage.info.seg));
                texto.Text = MainPage.info.hor + " : " + MainPage.info.min + " : " + MainPage.info.seg;
                
            };
            tiempo.Start();
        }
    }
}
