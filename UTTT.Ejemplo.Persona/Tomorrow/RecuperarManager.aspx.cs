#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Collections;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;
using UTTT.Ejemplo.Linq.Data.Entity;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.ComponentModel;


#endregion

namespace UTTT.Ejemplo.Persona
{
    public partial class RecuperarManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Usuario baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                this.Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idPersona = this.session.Parametros["idPersona"] != null ?
                    int.Parse(this.session.Parametros["idPersona"].ToString()) : 0;

                if (!(this.idPersona>=0))
                {
                    Response.Redirect("~/Tomorrow/LogIn.aspx");
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Usuario>().First(c => c.Id == this.idPersona);
                    this.tipoAccion = 2;
                    this.lblAccion.Text = this.baseEntity.strUsuario.ToString()+"  Ingrese su nueva contraseña ";
                }

                if (!this.IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    
                }

            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/Tomorrow/LogIn.aspx", false);
            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid)
                {
                    return;
                }

                DataContext dcGuardar = new DcGeneralDataContext();
                Usuario persona = dcGuardar.GetTable<Usuario>().First(c => c.Id == idPersona);
                
                persona.strPassword=this.txtCalle.Text.Trim();

                String mensaje = String.Empty;
                //validacion de datos correctos desde codigo

                if (persona.strPassword=="" && this.txtConfirm.Text=="")
                {
                    this.btnCancelar_Click(sender, e);
                    return;
                }

                if (!this.validacion(persona, ref mensaje))
                {
                    this.lblMensaje.Text = mensaje;
                    this.lblMensaje.Visible = true;
                    return;
                }

                persona.strPassword = this.Encriptar(persona.strPassword.ToString());
                dcGuardar.SubmitChanges();
                this.showMessage("El registro se edito correctamente.");
                this.Response.Redirect("~/Tomorrow/LogIn.aspx", false);

            }
            catch (Exception _e)
            {
                this.showMessageException(_e.Message);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {              
                this.Response.Redirect("~/Tomorrow/RecuperarPrincipal.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        #endregion


        public bool validacion(UTTT.Ejemplo.Linq.Data.Entity.Usuario _usuario, ref String _mensaje)
        {
            if (_usuario.strPassword != this.txtConfirm.Text)
            {
                _mensaje = "Las contraseñas no coinciden";
                return false;
            }
            if (_usuario.strPassword.Equals(string.Empty))
            {
                _mensaje = "La contraseña no puede ir vacia";
                return false;
            }
            if (_usuario.strPassword.Length < 8 || _usuario.strPassword.Length > 16)
            {
                _mensaje = "La contraseña debe rondar entre 8 y 16 caracteres";
                return false;
            }
            return true;
        }


        public string Encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

    }
}