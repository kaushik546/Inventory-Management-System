using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Sales : System.Web.UI.Page

{
    static SqlConnection sqlcon = new SqlConnection(@"Data Source =KAUSHIK\SQLEXPRESS;Initial Catalog=InventoryMangement;Integrated Security=true");

    static SqlDataAdapter daAd;
    static DataTable datbl;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        else if (!IsPostBack)
        {
            TextBox3.ReadOnly = txtQuantity1.ReadOnly = txtQuantity2.ReadOnly = true;
            btnsell.Enabled = false;
           

            FillGridView();
        }
    }
    //void FillGridView()
    //{
    //if (sqlcon.State == ConnectionState.Closed)
    //    sqlcon.Open();


    //string query = "select Product.ProductName,Supplier.CompanyName, Sum(Purchase.Quantity) as total from Product inner join Purchase on Product.ProductId = Purchase.ProductId inner join Supplier on Purchase.SupplierId = Supplier.SupplierId group by product.ProductName, Supplier.CompanyName";
    //SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlcon);

    //DataSet ds = new DataSet();
    //sqlDa.Fill(ds);

    //searchGrid.DataSource = sqlDa;
    //searchGrid.DataBind();
    //sqlcon.Close();

    //}
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetSearch(string prefixText)
    {
       
        string str = "select Product.ProductName,Supplier.CompanyName, Sum(Purchase.Quantity) as total from Product inner join Purchase on Product.ProductId = Purchase.ProductId inner join Supplier on Purchase.SupplierId = Supplier.SupplierId where Product.ProductName like '" + prefixText + "%' group by product.ProductName, Supplier.CompanyName";
        daAd = new SqlDataAdapter(str, sqlcon);
        datbl = new DataTable();
        daAd.Fill(datbl);
        List<string> Output = new List<string>();
        for (int i = 0; i < datbl.Rows.Count; i++)
            Output.Add(datbl.Rows[i][0].ToString());
        return Output;
    }

    protected void btnsell_Click(object sender, EventArgs e)
    {
       
        

    }

    protected void btndelete_Click(object sender, EventArgs e)
    {

    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        this.clear();        
    }
    protected void clear()
    {
        TextBox3.ReadOnly = txtQuantity1.ReadOnly = txtQuantity2.ReadOnly = true;
        TextBox2.Text = TextBox3.Text = txtQuantity1.Text = txtQuantity2.Text = "";
        btnsell.Enabled = false;
    }

    protected void Search()
    {
        if (sqlcon.State == ConnectionState.Closed)
            sqlcon.Open();


        string query = "select Product.ProductName,Supplier.CompanyName, Sum(Purchase.Quantity) as total from Product inner join Purchase on Product.ProductId = Purchase.ProductId inner join Supplier on Purchase.SupplierId = Supplier.SupplierId where Product.ProductName like '" + TextBox2.Text + "%' group by product.ProductName, Supplier.CompanyName";
        SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlcon);

        DataSet ds = new DataSet();
        sqlDa.Fill(ds);

        searchGrid.DataSource = ds;
        searchGrid.DataBind();
        sqlcon.Close();

        txtQuantity1.ReadOnly = txtQuantity2.ReadOnly = true;
        btnsell.Enabled = true;
        
    }
    //protected void lnk_onClick(object sender, EventArgs e)
    //{
    //    string ProductName = (sender as LinkButton).CommandArgument;
    //    if (sqlcon.State == ConnectionState.Closed)
    //        sqlcon.Open();

    //   // string query2 = "select Supplier.CompanyName, Sum(Purchase.Quantity) as total from Product inner join Purchase on Product.ProductId = Purchase.ProductId inner join Supplier on Purchase.SupplierId = Supplier.SupplierId group by product.ProductName, Supplier.CompanyName";

    //    SqlDataAdapter sqlDa = new SqlDataAdapter("RetriveSearch", sqlcon);
    //    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
    //    sqlDa.SelectCommand.Parameters.AddWithValue("@ProductName", ProductName);
    //    DataTable dtbl = new DataTable();
    //    sqlDa.Fill(dtbl);
    //    sqlcon.Close();
    //   // hfPurchaseId.Value = PurchaseId.ToString();
    //    TextBox3.Text = dtbl.Rows[0]["CompanyName"].ToString();
    //    txtQuantity1.Text = dtbl.Rows[0]["total"].ToString();
        
    //    btnsave.Text = "Save";
    //    btndelete.Enabled = true;


    //}



    protected void TextBox2_TextChanged(object sender, EventArgs e)
    {
        searchGrid.Visible = true;
        this.Search();
    }

    protected void searchGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = searchGrid.SelectedRow;
        TextBox3.Text = row.Cells[1].Text;
        txtQuantity1.Text = row.Cells[2].Text;

        txtQuantity2.ReadOnly = false;
    }

    protected void btnsell_Click1(object sender, EventArgs e)
    {
        if (Convert.ToInt32(txtQuantity1.Text) >= Convert.ToInt32(txtQuantity2.Text))
        {

            if (sqlcon.State == ConnectionState.Closed)
                sqlcon.Open();
            String updateQuery = "Update Purchase set Purchase.Quantity = Purchase.Quantity -'" + txtQuantity2.Text + "' from Product inner join Purchase on Product.ProductId = Purchase.ProductId inner join Supplier on Purchase.SupplierId = Supplier.SupplierId Where Product.ProductName = '" + TextBox2.Text + "' and Supplier.CompanyName = '" + TextBox3.Text + "'";
            SqlCommand cmd = new SqlCommand(updateQuery,sqlcon);
            cmd.ExecuteNonQuery();

            sqlcon.Close();

            SaveSell();
            this.clear();
            //searchGrid.Visible=false;
            lblsuccessmassage.Text = "Sell Successfully!";
            lblerrormessage.Text = "";
        }
        else
            lblerrormessage.Text = "Sell Quantity does not available.\n Please Check Available Quantity";

    }

    protected void SaveSell()
    {
        if (sqlcon.State == ConnectionState.Closed)
            sqlcon.Open();
        string insertquery = "insert into Sales (SalesProductName, SalesCompanyName, SalesQuantity) values ('"+TextBox2.Text+ "','"+TextBox3.Text+ "','"+txtQuantity2.Text+"')";
        SqlCommand cmd1 = new SqlCommand(insertquery, sqlcon);
        cmd1.ExecuteNonQuery();
        sqlcon.Close();
        //lblsuccessmassage.Text = "Saved Successfully";
       FillGridView();
      
        

    }
    void FillGridView()
    {
        if (sqlcon.State == ConnectionState.Closed)
            sqlcon.Open();
        string sales_grid_query = "Select SalesProductName, SalesCompanyName, SalesQuantity from Sales";
        SqlCommand cmd2 = new SqlCommand(sales_grid_query, sqlcon);
        SqlDataAdapter sda = new SqlDataAdapter(cmd2);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        sqlcon.Close();
        SalesGrid.DataSource = dt;
        SalesGrid.DataBind();

    }
}