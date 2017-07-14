using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    public void Page_Load(object sender, EventArgs e)
    {

        if (Page.PreviousPage != null)
        {
            TextBox inputUrl = (TextBox)Page.PreviousPage.FindControl("url");
            string url = inputUrl.Text.ToString();

            if (url != null)
            {
                Uri urlBase = new Uri(url);
                string urlLink = urlBase.Host;

                if (existeBuscaParaEsteHost(urlLink))
                {
                    DataTable query = buscarDados(urlLink);

                    DataRow row = query.Rows[0];
                    nameserver.Text = row["Nameservers"].ToString();
                    ipaddress.Text = row["Ip"].ToString();
                    hostname.Text = row["Hostname"].ToString(); ;
                }

                else
                {
                    nameserver.Text = RetornaNomeServidor(urlLink);
                    ipaddress.Text = RetornaIPServidor(urlLink);
                    hostname.Text = urlLink;
                }

            }
        }

    }

    public DataTable buscarDados(string urlLink)
    {
        string connString = ConfigurationManager.ConnectionStrings["conexao"].ConnectionString;
        SqlConnection conn = null;
        DataTable dados = new DataTable();

        try
        {
            conn = new SqlConnection(connString);
            conn.Open();

            using (var dt = new DataTable())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    using (var sda = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT * FROM umbler WHERE Hostname = @hostname";
                        cmd.Parameters.AddWithValue("@hostname", urlLink);
                        int rows = cmd.ExecuteNonQuery();
                        sda.Fill(dt);
                        dados = dt;
                    }
                }

            }

        }
        catch (Exception erroBanco)
        {
            new Exception("Não foi possível salvar os dados.", erroBanco);
        }

        return dados;

    }

    public bool existeBuscaParaEsteHost(string urlLink)
    {

        string connString = ConfigurationManager.ConnectionStrings["conexao"].ConnectionString;
        SqlConnection conn = null;
        bool existe = false;

        try
        {
            conn = new SqlConnection(connString);
            conn.Open();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM umbler WHERE Hostname = @hostname";
                cmd.Parameters.AddWithValue("@hostname", urlLink);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                    existe = true;
            }

        }
        catch (Exception erroBanco)
        {
            new Exception("Não foi possível salvar os dados.", erroBanco);
        }

        return existe;
    }

    public string RetornaIPServidor(string url)
    {
        string ipAddress = "";

        try
        {
            List<System.Net.IPAddress> listaIps = Dns.GetHostAddresses(url).ToList();
            ipAddress = listaIps[0].ToString();
        }

        catch (Exception e)
        {
            new Exception("Não foi possível encontrar o IP do link.", e);
        }

        return ipAddress;
    }

    public string RetornaNomeServidor(string url)
    {
        string nameServer = "";

        try
        {
            IPHostEntry hostInfo = Dns.GetHostEntry(url);
            var response = JHSoftware.DnsClient.Lookup(hostInfo.HostName, JHSoftware.DnsClient.RecordType.NS);
            foreach (var records in response.AnswerRecords)
            {
                nameServer += "<li>" + records.Name + " | " + records.Type + " | " + records.TTL + " | " + records.Data + "</li>";
            }

        }

        catch (Exception e)
        {
            new Exception("Não foi possível encontrar o nameserver.", e);
        }

        return nameServer;
    }

    public void Salvar(object sender, EventArgs e)
    {
        string dadosNameservers = nameserver.Text;
        string dadosHostname = hostname.Text;
        string dadosIp = ipaddress.Text;

        string connString = ConfigurationManager.ConnectionStrings["conexao"].ConnectionString;
        SqlConnection conn = null;
        try
        {
            conn = new SqlConnection(connString);
            conn.Open();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO umbler (Hostname, Nameservers, Ip) VALUES (@hostname, @nameservers, @ip)";
                cmd.Parameters.AddWithValue("@hostname", dadosHostname);
                cmd.Parameters.AddWithValue("@nameservers", dadosNameservers);
                cmd.Parameters.AddWithValue("@ip", dadosIp);
                int rowsAffected = cmd.ExecuteNonQuery();
            }

            Response.Redirect("Default.aspx", false);
        }
        catch (Exception erroBanco)
        {
            new Exception("Não foi possível salvar os dados.", erroBanco);
        }
    }
}