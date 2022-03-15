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
    public partial class PersonaManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Persona baseEntity;
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
                if (this.idPersona == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.Persona();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Persona>().First(c => c.id == this.idPersona);
                    this.tipoAccion = 2;
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

                    if (this.idPersona == 0)
                    {

                        CatSexo catTemp = new CatSexo();
                        catTemp.id = -1;
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
                        Calendar1.SelectedDate = this.baseEntity.dteFechaNacimiento.Value.Date;
                        this.txtNombre.Text = this.baseEntity.strNombre;
                        this.txtCurp.Text = this.baseEntity.strCurp;
                        this.txtAPaterno.Text = this.baseEntity.strAPaterno;
                        this.txtAMaterno.Text = this.baseEntity.strAMaterno;
                        this.txtClaveUnica.Text = this.baseEntity.strClaveUnica;
                        
                        this.ddlSexo.DataSource = lista;
                        this.ddlSexo.DataBind();
                        this.setItem(ref this.ddlSexo, baseEntity.CatSexo.strValor);

                    }
                    
                }

            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/PersonaPrincipal.aspx", false);
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
                UTTT.Ejemplo.Linq.Data.Entity.Persona persona = new Linq.Data.Entity.Persona();
                if (this.idPersona == 0)
                {
                    persona.strClaveUnica = this.txtClaveUnica.Text.Trim();
                    persona.strNombre = this.txtNombre.Text.Trim();
                    persona.strAMaterno = this.txtAMaterno.Text.Trim();
                    persona.strAPaterno = this.txtAPaterno.Text.Trim();
                    persona.strCurp = this.txtCurp.Text.Trim();
                    persona.dteFechaNacimiento = fechaNacimiento;
                    persona.idCatSexo = int.Parse(this.ddlSexo.Text);

                    String mensaje = String.Empty;
                    //validacion de datos correctos desde codigo

                    if (this.ValidVacio(persona))
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

                    dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().InsertOnSubmit(persona);
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se agrego correctamente.");
                    this.Response.Redirect("~/PersonaPrincipal.aspx", false);
                    
                }
                if (this.idPersona > 0)
                {
                    int x = -1;
                    if (this.ddlSexo.Text != "")
                    {
                        x = int.Parse(this.ddlSexo.Text);
                    }
                    persona = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().First(c => c.id == idPersona);
                    persona.strClaveUnica = this.txtClaveUnica.Text.Trim();
                    persona.strNombre = this.txtNombre.Text.Trim();
                    persona.strAMaterno = this.txtAMaterno.Text.Trim();
                    persona.strAPaterno = this.txtAPaterno.Text.Trim();
                    persona.strCurp = this.txtCurp.Text.Trim();
                    persona.dteFechaNacimiento = fechaNacimiento;
                    persona.idCatSexo = x;
                    String mensaje = String.Empty;
                    //validacion de datos correctos desde codigo

                    if (this.ValidVacio(persona))
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
                    this.Response.Redirect("~/PersonaPrincipal.aspx", false);
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
                this.Response.Redirect("~/PersonaPrincipal.aspx", false);
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

        public bool validacion(UTTT.Ejemplo.Linq.Data.Entity.Persona _persona, ref String _mensaje)
        {
            if (_persona.idCatSexo == -1)
            {
                _mensaje = "Seleccione Masculino o Femenino";
                return false;
            }
            if (_persona.strClaveUnica.Equals(String.Empty))
            {
                _mensaje = "La Clave Unica esta Vacia";
                return false;
            }
            int i = 0;
            //Verificar si un texto es un número
            if (int.TryParse(_persona.strClaveUnica, out i) == false)
            {
                _mensaje = "La Clave Unica no es un número";
                return false;
            }
            ////Validamos un número
            ////string, saber que es un número
            ////99 y 1000
            if (int.Parse(_persona.strClaveUnica) < 100 || int.Parse(_persona.strClaveUnica) > 999)
            {
                _mensaje = "La Clave Unica esta fuera de rango";
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
            if (_persona.strAMaterno.Equals(String.Empty))
            {
                _mensaje = "El campo AMaterno esta vacio";
                return false;
            }

            if (_persona.strAMaterno.Length > 50)
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
            if(_persona.dteFechaNacimiento.Value.Year.ToString() == "1751")
            {
                _mensaje = "El campo de fecha de nacimiento contiene un formato no Valido";
                return false;
            }
            if(_persona.dteFechaNacimiento.Value.Year.ToString() == "1750")
            {
                _mensaje = "El campo de fecha de nacimiento no puede estar vacio";
                return false;
            }
            if (_persona.dteFechaNacimiento.Value.Year == DateTime.Now.Year)
            {
                _mensaje = "El campo de fecha de nacimiento no puede ser del presente año";
                return false;
            }
            if (int.Parse(_persona.dteFechaNacimiento.Value.Year.ToString()) <= 1753 || int.Parse(_persona.dteFechaNacimiento.Value.Year.ToString()) >= 9999)
            {
                _mensaje = "El campo de fecha de nacimiento no debe estar entre 1753 y 9999";
                return false;
            }
            return true;

        }

        public bool ValidVacio(UTTT.Ejemplo.Linq.Data.Entity.Persona _persona)
        {
            bool x = true;
            for (int i = 0; i < 7; i++)
            {
                switch (i)
                {
                    case 0:
                        if (_persona.idCatSexo != -1)
                        {
                            x = false;
                            i = 6;
                        }
                        break;
                    case 1:
                        if (!_persona.strClaveUnica.Equals(String.Empty))
                        {
                            x = false;
                            i = 6;
                        }
                        break;
                    case 2:
                        if (!_persona.strNombre.Equals(String.Empty))
                        {
                            x = false;
                            i = 6;
                        }
                        break;
                    case 3:
                        if (!_persona.strAPaterno.Equals(String.Empty))
                        {
                            x = false;
                            i = 6;
                        }
                        break;
                    case 4:
                        if (!_persona.strAMaterno.Equals(String.Empty))
                        {
                            x = false;
                            i = 6;
                        }
                        break;
                    case 5:
                        if (!_persona.strCurp.Equals(String.Empty))
                        {
                            x = false;
                        }
                        break;
                    case 6:
                        if (_persona.dteFechaNacimiento.Value.Year != DateTime.Now.Year)
                        {
                            x = false;
                        }
                            break;
                }
            }
            return x;
        }

    }
}