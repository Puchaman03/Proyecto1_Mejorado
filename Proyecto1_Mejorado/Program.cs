using System.ComponentModel.Design;
using System.Diagnostics;
using System.Threading.Channels; // no se que es esto, simplemente aparecio 

// Declaracion de las variables
bool NoError = false; // el bool es paraa detectar si el promedio da error o no
Console.ForegroundColor = ConsoleColor.DarkBlue;
bool Repeticion = false;
string Finalizador = "";
int CantidadEstudiantes = 0;
string Identificacion = "";// variable para la identificacion de la cedula
bool encontrado = false;// variable para identificar que la cedula si fue entrada true, o si no false
int op = 0;// variable para el operador del menu; no se asusten puse 3 para cuando este haciendo pruebas no tenga que poner 10 estudiantes, luego se cambia
string[] Cedulas = new string[10];//arreglo de las cedulas
string[] Nombres = new string[10]; ;//arreglo de los nombres 
float[] promedio = new float[10]; ;//arreglo de los promedios
string[] Condicion = new string[10]; ;//arreglo de las condiciones de los estudiantes


while (op <= 6) // con este while hare funcionar el menu, hasta que el usuario ponga una numero = o > que 7;
{
    op = Menu(op);// llamo ala funcion Menu, y declaro que el valor de op dentro del while sera decido por la funcion
    switch (op)
    {
        case 1:// Inicializacion de vectores

            Console.WriteLine();
            for (int i = 0; i < Nombres.Length; i++)
            {
                Nombres[i] = "";

                Cedulas[i] = "";

                promedio[i] = 0;

                Condicion[i] = "";
            }
            Console.WriteLine("Vectores iniciados Exitosamente");
            break;

        case 2://incluir estudiantes
            do
            {
                Incluir_Estudiantes(ref CantidadEstudiantes, Cedulas, Nombres, promedio, Condicion, NoError);

                do
                {
                    Console.WriteLine("¿Desea continuar agregando más estudiantes? (s/n): ");
                    Finalizador = Console.ReadLine().ToLower();

                    if (Finalizador != "s" && Finalizador != "n")
                    {
                        Console.WriteLine("Opción incorrecta. Por favor, seleccione 's' para sí o 'n' para no.");
                    }
                } while (Finalizador != "s" && Finalizador != "n");


                if (Finalizador.ToLower() == "s")
                {
                    Repeticion = true;

                }
                else
                {
                    Repeticion = false;
                }

            } while (Repeticion);


            break;



        case 3: // Consultar Estudiantes
            Console.WriteLine("Digite la cedula del estudiante que desea consultar: ");
            string cedulaConsulta = Console.ReadLine();

            bool estudianteEncontrado = false;

            for (int i = 0; i < CantidadEstudiantes; i++)
            {
                if (cedulaConsulta.Equals(Cedulas[i]))
                {
                    estudianteEncontrado = true;
                    Console.WriteLine($"Cedula: {Cedulas[i]}");
                    Console.WriteLine($"Nombre: {Nombres[i]}");
                    Console.WriteLine($"Promedio: {promedio[i]}");
                    Console.WriteLine($"Condicion: {Condicion[i]}");
                    Console.WriteLine();
                    break;
                }
            }

            if (!estudianteEncontrado)
            {
                Console.WriteLine($"La Cedula {cedulaConsulta} no está registrada.");
            }
            break;


        case 4:// Modificar estudiantes 
            Console.ForegroundColor = ConsoleColor.DarkRed;//Estetico
            Console.WriteLine(" Digite la cedula del estudiante que quiere modificar: ");
            Identificacion = Console.ReadLine();
            for (int i = 0; i < CantidadEstudiantes; i++) // dentro del bucle hago que la variable identificacion repase el arreglo de cedulas 
            {
                if (Identificacion.Equals(Cedulas[i]))// aqui hago que si la variable identificacion dectacta que si hay una copia dentro del arreglo de cedulas haga lo siguiente
                {
                    encontrado = true; // esto es para decir que si esta la cedula 
                    Console.WriteLine($""""
                    Cedula {Cedulas[i]} Encontrada
                    """");
                    Console.WriteLine("Ponga el nombre del estudiante: ");
                    Nombres[i] = Console.ReadLine();
                    Console.WriteLine("Ponga el Promedio del estudiante: ");
                    NoError = false;
                    while (true)
                    {
                        try
                        {
                            promedio[i] = float.Parse(Console.ReadLine());

                            if (promedio[i] >= 0 && promedio[i] <= 100)
                            {
                                NoError = true;
                                break; // Salir del bucle si el promedio es válido
                            }
                            else
                            {
                                Console.WriteLine("El promedio debe estar entre 0 y 100. Digite el promedio del estudiante.");
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Error, digite nuevamente el promedio del estudiante.");
                        }
                    }

                    // Actualizar la condición según el nuevo promedio
                    Condicion[i] = (promedio[i] >= 70) ? "Aprobado" : "Reprobado";

                    Console.WriteLine();
                    break; // Salir del bucle una vez que se haya encontrado la cédula
                }
            }

            if (!encontrado)
            {
                Console.WriteLine($"La Cedula {Identificacion} no está dentro de las Cedulas Estudiantiles Registradas");
            }
            break;


        case 5:// Eliminar Estudiantes
            Console.ForegroundColor = ConsoleColor.White;//Estetico
            Console.WriteLine(" Digite la cedula del estudiante que quiere Eliminar: ");
            Identificacion = Console.ReadLine();
            for (int i = 0; i < Cedulas.Length; i++) // dentro del bucle hago que la variable identificacion repase el arreglo de cedulas 
            {
                if (Identificacion.Equals(Cedulas[i]))// aqui hago que si la variable identificacion dectacta que si hay una copia dentro del arreglo de cedulas haga lo siguiente
                {
                    encontrado = true; // esto es para decir que si esta la cedula 
                    Console.WriteLine($""""
                    Cedula {Cedulas[i]} Encontrada
                    """");
                    Cedulas[i] = "";
                    Nombres[i] = "";
                    promedio[i] = 0;
                    Condicion[i] = "";
                    Console.WriteLine("Datos Eliminados Exitosamente");
                    Console.WriteLine();
                    break; // Salir del bucle una vez que se haya encontrado la cédula
                }
            }
            if (encontrado == false)// aqui se usa el booleano para decir de forma consisa despuesde que la variable identificacion recorriese todo el arreglo de que esta la cedula que se quiere modificar
            {
                Console.WriteLine($" La Cedula {Identificacion} no esta dentro de la Cedulas Estudiantiles Registradas");

            }
            break;

        case 6: // submenu Reportes
            int opcionReporte = 0;
            while (opcionReporte != 3)
            {
                Console.WriteLine(@"
Submenú Reportes

1. Reporte Estudiantes por Condición
2. Reporte Todos los Datos
3. Regresar al Menú Principal
");

                try
                {
                    Console.Write("Por favor, seleccione una opción: ");
                    opcionReporte = int.Parse(Console.ReadLine());
                    if (opcionReporte <= 0 || opcionReporte > 3)
                    //Se almacena la la opcion del submenu y se valida que sea mayor a 0 y menor que 4
                    {
                        Console.WriteLine("\nSeleccione una opción válida.");
                        opcionReporte = 0;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\n(numero entero)");
                    opcionReporte = 0;
                }

                switch (opcionReporte)
                {
                    case 1: // Reporte estudiantes por condicion
                        Console.WriteLine("\nReporte Estudiantes por Condición:");
                        Console.WriteLine(" ");
                        Console.WriteLine("==================================================");
                        Console.WriteLine("* 1. Aprobado, 2. Reprobado, 3. Aplazado *");
                        Console.WriteLine("==================================================");
                        Console.WriteLine(" ");
                        Console.Write("Ingrese el número correspondiente a la condición de los estudiantes a reportar: "); //menu para elegir tipo de estudiante
                        int opcionCondicion = int.Parse(Console.ReadLine());
                        string condicion;
                        // se hace una variable de tipo string llamada condicion que se utiliza para
                        // almacenar la condicion seleccionada por el usuario en forma de texto
                        switch (opcionCondicion)
                        {
                            case 1:
                                condicion = "Aprobado";
                                break;
                            case 2:
                                condicion = "Reprobado";
                                break;
                            case 3:
                                condicion = "Aplazado";
                                break;
                            default:
                                Console.WriteLine("\nOpción incorrecta. Seleccionando por defecto Aprobado");
                                condicion = "Aprobado"; // en caso de seleccionar algo invalido automaticamente se elije la opcion 1 osea aprobado
                                break;
                        }
                        Console.WriteLine("\nCedula\tNombre\tPromedio\tCondición");
                        Console.WriteLine("================================================");
                        foreach (var estudianteIndex in Enumerable.Range(0, Cedulas.Length))
                        // Este bucle foreach recorre una secuencia de numeros del 0 al tamaño del arreglo Cedulas.Length
                        // estos numeros representan los indices de los estudiantes en los arreglos 
                        {
                            if (!string.IsNullOrEmpty(Cedulas[estudianteIndex]) && Condicion[estudianteIndex] == condicion)
                            // imprimir solo estudiantes registrados y con la condición que se le indica
                            {
                                Console.WriteLine($"{Cedulas[estudianteIndex]}\t{Nombres[estudianteIndex]}\t{promedio[estudianteIndex]}\t\t{Condicion[estudianteIndex]}");
                            }//si lo anterior se cumple aqui se imprime la información del estudiante en la consola
                        }
                        Console.WriteLine("================================================");
                        break;

                    case 2: // reporte Todos los Datos
                        Console.WriteLine("\nReporte Todos los Datos:");
                        Console.WriteLine("Cedula\tNombre\tPromedio\tCondición");
                        Console.WriteLine("===========================================================================");
                        foreach (var estudianteIndex in Enumerable.Range(0, Cedulas.Length))
                        //este bucle se repite la cantidad que tenga la longitud el arreglo
                        {
                            if (!string.IsNullOrEmpty(Cedulas[estudianteIndex])) // aqui se verifica que el espacio no este vacio 
                            {
                                Console.WriteLine($"{Cedulas[estudianteIndex]}\t{Nombres[estudianteIndex]}\t{promedio[estudianteIndex]}\t\t{Condicion[estudianteIndex]}");
                            }//se imprimen los estudiantes registrados
                        }
                        Console.WriteLine("===========================================================================");
                        // estadísticas
                        int cantidadEstudiantes = 0;
                        float promedioMayor = int.MinValue;
                        float promedioMenor = int.MaxValue;
                        string estudiantePromedioMayor = "";
                        string estudiantePromedioMenor = "";
                        foreach (var estudianteIndex in Enumerable.Range(0, Cedulas.Length))
                        {
                            if (!string.IsNullOrEmpty(Cedulas[estudianteIndex]))
                            {
                                //este procediminto recorre todo el arreglo hasta filtrar y encontrar el promedio mayor y menor
                                cantidadEstudiantes++;
                                if (promedio[estudianteIndex] > promedioMayor)
                                {
                                    promedioMayor = promedio[estudianteIndex];
                                    estudiantePromedioMayor = Nombres[estudianteIndex];
                                }
                                if (promedio[estudianteIndex] < promedioMenor)
                                {
                                    promedioMenor = promedio[estudianteIndex];
                                    estudiantePromedioMenor = Nombres[estudianteIndex];
                                }
                            }
                        }
                        Console.WriteLine(" ");
                        Console.WriteLine(" ");
                        Console.WriteLine("============================================================");
                        Console.WriteLine($"\nCantidad de Estudiantes: {cantidadEstudiantes}"); // se imprimen las estadisticas amteriormente calculadas
                        Console.WriteLine(" ");
                        Console.WriteLine($"Estudiantes con promedio mayor:  ({estudiantePromedioMayor})  (Promedio: {promedioMayor})");
                        Console.WriteLine($"Estudiantes con promedio menor:  ({estudiantePromedioMenor})  (Promedio: {promedioMenor})\n");
                        Console.WriteLine("============================================================");
                        break;
                    case 3: // regresar al menu Principal
                        Console.WriteLine("\nRegresando al Menú Principal...");
                        break;
                    default:
                        Console.WriteLine("\nOpcion invalida por favor, seleccione una opcion valida");
                        break;
                }
            }
            break;




    }




}

Console.WriteLine(@" 
Gracias por usar nuestro servicio");// un detalle final nada mas 

static int Menu(int op) // Esta una funcion, que crea el menu, se que la compañera se encargaba de esta parte pero es que ocupa un menu para hacer mi parte 
{
    Console.ForegroundColor = ConsoleColor.DarkYellow;// esto es para darle color nada mas 

    Console.WriteLine("""
        *Bienvenido al Sistema de Registro Estudiantil de la Universidad Hispanoamerica*

     -1 Inicializar Vectores

     -2 Inclir Estudiantes

     -3 Consultar Estudiantes

     -4 Modificar Estudiantes

     -5 Eliminar Estudiantes

     -6 Submenú Reportes

     -7 Salir
     """);

    try//pongo try para que si el usuario pone algo que no sea un numero, o un numero negativo para evitando dar error 
    {
        op = int.Parse(Console.ReadLine()); // Aqui pido el operador

        if (op <= 0)// por si el usuario pone numeros negativos
        {
            Console.WriteLine(@"
Porfavor no poner numeros negativos o ivalidos
");
            op = 0;

        }
        if (op > 7)
        {
            Console.WriteLine(" Si desea salir seleccione la opcion 7 de salir ");
            op = 0;
        }
    }
    catch (FormatException)// por si el usurio pone algo que no sea numeros. el formatExceotion captura elementos que no sea del mismo tipo, por ejemplo op es init pero si se pone una letra cuando se solicita seria string por lo que el catch los dectecta 
    {
        Console.WriteLine("Digite una opcion válida.");
        Console.WriteLine();
        op = 0; // Reiniciar op si se produce un error de formato
    }

    return op;
}
static void Incluir_Estudiantes(ref int CantidadEstudiantes, string[] Cedulas, string[] Nombres, float[] promedio, string[] Condicion, bool NoError)
{

    Console.WriteLine($"Ponga la cedula del estudiante {CantidadEstudiantes + 1}: ");
    Cedulas[CantidadEstudiantes] = Console.ReadLine();
    Console.WriteLine("Ponga el nombre del estudiante: ");
    Nombres[CantidadEstudiantes] = Console.ReadLine();
    Console.WriteLine("Ponga el Promedio del estudiante: ");
    do
    {
        try
        {
            while (NoError == false)
            {
                promedio[CantidadEstudiantes] = float.Parse(Console.ReadLine());

                while (promedio[CantidadEstudiantes] > 100 || promedio[CantidadEstudiantes] < 0)
                {
                    Console.WriteLine("El promedio no puede ser menor a 0 o mayor a 100, Digite el promedio del estudiante");
                    promedio[CantidadEstudiantes] = float.Parse(Console.ReadLine());

                }
                NoError = true;

            }


        }
        catch (FormatException)
        {
            Console.WriteLine(" Error, digite nuevamente el promedio del estudiante");
        }
    } while (!NoError);// mientras el bool NoError sea falso va seguier repitiendo la estrutura 

    if (promedio[CantidadEstudiantes] >= 70)
    {
        Condicion[CantidadEstudiantes] = "Aprobado";
    }
    else
    {
        Condicion[CantidadEstudiantes] = "Reprobado";
    }
    Console.WriteLine();


    CantidadEstudiantes++;

}