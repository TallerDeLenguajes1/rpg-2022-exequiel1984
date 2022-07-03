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

int EleccionMenu = 0;

System.Console.WriteLine("\n*****BIENVENIDO AL RPG*****\n");
System.Console.WriteLine("*****MENU*****\n");
System.Console.WriteLine("ELIJA LA OPCION:");
System.Console.WriteLine("1 - PARA CREAR PERSONAJES ALEATORIAMENTE");
System.Console.WriteLine("2 - CREAR PERSONAJES MANUALMENTE");
System.Console.WriteLine("3 - UTILIZAR PERSONAJES PREDEFINIDOS");
System.Console.WriteLine("4 - VER LISTA DE GANADORES");
EleccionMenu = Convert.ToInt32(Console.ReadLine());

switch (EleccionMenu)
{
    case 1:
        int CantidadPersonajes;

        CantidadPersonajes = rand.Next(1, 4) * 2;

        System.Console.WriteLine("\nCantidad de peleadores: " + CantidadPersonajes);

        CargarListadoPersonajesAleatorio(ListadoPersonajes, CantidadPersonajes);
        break;
    case 2:
        System.Console.WriteLine("\nCREACION DE PERSONAJES MANUAL\n");

        string ConsultaCrearPersonaje;

        do
        {
            ListadoPersonajes.Add(CrearPersonajeManual());
            System.Console.WriteLine("Desea crear un nuevo personaje (s/n)?");
            ConsultaCrearPersonaje = Console.ReadLine();
        } while (ConsultaCrearPersonaje != "n");

        CrearListaPersonajesJson(ListadoPersonajes, "Lista_Personajes_manuales.json");
        break;

    case 3:
        System.Console.WriteLine("\nUSAR LISTA DE PERSONAJES DE ARCHIVO JSON");
        ListadoPersonajes = DeserealizarArchivoJson("Lista_Personajes_manuales.json");
        break;
}

MostrarLista(ListadoPersonajes);


System.Console.WriteLine("*****ENFRENTAMIENTO*****\n");

string NombrePeleador = "";

System.Console.WriteLine("Ingrese el nombre del peleador 1: ");
NombrePeleador = Console.ReadLine();

foreach (var personaje in ListadoPersonajes)
{
    if (personaje.Nombre == NombrePeleador)
    {
        Peleador1 = personaje;
    }
}


System.Console.WriteLine("Ingrese el nombre del peleador 2: ");
NombrePeleador = Console.ReadLine();

foreach (var personaje in ListadoPersonajes)
{
    if (personaje.Nombre == NombrePeleador)
    {
        Peleador2 = personaje;
    }
}
System.Console.WriteLine(Peleador1.Nombre + " VS " + Peleador2.Nombre);

System.Console.WriteLine("\nPresione Enter para continuar\n");

Console.ReadKey();

for (int i = 0; i < 3; i++)
{
    System.Console.WriteLine("*****ROUND " + (i+1) + "*****");
    Round(Peleador1, Peleador2);
    Round(Peleador2,Peleador1);
}



void Round(Personaje PeleadorAtacante, Personaje PeleadorDefensor)
{
    Console.ReadKey();
    CalcularAtaque(PeleadorAtacante);
    Console.ReadKey();
    CalcularDefensa(PeleadorDefensor);
    Console.ReadKey();
    ResultadoAtaqueVsDefensa(PeleadorAtacante, PeleadorDefensor);
}

System.Console.WriteLine("-----------------------------------------------------------------\n");
System.Console.WriteLine("La salud de " + Peleador1.Nombre + " quedo en " + Peleador1.Salud);
System.Console.WriteLine("\nLa salud de " + Peleador2.Nombre + " quedo en " + Peleador2.Salud);

if (Peleador1.Salud > Peleador2.Salud)
{
    System.Console.WriteLine("\nEL GANADOR ES " + Peleador1.Nombre.ToUpper() + "\n");
}

if (Peleador1.Salud < Peleador2.Salud)
{
    System.Console.WriteLine("\nEL GANADOR ES " + Peleador2.Nombre.ToUpper() + "\n");
}

if (Peleador1.Salud == Peleador2.Salud)
{
    System.Console.WriteLine("El resultado fue un empate");
}


/* do
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
MostrarLista(ListadoPersonajes); */





Personaje CrearPersonajeAleatorio()
{
    Personaje NuevoPersonaje = new Personaje();
    CargarDatosAleatorios(NuevoPersonaje);
    CargarCaracteristicasAleatorios(NuevoPersonaje);
    return NuevoPersonaje;
}

Personaje CrearPersonajeManual()
{
    Personaje NuevoPersonaje = new Personaje();
    CargarDatosManual(NuevoPersonaje);
    CargarCaracteristicasManual(NuevoPersonaje);
    return NuevoPersonaje;
}

string GetNombre()
{
    var url = $"https://random-names-api.herokuapp.com/random";
    var request = (HttpWebRequest)WebRequest.Create(url);
    request.Method = "GET";
    request.ContentType = "application/json";
    request.Accept = "application/json";

    NombreYGeneroAleatorio NombreYGenero;

    try
    {
        using (WebResponse response = request.GetResponse())
        {
            using (Stream strReader = response.GetResponseStream())
            {
                if (strReader == null) return "";
                using (StreamReader objReader = new StreamReader(strReader))
                {
                    string responseBody = objReader.ReadToEnd();

                    NombreYGenero = JsonSerializer.Deserialize<NombreYGeneroAleatorio>(responseBody);

                    return NombreYGenero.Body.Name;

                    //System.Console.WriteLine(personaje.Nombre   );

                }
            }
        }
    }
    catch (WebException ex)
    {
        Console.WriteLine("Problemas de acceso a la API");
    }

    return "";
}

void CargarDatosAleatorios(Personaje personaje)
{
    Random rand = new Random();

    int IndexTipo = rand.Next(0, Enum.GetValues(typeof(tipos)).Length);
    personaje.Tipo = Enum.GetName(typeof(tipos), IndexTipo);
    personaje.Nombre = GetNombre();
    int IndexApodo = rand.Next(0, Enum.GetValues(typeof(apodos)).Length);
    personaje.Apodo = Enum.GetName(typeof(apodos), IndexApodo);
    personaje.FechaNacimiento = new DateTime(rand.Next(1722, 2022), rand.Next(01, 13), rand.Next(01, 32));
    personaje.Edad = personaje.CalcularEdad();
    personaje.Salud = 100;
}

void CargarDatosManual(Personaje personaje)
{
    System.Console.WriteLine("ingrese el tipo: ");
    personaje.Tipo = Console.ReadLine();
    System.Console.WriteLine("ingrese el nombre: ");
    personaje.Nombre = Console.ReadLine();
    System.Console.WriteLine("ingrese el apodo: ");
    personaje.Apodo = Console.ReadLine();
    personaje.FechaNacimiento = new DateTime(rand.Next(1722, 2022), rand.Next(01, 13), rand.Next(01, 32));
    personaje.Edad = personaje.CalcularEdad();
    personaje.Salud = 100;
}

void CargarCaracteristicasAleatorios(Personaje personaje)
{
    Random rand = new Random();
    personaje.Velocidad = rand.Next(1, 11);
    personaje.Destreza = rand.Next(1, 6);
    personaje.Fuerza = rand.Next(1, 11);
    personaje.Nivel = rand.Next(1, 11);
    personaje.Armadura = rand.Next(1, 11);
}

void CargarCaracteristicasManual(Personaje personaje)
{
    System.Console.WriteLine("Ingrese la velocidad entre 1 y 10:");
    personaje.Velocidad = Convert.ToInt32(Console.ReadLine());
    System.Console.WriteLine("Ingrese la destreza entre 1 y 5:");
    personaje.Destreza = Convert.ToInt32(Console.ReadLine());
    System.Console.WriteLine("Ingrese la fuerza entre 1 y 10:");
    personaje.Fuerza = Convert.ToInt32(Console.ReadLine());
    System.Console.WriteLine("Ingrese el nivel entre 1 y 10:");
    personaje.Nivel = Convert.ToInt32(Console.ReadLine());
    System.Console.WriteLine("Ingrese la armadura entre 1 y 10:");
    personaje.Armadura = Convert.ToInt32(Console.ReadLine());
}

void CargarListadoPersonajesAleatorio(List<Personaje> ListadoPersonajes, int CantidadPersonajes)
{
    for (int i = 0; i < CantidadPersonajes; i++)
        ListadoPersonajes.Add(CrearPersonajeAleatorio());
}

void MostrarLista(List<Personaje> Lista)
{
    foreach (var personaje in Lista)
        MostrarPersonaje(personaje);
}

void MostrarPersonaje(Personaje personaje)
{
    System.Console.WriteLine("\nTipo: " + personaje.Tipo);
    System.Console.WriteLine("Nombre: " + personaje.Nombre);
    System.Console.WriteLine("Apodo: " + personaje.Apodo);
    System.Console.WriteLine("Fecha de Nacimiento: " + personaje.FechaNacimiento.ToShortDateString());
    System.Console.WriteLine("Edad: " + personaje.Edad);
    System.Console.WriteLine("Salud: " + personaje.Salud);
    System.Console.WriteLine("Velocidad: " + personaje.Velocidad);
    System.Console.WriteLine("Destreza: " + personaje.Destreza);
    System.Console.WriteLine("Fuerza: " + personaje.Fuerza);
    System.Console.WriteLine("Nivel: " + personaje.Nivel);
    System.Console.WriteLine("Armadura: " + personaje.Armadura + "\n");
}

void CalcularAtaque(Personaje personaje)
{
    Random rand = new Random();

    double PoderDisparo = personaje.Destreza * personaje.Fuerza * personaje.Nivel;
    double EfectividadDisparo = rand.Next(1, 101);
    EfectividadDisparo /= 100;
    personaje.ValorAtaque = PoderDisparo * EfectividadDisparo;
    System.Console.WriteLine("\nEl ataque de " + personaje.Nombre + " es " + personaje.ValorAtaque);
}

void CalcularDefensa(Personaje personaje)
{
    personaje.PoderDefensa = personaje.Armadura * personaje.Velocidad;
    System.Console.WriteLine("\nEl poder de defensa de " + personaje.Nombre + " es " + personaje.PoderDefensa);
}

void ResultadoAtaqueVsDefensa(Personaje atacante, Personaje defensor)
{
    double DanioProvocado;
    int MaximoDanioProvocable = 500;

    if (atacante.ValorAtaque > defensor.PoderDefensa)
    {
        DanioProvocado = atacante.ValorAtaque - defensor.PoderDefensa;
        DanioProvocado /= MaximoDanioProvocable;
        DanioProvocado *= 100;
    }
    else
    {
        System.Console.WriteLine("\n¡¡¡EL VALOR DE LA DEFENSA ES MAYOR AL VALOR DEL ATAQUE!!!");

        DanioProvocado = 0;
    }

    System.Console.WriteLine("\nEl daño Provocado a " + defensor.Nombre + " fue " + DanioProvocado);

    defensor.Salud -= DanioProvocado;
    Console.ReadKey();
    System.Console.WriteLine("\nLa salud de " + defensor.Nombre + " quedo en " + defensor.Salud + "\n");
}

static void CrearListaPersonajesJson(List<Personaje> Lista, string NombreNuevoArchivoJson)
{
    string ListaSerealizada = JsonSerializer.Serialize(Lista);

    using (var NuevoArchivoJson = new FileStream(NombreNuevoArchivoJson, FileMode.Create))
    {
        using (var strWriter = new StreamWriter(NuevoArchivoJson))
        {
            strWriter.WriteLine("{0}", ListaSerealizada);
            strWriter.Close();
        }
    }
}

static List<Personaje> DeserealizarArchivoJson(string NombreArchivoJson)
{
    string StringADeserealizar;
    using (var archivoOpen = new FileStream(NombreArchivoJson, FileMode.Open))
    {
        using (var strReader = new StreamReader(archivoOpen))
        {
            StringADeserealizar = strReader.ReadToEnd();
            archivoOpen.Close();
        }
    }

    var ListaDeserealizada = JsonSerializer.Deserialize<List<Personaje>>(StringADeserealizar);
    return ListaDeserealizada;
}

