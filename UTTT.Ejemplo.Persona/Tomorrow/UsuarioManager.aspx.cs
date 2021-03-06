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
    public partial class UsuarioManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Empleado baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;
        private int idPerfil = 0;
        private int idDireccion = 0;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idPersona = this.session.Parametros["idPersona"] != null ?
                    int.Parse(this.session.Parametros["idPersona"].ToString()) : 0;

                this.idDireccion = this.session.Parametros["idDireccion"] != null ?
                    int.Parse(this.session.Parametros["idDireccion"].ToString()) : 0;

                this.idPerfil = this.session.Parametros["idPerfil"] != null ?
                    int.Parse(this.session.Parametros["idPerfil"].ToString()) : 0;


                if (!(idPerfil == 1 || idPerfil == 3))
                {
                    Hashtable parametrosRagion = new Hashtable();
                    parametrosRagion.Add("idPerfil", idPerfil.ToString());
                    this.session.Parametros = parametrosRagion;
                    this.Session["SessionManager"] = this.session;
                    this.session.Pantalla = String.Empty;
                    this.session.Pantalla = "~/Tomorrow/AccesoDenegado.aspx";
                    this.Response.Redirect(this.session.Pantalla, false);
                }

                if (!(idPerfil == 1 || idPerfil == 3))
                {
                    Hashtable parametrosRagion = new Hashtable();
                    parametrosRagion.Add("idPerfil", idPerfil.ToString());
                    this.session.Parametros = parametrosRagion;
                    this.Session["SessionManager"] = this.session;
                    this.session.Pantalla = String.Empty;
                    this.session.Pantalla = "~/Tomorrow/AccesoDenegado.aspx";
                    this.Response.Redirect(this.session.Pantalla, false);
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


                if (!this.IsPostBack)
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Empleado>().First(c => c.Id == this.idPersona);
                    this.txtPersona.Text = this.baseEntity.strNombre + " " + this.baseEntity.strAPaterno + " " + this.baseEntity.strAMAterno;
                    if (this.baseEntity.Usuario.Count() == 0)
                    {
                        this.lblAccion.Text = "Agregar";
                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        this.btnAgregar.Visible = false;
                    }
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/Tomorrow/EmpleadoP.aspx", false);
            }
        }

        protected void LinqDataSourceDireccion_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                DataContext dcConsulta = new DcGeneralDataContext();               
                Expression<Func<UTTT.Ejemplo.Linq.Data.Entity.Usuario, bool>>
                    predicate = c => c.Empleado_id == this.idPersona;
                predicate.Compile();
                List<UTTT.Ejemplo.Linq.Data.Entity.Usuario> listaPersona =
                    dcConsulta.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuario>().Where(predicate).ToList();
                e.Result = listaPersona;
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/Tomorrow/EmpleadoP.aspx", false);
            }
        }

        protected void dgvDireccion_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al seleccionar");
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                this.session.Pantalla = "~/Tomorrow/UsuarioExtraManager.aspx";
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPersona", this.idPersona.ToString());
                parametrosRagion.Add("idPerfil", idPerfil.ToString());
                parametrosRagion.Add("idDireccion", "0");
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.Response.Redirect(this.session.Pantalla, false);

            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al agregar");
            }
        }


        #endregion

        #region Metodos

        private void editar(int _direccion)
        {
            try
            {
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPersona", this.idPersona.ToString());
                parametrosRagion.Add("idDireccion", _direccion.ToString());
                parametrosRagion.Add("idPerfil", idPerfil.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.session.Pantalla = String.Empty;
                this.session.Pantalla = "~/Tomorrow/UsuarioExtraManager.aspx";
                this.Response.Redirect(this.session.Pantalla, false);

            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        private void eliminar(int _idDireccion)
        {
            try
            {
                DataContext dcDelete = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Usuario direccion = dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuario>().First(
                    c => c.Id == _idDireccion);
                dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuario>().DeleteOnSubmit(direccion);
                dcDelete.SubmitChanges();
                this.showMessage("El registro se elimino correctamente.");
                this.LinqDataSourceDireccion.RaiseViewChanged();
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }


        #endregion

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Tomorrow/EmpleadoP.aspx", false);
        }
    }
}