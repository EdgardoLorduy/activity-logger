using CommandLine;

namespace ActivityLogger.CommandOptions
{
    [Verb("add", HelpText = "En caso que exista una actividad en curso le asigna como fin la fecha/hora actual y agrega una nueva actividad con esa fecha/hora")]
    internal class AddVerb
    {
        [Value(0, MetaName = "logPath", HelpText = "Ruta del archivo log para registrar actividades", Required = true)]
        public string LogPath { get; set; }

        [Value(1, MetaName = "actividad", HelpText = "Nombre de la actividad", Required = true)]
        public string Activity { get; set; }

        [Value(2, MetaName = "descripcion", HelpText = "[Opcional] Descipcion de la actividad", Required = false)]
        public string Description { get; set; }
    }
}
