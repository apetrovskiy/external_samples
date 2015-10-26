using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

public partial class _Default : System.Web.UI.Page
    {
    protected void Page_Load(object sender, EventArgs e)
        {
        if (!IsPostBack)
            {
            List<Items> dataList = new List<Items>();

            dataList.Add(new Items("Column 1", 100, "Male", 25));
            dataList.Add(new Items("Column 2", 150, "Male", 37));
            dataList.Add(new Items("Column 3", 250, "Female", 25));
            dataList.Add(new Items("Column 4", 400, "Female", 35));
            dataList.Add(new Items("Column 5", 450, "Male", 35));
            dataList.Add(new Items("Column 6", 460, "Female", 26));
            dataList.Add(new Items("Column 7", 470, "Female", 30));
            dataList.Add(new Items("Column 8", 500, "Male", 31));
            dataList.Add(new Items("Column 9", 550, "Male", 30));
            dataList.Add(new Items("Column 10", 600, "Female", 33));

            JavaScriptSerializer jss = new JavaScriptSerializer();

            ClientScript.RegisterStartupScript(this.GetType(), "TestInitPageScript",
                string.Format("<script type=\"text/javascript\">drawVisualization({0},'{1}','{2}','{3}');</script>",
                jss.Serialize(dataList),
                "Text Example",
                "Name,Value,Gender,Age",
                "--Select--"));
            }

        }
    public class Items
        {
        #region Internal Members

        private string _ColumnName = "";
        private double _Value1 = 0;
        private string _Value2 = null;
        private int _Value3 = 0;

        #endregion

        #region Public Properties

        public string ColumnName
            {
            get { return _ColumnName; }
            set { _ColumnName = value; }
            }
        public double Value1
            {
            get { return _Value1; }
            set { _Value1 = value; }
            }
        public string Value2
            {
            get { return _Value2; }
            set { _Value2 = value; }
            }
        public int Value3
            {
            get { return _Value3; }
            set { _Value3 = value; }
            }

        #endregion

        #region Constructors

        public Items(string columnName, double value1, string value2, int value3)
            {
            _ColumnName = columnName;
            _Value1 = value1;
            _Value2 = value2;
            _Value3 = value3;
            }

        #endregion
        }
    }