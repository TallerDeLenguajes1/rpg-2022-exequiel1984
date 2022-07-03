using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;

namespace rpg_2022_exequiel1984
{
    public enum tipos{
            Boxeador,
            MMA,
            Titanes
        };

    public enum apodos{
            Rojo,
            Camacho,
            Diabolo,
            Acuananuta,
            Cacique,
            Cerebrus,
            Hacker,
            Pibe,
            Capanga,
            Hormiga,
            Momia,
            Karadagian
        };

    public class Personaje {
        
        private string tipo;
        private string nombre;
        private string apodo;
        private DateTime fechaNacimiento;   
        private int edad;     
        private double salud;

        private int velocidad;
        private int destreza;
        private int fuerza;
        private int nivel;
        private int armadura;

        private double valorAtaque;
        private int poderDefensa;
        private int estado;

        public string Tipo { get => tipo; set => tipo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apodo { get => apodo; set => apodo = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public double Salud { get => salud; set => salud = value; }
        public int Edad { get => edad; set => edad = value; }

        public int Velocidad { get => velocidad; set => velocidad = value; }
        public int Destreza { get => destreza; set => destreza = value; }
        public int Fuerza { get => fuerza; set => fuerza = value; }
        public int Nivel { get => nivel; set => nivel = value; }        
        public int Armadura { get => armadura; set => armadura = value; }

        public double ValorAtaque { get => valorAtaque; set => valorAtaque = value; }
        public int PoderDefensa { get => poderDefensa; set => poderDefensa = value; }
        public int Estado { get => estado; set => estado = value; }

        public Personaje(){
            this.ValorAtaque = 0;
        }

        public int CalcularEdad()
        {
            int edad = DateTime.Now.Year - FechaNacimiento.Year;
            if (DateTime.Now.Month < FechaNacimiento.Month)
            {
                --edad; 
            }
            return edad;
        }  

    }
}