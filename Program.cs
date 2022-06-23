using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;

using rpg_2022_exequiel1984;

Random rand = new Random();

var ListadoPersonajes = new List<Personaje>();
var Ganadores = new List<Personaje>();

Personaje Peleador1 = new Personaje();
Personaje Peleador2 = new Personaje();

int CantidadPersonajes;

CantidadPersonajes = rand.Next(1,4)*2;

System.Console.WriteLine("\nCantidad de peleadores: " + CantidadPersonajes);

CargarListadoPersonajes(ListadoPersonajes, CantidadPersonajes);

MostrarLista(ListadoPersonajes);

System.Console.WriteLine("Ingrese el nombre del Peleador 1: ");
string NombrePeleador = Console.ReadLine();

foreach (var personaje in ListadoPersonajes)
{
    if (personaje.Nombre == NombrePeleador)
    {
        Peleador1 = personaje;
    }
}

System.Console.WriteLine("Ingrese el nombre del Peleador 2: ");
NombrePeleador = Console.ReadLine();

foreach (var personaje in ListadoPersonajes)
{
    if (personaje.Nombre == NombrePeleador)
    {
        Peleador2 = personaje;
    }
}


do
{
    CalcularAtaque(Peleador1);
    CalcularDefensa(Peleador2);
    ResultadoEnfrentamiento(Peleador1, Peleador2);
} while (Peleador2.Salud > 0);


if (Peleador2.Salud == 0 || Peleador2.Salud < 0)
{
    Ganadores.Add(Peleador1);
    ListadoPersonajes = ListadoPersonajes.Except(Ganadores).ToList();
}


System.Console.WriteLine("\n-----LISTADO GANADORES-----");
MostrarLista(Ganadores);

System.Console.WriteLine("\n-----LISTADO PERSONAJES SIN GANADORES-----");
MostrarLista(ListadoPersonajes);





/* Peleador1 = SeleccionarPersonaje(ListadoPersonajes);
MostrarPersonaje(Peleador1);
Peleador2 = SeleccionarPersonaje(ListadoPersonajes);
if (Peleador1.Nombre == Peleador2.Nombre)
{
    Peleador2 = SeleccionarPersonaje(ListadoPersonajes); 
} 

Personaje SeleccionarPersonaje(List<Personaje> ListaPersonajes){

    return ListaPersonajes[rand.Next(0,ListadoPersonajes.Count())];
}*/


Personaje CrearPersonaje(){
    Personaje NuevoPersonaje = new Personaje();
    CargarDatos(NuevoPersonaje);
    CargarCaracteristicas(NuevoPersonaje);
    return NuevoPersonaje;
}

void CargarDatos(Personaje personaje)
{            
        Random rand = new Random();

        int IndexTipo = rand.Next(0, Enum.GetValues(typeof(tipos)).Length);
        personaje.Tipo = Enum.GetName(typeof(tipos), IndexTipo);
        GetNombre(personaje);
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

void CargarListadoPersonajes(List<Personaje> ListadoPersonajes, int CantidadPersonajes)
{
    for (int i = 0; i < CantidadPersonajes; i++)
    {
        Personaje PersonajeACargar = CrearPersonaje();

        ListadoPersonajes.Add(PersonajeACargar);
    }
}

void MostrarLista(List<Personaje> Lista)
{
    foreach (var personaje in Lista)
    {
        MostrarPersonaje(personaje);
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
    //System.Console.WriteLine("Maximo daño provocable: " + MaximoDanioProvocable);

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
    System.Console.WriteLine("Salud Defensor: " + defensor.Salud + "\n");
}

void GetNombre(Personaje personaje)
{
    var url = $"https://random-names-api.herokuapp.com/random";
    var request = (HttpWebRequest)WebRequest.Create(url);
    request.Method = "GET";
    request.ContentType = "application/json";
    request.Accept = "application/json";
    try
    {
        using (WebResponse response = request.GetResponse())
        {
            using (Stream strReader = response.GetResponseStream())
            {
                if (strReader == null) return;
                using (StreamReader objReader = new StreamReader(strReader))
                {
                    string responseBody = objReader.ReadToEnd();

                    NombreYGeneroAleatorio NombreYGenero = JsonSerializer.Deserialize<NombreYGeneroAleatorio>(responseBody);

                    personaje.Nombre = NombreYGenero.Body.Name;

                    //System.Console.WriteLine(personaje.Nombre   );

                }
            }
        }
    }
    catch (WebException ex)
    {
        Console.WriteLine("Problemas de acceso a la API");
    }
}

