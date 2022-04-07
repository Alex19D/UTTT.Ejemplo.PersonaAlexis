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
    public partial class ProductoManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idEmpleado = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Producto baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;
        private string claveEditar="";
        private int idPerfil=0;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idEmpleado = this.session.Parametros["idEmpleado"] != null ?
                    int.Parse(this.session.Parametros["idEmpleado"].ToString()) : 0;

                this.idPerfil = this.session.Parametros["idPerfil"] != null ?
                    int.Parse(this.session.Parametros["idPerfil"].ToString()) : 0;

                if (this.idEmpleado == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.Producto();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Producto>().First(c => c.Id == this.idEmpleado);
                    this.tipoAccion = 2;
                }

                if (!(idPerfil == 1 || idPerfil == 2))
                {
                    Hashtable parametrosRagion = new Hashtable();
                    parametrosRagion.Add("idPerfil", idPerfil.ToString());
                    this.session.Parametros = parametrosRagion;
                    this.Session["SessionManager"] = this.session;
                    this.session.Pantalla = String.Empty;
                    this.session.Pantalla = "~/Tomorrow/AccesoDenegado.aspx";
                    this.Response.Redirect(this.session.Pantalla, false);
                }

                if (!(idPerfil == 1 || idPerfil == 2))
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
                    List<CatTipo> lista = dcGlobal.GetTable<CatTipo>().ToList();
                    this.ddlSexo.DataTextField = "strValor";
                    this.ddlSexo.DataValueField = "id";

                    if (this.idEmpleado == 0)
                    {

                        CatTipo catTemp = new CatTipo();
                        catTemp.Id = -1;
                        catTemp.strValor = "Seleccionar";
                        lista.Insert(0, catTemp);
                        this.ddlSexo.DataSource = lista;
                        this.ddlSexo.DataBind();
                        this.lblAccion.Text = "Agregar";
                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        this.txtNombre.Text = this.baseEntity.strNombre;
                        this.txtCurp.Text = this.baseEntity.strDescripcion;
                        this.txtPrecio.Text = this.baseEntity.Precio;
                        this.txtClaveUnica.Text = this.baseEntity.strClaveProducto;

                        this.txtClaveUnica.Enabled = false;

                        this.ddlSexo.DataSource = lista;
                        this.ddlSexo.DataBind();
                        this.setItem(ref this.ddlSexo, baseEntity.CatTipo.strValor);

                    }
                    
                }

            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/Tomorrow/ProductoPrincipal.aspx");
            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (!Page.IsValid)
                {
                    return;
                }

                DataContext dcGuardar = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Producto persona = new Linq.Data.Entity.Producto();
                if (this.idEmpleado == 0)
                {
                    int x = -1;
                    if (this.ddlSexo.Text != "")
                    {
                        x = int.Parse(this.ddlSexo.Text);
                    }
                    persona.strClaveProducto = this.txtClaveUnica.Text.Trim();
                    persona.strNombre = this.txtNombre.Text.Trim();
                    persona.Precio = this.txtPrecio.Text;
                    persona.strDescripcion = this.txtCurp.Text.Trim();
                    persona.Tipo_id = x;

                    String mensaje = String.Empty;
                    //validacion de datos correctos desde codigo

                    if (persona.strClaveProducto=="" && persona.strNombre=="" && persona.strDescripcion=="" && persona.Tipo_id==-1 && persona.Precio == "")
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

                    dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Producto>().InsertOnSubmit(persona);
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se agrego correctamente.");


                    this.session.Pantalla = "~/Tomorrow/ProductoPrincipal.aspx";
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

                    persona = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Producto>().First(c => c.Id == idEmpleado);
                    persona.strNombre = this.txtNombre.Text.Trim();
                    persona.strDescripcion = this.txtCurp.Text.Trim();
                    persona.Tipo_id = x;
                    persona.Precio = this.txtPrecio.Text.Trim();

                    String mensaje = String.Empty;
                    //validacion de datos correctos desde codigo

                    if (persona.strClaveProducto == "" && persona.strNombre == "" && persona.strDescripcion == "" && persona.Tipo_id == -1 && persona.Precio=="")
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

                    this.session.Pantalla = "~/Tomorrow/ProductoPrincipal.aspx";
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
                this.session.Pantalla = "~/Tomorrow/ProductoPrincipal.aspx";
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
                Expression<Func<CatTipo, bool>> predicateSexo = c => c.Id == idSexo;
                predicateSexo.Compile();
                List<CatTipo> lista = dcGlobal.GetTable<CatTipo>().Where(predicateSexo).ToList();
                CatTipo catTemp = new CatTipo();            
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

        public bool validacion(UTTT.Ejemplo.Linq.Data.Entity.Producto _persona, ref String _mensaje)
        {
            Linq.Data.Entity.Producto producto = new Producto();
            using(var x = new Linq.Data.Entity.DcGeneralDataContext())
            {
                producto = x.Producto.FirstOrDefault(c => c.strClaveProducto == _persona.strClaveProducto);
            }

            if (_persona.Tipo_id == -1)
            {
                _mensaje = "Seleccione Tipo de Producto";
                return false;
            }


            if (_persona.strClaveProducto.Equals(String.Empty))
            {
                _mensaje = "La Clave Producto esta Vacia";
                return false;
            }
            int i = 0;
            //Verificar si un texto es un número
            if (int.TryParse(_persona.strClaveProducto, out i) == false)
            {
                _mensaje = "La Clave Producto no es un número";
                return false;
            }
            ////Validamos un número
            ////string, saber que es un número
            ////99 y 1000
            if (int.Parse(_persona.strClaveProducto) < 100 || int.Parse(_persona.strClaveProducto) > 999)
            {
                _mensaje = "La Clave Producto esta fuera de rango";
                return false;
            }
            if (producto != null && idEmpleado==0)
            {
                _mensaje = "La clave de Producto esta siedo utilizada";
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


            if (_persona.Precio.Equals(String.Empty))
            {
                _mensaje = "El Precio esta Vacio";
                return false;
            }
            i = 0;

            if (int.TryParse(_persona.Precio, out i) == false)
            {
                _mensaje = "El Precio no es un número";
                return false;
            }

            if (int.Parse(_persona.Precio) < 3 || int.Parse(_persona.Precio) > 999)
            {
                _mensaje = "La Clave Producto esta fuera de rango";
                return false;
            }


            if (_persona.strDescripcion.Equals(String.Empty))
            {
                _mensaje = "El campo Descripcion esta vacio";
                return false;
            }

            if (_persona.strDescripcion.Length > 50)
            {
                _mensaje = "Los caracteres permitidos para Descripcion rebasan lo establecido de 50";
                return false;
            }
            return true;

        }

    }
}