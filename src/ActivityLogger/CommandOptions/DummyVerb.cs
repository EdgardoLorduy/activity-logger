using CommandLine;

namespace ActivityLogger.CommandOptions
{
    /// <summary>
    /// Se creó este verbo dummy solamente para que exista mas de un verbo de tal forma que CommandLineParser exija el verbo como requerido
    ///     de lo contrario CommandLineParser asume que siempre es 'add' y cuando no se especifica o se coloca otro nombre de verbo las opciones llegan con valores desfasados
    /// </summary>
    [Verb("dummy", HelpText = "Verbo dummy")]
    internal class DummyVerb
    {
    }
}
