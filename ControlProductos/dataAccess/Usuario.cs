using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class UsuarioDa : Base
    { 
        public Entity.Usuario GetUsuario(int UsuarioId)
        {
            string json = methodGet("GetUsuario/" + UsuarioId.ToString());
            Entity.GetUsuarioResult_ regreso = JsonConvert.DeserializeObject<Entity.GetUsuarioResult_>(json);
            return regreso.GetUsuarioResult;
        }
        
        public List<Entity.Usuario> GetUsuarioByUsername(string Username)
        {
            string json = methodGet("GetUsuarioByUsername/" + Username);
            Entity.GetUsuarioByUsernameResult_ regreso = JsonConvert.DeserializeObject<Entity.GetUsuarioByUsernameResult_>(json);
            return regreso.GetUsuarioByUsernameResult;
        }

        public List<Entity.Usuario> GetCatalog(string CodigoPosicion, string CodigoProveedor, string Username, string NombreCompleto, bool Activo)
        {
            string json = methodGet("GetUsuarios/" + CodigoPosicion + "/" + CodigoProveedor + "/" + Username +"/" + NombreCompleto +"/" + Activo);
            Entity.GetUsuariosResult_ regreso = JsonConvert.DeserializeObject<Entity.GetUsuariosResult_>(json);
            return regreso.GetUsuariosResult;
        }

        public List<Entity.Usuario> GetCombo(string CodigoPosicion)
        {
            string json = methodGet("GetCmbUsuarios/" + CodigoPosicion);
            Entity.GetCmbUsuariosResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCmbUsuariosResult_>(json);
            return regreso.GetCmbUsuariosResult;
        }

        public int ValidateUsuario(string Username, string Contrasena)
        {
            Entity.ValidateUsuarioResult_ regreso = JsonConvert.DeserializeObject<Entity.ValidateUsuarioResult_>(methodGet("ValidateUsuario/" + Username + "/" + Contrasena));
            return regreso.ValidateUsuarioResult;
        }
        
        public int ChangePassword(int UsuarioId, string oldpassword, string newpassword)
        {
            Entity.ChangePasswordResult_ regreso = JsonConvert.DeserializeObject<Entity.ChangePasswordResult_>(methodPost("ChangePassword/" + UsuarioId + "/" + oldpassword + "/" + newpassword));
            return regreso.ChangePasswordResult;
        }

        public int GetPerfileByUsuario(int UsuarioId)
        {
            Entity.GetPerfileByUsuarioResult_ regreso = JsonConvert.DeserializeObject<Entity.GetPerfileByUsuarioResult_>(methodGet("GetPerfileByUsuario/" + UsuarioId));
            return regreso.GetPerfileByUsuarioResult;
        }

        public int InsUsuario(int ModUsuarioId, string Usuario, string Contrasena, int EmpleadoId, string CodigoPerfil)
        {
            Entity.Usuario usu = new Entity.Usuario();
            usu.Username = Usuario;
            usu.Contrasena = Contrasena;
            usu.EmpleadoId = EmpleadoId;
            usu.CodigoPerfil = CodigoPerfil;

            Entity.InsUsuarioResult_ regreso = JsonConvert.DeserializeObject<Entity.InsUsuarioResult_>(methodPost("InsUsuario/" + ModUsuarioId, JsonConvert.SerializeObject(usu)));
            return regreso.InsUsuarioResult;

        }

        public int UpdUsuario(int ModUsuarioId, int UsuarioId, string Usuario, int EmpleadoId, string CodigoPerfil)
        {
            Entity.Usuario usu = new Entity.Usuario();
            usu.UsuarioId = UsuarioId;
            usu.Username = Usuario;
            usu.EmpleadoId = EmpleadoId;
            usu.CodigoPerfil = CodigoPerfil;

            Entity.UpdUsuarioResult_ regreso = JsonConvert.DeserializeObject<Entity.UpdUsuarioResult_>(methodPost("UpdUsuario/" + ModUsuarioId, JsonConvert.SerializeObject(usu)));
            return regreso.UpdUsuarioResult;
        }
      
        public int DelUsuario(int UsuarioId, int ModUsuarioId)
        {
            Entity.Usuario usu = new Entity.Usuario();
            usu.UsuarioId = UsuarioId;

            Entity.DelUsuarioResult_ regreso = JsonConvert.DeserializeObject<Entity.DelUsuarioResult_>(methodPost("DelUsuario/" + ModUsuarioId, JsonConvert.SerializeObject(usu)));
            return regreso.DelUsuarioResult;
        }
        
        public string GetPassword(int UsuarioId)
        {
            Entity.GetPasswordResult_ regreso = JsonConvert.DeserializeObject<Entity.GetPasswordResult_>(methodPost("GetPassword/" + UsuarioId.ToString()));
            return regreso.GetPasswordResult;
        }

        public int ChangePhoto(int UsuarioId, string ImageUrl)
        {
            Entity.ChangePhotoResult_ regreso = JsonConvert.DeserializeObject<Entity.ChangePhotoResult_>(methodPost("ChangePhoto/" + UsuarioId.ToString()+ "/" + ImageUrl));
            return regreso.ChangePhotoResult;
        }

        public int DelUsuarioSelected(int UsuarioId, string Valores, bool Activo)
        {
            Entity.DelUsuarioSelectedResult_ regreso = JsonConvert.DeserializeObject<Entity.DelUsuarioSelectedResult_>(methodPost("DelUsuarioSelected/" + UsuarioId.ToString() + "/" + Valores + "/" + Activo.ToString()));
            return regreso.DelUsuarioSelectedResult;
        }

        public int DelUsuarioAll(int UsuarioId, string CodigoPosicion, string CodigoProveedor, bool Activo)
        {
            Entity.DelUsuarioAllResult_ regreso = JsonConvert.DeserializeObject<Entity.DelUsuarioAllResult_>(methodPost("DelUsuarioAll/" + UsuarioId.ToString() + "/" + CodigoPosicion + "/" + CodigoProveedor + "/" + Activo.ToString()));
            return regreso.DelUsuarioAllResult;
        }
    }
}