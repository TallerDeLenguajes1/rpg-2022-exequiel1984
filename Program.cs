using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System;

using rpg_2022_exequiel1984;

Random rand = new Random();

var ListadoPersonajes = new List<Personaje>();

int CantidadPersonajes;
CantidadPersonajes = rand.Next(1,6);

var NuevoPersonaje = new Personaje(); 

    CargarDatos(NuevoPersonaje);
    CargarCaracteristicas(NuevoPersonaje); 
    Ataque(NuevoPersonaje);

/* for (int i = 0; i < CantidadPersonajes; i++)
{
    var NuevoPersonaje = new Personaje(); 

    CargarDatos(NuevoPersonaje);
    CargarCaracteristicas(NuevoPersonaje);  

    ListadoPersonajes.Add(NuevoPersonaje);
}

MostrarLista(ListadoPersonajes); */

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

void CargarCaracteristicas(Personaje personaje){
        Random rand = new Random();
        personaje.Velocidad = rand.Next(1, 11);
        personaje.Destreza = rand.Next(1, 6);
        personaje.Fuerza = rand.Next(1, 11);
        personaje.Nivel = rand.Next(1, 11);
        personaje.Armadura = rand.Next(1, 11);
}

static void MostrarLista(List<Personaje> Lista)
{
    foreach (var personaje in Lista)
    {
        System.Console.WriteLine("\nTipo: " + personaje.Tipo);
        System.Console.WriteLine("Nombre: " + personaje.Nombre);
        System.Console.WriteLine("Fecha de Nacimiento: " + personaje.FechaNacimiento.ToShortDateString());
        System.Console.WriteLine("Edad: " + personaje.Edad);
        System.Console.WriteLine("Salud: " + personaje.Salud);
        System.Console.WriteLine("Velocidad: " + personaje.Velocidad);
        System.Console.WriteLine("Destreza: " + personaje.Destreza);
        System.Console.WriteLine("Fuerza: " + personaje.Fuerza);
        System.Console.WriteLine("Nivel: " + personaje.Nivel);
        System.Console.WriteLine("Armadura: " + personaje.Armadura + "\n");
    }
}

int Ataque(Personaje personaje){
    Random rand = new Random();

    int PoderDisparo = personaje.Destreza * personaje.Fuerza * personaje.Nivel; 
    System.Console.WriteLine("Poder: " + PoderDisparo);
    int EfectividadDisparo = rand.Next(1, 101);
    EfectividadDisparo = EfectividadDisparo/100; // ERROR
    System.Console.WriteLine("Efectividad: " + EfectividadDisparo);
    int ValorAtaque = PoderDisparo * EfectividadDisparo;
    System.Console.WriteLine("Valor Ataque: " + ValorAtaque);
    return ValorAtaque;
}

