﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class admin_Default : System.Web.UI.Page
{
    
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddCountryDataBind();
        }
    }

    protected void ddCountryDataBind()
    {
        try
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            string sql_query = "select * from country";
            SqlCommand cmd = new SqlCommand(sql_query, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count > 0 && dt != null)
            {
                ddcountry.DataSource = dt;
                ddcountry.DataValueField = "country_id";
                ddcountry.DataTextField = "country_name";
                ddcountry.DataBind();
                ddcountry.Items.Insert(0, "Select country");
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
            Label1.ForeColor = System.Drawing.Color.Red;
        }
        finally
        {
            con.Close();
        }
    }

    protected void ddcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddStateDataBind();
    }

    protected void ddStateDataBind()
    {
        try
        {
            if (con.State != ConnectionState.Open)
                con.Open();

            string sql_query = "select * from state where country_id = @country_id and status= @status";
            SqlCommand cmd = new SqlCommand(sql_query, con);
            cmd.Parameters.AddWithValue("@country_id", ddcountry.SelectedIndex.ToString());
            cmd.Parameters.AddWithValue("@status", 1);

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);

            if (dt.Rows.Count > 0 && dt != null)
            {
                ddstate.DataSource = dt;
                ddstate.DataValueField = "state_id";
                ddstate.DataTextField = "state_name";
                ddstate.DataBind();
                ddstate.Items.Insert(0, "Select state");
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
            Label1.ForeColor = System.Drawing.Color.Red;
        }
        finally
        {
            con.Close();
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        Adddistrict();

    }
    protected void Adddistrict()
    {
        try
        {
            if (ddcountry.SelectedValue == "")
                throw new Exception("Please select Country");

            if (ddstate.SelectedValue == "")
                throw new Exception("Please select state");

          

            if (txtdistrict.Text == "")
                throw new Exception("Please Enter District");

            if (con.State != ConnectionState.Open)
                con.Open();



            string sql_query = "insert into city (district_name, state_id , status) values(@district_name, @state_id , @status)";
            SqlCommand cmd = new SqlCommand(sql_query, con);
            cmd.Parameters.AddWithValue("@district_name", txtdistrict.Text.ToString());
            cmd.Parameters.AddWithValue("@state_id", ddstate.SelectedValue.ToString());

            cmd.Parameters.AddWithValue("@status", 1);

            if (cmd.ExecuteNonQuery() > 0)
                Label1.Text = "District  added";
            Label1.ForeColor = System.Drawing.Color.Green;
            clear();

        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
            Label1.ForeColor = System.Drawing.Color.Red;
            clear();
        }
        finally
        {
            con.Close();
        }
    }
     
    protected void clear()
    {
        txtdistrict.Text = "";
      
       ddstate.SelectedIndex= 0;
        ddcountry.SelectedIndex = 0;
    }


  
}
