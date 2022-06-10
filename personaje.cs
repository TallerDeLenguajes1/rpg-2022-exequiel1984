using System.Collections.Generic;
using System.Text.Json;
using System.IO;

using rpg_2022_exequiel1984;

namespace rpg_2022_exequiel1984
{
    public class Personaje {
        
        private string tipo;
        private string nombre;
        private string apodo;
        private DateTime fechaNacimiento;        
        private int salud;

        private int velocidad;
        private int destreza;
        private int fuerza;
        private int nivel;
        private int armadura;

        public int Velocidad { get => velocidad; set => velocidad = value; }
        public int Destreza { get => destreza; set => destreza = value; }
        public int Fuerza { get => fuerza; set => fuerza = value; }
        public int Nivel { get => nivel; set => nivel = value; }        

        public string Tipo { get => tipo; set => tipo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apodo { get => apodo; set => apodo = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public int Salud { get => salud; set => salud = value; }        

        public Personaje()
        {
        
        }

        public int Edad()
        {
            return DateTime.Now.Year - FechaNacimiento.Year;
        }
        /*
        static void CrearPersonaje(){
            var NuevoPersonaje = new personaje();
            NuevoPersonaje.Datos.CargarDatos();
            System.Console.WriteLine(NuevoPersonaje.Datos.Salud);
        } 
        */      
    }
}