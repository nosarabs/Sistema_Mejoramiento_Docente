using AppIntegrador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIntegrador.Controllers
{
    public class ValidadorGuia : Validador
    {
        public bool Validar(GuiaHorario guia)
        {
            //Codigo Carrera
            if (!ValidarTamanoText(guia.CodigoCarreraCurso, 10) || checkForSQLInjection(guia.CodigoCarreraCurso))
            {
                return false;
            }

            //Codigo Enfasis
            if (!ValidarTamanoText(guia.CodigoEnfasisCurso, 10) || checkForSQLInjection(guia.CodigoEnfasisCurso))
            {
                return false;
            }

            //Sigla Curso Carrera
            if (!ValidarTamanoText(guia.SiglaCursoCarrera, 10) || checkForSQLInjection(guia.SiglaCursoCarrera))
            {
                return false;
            }

            //Sigla Curso
            if (!ValidarTamanoText(guia.SiglaCursoCarrera, 10) || checkForSQLInjection(guia.SiglaCursoCarrera))
            {
                return false;
            }

            //Sigla Curso Grupo
            if (!ValidarTamanoText(guia.SiglaCursoGrupo, 10) || checkForSQLInjection(guia.SiglaCursoGrupo))
            {
                return false;
            }

            //Sigla Curso Grupo Imparte
            if (!ValidarTamanoText(guia.SiglaCursoImparte, 10) || checkForSQLInjection(guia.SiglaCursoImparte))
            {
                return false;
            }

            //Sigla Curso Matricula
            if (!ValidarTamanoText(guia.SiglaCursoMatricula, 10) || checkForSQLInjection(guia.SiglaCursoMatricula))
            {
                return false;
            }

            //Nombre Curso
            if (!ValidarTamanoText(guia.NombreCurso, 50) || checkForSQLInjection(guia.NombreCurso))
            {
                return false;
            }

            //Num Grupo
            if (!ValidarNumero(guia.NombreCurso) || checkForSQLInjection(guia.NumeroGrupo))
            {
                return false;
            }


            //Semestre
            if (!ValidarNumero(guia.Semestre) || checkForSQLInjection(guia.Semestre))
            {
                return false;
            }


            //Anno
            if (!ValidarNumero(guia.Anno) || checkForSQLInjection(guia.Anno))
            {
                return false;
            }

            if (!ValidarEmail(guia.CorreoProfesorImparte) || checkForSQLInjection(guia.CorreoProfesorImparte))
            {
                return false; //email invalidao
            }

            //Num Grupo Imparte
            if (!ValidarNumero(guia.NumeroGrupoImparte) || checkForSQLInjection(guia.NumeroGrupoImparte))
            {
                return false;
            }

            //Semestre Imparte
            if (!ValidarNumero(guia.SemestreGrupoImparte) || checkForSQLInjection(guia.SemestreGrupoImparte))
            {
                return false;
            }


            //Anno Imparte
            if (!ValidarNumero(guia.AnnoGrupoImparte) || checkForSQLInjection(guia.AnnoGrupoImparte))
            {
                return false;
            }


            //Correo Matricula
            if (!ValidarEmail(guia.CorreoMatricula) || checkForSQLInjection(guia.CorreoMatricula))
            {
                return false; //email invalidao
            }

            //Num Grupo Matricula
            if (!ValidarNumero(guia.NumeroGrupoMatricula) || checkForSQLInjection(guia.NumeroGrupoMatricula))
            {
                return false;
            }

            //Semestre Imparte
            if (!ValidarNumero(guia.SemestreMatricula) || checkForSQLInjection(guia.SemestreMatricula))
            {
                return false;
            }


            //Anno Imparte
            if (!ValidarNumero(guia.AnnoMatricula) || checkForSQLInjection(guia.AnnoMatricula))
            {
                return false;
            }

            return true;

        }
    }
}