using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;

using rpg_2022_exequiel1984;

var ListadoPersonajes = new List<Personaje>();

int EleccionMenu = 0;

System.Console.WriteLine("\n*****BIENVENIDO AL RPG*****\n");
System.Console.WriteLine("*****MENU*****\n");
System.Console.WriteLine("ELIJA LA OPCION:");
System.Console.WriteLine("1 - PARA CREAR PERSONAJES ALEATORIAMENTE");
System.Console.WriteLine("2 - CREAR PERSONAJES MANUALMENTE");
System.Console.WriteLine("3 - UTILIZAR PERSONAJES PREDEFINIDOS");
System.Console.WriteLine("4 - VER RANKING ANTERIOR");
System.Console.WriteLine("\nSu eleccion: ");
EleccionMenu = Convert.ToInt32(Console.ReadLine());

switch (EleccionMenu)
{
    case 1:
        int CantidadPersonajes;
        //CantidadPersonajes = rand.Next(1, 4);
        CantidadPersonajes = 3;
        System.Console.WriteLine("\nCantidad de peleadores: " + CantidadPersonajes);
        CargarListadoPersonajesAleatorio(ListadoPersonajes, CantidadPersonajes);
        Torneo(ListadoPersonajes);
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
        Torneo(ListadoPersonajes);

        break;

    case 3:
        System.Console.WriteLine("\nUSAR LISTA DE PERSONAJES DE ARCHIVO JSON");
        ListadoPersonajes = DeserealizarArchivoJson("Lista_Personajes_manuales.json");
        Torneo(ListadoPersonajes);
        break;

    case 4:
        VerRanking();
        break;
}





List<string[]> LeerCSV(string nombreDeArchivo, char caracter)
{
    FileStream MiArchivo = new FileStream(nombreDeArchivo, FileMode.Open);
    StreamReader StrReader = new StreamReader(MiArchivo);

    string Linea = "";
    List<string[]> LecturaDelArchivo = new List<string[]>();

    while ((Linea = StrReader.ReadLine()) != null)
    {
        string[] Fila = Linea.Split(caracter);
        LecturaDelArchivo.Add(Fila);
    }

    return LecturaDelArchivo;
}

void CrearArchicoCSV(List<string> ListadoString)
{
    List<string> ListadoStringFormatoCSV = new List<string>();
    foreach (var Linea in ListadoString)
    {
        ListadoStringFormatoCSV.Add(Linea);
    }
    File.WriteAllLines("Ranking.csv", ListadoStringFormatoCSV);
}


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
    Random rand = new Random();

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

void VerRanking()
{
    List<string[]> LecturaDelArchivo = LeerCSV("Ranking.csv", ',');
    System.Console.WriteLine("\n       *****RANKING*****\n");
    System.Console.WriteLine("Posicion - PELEADOR - BATALLAS GANADAS");

    for (int i = LecturaDelArchivo.Count() - 1; i >= 0; i--)
    {
        Console.WriteLine(LecturaDelArchivo[i][0] + "        - " + LecturaDelArchivo[i][1] + " -            " + LecturaDelArchivo[i][2]);
    }
}


void CalcularAtaque(Personaje Atacante, Personaje Defensor)
{
    Random rand = new Random();

    Console.ReadKey();
    System.Console.WriteLine("\nATACA " + Atacante.Nombre);

    double PoderDisparo = Atacante.Destreza * Atacante.Fuerza * Atacante.Nivel;
    double EfectividadDisparo = rand.Next(1, 101);
    EfectividadDisparo /= 100;
    double ValorAtaque = PoderDisparo * EfectividadDisparo;
    Console.ReadKey();
    System.Console.WriteLine("\nEl ataque de " + Atacante.Nombre + " es " + ValorAtaque);

    double PoderDefensa = Defensor.Armadura * Defensor.Velocidad;
    Console.ReadKey();
    System.Console.WriteLine("\nEl poder de defensa de " + Defensor.Nombre + " es " + PoderDefensa);

    double DanioProvocado;
    int MaximoDanioProvocable = 500;

    if (ValorAtaque > PoderDefensa)
    {
        DanioProvocado = ValorAtaque - PoderDefensa;
        DanioProvocado /= MaximoDanioProvocable;
        DanioProvocado *= 100;
    }
    else
    {
        Console.ReadKey();
        System.Console.WriteLine("\n¡¡¡EL VALOR DE LA DEFENSA ES MAYOR AL VALOR DEL ATAQUE!!!");

        DanioProvocado = 0;
    }

    Console.ReadKey();
    System.Console.WriteLine("\nEl daño Provocado a " + Defensor.Nombre + " fue " + DanioProvocado);

    Defensor.Salud -= DanioProvocado;

    if (Defensor.Salud <= 0)
    {
        Defensor.Salud = 0;
    }

    Console.ReadKey();
    System.Console.WriteLine("\nLa salud de " + Defensor.Nombre + " quedo en " + Defensor.Salud + "\n");
}


void Torneo(List<Personaje> ListadoPersonajes)
{
    Random rand = new Random();

    var Eliminados = new List<Personaje>();
    var ListadoStringRanking = new List<string>();

    Personaje Peleador1 = new Personaje();
    Personaje Peleador2 = new Personaje();

    MostrarLista(ListadoPersonajes);

    do
    {
        System.Console.WriteLine("\n*****ENFRENTAMIENTO*****\n");

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
        System.Console.WriteLine("\n" + Peleador1.Nombre.ToUpper() + " VS " + Peleador2.Nombre.ToUpper());

        System.Console.WriteLine("\nPresione Enter para continuar\n");


        for (int i = 0; i < 3; i++)
        {
            Console.ReadKey();
            System.Console.WriteLine("*****ROUND " + (i + 1) + "*****");
            CalcularAtaque(Peleador1, Peleador2);
            CalcularAtaque(Peleador2, Peleador1);

            if (Peleador1.Salud <= 0 || Peleador2.Salud <= 0)
            {
                i = 3;
            }
        }




        Console.ReadKey();
        System.Console.WriteLine("-----------------------------------------------------------------\n");
        System.Console.WriteLine("\n*****RESULTADO DE LA BATALLA*****");
        System.Console.WriteLine("\nLa salud de " + Peleador1.Nombre + " quedo en " + Peleador1.Salud);
        Console.ReadKey();
        System.Console.WriteLine("\nLa salud de " + Peleador2.Nombre + " quedo en " + Peleador2.Salud);

        if (Peleador1.Salud > Peleador2.Salud)
        {
            Console.ReadKey();
            System.Console.WriteLine("\nEL GANADOR ES " + Peleador1.Nombre.ToUpper() + "\n");
            Peleador1.Salud = 100;
            Peleador1.BatallasGanadas++;
            ListadoStringRanking.Add(ListadoPersonajes.Count() + "," + Peleador2.Nombre + "," + Peleador2.BatallasGanadas);
            Console.ReadKey();
            System.Console.WriteLine(Peleador2.Nombre + " quedo en la posicion " + ListadoPersonajes.Count());
            Eliminados.Add(Peleador2);
            ListadoPersonajes = ListadoPersonajes.Except(Eliminados).ToList();
        }

        if (Peleador1.Salud < Peleador2.Salud)
        {
            Console.ReadKey();
            System.Console.WriteLine("\nEL GANADOR ES " + Peleador2.Nombre.ToUpper() + "\n");
            Peleador2.Salud = 100;
            Peleador2.BatallasGanadas++;
            ListadoStringRanking.Add(ListadoPersonajes.Count() + "," + Peleador1.Nombre + "," + Peleador1.BatallasGanadas);
            Console.ReadKey();
            System.Console.WriteLine(Peleador1.Nombre + " quedo en la posicion " + ListadoPersonajes.Count());
            Eliminados.Add(Peleador1);
            ListadoPersonajes = ListadoPersonajes.Except(Eliminados).ToList();
        }

        if (Peleador1.Salud == Peleador2.Salud)
        {
            Console.ReadKey();
            System.Console.WriteLine("El resultado fue un empate");
            Peleador1.Salud = 100;
            Peleador2.Salud = 100;
        }

        Console.ReadKey();
        System.Console.WriteLine("\nLos peleadores que siguen en competencia son:");

        foreach (var personaje in ListadoPersonajes)
        {
            System.Console.WriteLine(personaje.Nombre);
        }

    } while (ListadoPersonajes.Count() > 1);


    //Para agregar el ultimo personaje al ranking
    foreach (var personaje in ListadoPersonajes)
    {
        ListadoStringRanking.Add(ListadoPersonajes.Count() + "," + personaje.Nombre + "," + personaje.BatallasGanadas);
        Console.ReadKey();
        System.Console.WriteLine("\n" + personaje.Nombre + " quedo en la posicion " + ListadoPersonajes.Count());
        Eliminados.Add(personaje);
        ListadoPersonajes = ListadoPersonajes.Except(Eliminados).ToList();
    }

    CrearArchicoCSV(ListadoStringRanking);

    VerRanking();
}