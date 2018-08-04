using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Store : System.Web.UI.Page

{
    SqlConnection sqlcon = new SqlConnection(@"Data Source =KAUSHIK\SQLEXPRESS;Initial Catalog=InventoryMangement;Integrated Security=true");
    SqlCommand cmd;
    SqlDataAdapter da;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else if (!IsPostBack)
        {
            filldropdownlist();
            FillGridView();
        }


    }

    protected void filldropdownlist()
    {
        if (sqlcon.State == ConnectionState.Closed)
            sqlcon.Open();
        //for product dropdown
         
        string productquery = "SELECT ProductId,ProductName FROM Product";
        cmd = new SqlCommand(productquery, sqlcon);
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds, "product");
        DropDownProduct.DataSource = ds.Tables["product"];
        DropDownProduct.DataTextField = "ProductName";
        DropDownProduct.DataValueField = "ProductId";
        DropDownProduct.DataBind();

        //for supplier dropdownlist
      
            string supplierquery = "SELECT SupplierId,CompanyName FROM Supplier";
            cmd = new SqlCommand(supplierquery, sqlcon);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "supplier");
            DropDownSupplier.DataSource = ds.Tables["supplier"];
            DropDownSupplier.DataTextField = "CompanyName";
            DropDownSupplier.DataValueField = "SupplierId";
            DropDownSupplier.DataBind();
        
        sqlcon.Close();


    }

   

    protected void btnsave_Click(object sender, EventArgs e)
    {
        //Console.Write(DropDownProduct.SelectedValue);
        //Console.Write(DropDownProduct.SelectedItem.Text);
        //Console.WriteLine(DropDownSupplier.SelectedValue);
        //Console.WriteLine(DropDownSupplier.SelectedItem.Text);


        if (sqlcon.State == ConnectionState.Closed)
            sqlcon.Open();
        SqlCommand sqlcmd = new SqlCommand("PurchaseCreateOrUpdate", sqlcon);
        sqlcmd.CommandType = CommandType.StoredProcedure;
        sqlcmd.Parameters.AddWithValue("@PurchaseId", hfPurchaseId.Value == "" ? 0 : Convert.ToInt32(hfPurchaseId.Value));
        sqlcmd.Parameters.AddWithValue("@ProductId", Convert.ToInt32(DropDownProduct.SelectedValue));
        sqlcmd.Parameters.AddWithValue("@SupplierId", Convert.ToInt32(DropDownSupplier.SelectedValue));
        sqlcmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(txtQuantity.Text.Trim()));
        sqlcmd.Parameters.AddWithValue("@Others", txtOthers.Text);
        sqlcmd.ExecuteNonQuery();
        sqlcon.Close();
        string PurchaseId = hfPurchaseId.Value;
        

        if (PurchaseId == "")
            lblsuccessmassage.Text = "Saved Successfully";
        else
            lblsuccessmassage.Text = "Updated Successfully";
        FillGridView();
        clear();
        DropDownProduct.ClearSelection();
        DropDownSupplier.ClearSelection();


    }
    void FillGridView()
    {
        if (sqlcon.State == ConnectionState.Closed)
            sqlcon.Open();
        //string pronamequery = "Select Purchase.PurchaseID,Product.ProductName,Supplier.CompanyName,Purchase.Quantity,Purchase.Others from purchase INNER JOIN Product on Product.ProductId = Purchase.PurchaseId INNER JOIN Supplier on Supplier.SupplierId = Purchase.PurchaseId";
        //SqlCommand scmd = new SqlCommand(pronamequery,sqlcon);
        //SqlDataAdapter sda= new SqlDataAdapter(scmd);
        //DataTable dt = new DataTable();
        //sda.Fill(dt);
        //sqlcon.Close();
        //purchaseGrid.DataSource = dt;
        //purchaseGrid.DataBind();

        SqlDataAdapter sqlDa = new SqlDataAdapter("ViewPurchaseGrid", sqlcon);
        sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
        DataTable dtbl = new DataTable();
        sqlDa.Fill(dtbl);
        sqlcon.Close();
        purchaseGrid.DataSource = dtbl;
        purchaseGrid.DataBind();

    }

    protected void btndelete_Click(object sender, EventArgs e)
    {
        if (sqlcon.State == ConnectionState.Closed)
            sqlcon.Open();
        SqlCommand cmd = new SqlCommand("PurchaseDeleteById", sqlcon);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@PurchaseId", Convert.ToInt32(hfPurchaseId.Value));
        cmd.ExecuteNonQuery();
        sqlcon.Close();
        hfPurchaseId.Value = "";
        txtQuantity.Text = txtOthers.Text = "";
        DropDownProduct.ClearSelection();
        DropDownSupplier.ClearSelection();
        FillGridView();
        lblsuccessmassage.Text=("Delete Successfully!");

    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        this.clear();
    }
    public void clear()
    {
        hfPurchaseId.Value = "";
        txtQuantity.Text = txtOthers.Text= "";
        DropDownProduct.ClearSelection();
        DropDownSupplier.ClearSelection();
        
        lblerrormessage.Text = lblsuccessmassage.Text = "";
        btnsave.Text = "Save";
        btndelete.Enabled = true;

    }
    protected void lnk_onClick(object sender, EventArgs e)
    {
        int PurchaseId = Convert.ToInt32((sender as LinkButton).CommandArgument);
        if (sqlcon.State == ConnectionState.Closed)
            sqlcon.Open();
        SqlDataAdapter sqlDa = new SqlDataAdapter("PurchaseViewById", sqlcon);
        sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
        sqlDa.SelectCommand.Parameters.AddWithValue("@PurchaseId", PurchaseId);
        DataTable dtbl = new DataTable();
        sqlDa.Fill(dtbl);
        sqlcon.Close();
        hfPurchaseId.Value = PurchaseId.ToString();
        DropDownProduct.SelectedItem.Text = dtbl.Rows[0]["ProductName"].ToString();
        DropDownSupplier.SelectedItem.Text = dtbl.Rows[0]["CompanyName"].ToString();
        txtQuantity.Text = dtbl.Rows[0]["Quantity"].ToString();
        txtOthers.Text = dtbl.Rows[0]["Others"].ToString();
        btnsave.Text = "Update";
        btndelete.Enabled = true;


    }

    protected void purchaseGrid_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DropDownProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void DropDownSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}