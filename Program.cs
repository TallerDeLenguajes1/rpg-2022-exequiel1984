using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System;

using rpg_2022_exequiel1984;

var ListadoPersonajes = new List<Personaje>();

var NuevoPersonaje = new Personaje(); 

CargarDatos(NuevoPersonaje);
//string salida = NuevoPersonaje.Edad().ToString();

mostrarPersonaje(NuevoPersonaje);


void CargarDatos(Personaje personaje)
{            
        Random rand = new Random();

        int IndexTipo = rand.Next(0, Enum.GetValues(typeof(tipos)).Length);
        personaje.Tipo = Enum.GetName(typeof(tipos), IndexTipo);
        int IndexNombre = rand.Next(0, Enum.GetValues(typeof(nombres)).Length);
        personaje.Nombre = Enum.GetName(typeof(nombres), IndexNombre);
        personaje.FechaNacimiento = new DateTime(rand.Next(1722, 2022), rand.Next(01, 13), rand.Next(01, 32));  
        personaje.Edad = personaje.CalcularEdad();          
        personaje.Salud = 100;
}

void mostrarPersonaje(Personaje personaje){
    System.Console.WriteLine("Tipo: " + personaje.Tipo);
    System.Console.WriteLine("Nombre: " + personaje.Nombre);
    System.Console.WriteLine("Fecha de Nacimiento: " + personaje.FechaNacimiento);
    System.Console.WriteLine("Edad: " + personaje.Edad);
    System.Console.WriteLine("Salud: " + personaje.Salud);
}