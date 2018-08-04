using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Supplier : System.Web.UI.Page
{
    SqlConnection sqlcon = new SqlConnection(@"Data Source =KAUSHIK\SQLEXPRESS;Initial Catalog=InventoryMangement;Integrated Security=true");
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else if (!IsPostBack)
        {
            btndelete.Enabled = false;
            FillGridView();
        }
    }
    void FillGridView()
    {
        if (sqlcon.State == ConnectionState.Closed)
            sqlcon.Open();
        SqlDataAdapter sqlDa = new SqlDataAdapter("SupplierViewAll", sqlcon);
        sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
        DataTable dtbl = new DataTable();
        sqlDa.Fill(dtbl);
        sqlcon.Close();
        supplierGrid.DataSource = dtbl;
        supplierGrid.DataBind();
       
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (sqlcon.State == ConnectionState.Closed)
            sqlcon.Open();
        SqlCommand sqlcmd = new SqlCommand("SupplierCreateOrUpdate", sqlcon);
        sqlcmd.CommandType = CommandType.StoredProcedure;
        sqlcmd.Parameters.AddWithValue("@SupplierId", hfSupplierId.Value == "" ? 0 : Convert.ToInt32(hfSupplierId.Value));
        sqlcmd.Parameters.AddWithValue("@CompanyName", txtComName.Text.Trim());
        sqlcmd.Parameters.AddWithValue("@TradeNo", txtTradeLiNo.Text.Trim());
        sqlcmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text.Trim());
        sqlcmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
        sqlcmd.ExecuteNonQuery();
        sqlcon.Close();
        string SupplierId = hfSupplierId.Value;
        clear();

        if (SupplierId == "")
            lblsuccessmassage.Text = "Saved Successfully";
        else
            lblsuccessmassage.Text = "Updated Successfully";
        FillGridView();
    }
    protected void lnk_onClick(object sender, EventArgs e)
    {
        int SupplierId = Convert.ToInt32((sender as LinkButton).CommandArgument);
        if (sqlcon.State == ConnectionState.Closed)
            sqlcon.Open();
        SqlDataAdapter sqlDa = new SqlDataAdapter("SupplierViewById", sqlcon);
        sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
        sqlDa.SelectCommand.Parameters.AddWithValue("@SupplierId", SupplierId);
        DataTable dtbl = new DataTable();
        sqlDa.Fill(dtbl);
        sqlcon.Close();
        hfSupplierId.Value = SupplierId.ToString();
        txtComName.Text = dtbl.Rows[0]["CompanyName"].ToString();
        txtTradeLiNo.Text = dtbl.Rows[0]["TradeNo"].ToString();
        txtMobileNo.Text = dtbl.Rows[0]["MobileNo"].ToString();
        txtAddress.Text = dtbl.Rows[0]["Address"].ToString();
        btnsave.Text = "Update";
        btndelete.Enabled = true;


    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        this.clear();
    }
    public void clear()
    {
        hfSupplierId.Value = "";
        txtComName.Text = txtAddress.Text=txtMobileNo.Text =txtTradeLiNo.Text= "";

        lblerrormessage.Text = lblsuccessmassage.Text = "";
        btnsave.Text = "Save";
        btndelete.Enabled = true;

    }


    protected void btndelete_Click(object sender, EventArgs e)
    {
        if (sqlcon.State == ConnectionState.Closed)
            sqlcon.Open();
        SqlCommand sqlcmd = new SqlCommand("SupplierDeleteById", sqlcon);
        sqlcmd.CommandType = CommandType.StoredProcedure;
        sqlcmd.Parameters.AddWithValue("@SupplierId", Convert.ToInt32(hfSupplierId.Value));
        sqlcmd.ExecuteNonQuery();
        sqlcon.Close();
        clear();
        FillGridView();
        lblsuccessmassage.Text = "Deleted Successfully";
    }
}