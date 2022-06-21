using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System;

using rpg_2022_exequiel1984;

Random rand = new Random();

var ListadoPersonajes = new List<Personaje>();
var Ganadores = new List<Personaje>();

Personaje Peleador1 = new Personaje();
Personaje Peleador2 = new Personaje();

int CantidadPersonajes;
CantidadPersonajes = rand.Next(1,6);

/* var NuevoPersonaje = new Personaje(); 

    CargarDatos(NuevoPersonaje);
    CargarCaracteristicas(NuevoPersonaje); 
    CalcularAtaque(NuevoPersonaje);

var Defensor = new Personaje();

    CargarDatos(Defensor);
    CargarCaracteristicas(Defensor); 
    CalcularDefensa(Defensor);

ResultadoEnfrentamiento(NuevoPersonaje, Defensor); */

for (int i = 0; i < CantidadPersonajes; i++)
{
    var NuevoPersonaje = new Personaje(); 

    CargarDatos(NuevoPersonaje);
    CargarCaracteristicas(NuevoPersonaje);  

    ListadoPersonajes.Add(NuevoPersonaje);
}

Peleador1 = SeleccionarPersonaje(ListadoPersonajes);
MostrarPersonaje(Peleador1);
Peleador2 = SeleccionarPersonaje(ListadoPersonajes);
if (Peleador1.Nombre == Peleador2.Nombre)
{
    Peleador2 = SeleccionarPersonaje(ListadoPersonajes); 
}

MostrarPersonaje(Peleador2);




//MostrarLista(ListadoPersonajes);


Personaje SeleccionarPersonaje(List<Personaje> ListaPersonajes){

    
    return ListaPersonajes[rand.Next(0,ListadoPersonajes.Count())];
}


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

void MostrarPersonaje(Personaje personaje){
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

void CalcularAtaque(Personaje personaje){
    Random rand = new Random();

    int PoderDisparo = personaje.Destreza * personaje.Fuerza * personaje.Nivel; 
    int EfectividadDisparo = rand.Next(1, 101);
    personaje.ValorAtaque = PoderDisparo * EfectividadDisparo / 100;
}

void CalcularDefensa(Personaje personaje){
    Random rand = new Random();

    personaje.PoderDefensa = personaje.Armadura * personaje.Velocidad; 
}

void ResultadoEnfrentamiento(Personaje atacante, Personaje defensor){
    int DanioProvocado;
    int MaximoDanioProvocable = 500;

    System.Console.WriteLine("Valor ataque: " + atacante.ValorAtaque);
    System.Console.WriteLine("Poder Defensa: " + defensor.PoderDefensa);
    System.Console.WriteLine("Maximo daño provocable: " + MaximoDanioProvocable);

    if (atacante.ValorAtaque > defensor.PoderDefensa)
    {
        DanioProvocado = atacante.ValorAtaque - defensor.PoderDefensa;
        DanioProvocado *= 100;
        DanioProvocado /= MaximoDanioProvocable;
    } else
    {
        System.Console.WriteLine("¡¡¡EL VALOR DE LA DEFENSA ES MAYOR AL VALOR DEL ATAQUE!!!");

        DanioProvocado = 0;
    }

    System.Console.WriteLine("Daño Provocado: " + DanioProvocado);

    defensor.Salud -= DanioProvocado;
    System.Console.WriteLine("Salud Defensor: " + defensor.Salud);
}