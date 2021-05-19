using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Viaje
    {
        private static List<string> mCities { get; set; }

        private static List<List<int>> mConnections { get; set; }
        private static List<string> mRoutes { get; set; }

        public Viaje(List<string> pCities, List<List<int>> pConnections)
        {
            mCities = pCities;
            mConnections = pConnections;
        }

        private static void ComprobarCiudad(string pCiudad)
        {
            if (!mCities.Contains(pCiudad))
            {
                throw new Exception("La Ciudad que esta intentando introducir no existe");
            }
        }

        public static Dictionary<string, int> DevolverRutasCiudades(string pCiudadOrigen)
        {
            Dictionary<string, int> rutasDevolver = new Dictionary<string, int>();
            ComprobarCiudad(pCiudadOrigen);
            foreach (string ciudad in mCities)
            {
                if (!ciudad.Equals(pCiudadOrigen))
                {
                    Dictionary<string, int> ListaDestinoOptimo = new Dictionary<string, int>();
                    ObtenerRutaOptima(pCiudadOrigen, ciudad, ListaDestinoOptimo, pCiudadOrigen, 0);
                    if (ListaDestinoOptimo.Count() > 0 )
                    {
                        rutasDevolver.TryAdd(ListaDestinoOptimo.First().Key, ListaDestinoOptimo.First().Value);
                    }                  
                }
            }
            return rutasDevolver;
        }

        public static bool ObtenerRutaOptima(string pCiudadOrigen, string pCiudadDestino, Dictionary<string, int> pListaDestinos, string pOrigen, int pDistancia)
        {
            ComprobarCiudad(pCiudadOrigen);
            ComprobarCiudad(pCiudadDestino);
            bool volver = false;
            List<string> listaPosiblesDestinos = RutasPosiblesDeCiudad(pCiudadOrigen);
            if (listaPosiblesDestinos.Contains(pCiudadDestino))
            {
                pDistancia += DevolverDistancia(pCiudadOrigen, pCiudadDestino);
                if(pListaDestinos.Keys.Count == 0)
                {
                    pListaDestinos.TryAdd(pOrigen + "," + pCiudadDestino, pDistancia);
                }
                else
                {
                    int distancia = pListaDestinos.First().Value;
                    if(distancia > pDistancia)
                    {
                        pListaDestinos.Clear();
                        pListaDestinos.TryAdd(pOrigen + "," + pCiudadDestino, pDistancia);
                    }
                }
                volver = true;
            }
            else
            {
                foreach (string ciudad in listaPosiblesDestinos)
                {
                    if (!pOrigen.Contains(ciudad))
                    {
                        pDistancia += DevolverDistancia(pCiudadOrigen, ciudad);
                        if (!ObtenerRutaOptima(ciudad, pCiudadDestino, pListaDestinos, pOrigen + "," + ciudad, pDistancia))
                        {
                            pDistancia -= DevolverDistancia(pCiudadOrigen, ciudad);
                        }
                    }
                }
            }
            return volver;
        }


        public static void ObtenerPosiblesRutas(string pCiudadOrigen, string pCiudadDestino, List<string> pListaDestinos, string pOrigen)
        {
            List<string> listaPosiblesDestinos = RutasPosiblesDeCiudad(pCiudadOrigen);
            if (listaPosiblesDestinos.Contains(pCiudadDestino))
            {
                pListaDestinos.Add(pOrigen + "," + pCiudadDestino);
            }
            else
            {
                foreach(string ciudad in listaPosiblesDestinos)
                {
                    if (!pOrigen.Contains(ciudad))
                    {
                        ObtenerPosiblesRutas(ciudad, pCiudadDestino, pListaDestinos, pOrigen + "," + ciudad);
                    }
                }
            }
        }

        private static bool TieneConexion(List<string> pCities, List<List<int>> pConnections, string pCiudadOrigen, string pCudadDestino)
        {
            bool tieneConexion = false;
            if (pConnections[pCities.IndexOf(pCiudadOrigen)][pCities.IndexOf(pCudadDestino)] != 0)
            {
                tieneConexion = true;
            }
            return tieneConexion;
        }

        private static int DevolverConexion(List<string> pCities, List<List<int>> pConnections, string pInicio, string pFin)
        {
            int conexion = 0;
            if (pConnections[pCities.IndexOf(pInicio)][pCities.IndexOf(pFin)] != 0)
            {
                conexion = pConnections[pCities.IndexOf(pInicio)][pCities.IndexOf(pFin)];
            }
            return conexion;
        }

        private static int DevolverDistancia(string pCiudadOrigen, string pCudadDestino)
        {
            return mConnections[mCities.IndexOf(pCiudadOrigen)][mCities.IndexOf(pCudadDestino)];
        }

        private static List<string> RutasPosiblesDeCiudad (string pCiudad)
        {
            List<string> rutas = new List<string>();
            foreach (string ciudad in mCities)
            {
                if (pCiudad != ciudad)
                {
                    if(DevolverDistancia(pCiudad, ciudad) > 0)
                    {
                        rutas.Add(ciudad);
                    }
                }
            }
            return rutas;
        }
       
    }
}
