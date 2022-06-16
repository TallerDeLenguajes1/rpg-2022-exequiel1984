using System.Collections.Generic;
using System.Text.Json;
using System.IO;

using espacioDatos;

namespace rpg_2022_exequiel1984
{
    public class personaje {
        private Caracteristicas caracteristicas; 
        private Datos datos;

        public Caracteristicas Caracteristicas { get => caracteristicas; set => caracteristicas = value; }
        public Datos Datos { get => datos; set => datos = value; }

        public personaje(){
            this.Caracteristicas = new Caracteristicas();
            this.Datos = new Datos();
        }

        static void CrearPersonaje(){
            var NuevoPersonaje = new personaje();
            NuevoPersonaje.Datos.CargarDatos();
            System.Console.WriteLine(NuevoPersonaje.Datos.Salud);
        }       
    }
}