using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Product : System.Web.UI.Page
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

    protected void btnclear_Click(object sender, EventArgs e)
    {
        this.clear();
    }
   
    public void clear()
    {
        hfProductId.Value = "";
        txtproname.Text = txtprodes.Text = "";
        lblerrormessage.Text = lblsuccessmassage.Text = "";
        btnsave.Text = "Save";
        btndelete.Enabled = true;
        
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        if (sqlcon.State == ConnectionState.Closed)
        sqlcon.Open();
        SqlCommand sqlcmd = new SqlCommand("ProductCreateOrUpdate",sqlcon);
        sqlcmd.CommandType = CommandType.StoredProcedure;
        sqlcmd.Parameters.AddWithValue("@ProductId",hfProductId.Value==""?0:Convert.ToInt32(hfProductId.Value));
        sqlcmd.Parameters.AddWithValue("@ProductName",txtproname.Text.Trim());
        sqlcmd.Parameters.AddWithValue("@ProductDescription",txtprodes.Text.Trim());
        sqlcmd.ExecuteNonQuery();
        sqlcon.Close();
        string ProductId = hfProductId.Value;
        clear();

        if (ProductId == "")
            lblsuccessmassage.Text = "Saved Successfully";
        else
            lblsuccessmassage.Text = "Updated Successfully";
        FillGridView();
    }

    void FillGridView()
    {
        if (sqlcon.State == ConnectionState.Closed) 
            sqlcon.Open();
        SqlDataAdapter sqlDa = new SqlDataAdapter("ProductViewAll",sqlcon);
        sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
        DataTable dtbl = new DataTable();
        sqlDa.Fill(dtbl);
        sqlcon.Close();
        productGrid.DataSource = dtbl;
        productGrid.DataBind();
    }

    protected void lnk_onClick(object sender, EventArgs e)
    {
        int ProductId = Convert.ToInt32((sender as LinkButton).CommandArgument);
        if (sqlcon.State == ConnectionState.Closed) 
        sqlcon.Open();
        SqlDataAdapter sqlDa = new SqlDataAdapter("ProductViewById", sqlcon);
        sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
        sqlDa.SelectCommand.Parameters.AddWithValue("@ProductId", ProductId);
        DataTable dtbl = new DataTable();
        sqlDa.Fill(dtbl);
        sqlcon.Close();
        hfProductId.Value = ProductId.ToString();
        txtproname.Text = dtbl.Rows[0]["ProductName"].ToString();
        txtprodes.Text = dtbl.Rows[0]["ProductDescription"].ToString();
        btnsave.Text = "Update";
        btndelete.Enabled = true;
        
        
    }

    protected void btndelete_Click(object sender, EventArgs e)
    {
        if (sqlcon.State == ConnectionState.Closed)
            sqlcon.Open();
        SqlCommand sqlcmd = new SqlCommand("ProductDeleteById",sqlcon);
        sqlcmd.CommandType = CommandType.StoredProcedure;
        sqlcmd.Parameters.AddWithValue("@ProductId",Convert.ToInt32(hfProductId.Value));
        sqlcmd.ExecuteNonQuery();
        sqlcon.Close();
        clear();
        FillGridView();
        lblsuccessmassage.Text = "Deleted Successfully";
    }
}