using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using QualDDD.Resources;
using System.Xml.Serialization;
using System.Xml;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Services;
using System.Device.Location;

namespace QualDDD
{
    public partial class MainPage : PhoneApplicationPage
    {
        XmlSerializer xmlSerializer;
        XmlReader xmlReader;
        Cidades cidades;

        //Location location;
        string location;

        List<String> autoCompleteSource = new List<string>();
        List<String> listBuscaDDDCidade = new List<string>();

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnBusca_Click(object sender, RoutedEventArgs e)
        {

            if (listBuscaDDDCidade.Count != 0)
            {
                listBuscaDDDCidade = new List<string>();
            }

            foreach (var item in cidades.ListaCidades)
            {
                if (item.Nome == txbBusca.Text)
                {
                    listBuscaDDDCidade.Add(item.DDD);
                    //location = new Location(item.Nome);
                    //map.Center = location.locationCoordinate;
                    //map.ZoomLevel = 10;
                    location = item.Nome;
                    GetCoordinates();
                    break;
                }

                if (item.DDD == txbBusca.Text)
                {
                    listBuscaDDDCidade.Add(item.Nome);

                }
            }

            listBusca.ItemsSource = listBuscaDDDCidade;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            xmlSerializer = new XmlSerializer(typeof(Cidades));

            xmlReader = XmlReader.Create("XML/cidades.xml");

            cidades = (Cidades)xmlSerializer.Deserialize(xmlReader);

            foreach (var item in cidades.ListaCidades)
            {
                autoCompleteSource.Add(item.Nome);
            }

            txbBusca.ItemsSource = autoCompleteSource;
        }

        GeocodeQuery geoQuery = null;

        private  void GetCoordinates()
        {
            try
            {
                geoQuery = new GeocodeQuery();
                geoQuery.SearchTerm = location;
                geoQuery.GeoCoordinate = new GeoCoordinate();
                geoQuery.QueryCompleted += geoQuery_QueryCompleted;
                geoQuery.QueryAsync();
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception)
            {
                // Something else happened while acquiring the location.
                throw;
            }
        }

        void geoQuery_QueryCompleted(object sender, QueryCompletedEventArgs<IList<MapLocation>> e)
        {
            if (e.Error == null)
            {
                map.Center = e.Result[0].GeoCoordinate;
                map.ZoomLevel = 9;
                geoQuery.Dispose();
            }
        }

    }
}