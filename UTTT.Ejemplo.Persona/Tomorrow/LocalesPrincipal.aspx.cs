﻿#region Using
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

namespace UTTT.Ejemplo.Persona
{
    public partial class LocalesPrincipal : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idPerfil = 0;

        #endregion

        #region Eventos

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

            try
            {
                Response.Buffer = true;
                DataContext dcTemp = new DcGeneralDataContext();
                if (!this.IsPostBack)
                {
                    List<CatTipoLocal> lista = dcTemp.GetTable<CatTipoLocal>().ToList();
                    CatTipoLocal catTemp = new CatTipoLocal();
                    catTemp.Id = -1;
                    catTemp.strValor = "Todos";
                    lista.Insert(0, catTemp);
                    this.ddlLocales.DataTextField = "strValor";
                    this.ddlLocales.DataValueField = "id";
                    this.ddlLocales.DataSource = lista;
                    this.ddlLocales.DataBind();
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");               
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                this.DataSourcePersona.RaiseViewChanged();
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al buscar");
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                this.session.Pantalla = "~/Tomorrow/LocalesManager.aspx";
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPersona", "0");
                parametrosRagion.Add("idPerfil", idPerfil.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.Response.Redirect(this.session.Pantalla, false);               
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al agregar");
            }
        }

        protected void DataSourcePersona_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                DataContext dcConsulta = new DcGeneralDataContext();
                bool nombreBool = false;
                bool sexoBool = false;
                if (!this.txtClaveLocal.Text.Equals(String.Empty))
                {
                    nombreBool = true;
                }
                if (this.ddlLocales.Text != "-1")
                {
                    sexoBool = true;
                }

                Expression<Func<UTTT.Ejemplo.Linq.Data.Entity.Locales, bool>> 
                    predicate =
                    (c =>
                    ((sexoBool) ? c.TipoLocal_id == int.Parse(this.ddlLocales.Text) : true) &&             
                    ((nombreBool) ? (((nombreBool) ? c.strClaveLocal.Contains(this.txtClaveLocal.Text.Trim()) : false)) : true)
                    );

                predicate.Compile();

                List<UTTT.Ejemplo.Linq.Data.Entity.Locales> listaPersona =
                    dcConsulta.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Locales>().Where(predicate).ToList();
                e.Result = listaPersona;    
                
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        protected void dgvPersonas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idPersona = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Editar":
                        this.editar(idPersona);
                        break;
                    case "Eliminar":
                        this.eliminar(idPersona);
                        break;
                    case "Direccion":
                        this.direccion(idPersona);
                        break;
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al seleccionar");
            }
        }

        #endregion 

        #region Metodos

        private void editar(int _idPersona)
        {
            try
            {
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPersona", _idPersona.ToString());
                parametrosRagion.Add("idPerfil", idPerfil.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.session.Pantalla = String.Empty;
                this.session.Pantalla = "~/Tomorrow/LocalesManager.aspx";
                this.Response.Redirect(this.session.Pantalla, false);

            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        private void eliminar(int _idPersona)
        {
            try
            {
                DataContext dcDelete = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Locales persona = dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Locales>().First(
                    c => c.Id == _idPersona);
                dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Locales>().DeleteOnSubmit(persona);
                dcDelete.SubmitChanges();
                this.showMessage("El registro se Elimino correctamente.");
                this.DataSourcePersona.RaiseViewChanged();                
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        private void direccion(int _idPersona)
        {
            try
            {
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPersona", _idPersona.ToString());
                parametrosRagion.Add("idPerfil", idPerfil.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.session.Pantalla = String.Empty;
                this.session.Pantalla = "~/Tomorrow/DireccionLManager.aspx";
                this.Response.Redirect(this.session.Pantalla, false);
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        #endregion

        protected void onTxtNombreChange(object sender, EventArgs e) 
        {
            try
            {
                this.DataSourcePersona.RaiseViewChanged();
            }
            catch(Exception f)
            {
                this.showMessage("Error al buscar");
            }
        }

        protected void buscarTextBox(object sender, EventArgs e)
        {
            this.DataSourcePersona.RaiseViewChanged();
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            try
            {
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPerfil", idPerfil.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.session.Pantalla = String.Empty;
                this.session.Pantalla = "~/Tomorrow/Pantallap.aspx";
                this.Response.Redirect(this.session.Pantalla, false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }
    }
}