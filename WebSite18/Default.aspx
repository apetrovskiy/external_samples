<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="//www.google.com/jsapi"></script>
    <script type="text/javascript">
        google.load('visualization', '1.1', { packages: ['controls'] });
    </script>
    <script type="text/javascript">

        function drawVisualization(dataValues, chartTitle, columnNames, categoryCaption) {
            if (dataValues.length < 1)
                return;

            var data = new google.visualization.DataTable();
            data.addColumn('string', columnNames.split(',')[0]);
            data.addColumn('number', columnNames.split(',')[1]);
            data.addColumn('string', columnNames.split(',')[2]);
            data.addColumn('number', columnNames.split(',')[3]);

            for (var i = 0; i < dataValues.length; i++) {
                data.addRow([dataValues[i].ColumnName, dataValues[i].Value1, dataValues[i].Value2, dataValues[i].Value3]);
            }
                       
            var categoryPicker = new google.visualization.ControlWrapper({
                'controlType': 'CategoryFilter',
                'containerId': 'CategoryPickerContainer',
                'options': {
                    'filterColumnLabel': columnNames.split(',')[2],
                    'ui': {
                        'labelStacking': 'horizontal',
                        'allowTyping': false,
                        'allowMultiple': false,
                        'caption': categoryCaption,
                        'label': columnNames.split(',')[2]
                    }
                }
            });
          
            var pie = new google.visualization.ChartWrapper({
                'chartType': 'PieChart',
                'containerId': 'PieChartContainer',
                'options': {
                    'width': 600,
                    'height': 350,
                    'legend': 'right',
                    'title': chartTitle,
                    'chartArea': { 'left': 50, 'top': 15, 'right': 0, 'bottom': 0 },
                    'pieSliceText': 'label',
                    'tooltip': { 'text': 'percentage' }
                },
                'view': { 'columns': [0, 1] }
            });
           
            var table = new google.visualization.ChartWrapper({
                'chartType': 'Table',
                'containerId': 'TableContainer',
                'options': {
                    'width': '300px'
                }
            });
          
            var slider = new google.visualization.ControlWrapper({
                'controlType': 'NumberRangeFilter',
                'containerId': 'SliderContainer',
                'options': {
                    'filterColumnLabel': columnNames.split(',')[3],
                    'ui': { 'labelStacking': 'horizontal' }
                }
            });

            new google.visualization.Dashboard(document.getElementById('PieChartExample')).bind([categoryPicker, slider], [pie, table]).draw(data);
        }
         
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div style="font-size:50px;" align="center">Google-pi chart Example</div></br></br></br>
        <div id="PieChartExample">
            <table>
                <tr style='vertical-align: top'>
                    <td>
                        <div id="CategoryPickerContainer" align="center">
                        </div></br>
                        <div id="SliderContainer" align="center"></br>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="float: left;" id="PieChartContainer">
                        </div>
                        <div style="float: left;" id="TableContainer">
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
