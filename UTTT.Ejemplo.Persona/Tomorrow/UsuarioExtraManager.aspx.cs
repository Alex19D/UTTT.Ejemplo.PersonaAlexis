
#region Using

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

#endregion

namespace UTTT.Ejemplo.Persona
{
    public partial class UsuarioExtraManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Usuario baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;
        private int idDireccion = 0;
        private string AntiguoUser = "";
        private int idPerfil = 0;

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

                this.idPerfil = this.session.Parametros["idPerfil"] != null ?
                    int.Parse(this.session.Parametros["idPerfil"].ToString()) : 0;

                if (this.idDireccion == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.Usuario();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Usuario>().First(c => c.Id == this.idDireccion);
                    this.tipoAccion = 2;
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

                    List<CatPerfil> lista = dcGlobal.GetTable<CatPerfil>().ToList();
                    this.ddlSexo.DataTextField = "strValor";
                    this.ddlSexo.DataValueField = "id";

                    List<CatStatus> listaP = dcGlobal.GetTable<CatStatus>().ToList();
                    this.ddlPuesto.DataTextField = "strValor";
                    this.ddlPuesto.DataValueField = "id";


                    if (this.idDireccion == 0)
                    {
                        CatPerfil catTemp = new CatPerfil();
                        catTemp.Id = -1;
                        catTemp.strValor = "Seleccionar";
                        lista.Insert(0, catTemp);
                        this.ddlSexo.DataSource = lista;
                        this.ddlSexo.DataBind();


                        CatStatus catTempP = new CatStatus();
                        catTempP.Id = -1;
                        catTempP.strValor = "Seleccionar";
                        listaP.Insert(0, catTempP);
                        this.ddlPuesto.DataSource = listaP;
                        this.ddlPuesto.DataBind();
                        this.lblAccion.Text = "Agregar";


                        this.lblAccion.Text = "Agregar";
                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        this.txtColonia.Text = this.baseEntity.strUsuario;
                        string pass = this.DesEncriptar(this.baseEntity.strPassword.ToString());
                        this.txtCalle.Text = pass;
                        this.txtConfirm.Text = pass;


                        this.ddlSexo.DataSource = lista;
                        this.ddlSexo.DataBind();
                        this.setItem(ref this.ddlSexo, baseEntity.CatPerfil.strValor);


                        this.ddlPuesto.DataSource = listaP;
                        this.ddlPuesto.DataBind();
                        this.setItem(ref this.ddlPuesto, baseEntity.CatStatus.strValor);
                    }
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/Tomorrow/UsuarioManager.aspx", false);
            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                DataContext dcGuardar = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Usuario direccion = new Linq.Data.Entity.Usuario();
                if (this.idDireccion == 0)
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


                    direccion.Empleado_id = this.idPersona;
                    direccion.strPassword = this.txtCalle.Text;
                    direccion.strUsuario = this.txtColonia.Text.Trim();
                    direccion.perfil_id = x;
                    direccion.Status_id = y;

                    String mensaje = String.Empty;

                    if (direccion.strPassword == "" && this.txtConfirm.Text == "" && direccion.strUsuario == "" && direccion.Status_id == -1 && direccion.perfil_id == -1)
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

                    direccion.strPassword = this.Encriptar(direccion.strPassword.ToString());

                    dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuario>().InsertOnSubmit(direccion);
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se agrego correctamente.");

                    this.session.Pantalla = "~/Tomorrow/UsuarioManager.aspx";
                    Hashtable parametrosRagion = new Hashtable();
                    parametrosRagion.Add("idPerfil", idPerfil.ToString());
                    parametrosRagion.Add("idPersona", this.idPersona.ToString());
                    this.session.Parametros = parametrosRagion;
                    this.Session["SessionManager"] = this.session;
                    this.Response.Redirect(this.session.Pantalla, false);
                }
                if (this.idDireccion > 0)
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

                    direccion = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuario>().First(c => c.Id == this.idDireccion);
                    this.AntiguoUser = direccion.strUsuario.ToString();
                    direccion.strPassword = this.txtCalle.Text.Trim();
                    direccion.strUsuario = this.txtColonia.Text.Trim();
                    direccion.perfil_id = x;
                    direccion.Status_id = y;

                    String mensaje = String.Empty;

                    if (direccion.strPassword == "" && this.txtConfirm.Text == "" && direccion.strUsuario == "" && direccion.Status_id == -1 && direccion.perfil_id == -1)
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

                    direccion.strPassword = this.Encriptar(direccion.strPassword.ToString());

                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se edito correctamente.");


                    this.session.Pantalla = "~/Tomorrow/UsuarioManager.aspx";
                    Hashtable parametrosRagion = new Hashtable();
                    parametrosRagion.Add("idPerfil", idPerfil.ToString());
                    parametrosRagion.Add("idPersona", this.idPersona.ToString());
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
                this.session.Pantalla = "~/Tomorrow/UsuarioManager.aspx";
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPerfil", idPerfil.ToString());
                parametrosRagion.Add("idPersona", this.idPersona.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.Response.Redirect(this.session.Pantalla, false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        #endregion



        protected void ddlSexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idSexo = int.Parse(this.ddlSexo.Text);
                Expression<Func<CatPerfil, bool>> predicateSexo = c => c.Id == idSexo;
                predicateSexo.Compile();
                List<CatPerfil> lista = dcGlobal.GetTable<CatPerfil>().Where(predicateSexo).ToList();
                CatPerfil catTemp = new CatPerfil();
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
                Expression<Func<CatStatus, bool>> predicatePuesto = c => c.Id == idPuesto;
                predicatePuesto.Compile();
                List<CatStatus> listaP = dcGlobal.GetTable<CatStatus>().Where(predicatePuesto).ToList();
                CatStatus catTempP = new CatStatus();
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



        public bool Validacion(Linq.Data.Entity.Usuario _usuario, ref String _mensaje)
        {
            Usuario userEx = new Usuario();
            Usuario emplEx = new Usuario();

            if (this.AntiguoUser != _usuario.strUsuario.ToString())
            {
                using (var x = new DcGeneralDataContext())
                {
                    userEx = x.Usuario.FirstOrDefault(c => c.strUsuario == _usuario.strUsuario);
                    emplEx = x.Usuario.FirstOrDefault(p => p.Empleado_id == this.idPersona);
                }
            }
            else
            {
                userEx = null;
                emplEx = null;
            }

            if (_usuario.strUsuario.Equals(string.Empty))
            {
                _mensaje = "El Usuario no debe de ir vacio";
                return false;
            }
            if (_usuario.strUsuario.Length < 5 || _usuario.strUsuario.Length > 10)
            {
                _mensaje = "El Nombre de usuario debe rondar entre 5 y 10 caracteres";
                return false;
            }
            if (userEx != null)
            {
                _mensaje = "Este Usuario ya se Encuentra en Uso";
                return false;
            }
            if (_usuario.strPassword != this.txtConfirm.Text)
            {
                _mensaje = "Las contraseñas no coinciden";
                return false;
            }
            if (_usuario.strPassword.Equals(string.Empty))
            {
                _mensaje = "La contraseña no puede ir vacia";
                return false;
            }
            if (_usuario.strPassword.Length < 8 || _usuario.strPassword.Length > 16)
            {
                _mensaje = "La contraseña debe rondar entre 8 y 16 caracteres";
                return false;
            }
            if (emplEx != null && this.idDireccion == 0)
            {
                _mensaje = "Este Empleado ya esta registrado";
                return false;
            }
            if (_usuario.perfil_id == -1)
            {
                _mensaje = "seleccione algun Perfil";
                return false;
            }
            if (_usuario.Status_id == -1)
            {
                _mensaje = "seleccione algun status";
                return false;
            }
            return true;
        }




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



        public string Encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }


        public string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

    }
}