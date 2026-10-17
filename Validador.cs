using System;

static class Validador
{
    // El texto no puede estar vacío ni ser muy corto
    public static bool ValidarTexto(string texto, int minimo)
    {
        return texto.Trim().Length >= minimo;
    }

    // Los folios deben ser un entero mayor o igual a 1
    public static bool ValidarFolios(string valor, out int folios)
    {
        return int.TryParse(valor, out folios) && folios >= 1;
    }

    // La opción debe ser un número entre 1 y cantidad
    public static bool ValidarOpcion(string valor, int cantidad, out int opcion)
    {
        return int.TryParse(valor, out opcion) && opcion >= 1 && opcion <= cantidad;
    }
}

