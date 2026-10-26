using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

class MesaPartes
{
    public string[] Tipos = { "Certificado de notas", "Traslado de matrícula",
                              "Constancia de vacantes", "Reclamo" };

    // ALMACENAMIENTO
    private Dictionary<string, Tramite> tramites = new Dictionary<string, Tramite>();
    private int contador = 0;

    // Archivo donde se guardan los datos (junto al ejecutable)
    private static readonly string RutaArchivo =
        Path.Combine(AppContext.BaseDirectory, "tramites.json");

    // Estructura que se guarda en el archivo
    private class DatosGuardados
    {
        public int Contador { get; set; }
        public List<Tramite> Tramites { get; set; } = new List<Tramite>();
    }

    public MesaPartes()
    {
        Cargar();
    }

    public int Cantidad
    {
        get { return tramites.Count; }
    }

    // GUARDAR en disco
    private void Guardar()
    {
        try
        {
            var datos = new DatosGuardados
            {
                Contador = contador,
                Tramites = tramites.Values.ToList()
            };
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(RutaArchivo, JsonSerializer.Serialize(datos, opciones));
        }
        catch (Exception ex)
        {
            Console.WriteLine("No se pudo guardar los datos: " + ex.Message);
        }
    }

    // CARGAR desde disco
    private void Cargar()
    {
        if (!File.Exists(RutaArchivo)) return;

        try
        {
            string json = File.ReadAllText(RutaArchivo);
            DatosGuardados? datos = JsonSerializer.Deserialize<DatosGuardados>(json);
            if (datos == null) return;

            contador = datos.Contador;
            foreach (Tramite t in datos.Tramites)
            {
                tramites[t.Codigo] = t;
            }
            ActualizarEstados();
        }
        catch (Exception ex)
        {
            Console.WriteLine("No se pudo leer el archivo de datos: " + ex.Message);
        }
    }

    private string GenerarCodigo()
    {
        contador++;
        return $"EXP-{DateTime.Now.Year}-{contador:D4}";
    }

    // Un trámite pasa a "Completado" cuando ya hay 2 registros posteriores a él
    private void ActualizarEstados()
    {
        List<Tramite> lista = tramites.Values.OrderBy(t => t.Codigo).ToList();
        for (int i = 0; i < lista.Count; i++)
        {
            int posteriores = lista.Count - 1 - i;
            lista[i].Estado = posteriores >= 2 ? "Completado" : "Tramitándose";
        }
    }

    // REGISTRAR
    public Tramite Registrar(string remitente, string dni, int tipo, string asunto, int folios)
    {
        string codigo = GenerarCodigo();
        string fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        Tramite nuevo = new Tramite(codigo, remitente, dni, Tipos[tipo - 1], asunto, folios, fecha);
        tramites.Add(codigo, nuevo);
        ActualizarEstados();
        Guardar();
        return nuevo;
    }

    // BUSCAR por código
    public Tramite? BuscarPorCodigo(string codigo)
    {
        bool encontrado = tramites.ContainsKey(codigo);
        if (encontrado)
        {
            return tramites[codigo];
        }
        return null;
    }

    // ORDENAR por folios
    public List<Tramite> OrdenarPorFolios(bool descendente)
    {
        List<Tramite> lista = tramites.Values.ToList();
        if (descendente)
        {
            return lista.OrderByDescending(x => x.Folios).ToList();
        }
        return lista.OrderBy(x => x.Folios).ToList();
    }
}