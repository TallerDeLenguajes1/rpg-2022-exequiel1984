using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace espacioDatos
{
    public enum tipos{
            Boxeador,
            MMA,
            Titanes
        };

    public enum nombres{
            Tyson,
            McGregor,
            Bonavena
        };

    public class Datos {
        
        private string tipo;
        private string nombre;
        private string apodo;
        private DateTime fechaNacimiento;
        private int edad;
        private int salud;

        public string Tipo { get => tipo; set => tipo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apodo { get => apodo; set => apodo = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public int Edad { get => edad; set => edad = value; }
        public int Salud { get => salud; set => salud = value; }

        public Datos(){
            this.Tipo = "";
            this.Nombre = "";
            this.Apodo = "";
            //this.FechaNacimiento = 0;
            this.Edad = 0;
            this.Salud = 0;
        }

        public Datos(string _tipo, string _nombre, string _apodo, DateTime _fechaDeNacimiento, int _edad, int _salud){
            this.Tipo = _tipo;
            this.Nombre = _nombre;
            this.Apodo = _apodo;
            this.FechaNacimiento = _fechaDeNacimiento;
            this.Edad = _edad;
            this.Salud = _salud;
        }

        public static void CargarDatos(){
            //Datos nuevoDato = new Datos();

            Random rand = new Random();

            
            int IndexTipo = rand.Next(0, Enum.GetValues(typeof(tipos)).Length);
            Tipo = Enum.GetName(typeof(tipos), IndexTipo);

            int IndexNombre = rand.Next(0, Enum.GetValues(typeof(nombres)).Length);
            Nombre = Enum.GetName(typeof(nombres), IndexNombre);

            FechaNacimiento = new DateTime(rand.Next(1722, 2022), rand.Next(01, 13), rand.Next(01, 32));
            Edad = DateTime.Now.Year - FechaNacimiento.Year;
            Datos.Salud = 100;
        }
    }
}