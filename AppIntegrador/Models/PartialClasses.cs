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
}