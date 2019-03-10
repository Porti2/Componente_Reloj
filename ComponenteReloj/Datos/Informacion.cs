using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace ComponenteReloj.Datos
{
    /// <summary>
    /// Clase Informacion
    /// Esta clase contiene todos los atributos que vamos a necesitar en este componente.
    /// </summary>
    public class Informacion
    {
        /// <summary>
        /// Atributo hor
        /// Este atributo es el que almacenara la hora que nos devuelva la API.
        /// </summary>
        /// <value>string</value>
        public string hor { get; set; }

        /// <summary>
        /// Atributo min 
        /// Este atributo es el que almacenara el minuto que nos devuelva la API.
        /// </summary>
        /// <value>string</value>
        public string min { get; set; }

        /// <summary>
        /// Atributo seg
        /// Este atributo es el que almacenara el segundo que nos devuelva la API.
        /// </summary>
        /// <value>string</value>
        public string seg { get; set; }

        /// <summary>
        /// Atributo formatted
        /// Este atributo es el que almacenara la fecha y hora que nos devuelva la API.
        /// </summary>
        /// <value>string</value>
        public string formatted { get; set; }

        /// <summary>
        /// Atributo continente
        /// Este atributo es el que almacenara el continente que escoga el usuario.
        /// </summary>
        /// <value>string</value>
        public string continente { get; set; }

        /// <summary>
        /// Atributo ciudad
        /// Este atributo es el que almacenara la ciudad que escoga el usuario.
        /// </summary>
        /// <value>string</value>
        public string ciudad { get; set; }

    }


}
