using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArchivosBinarios
{
    class ArchivosBinariosEmpleados
    {
        //Declaracion de Variables
        BinaryWriter bw = null;//Flujo de salida = escritura de datos
        BinaryReader br = null;//Flujo de entrada = lectura de datos

        //Campos de la clase
        string Nombre, Direccion;
        long Telefono;
        int NumEmp, DiasTrabajados;
        float SalarioDiario;

        public void CrearArchivo(string Archivo)
        {
            //Variable local Metodo
            char resp;

            try
            {
                //Creacion del flujo para escribir datos al archivo
                bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));

                //Captura de datos
                do
                {
                    Console.Clear();
                    Console.Write("Numero de Empleado: ");
                    NumEmp = Int32.Parse(Console.ReadLine());
                    Console.Write("Nombre del Empleado: ");
                    Nombre = Console.ReadLine();
                    Console.Write("Direccion del empleado: ");
                    Direccion = Console.ReadLine();
                    Console.Write("telefono del Empleado: ");
                    Telefono = Int64.Parse(Console.ReadLine());
                    Console.Write("Dias Trabajados del Empleado: ");
                    DiasTrabajados = Int32.Parse(Console.ReadLine());
                    Console.Write("Salario del Empleado: ");
                    SalarioDiario = Single.Parse(Console.ReadLine());

                    //Escribe los datos del Archivo
                    bw.Write(NumEmp);
                    bw.Write(Nombre);
                    bw.Write(Direccion);
                    bw.Write(Telefono);
                    bw.Write(DiasTrabajados);
                    bw.Write(SalarioDiario);
                    Console.Write("\n\nDesas Almacenar otro Refistro (s/n)? ");
                    resp = char.Parse(Console.ReadLine());
                } while ((resp == 's') || (resp == 'S'));

            }
            catch (IOException e)
            {
                Console.WriteLine("\nError : " + e.Message);
                Console.WriteLine("\nRuta :" + e.StackTrace);
            }
            finally
            {
                if (bw != null) bw.Close(); //Cierre el flujo - escritura
                Console.Write("\nPresione <Enter> para termianr la Escritura de datos y Regresar al Menu");
                Console.ReadKey();
            }
        }
        public void MostrarArchivo(string Archivo)
        {
            try
            {
                //Verifica si existe el Archivo
                if (File.Exists(Archivo))
                {
                    //Creacion flujo para leer datos del archivo
                    br = new BinaryReader(new FileStream(Archivo, FileMode.Open, FileAccess.Read));

                    //Despliegue de datos en pantalla
                    Console.Clear();
                    do
                    {
                        //Lectura de Registro mientras no llegue a End0File
                        NumEmp = br.ReadInt32();
                        Nombre = br.ReadString();
                        Direccion = br.ReadString();
                        Telefono = br.ReadInt64();
                        DiasTrabajados = br.ReadInt32();
                        SalarioDiario = br.ReadSingle();

                        //Muestra los datos
                        Console.WriteLine("Numero del Empleado : " + NumEmp);
                        Console.WriteLine("Nombre del Empleado : " + Nombre);
                        Console.WriteLine("Direccion del Empleado : " + Direccion);
                        Console.WriteLine("Telefono del Empleado : " + Telefono);
                        Console.WriteLine("Dias Trabajados del Empleado : " + DiasTrabajados);
                        Console.WriteLine("Salario Diario del Empleado :{0:C} ", SalarioDiario);
                        Console.WriteLine("SUELDO TOTAL del Empleado :{0:C} ", (DiasTrabajados * SalarioDiario));
                        Console.WriteLine("\n");
                    } while (true);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\nEl Archivo " + Archivo + " No Existe en el Disco!!");
                    Console.Write("\nPresione <Enter> para Continuar...");
                    Console.ReadKey();
                }
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine("\n\nFin del Listado de Empleados");
                Console.Write("\nPresione <Enter> para Continuar...");
                Console.ReadKey();
            }
            finally
            {
                if (br != null) br.Close();//Cierre del flujo
                Console.Write("\nPresione <Enetr> Para Terminar la Lectura de Datos y Regresar al Menu.");
                Console.ReadKey();
            }
        }
        
    }
   

    class Program
    {
        static void Main(string[] args)
        {
            //Declaracion de Variables
            string Arch = null;
            int opcion;

            //Creacion del Objeto
            ArchivosBinariosEmpleados A1 = new ArchivosBinariosEmpleados();

            //Menu de Opciones
            do
            {
                Console.Clear();
                Console.WriteLine("\n*** ARCHIVO BINARIO EMPLEADOS***");
                Console.WriteLine("1.- Creación de un Archivo.");
                Console.WriteLine("2.- Lectura de un Archivo.");
                Console.WriteLine("3.- Salida del Programa.");
                Console.Write("\nQue opción deseas: ");
                opcion = Int16.Parse(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo a Crear: "); Arch = Console.ReadLine();
                            //verifica si esxiste el archivo
                            char resp = 's';
                            if (File.Exists(Arch))
                            {
                                Console.Write("\nEl Archivo Existe!!, Deseas Sobreescribirlo(s / n) ? ");
                                resp = Char.Parse(Console.ReadLine());
                            }

                            if ((resp == 's') || (resp == 'S'))
                                A1.CrearArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;
                    case 2:
                        //bloque de lectura
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo que deseas Leer: "); Arch = Console.ReadLine();
                            A1.MostrarArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;
                    case 3:
                        Console.Write("\nPresione <enter> para Salir del Programa.");
                        Console.ReadKey();
                        break;
                    default:
                        Console.Write("\nEsa Opción No Existe!!, Presione < enter > para Continuar...");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != 3);
        }
    }
}
