using System.Data;
using System.Data.SqlClient;

namespace CTM.Win.Util
{
    public class DataSouceHelper
    {
        public static DataSet GenerateMasterDetail(string connectionString, string masterSql, string detailSql, string relationName, string keyColumnName, string foreignKeyColumnName)
        {
            DataSet dsResult = new DataSet();

            try
            {
                SqlDataAdapter sdaMaster = new SqlDataAdapter(masterSql, connectionString);
                DataTable dtMaster = new DataTable("Master");
                sdaMaster.Fill(dtMaster);

                SqlDataAdapter sdaDetail = new SqlDataAdapter(detailSql, connectionString);
                DataTable dtDetail = new DataTable("Detail");
                sdaDetail.Fill(dtDetail);

                dsResult.Tables.Add(dtMaster);
                dsResult.Tables.Add(dtDetail);

                DataColumn keyColumn = dtMaster.Columns[keyColumnName];
                DataColumn foreignKeyColumn = dtDetail.Columns[foreignKeyColumnName];

                dsResult.Relations.Add(relationName, keyColumn, foreignKeyColumn);

                return dsResult;
            }
            catch
            {
                return null;
            }
        }
    }
}