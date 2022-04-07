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

namespace UTTT.Ejemplo.Persona
{
    public partial class RecuperarPrincipal : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            
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

        protected void DataSourcePersona_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                DataContext dcConsulta = new DcGeneralDataContext();
                bool nombreBool = false;
                if (!this.txtNombre.Text.Equals(String.Empty))
                {
                    nombreBool = true;
                }

                List<UTTT.Ejemplo.Linq.Data.Entity.Usuario> listaPersona= new List<Usuario>();
                Usuario usuario = new Usuario();

                if (nombreBool)
                {
                    usuario = dcConsulta.GetTable<Usuario>().FirstOrDefault(c=>c.strUsuario == this.txtNombre.Text);

                    if (usuario != null)
                    {
                        listaPersona.Add(usuario);
                        this.lblEnc.Visible = true;
                    }
                }
                
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
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.session.Pantalla = String.Empty;
                this.session.Pantalla = "~/Tomorrow/RecuperarManager.aspx";
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
    }
}