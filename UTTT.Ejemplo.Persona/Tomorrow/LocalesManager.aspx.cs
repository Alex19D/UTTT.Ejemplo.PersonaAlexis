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
    public partial class LocalesManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idEmpleado = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Locales baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;
        private int idPerfil = 0;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idEmpleado = this.session.Parametros["idPersona"] != null ?
                    int.Parse(this.session.Parametros["idPersona"].ToString()) : 0;

                this.idPerfil = this.session.Parametros["idPerfil"] != null ?
                   int.Parse(this.session.Parametros["idPerfil"].ToString()) : 0;

                if (this.idEmpleado == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.Locales();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Locales>().First(c => c.Id == this.idEmpleado);
                    this.tipoAccion = 2;
                }

                if (!(idPerfil == 1 || idPerfil == 4))
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
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    List<CatTipoLocal> lista = dcGlobal.GetTable<CatTipoLocal>().ToList();
                    this.ddlSexo.DataTextField = "strValor";
                    this.ddlSexo.DataValueField = "id";

                    if (this.idEmpleado == 0)
                    {
                        CatTipoLocal catTemp = new CatTipoLocal();
                        catTemp.Id = -1;
                        catTemp.strValor = "Seleccionar";
                        lista.Insert(0, catTemp);
                        this.ddlSexo.DataSource = lista;
                        this.ddlSexo.DataBind();
                        this.lblAccion.Text = "Agregar";

                        this.Calendar1.SelectedDate = DateTime.Now;
                        this.Calendar1.EndDate = DateTime.Now;
                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        Calendar1.SelectedDate = this.baseEntity.dteFechaDeInauguracion.Value.Date;
                        this.txtCurp.Text = this.baseEntity.Descripcion;
                        this.txtClaveUnica.Text = this.baseEntity.strClaveLocal;

                        this.txtClaveUnica.Enabled = false;

                        this.ddlSexo.DataSource = lista;
                        this.ddlSexo.DataBind();
                        this.setItem(ref this.ddlSexo, baseEntity.CatTipoLocal.strValor);

                    }
                    
                }

            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/Tomorrow/LocalesPrincipal.aspx");
            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaNacimiento;
                if (!Page.IsValid)
                {
                    return;
                }

                if (this.txtFechaNacimiento.Text == "")
                {
                    fechaNacimiento = DateTime.Parse("10/12/1750");
                }
                else
                {
                    try
                    {
                        string date = Request.Form[this.txtFechaNacimiento.UniqueID];
                        fechaNacimiento = Convert.ToDateTime(date);
                    }
                    catch
                    {
                        fechaNacimiento = DateTime.Parse("10/12/1751");
                    }

                }

                DataContext dcGuardar = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Locales persona = new Linq.Data.Entity.Locales();
                if (this.idEmpleado == 0)
                {
                    int x = -1;
                    if (this.ddlSexo.Text != "")
                    {
                        x = int.Parse(this.ddlSexo.Text);
                    }
                    persona.strClaveLocal = this.txtClaveUnica.Text.Trim();
                    persona.Descripcion = this.txtCurp.Text.Trim();
                    persona.TipoLocal_id = x;
                    persona.dteFechaDeInauguracion = fechaNacimiento;

                    String mensaje = String.Empty;
                    //validacion de datos correctos desde codigo

                    if (persona.strClaveLocal=="" && persona.Descripcion=="" && persona.TipoLocal_id==-1)
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

                    dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Locales>().InsertOnSubmit(persona);
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se agrego correctamente.");

                    this.session.Pantalla = "~/Tomorrow/LocalesPrincipal.aspx";
                    Hashtable parametrosRagion = new Hashtable();
                    parametrosRagion.Add("idPerfil", idPerfil.ToString());
                    this.session.Parametros = parametrosRagion;
                    this.Session["SessionManager"] = this.session;
                    this.Response.Redirect(this.session.Pantalla, false);

                    
                }
                if (this.idEmpleado > 0)
                {
                    int x = -1;
                    if (this.ddlSexo.Text != "")
                    {
                        x = int.Parse(this.ddlSexo.Text);
                    }

                    persona = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Locales>().First(c => c.Id == idEmpleado);
                    persona.Descripcion = this.txtCurp.Text.Trim();
                    persona.dteFechaDeInauguracion = fechaNacimiento;
                    persona.TipoLocal_id = x;

                    String mensaje = String.Empty;
                    //validacion de datos correctos desde codigo

                    if (persona.strClaveLocal == "" && persona.Descripcion == "" && persona.TipoLocal_id == -1)
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
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se edito correctamente.");


                    this.session.Pantalla = "~/Tomorrow/LocalesPrincipal.aspx";
                    Hashtable parametrosRagion = new Hashtable();
                    parametrosRagion.Add("idPerfil", idPerfil.ToString());
                    this.session.Parametros = parametrosRagion;
                    this.Session["SessionManager"] = this.session;
                    this.Response.Redirect(this.session.Pantalla, false);
                }
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
                this.session.Pantalla = "~/Tomorrow/LocalesPrincipal.aspx";
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPerfil", idPerfil.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.Response.Redirect(this.session.Pantalla, false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void ddlSexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idSexo = int.Parse(this.ddlSexo.Text);
                Expression<Func<CatTipoLocal, bool>> predicateSexo = c => c.Id == idSexo;
                predicateSexo.Compile();
                List<CatTipoLocal> lista = dcGlobal.GetTable<CatTipoLocal>().Where(predicateSexo).ToList();
                CatTipoLocal catTemp = new CatTipoLocal();            
                this.ddlSexo.DataTextField = "strValor";
                this.ddlSexo.DataValueField = "id";
                this.ddlSexo.DataSource = lista;
                this.ddlSexo.DataBind();
            }
            catch (Exception)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        #endregion

        #region Metodos

        public void setItem(ref DropDownList _control, String _value)
        {
            foreach (ListItem item in _control.Items)
            {
                if (item.Value == _value)
                {
                    item.Selected = true;
                    break;
                }
            }
            _control.Items.FindByText(_value).Selected = true;
        }

        #endregion

        public bool validacion(UTTT.Ejemplo.Linq.Data.Entity.Locales _persona, ref String _mensaje)
        {
            Linq.Data.Entity.Locales locales = new Locales();
            using(var x = new Linq.Data.Entity.DcGeneralDataContext())
            {
                locales=x.Locales.FirstOrDefault(c=>c.strClaveLocal==_persona.strClaveLocal);
            }

            if (_persona.TipoLocal_id == -1)
            {
                _mensaje = "Seleccione Tipo de Local";
                return false;
            }

            if (_persona.strClaveLocal.Equals(String.Empty))
            {
                _mensaje = "La Clave Local esta Vacia";
                return false;
            }
            int i = 0;
            //Verificar si un texto es un número
            if (int.TryParse(_persona.strClaveLocal, out i) == false)
            {
                _mensaje = "La Clave Local no es un número";
                return false;
            }
            ////Validamos un número
            ////string, saber que es un número
            ////99 y 1000
            if (int.Parse(_persona.strClaveLocal) < 100 || int.Parse(_persona.strClaveLocal) > 999)
            {
                _mensaje = "La Clave Local esta fuera de rango";
                return false;
            }
            if (locales != null && this.idEmpleado==0)
            {
                _mensaje = "La Clave unica ya existe";
                return false;
            }

            if (_persona.dteFechaDeInauguracion.Value.Year.ToString() == "1751")
            {
                _mensaje = "El campo de fecha de nacimiento contiene un formato no Valido";
                return false;
            }
            if (_persona.dteFechaDeInauguracion.Value.Year.ToString() == "1750")
            {
                _mensaje = "El campo de fecha de nacimiento no puede estar vacio";
                return false;
            }
            if (int.Parse(_persona.dteFechaDeInauguracion.Value.Year.ToString()) <= 1753 || int.Parse(_persona.dteFechaDeInauguracion.Value.Year.ToString()) >= 9999)
            {
                _mensaje = "El campo de fecha de nacimiento no debe estar entre 1753 y 9999";
                return false;
            }

            if (_persona.Descripcion.Equals(String.Empty))
            {
                _mensaje = "El campo Descripcion esta vacio";
                return false;
            }

            if (_persona.Descripcion.Length > 50)
            {
                _mensaje = "Los caracteres permitidos para Descripcion rebasan lo establecido de 50";
                return false;
            }
            return true;

        }

    }
}