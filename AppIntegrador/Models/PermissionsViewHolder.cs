using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppIntegrador.Models
{
    public class PermissionsViewHolder
    {
        private Entities db = new Entities();
        public List<PerfilCodigo> Perfiles { get; set; }

        public List<Carrera> Carreras { get; set; }

        public List<Permiso> Permisos { get; set; }

        public List<Enfasis> EnfasisView { get; set; }

        public MultiSelectList ListaCarreras { get; set; }

        public MultiSelectList ListaPermisos { get; set; }

        public MultiSelectList ListaEnfasis { get; set; }

        public MultiSelectList ListaPerfiles { get; set; }

        public string[] CarrerasSeleccionadas { get; set; }

        public string[] EnfasisSeleccionados { get; set; }

        public int[] PermisosSeleccionados { get; set; }

        public int[] PerfilesSeleccionados { get; set; }

        public PermissionsViewHolder()
        {
            List<Perfil> perfiles = db.Perfil.ToList();
            this.Perfiles = new List<PerfilCodigo>();
            int count = 0;
            foreach (Perfil perfil in perfiles) {
                this.Perfiles.Add(new PerfilCodigo(perfil.Nombre, count++));
            } 
            this.Carreras = db.Carrera.ToList();
            this.EnfasisView = db.Enfasis.ToList();
            this.Permisos = db.Permiso.ToList();
            this.ListaCarreras = GetCarreras();
            this.ListaPermisos = GetPermisos();
            this.ListaEnfasis = GetEnfasis();
            this.ListaPerfiles = GetPerfiles();

        }

        public MultiSelectList GetCarreras()
        {
            return new MultiSelectList(this.Carreras, "Codigo", "Nombre", null);
        }
        public MultiSelectList GetPermisos()
        {
            return new MultiSelectList(this.Permisos, "Id", "Descripcion", null);
        }
        public MultiSelectList GetEnfasis()
        {
            return new MultiSelectList(this.EnfasisView, "Codigo", "Nombre", null);
        }
        public MultiSelectList GetPerfiles()
        {
            return new MultiSelectList(this.Perfiles, "Codigo", "NombrePerfil", null);
        }
    }

    public class PerfilCodigo
    {
        public string NombrePerfil { get; set; }

        public int Codigo { get; set; }

        public PerfilCodigo(string perfil, int codigo)
        {
            this.NombrePerfil = perfil;
            this.Codigo = codigo;
        }
    }
}