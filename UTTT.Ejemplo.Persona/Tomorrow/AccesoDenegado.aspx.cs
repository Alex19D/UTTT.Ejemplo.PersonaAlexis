#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Collections;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

#endregion

namespace UTTT.Ejemplo.Persona.Tomorrow
{
    public partial class AccesoDenegado : System.Web.UI.Page
    {
        private SessionManager session = new SessionManager();
        private int idPerfil = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idPerfil = this.session.Parametros["idPerfil"] != null ?
                    int.Parse(this.session.Parametros["idPerfil"].ToString()) : 0;
            }
            catch
            {
                Response.Redirect("~/Tomorrow/LogIn.aspx");
            }
            if (this.idPerfil == 0)
            {
                Response.Redirect("~/Tomorrow/LogIn.aspx");
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Hashtable parametrosRagion = new Hashtable();
            parametrosRagion.Add("idPerfil", idPerfil.ToString());
            this.session.Parametros = parametrosRagion;
            this.Session["SessionManager"] = this.session;
            this.session.Pantalla = String.Empty;
            this.session.Pantalla = "~/Tomorrow/Pantallap.aspx";
            this.Response.Redirect(this.session.Pantalla, false);
        }
    }
}