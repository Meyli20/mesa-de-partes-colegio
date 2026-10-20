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
        Console.WriteLine($"{"Código",-14} {"Remitente",-20} {"DNI",-9} {"Trámite",-22} {"Folios",-7} {"Fecha",-17} {"Asunto"}");
    }

    public void Mostrar()
    {
        Console.WriteLine($"{Codigo,-14} {Remitente,-20} {Dni,-9} {TipoTramite,-22} {Folios,-7} {FechaRegistro,-17} {Asunto}");
    }
}
