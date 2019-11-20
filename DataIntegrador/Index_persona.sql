/*CI0127 - Bases de datos
Tarea 02 - CodeBakers*/
CREATE UNIQUE INDEX index_persona
ON Persona(Correo)
INCLUDE(Apellido1, Apellido2, Nombre1, Nombre2);