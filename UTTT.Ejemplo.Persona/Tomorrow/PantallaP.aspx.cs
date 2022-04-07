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

namespace UTTT.Ejemplo.Persona.Tomorrow
{
    public partial class PantallaP : System.Web.UI.Page
    {
        private SessionManager session = new SessionManager();
        private int idPerfil;


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    this.Response.Buffer = true;
                    this.session = (SessionManager)this.Session["SessionManager"];
                    this.idPerfil = this.session.Parametros["idPerfil"] != null ?
                        int.Parse(this.session.Parametros["idPerfil"].ToString()) : 0;
                }
                catch(Exception ex)
                {
                    idPerfil = 0;
                }

                if (idPerfil > 0)
                {
                    var y = new Linq.Data.Entity.CatPerfil();
                    using (var x = new DcGeneralDataContext())
                    {
                        y = x.CatPerfil.FirstOrDefault(c => c.Id == idPerfil);
                    }
                    this.lblPerfil.Text = y.strValor;
                    this.lblPerfil.Visible = true;
                }

                if (this.idPerfil==1)
                {
                    this.btnEmpleados.Visible = true;
                    this.btnProductos.Visible = true;
                    this.btnLocales.Visible = true;
                }
                else if (this.idPerfil==2)
                {
                    this.btnProductos.Visible = true;
                }
                else if (this.idPerfil==3)
                {
                    this.btnEmpleados.Visible = true;
                }
                else if(this.idPerfil==4)
                {
                    this.btnLocales.Visible = true;
                }
                else
                {
                    Response.Redirect("~/Tomorrow/LogIn.aspx");
                }
                
            }
            catch
            {

            }
        }

        protected void btnProductos_Click(object sender, EventArgs e)
        {

            this.session.Pantalla = "~/Tomorrow/ProductoPrincipal.aspx";
            Hashtable parametrosRagion = new Hashtable();
            parametrosRagion.Add("idPerfil", this.idPerfil.ToString());
            this.session.Parametros = parametrosRagion;
            this.Session["SessionManager"] = this.session;
            this.Response.Redirect(this.session.Pantalla, false);
        }

        protected void btnEmpleados_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Tomorrow/EmpleadoP.aspx");
        }

        protected void btnLocales_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Tomorrow/LocalesPrincipal.aspx");
        }
    }
}