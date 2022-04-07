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
            if (this.Existe(txtUsuario.Text))
            {
                string pass = this.DesEncriptar(this.baseEntity.strPassword.ToString());
                if (this.txtPassword.Text == pass && this.baseEntity.Status_id==1)
                {
                    this.session.Pantalla = "~/Tomorrow/PantallaP.aspx";
                    Hashtable parametrosRagion = new Hashtable();
                    parametrosRagion.Add("idPerfil", this.baseEntity.perfil_id.ToString());
                    this.session.Parametros = parametrosRagion;
                    this.Session["SessionManager"] = this.session;
                    this.Response.Redirect(this.session.Pantalla, false);
                }
            }
        }

        private bool Existe(string name)
        {
            bool resp = false;
            try
            {
                this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Usuario>().First(c => c.strUsuario == name);
                resp = true;
            }
            catch
            {

            }
            return resp;
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