using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        MesaPartes mesa = new MesaPartes();
        bool salir = false;

        while (!salir)
        {
            MostrarMenu();
            string opcion = Console.ReadLine() ?? "";

            if (opcion == "1") RegistrarSolicitud(mesa);
            else if (opcion == "2") ConsultarPorCodigo(mesa);
            else if (opcion == "3") ListarOrdenado(mesa, false);
            else if (opcion == "4") ListarOrdenado(mesa, true);
            else if (opcion == "5")
            {
                salir = true;
                Console.WriteLine("Hasta pronto.");
            }
            else Console.WriteLine("Opción inválida.");
        }
    }

    static void MostrarMenu()
    {
        Console.WriteLine("\n=== MESA DE PARTES DIGITAL - I.E. 3091 HUACA DE ORO ===");
        Console.WriteLine("1. Registrar solicitud");
        Console.WriteLine("2. Consultar por código");
        Console.WriteLine("3. Listar ordenado por folios (menor a mayor)");
        Console.WriteLine("4. Listar ordenado por folios (mayor a menor)");
        Console.WriteLine("5. Salir");
        Console.WriteLine("Elige una opción:");
    }

    static void RegistrarSolicitud(MesaPartes mesa)
    {
        Console.WriteLine("Remitente (nombres y apellidos):");
        string remitente = Console.ReadLine() ?? "";
        while (!Validador.ValidarTexto(remitente, 3))
        {
            Console.WriteLine("Dato inválido. Intenta de nuevo:");
            remitente = Console.ReadLine() ?? "";
        }

        Console.WriteLine("Tipos de trámite:");
        for (int i = 0; i < mesa.Tipos.Length; i++)
        {
            Console.WriteLine($"{i + 1} - {mesa.Tipos[i]}");
        }
        Console.WriteLine("Elige el tipo:");
        int tipo;
        while (!Validador.ValidarOpcion(Console.ReadLine() ?? "", mesa.Tipos.Length, out tipo))
        {
            Console.WriteLine("Opción inválida. Elige de nuevo:");
        }

        Console.WriteLine("Asunto:");
        string asunto = Console.ReadLine() ?? "";
        while (!Validador.ValidarTexto(asunto, 5))
        {
            Console.WriteLine("Asunto muy corto. Intenta de nuevo:");
            asunto = Console.ReadLine() ?? "";
        }

        Console.WriteLine("Número de folios:");
        int folios;
        while (!Validador.ValidarFolios(Console.ReadLine() ?? "", out folios))
        {
            Console.WriteLine("Debe ser un entero mayor o igual a 1:");
        }

        // Primera letra en mayúscula
        remitente = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(remitente.Trim().ToLower());
        asunto = asunto.Trim();
        asunto = char.ToUpper(asunto[0]) + asunto.Substring(1);

        Tramite nuevo = mesa.Registrar(remitente, tipo, asunto, folios);
        Console.WriteLine("\n=== COMPROBANTE DE REGISTRO ===");
        Tramite.MostrarEncabezado();
        nuevo.Mostrar();
        Console.WriteLine("Guarda tu código: " + nuevo.Codigo);
    }

    static void ConsultarPorCodigo(MesaPartes mesa)
    {
        Console.WriteLine("Buscar código:");
        string codbus = (Console.ReadLine() ?? "").ToUpper();
        Tramite? hallado = mesa.BuscarPorCodigo(codbus);
        if (hallado == null)
        {
            Console.WriteLine("No se encontró el expediente.");
        }
        else
        {
            Tramite.MostrarEncabezado();
            hallado.Mostrar();
        }
    }

    static void ListarOrdenado(MesaPartes mesa, bool descendente)
    {
        if (mesa.Cantidad == 0)
        {
            Console.WriteLine("No hay solicitudes registradas.");
            return;
        }
        List<Tramite> listaOrdenada = mesa.OrdenarPorFolios(descendente);
        Tramite.MostrarEncabezado();
        foreach (var fila in listaOrdenada)
        {
            fila.Mostrar();
        }
    }
}
