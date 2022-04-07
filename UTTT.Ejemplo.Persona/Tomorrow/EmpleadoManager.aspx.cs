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
    public partial class EmpleadoManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Empleado baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;
        private int idPerfil=0;

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

                this.idPerfil = this.session.Parametros["idPerfil"] != null ?
                    int.Parse(this.session.Parametros["idPerfil"].ToString()) : 0;

                if (this.idPersona == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.Empleado();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Empleado>().First(c => c.Id == this.idPersona);
                    this.tipoAccion = 2;
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
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    List<CatSexo> lista = dcGlobal.GetTable<CatSexo>().ToList();
                    this.ddlSexo.DataTextField = "strValor";
                    this.ddlSexo.DataValueField = "id";

                    List<CatPuesto> listaP = dcGlobal.GetTable<CatPuesto>().ToList();
                    this.ddlPuesto.DataTextField = "strValor";
                    this.ddlPuesto.DataValueField = "id";


                    if (this.idPersona == 0)
                    {

                        CatSexo catTemp = new CatSexo();
                        catTemp.id = -1;
                        catTemp.strValor = "Seleccionar";
                        lista.Insert(0, catTemp);
                        this.ddlSexo.DataSource = lista;
                        this.ddlSexo.DataBind();


                        CatPuesto catTempP = new CatPuesto();
                        catTempP.Id = -1;
                        catTempP.strValor = "Seleccionar";
                        listaP.Insert(0, catTempP);
                        this.ddlPuesto.DataSource = listaP;
                        this.ddlPuesto.DataBind();
                        this.lblAccion.Text = "Agregar";



                        this.Calendar1.SelectedDate = DateTime.Now;
                        this.Calendar1.EndDate = DateTime.Now;
                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        Calendar1.SelectedDate = this.baseEntity.dteFechaDeContratacion.Value.Date;
                        this.txtNombre.Text = this.baseEntity.strNombre;
                        this.txtCurp.Text = this.baseEntity.strCurp;
                        this.txtAPaterno.Text = this.baseEntity.strAPaterno;
                        this.txtAMaterno.Text = this.baseEntity.strAMAterno;
                        
                        this.ddlSexo.DataSource = lista;
                        this.ddlSexo.DataBind();
                        this.setItem(ref this.ddlSexo, baseEntity.CatSexo.strValor);


                        this.ddlPuesto.DataSource = listaP;
                        this.ddlPuesto.DataBind();
                        this.setItem(ref this.ddlPuesto, baseEntity.CatPuesto.strValor);

                    }
                    
                }

            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/Tomorrow/EmpleadoP.aspx");
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

                if (this.txtFechaNacimiento.Text=="")
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
                UTTT.Ejemplo.Linq.Data.Entity.Empleado persona = new Linq.Data.Entity.Empleado();
                if (this.idPersona == 0)
                {
                    int x = -1;
                    int y = -1;

                    if (this.ddlSexo.Text != "")
                    {
                        x = int.Parse(this.ddlSexo.Text);
                    }

                    if (this.ddlPuesto.Text != "")
                    {
                        y = int.Parse(this.ddlPuesto.Text);
                    }

                    persona.strNombre = this.txtNombre.Text.Trim();
                    persona.strAMAterno = this.txtAMaterno.Text.Trim();
                    persona.strAPaterno = this.txtAPaterno.Text.Trim();
                    persona.strCurp = this.txtCurp.Text.Trim();
                    persona.dteFechaDeContratacion = fechaNacimiento;
                    persona.Sexo_id = x; 
                    persona.Puesto_id= y;

                    String mensaje = String.Empty;
                    //validacion de datos correctos desde codigo

                    if (persona.strNombre=="" && persona.strAPaterno=="" && persona.strAMAterno=="" && persona.strCurp=="" && persona.Sexo_id==-1 && persona.Puesto_id==-1)
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

                    dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Empleado>().InsertOnSubmit(persona);
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se agrego correctamente.");


                    this.session.Pantalla = "~/Tomorrow/EmpleadoP.aspx";
                    Hashtable parametrosRagion = new Hashtable();
                    parametrosRagion.Add("idPerfil", idPerfil.ToString());
                    this.session.Parametros = parametrosRagion;
                    this.Session["SessionManager"] = this.session;
                    this.Response.Redirect(this.session.Pantalla, false);
                    
                }
                if (this.idPersona > 0)
                {
                    int x = -1;
                    int y = -1;

                    if (this.ddlSexo.Text != "")
                    {
                        x = int.Parse(this.ddlSexo.Text);
                    }
                    
                    if (this.ddlPuesto.Text != "")
                    {
                        y = int.Parse(this.ddlPuesto.Text);
                    }


                    persona = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Empleado>().First(c => c.Id == idPersona);
                    persona.strNombre = this.txtNombre.Text.Trim();
                    persona.strAMAterno = this.txtAMaterno.Text.Trim();
                    persona.strAPaterno = this.txtAPaterno.Text.Trim();
                    persona.strCurp = this.txtCurp.Text.Trim();
                    persona.dteFechaDeContratacion = fechaNacimiento;
                    persona.Sexo_id = x;
                    persona.Puesto_id = y;
                    String mensaje = String.Empty;
                    //validacion de datos correctos desde codigo

                    if (persona.strNombre == "" && persona.strAPaterno == "" && persona.strAMAterno == "" && persona.strCurp == "" && persona.Sexo_id == -1 && persona.Puesto_id==-1)
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


                    this.session.Pantalla = "~/Tomorrow/EmpleadoP.aspx";
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
                this.session.Pantalla = "~/Tomorrow/EmpleadoP.aspx";
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
                Expression<Func<CatSexo, bool>> predicateSexo = c => c.id == idSexo;
                predicateSexo.Compile();
                List<CatSexo> lista = dcGlobal.GetTable<CatSexo>().Where(predicateSexo).ToList();
                CatSexo catTemp = new CatSexo();            
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



        protected void ddlPuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idPuesto = int.Parse(this.ddlPuesto.Text);
                Expression<Func<CatPuesto, bool>> predicatePuesto = c => c.Id == idPuesto;
                predicatePuesto.Compile();
                List<CatPuesto> listaP = dcGlobal.GetTable<CatPuesto>().Where(predicatePuesto).ToList();
                CatPuesto catTempP = new CatPuesto();
                this.ddlPuesto.DataTextField = "strValor";
                this.ddlPuesto.DataValueField = "id";
                this.ddlPuesto.DataSource = listaP;
                this.ddlPuesto.DataBind();
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

        public bool validacion(UTTT.Ejemplo.Linq.Data.Entity.Empleado _persona, ref String _mensaje)
        {
            if (_persona.Sexo_id == -1)
            {
                _mensaje = "Seleccione Masculino o Femenino";
                return false;
            }

            if (_persona.strNombre.Equals(String.Empty))
            {
                _mensaje = "El campo Nombre está vacio";
                return false;
            }
            if (_persona.strNombre.Length > 50)
            {
                _mensaje = "Los caracteres permitidos para nombre rebasan lo establecido de 50";
                return false;
            }

            if (_persona.strAPaterno.Equals(String.Empty))
            {
                _mensaje = "El campo APaterno esta vacio";
                return false;
            }

            if (_persona.strAPaterno.Length > 50)
            {
                _mensaje = "Los caracteres permitidos para nombre rebasan lo establecido de 50 para A Paterno";
                return false;
            }
            if (_persona.strAMAterno.Equals(String.Empty))
            {
                _mensaje = "El campo AMaterno esta vacio";
                return false;
            }

            if (_persona.strAMAterno.Length > 50)
            {
                _mensaje = "Los caracteres permitidos para nombre rebasan lo establecido de 50 para A Materno";
                return false;
            }
            if (_persona.strCurp.Equals(String.Empty))
            {
                _mensaje = "El campo Curp esta vacio";
                return false;
            }

            if (_persona.strCurp.Length > 50)
            {
                _mensaje = "Los caracteres permitidos para Curp rebasan lo establecido de 50";
                return false;
            }
            if(_persona.dteFechaDeContratacion.Value.Year.ToString() == "1751")
            {
                _mensaje = "El campo de fecha de nacimiento contiene un formato no Valido";
                return false;
            }
            if(_persona.dteFechaDeContratacion.Value.Year.ToString() == "1750")
            {
                _mensaje = "El campo de fecha de nacimiento no puede estar vacio";
                return false;
            }
            if (int.Parse(_persona.dteFechaDeContratacion.Value.Year.ToString()) <= 1753 || int.Parse(_persona.dteFechaDeContratacion.Value.Year.ToString()) >= 9999)
            {
                _mensaje = "El campo de fecha de nacimiento no debe estar entre 1753 y 9999";
                return false;
            }
            return true;

        }

    }
}