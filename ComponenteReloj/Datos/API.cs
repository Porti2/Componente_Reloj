using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ComponenteReloj.Datos
{
    public class API
    {
        /// <summary>
        /// Atributo enlace
        /// Este atributo es donde se almacena la URL de la API que nos proporciona
        /// la hora.
        /// </summary>
        /// <value>string</value>
        public static string enlace;

        /// <summary>
        /// Metodo getDatos
        /// Este metodo realiza la conexion con la API, obtiene los datos en formato
        /// JSON y los devuelve en un objeto. 
        /// Para realizar la conexion con la API y obtener los datos se realiza 
        /// lo siguiente: 
        /// <code>
        /// public async static Task<RootObject> getDatos()
        /// {
        ///     enlace = "http://api.timezonedb.com/v2.1/get-time-zone?key=WY2WGT1FT2DX&format=json&by=zone&zone=" + MainPage.info.continente + "/" + MainPage.info.ciudad;
        ///     HttpClient http = new HttpClient();
        ///     var respuesta = await http.GetAsync(enlace.Trim());
        ///     var resultado = await respuesta.Content.ReadAsStringAsync();
        ///     
        ///     DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(RootObject));
        ///     MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(resultado));
        ///     RootObject datos = (RootObject)serializer.ReadObject(ms);
        ///     
        ///     return datos;
        /// }
        /// </code>
        /// </summary>
        /// <returns>Task<RootObjects></returns>
    public async static Task<RootObject> getDatos()
        {

            enlace = "http://api.timezonedb.com/v2.1/get-time-zone?key=WY2WGT1FT2DX&format=json&by=zone&zone=" + MainPage.info.continente + "/" + MainPage.info.ciudad;

            HttpClient http = new HttpClient();
            var respuesta = await http.GetAsync(enlace.Trim());
            var resultado = await respuesta.Content.ReadAsStringAsync();

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(RootObject));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(resultado));

            RootObject datos = (RootObject)serializer.ReadObject(ms);
            return datos;

        }
    }

    /// <summary>
    /// Clase RootObject
    /// Esta clase es al que contiene todos los atributos que devuelve la API.
    /// </summary>
    [DataContract]
    public class RootObject
    {
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public string countryCode { get; set; }
        [DataMember]
        public string countryName { get; set; }
        [DataMember]
        public string zoneName { get; set; }
        [DataMember]
        public string abbreviation { get; set; }
        [DataMember]
        public int gmtOffset { get; set; }
        [DataMember]
        public string dst { get; set; }
        [DataMember]
        public int zoneStart { get; set; }
        [DataMember]
        public int zoneEnd { get; set; }
        [DataMember]
        public string nextAbbreviation { get; set; }
        [DataMember]
        public int timestamp { get; set; }
        [DataMember]
        public string formatted { get; set; }
    }
}