using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppIntegrador.Models.Metadata
{
    public class AccionDeMejoraMetadata
    {
        public int codPlan { get; set; }
        public string nombreObj { get; set; }
        public string descripcion { get; set; }
        public Nullable<System.DateTime> fechaInicio { get; set; }
        public Nullable<System.DateTime> fechaFin { get; set; }
        public Nullable<int> codPlantilla { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Accionable> Accionable { get; set; }
        public virtual Objetivo Objetivo { get; set; }
        public virtual PlantillaAccionDeMejora PlantillaAccionDeMejora { get; set; }
    }
}