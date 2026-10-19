using System;

class Tramite
{
    public string Codigo { get; set; }
    public string Remitente { get; set; }
    public string Dni { get; set; }
    public string TipoTramite { get; set; }
    public string Asunto { get; set; }
    public int Folios { get; set; }
    public string FechaRegistro { get; set; }

    public Tramite(string codigo, string remitente, string dni, string tipoTramite,
                   string asunto, int folios, string fechaRegistro)
    {
        Codigo = codigo;
        Remitente = remitente;
        Dni = dni;
        TipoTramite = tipoTramite;
        Asunto = asunto;
        Folios = folios;
        FechaRegistro = fechaRegistro;
    }

    public static void MostrarEncabezado()
    {
        Console.WriteLine($"{"Código",-15} {"Remitente",-22} {"DNI",-10} {"Trámite",-24} {"Asunto",-28} {"Folios",6} {"Fecha",-17}");
    }

    public void Mostrar()
    {
        Console.WriteLine($"{Codigo,-15} {Remitente,-22} {Dni,-10} {TipoTramite,-24} {Asunto,-28} {Folios,6} {FechaRegistro,-17}");
    }
}
