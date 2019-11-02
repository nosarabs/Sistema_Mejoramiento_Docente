using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AppIntegrador.Models.Metadata;

namespace AppIntegrador.Models
{
    [MetadataType(typeof(PlanDeMejoraMetadata))]
    public partial class PlanDeMejora { }

    [MetadataType(typeof(AccionDeMejoraMetadata))]
    public partial class AccionDeMejora { }

    [MetadataType(typeof(ObjetivoMetadata))]
    public partial class Objetivo { }

    [MetadataType(typeof(TipoObjetivoMetadata))]
    public partial class TipoObjetivo { }

    [MetadataType(typeof(PlantillaObjetivoMetadata))]
    public partial class PlantillaObjetivo { }

    [MetadataType(typeof(AccionableMetadata))]
    public partial class Accionable { }

    [MetadataType(typeof(PersonaMetadata))]
    public partial class Persona { }

    [MetadataType(typeof(EstudianteMetadata))]
    public partial class Estudiante { }
}