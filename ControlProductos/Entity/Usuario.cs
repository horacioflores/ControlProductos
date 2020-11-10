
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class ValidateUsuarioResult_
    {
       public int ValidateUsuarioResult { get; set; }
    }
    public class GetPerfileByUsuarioResult_
    {
        public int GetPerfileByUsuarioResult { get; set; }
    }
    public class ChangePasswordResult_
    {
        public int ChangePasswordResult { get; set; }
    }
    public class InsUsuarioResult_
    {
        public int InsUsuarioResult;
    }
    public class UpdUsuarioResult_
    {
        public int UpdUsuarioResult;
    }
    public class DelUsuarioResult_
    {
        public int DelUsuarioResult;
    }
    public class GetPasswordResult_
    {
        public string GetPasswordResult { get; set; }
    }
    public class ChangePhotoResult_
    {
        public int ChangePhotoResult { get; set; }
    }
    public class DelUsuarioSelectedResult_
    {
        public int DelUsuarioSelectedResult { get; set; }
    }
    public class DelUsuarioAllResult_
    {
        public int DelUsuarioAllResult { get; set; }
    }

    public class GetUsuarioResult_
    {
        public Usuario GetUsuarioResult;
    }
    public class GetUsuarioByUsernameResult_
    {
        public List<Usuario> GetUsuarioByUsernameResult;
    }
    public class GetUsuariosResult_
    {
        public List<Usuario> GetUsuariosResult;
    }
    public class GetCmbUsuariosResult_
    {
        public List<Usuario> GetCmbUsuariosResult;
    }

    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Username { get; set; }
        public string Contrasena { get; set; }
        public string ImagenUrl { get; set; }
        public string CodigoPerfil { get; set; }
        public string Perfil { get; set; }
        public bool Activo { get; set; }
        public int EmpleadoId { get; set; }
        public string Codigo { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public string Credencial { get; set; }
        public string NSS { get; set; }
        public string CodigoPlanta { get; set; }
        public string Planta { get; set; }
        public string CodigoDepto { get; set; }
        public string Departamento { get; set; }
        public string CodigoPosicion { get; set; }
        public string Posicion { get; set; }
        public string CodigoProveedor { get; set; }
        public string Proveedor { get; set; }
        public string CodigoYNombreCompleto { get; set; }

    }
}