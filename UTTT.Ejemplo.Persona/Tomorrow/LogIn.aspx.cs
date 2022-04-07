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

namespace UTTT.Ejemplo.Persona
{
    public partial class LogIn : System.Web.UI.Page
    {
        private SessionManager session = new SessionManager();
        private UTTT.Ejemplo.Linq.Data.Entity.Usuario baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (this.txtUsuario.Text.ToString() == "")
            {
                this.lblMensaje.Text = "* El Usuario esta vacio";
                this.lblMensaje.Visible = true;
                return;
            }
            String mensaje = String.Empty;
            Usuario usuario = new Usuario(); 
            using(var x = new DcGeneralDataContext())
            {
                usuario = x.Usuario.FirstOrDefault(c=>c.strUsuario.ToString()==this.txtUsuario.Text.ToString());
            }
            

            if(!this.Validacion(usuario, ref mensaje))
            {
                this.lblMensaje.Text = mensaje;
                this.lblMensaje.Visible = true;
                return;
            }

            this.session.Pantalla = "~/Tomorrow/PantallaP.aspx";
            Hashtable parametrosRagion = new Hashtable();
            parametrosRagion.Add("idPerfil", usuario.perfil_id.ToString());
            this.session.Parametros = parametrosRagion;
            this.Session["SessionManager"] = this.session;
            this.Response.Redirect(this.session.Pantalla, false);

        }



        public bool Validacion(Usuario usuario, ref String _mensaje)
        {


            if (usuario == null)
            {
                _mensaje = "* El usuario ingresado no existe";
                return false;
            }

            if (this.txtPassword.Text.ToString() == "")
            {
                _mensaje = "* La contraseña esta vacia";
                return false;
            }

            string x = this.DesEncriptar(usuario.strPassword.ToString());

            if (this.txtPassword.Text.ToString() != x)
            {
                _mensaje = "* La contraseña es Incorrecta";
                return false;
            }
            if (usuario.Status_id != 1)
            {
                _mensaje = "* El usuario no se encuantra Activo";
                return false;
            }
            return true;
        }




        public string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }
    }
}