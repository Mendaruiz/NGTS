using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Dictionary<string, List<int>> diccionario = new Dictionary<string, List<int>>();

            //Entrada de Datos.
            List<string> cities = new List<string> { "Logroño", "Zaragoza", "Teruel", "Madrid", "Lleida", "Alicante", "Castellón", "Segovia", "Ciudad Real" };
            List<List<int>> connections = new List<List<int>> {
                new List<int>{0,4,6,8,0,0,0,0,0},
                new List<int>{4,0,2,0,2,0,0,0,0},
                new List<int>{6,2,0,3,5,7,0,0,0},
                new List<int>{8,0,3,0,0,0,0,0,0},
                new List<int>{0,2,5,0,0,0,4,8,0},
                new List<int>{0,0,7,0,0,0,3,0,7},
                new List<int>{0,0,0,0,4,3,0,0,6},
                new List<int>{0,0,0,0,8,0,0,0,4},
                new List<int>{0,0,0,0,0,7,6,4,0}
            };
            string ciudadOrigen = "Logroño"; //Ciudad Real
            string ciudadDestino = "Ciudad Real";//Logroño
            Dictionary<string, int> listaOptima = new Dictionary<string, int>();
            Viaje viaje = new Viaje(cities, connections);
            Viaje.ObtenerRutaOptima(ciudadOrigen, ciudadDestino, listaOptima, ciudadOrigen, 0);
            Console.WriteLine($"La Ciudad {ciudadOrigen} con ruta a {ciudadDestino}, tiene la siguiente ruta {listaOptima.First().Key}, con un coste de {listaOptima.First().Value}");
            Dictionary<string, int> listaRutasCiudad = new Dictionary<string, int>();
            listaRutasCiudad = Viaje.DevolverRutasCiudades(ciudadOrigen);
            Console.WriteLine($"La Ciudad {ciudadOrigen}, tiene la siguientes rutas:");
            foreach (KeyValuePair<string,int> ruta in listaRutasCiudad)
            {
                Console.WriteLine($"{ruta.Key}, con un coste de {ruta.Value}");
            }
            Console.WriteLine($"La Ciudad {ciudadOrigen} con ruta a {ciudadDestino}, tiene la siguiente ruta {listaOptima.First().Key}, con un coste de {listaOptima.First().Value}");

        }
    }
}
