using System;
using System.Collections.Generic;
using System.Linq;

class MesaPartes
{
    public string[] Tipos = { "Certificado de notas", "Traslado de matrícula",
                              "Constancia de vacantes", "Reclamo" };

    // ALMACENAMIENTO
    private Dictionary<string, Tramite> tramites = new Dictionary<string, Tramite>();
    private int contador = 0;

    public int Cantidad
    {
        get { return tramites.Count; }
    }

    private string GenerarCodigo()
    {
        contador++;
        return $"EXP-{DateTime.Now.Year}-{contador:D4}";
    }

    // REGISTRAR
    public Tramite Registrar(string remitente, string dni, int tipo, string asunto, int folios)
    {
        string codigo = GenerarCodigo();
        string fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        Tramite nuevo = new Tramite(codigo, remitente, dni, Tipos[tipo - 1], asunto, folios, fecha);
        tramites.Add(codigo, nuevo);
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
