
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
    public partial class DireccionExtraLManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.DireccionLocal baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;
        private int idDireccion = 0;

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

                this.idDireccion = this.session.Parametros["idDireccion"] != null ?
                    int.Parse(this.session.Parametros["idDireccion"].ToString()) : 0;

                if (this.idDireccion == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.DireccionLocal();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.DireccionLocal>().First(c => c.Id == this.idDireccion);
                    this.tipoAccion = 2;
                }

                if (!this.IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    if (this.idDireccion == 0)
                    {
                        this.lblAccion.Text = "Agregar";
                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";

                        if (this.baseEntity.strNumero != "S/N")
                        {
                            this.txtNumero.Text = this.baseEntity.strNumero;
                        }

                        this.txtColonia.Text = this.baseEntity.strColonia;
                        this.txtCalle.Text = this.baseEntity.strCalle;
                        this.txtCP.Text = this.baseEntity.strCP;
                    }
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/Tomorrow/DireccionLManager.aspx", false);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                DataContext dcGuardar = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.DireccionLocal direccion = new Linq.Data.Entity.DireccionLocal();
                if (this.idDireccion == 0)
                {
                    string x = "S/N";
                    if (this.txtNumero.Text != "")
                    {
                        x = this.txtNumero.Text;
                    }

                    direccion.Local_id = this.idPersona;
                    direccion.strCalle = this.txtCalle.Text.Trim();
                    direccion.strColonia = this.txtColonia.Text.Trim();
                    direccion.strNumero = this.txtNumero.Text.Trim();
                    direccion.strCP = x;

                    String mensaje = String.Empty;

                    if (direccion.strCalle == "" && direccion.strColonia == "" && direccion.strNumero == "S/N" && direccion.strCP == "")
                    {
                        this.btnCancelar_Click(sender, e);
                        return;
                    }

                    if (!this.Validacion(direccion, ref mensaje))
                    {
                        this.lblMensaje.Text = mensaje;
                        this.lblMensaje.Visible = true;
                        return;
                    }

                    dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.DireccionLocal>().InsertOnSubmit(direccion);
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se agrego correctamente.");
                    this.Response.Redirect("~/Tomorrow/DireccionLManager.aspx");
                }
                if (this.idDireccion > 0)
                {
                    string x = "S/N";
                    if (this.txtNumero.Text != "")
                    {
                        x = this.txtNumero.Text;
                    }

                    direccion = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.DireccionLocal>().First(c => c.Id == this.idDireccion);
                    direccion.strCalle = this.txtCalle.Text.Trim();
                    direccion.strColonia = this.txtColonia.Text.Trim();
                    direccion.strNumero = x;
                    direccion.strCP = this.txtCP.Text.Trim();

                    String mensaje = String.Empty;

                    if (direccion.strCalle == "" && direccion.strColonia == "" && direccion.strNumero == "S/N" && direccion.strCP == "")
                    {
                        this.btnCancelar_Click(sender, e);
                        return;
                    }

                    if (!this.Validacion(direccion, ref mensaje))
                    {
                        this.lblMensaje.Text = mensaje;
                        this.lblMensaje.Visible = true;
                        return;
                    }

                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se edito correctamente.");
                    this.Server.Transfer("~/Tomorrow/DireccionLManager.aspx");

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
                this.Response.Redirect("~/Tomorrow/DireccionLManager.aspx");
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        #endregion

        public bool Validacion(Linq.Data.Entity.DireccionLocal _DireccionL, ref String _mensaje)
        {
            if (_DireccionL.strColonia.Equals(String.Empty))
            {
                _mensaje = "El campo Colonia no puede ir vacio";
                return false;
            }
            if (_DireccionL.strColonia.Length > 50)
            {
                _mensaje = "El campo Colonia no soporta mas de 50 caracteres";
                return false;
            }
            if (_DireccionL.strCalle.Equals(string.Empty))
            {
                _mensaje = "El campo Calle no puede ir vacio";
                return false;
            }
            if (_DireccionL.strCalle.Length > 50)
            {
                _mensaje = "El campo calle no soporta mas de 50 caracteres";
                return false;
            }
            if (_DireccionL.strNumero != "S/N")
            {
                int j = 0;
                //Verificar si un texto es un número
                if (int.TryParse(_DireccionL.strNumero, out j) == false)
                {
                    _mensaje = "El numero no es un número";
                    return false;
                }
                if (int.Parse(_DireccionL.strNumero) < 1 || int.Parse(_DireccionL.strNumero) > 99)
                {
                    _mensaje = "El numero esta fuera de rango (1-99)";
                    return false;
                }
            }
            if (_DireccionL.strCP.Equals(string.Empty))
            {
                _mensaje = "El codigo Postal no debe ir vacio";
                return false;
            }
            int i = 0;
            //Verificar si un texto es un número
            if (int.TryParse(_DireccionL.strCP, out i) == false)
            {
                _mensaje = "El Codigo Postal no es un número";
                return false;
            }
            if (int.Parse(_DireccionL.strCP) < 1 || int.Parse(_DireccionL.strCP) > 99999)
            {
                _mensaje = "El numero esta fuera de rango";
                return false;
            }

            return true;
        }
    }
}