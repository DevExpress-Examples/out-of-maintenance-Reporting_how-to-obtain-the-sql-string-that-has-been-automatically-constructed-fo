#region #Reference
using System;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;
// ...
#endregion #Reference

namespace TableQueryGetSql {
    public partial class XtraReport1 : DevExpress.XtraReports.UI.XtraReport {
        public XtraReport1() {
            InitializeComponent();
        }

        #region #Code
        private void XtraReport1_DataSourceDemanded(object sender, EventArgs e) {
            // Create a new SQL data source and specify its connection parameters.
            var sqlDataSource = new SqlDataSource(new Access97ConnectionParameters() {
                FileName = @"..//..//nwind.mdb"
            });

            // Create a query and assign it to the data source.
            TableQuery tableQuery = new TableQuery("Categories");
            TableInfo table = new TableInfo("Categories");
            table.SelectColumns("CategoryID", "CategoryName");
            tableQuery.Tables.Add(table);
            sqlDataSource.Queries.Add(tableQuery);

            //  Open the connection to the data source.
            sqlDataSource.Connection.Open();
            // Obtain the database schema of the connected data source.
            var sql = tableQuery.GetSql(sqlDataSource.Connection.GetDBSchema());

            // Assign the data source to the report.
            this.DataSource = sqlDataSource;
            this.DataMember = "Categories";
        }
        #endregion #Code


        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
            // Display the obtained data in the report.
            this.xrLabel1.DataBindings.AddRange(new XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Categories.CategoryName")});
        }

    }
}
