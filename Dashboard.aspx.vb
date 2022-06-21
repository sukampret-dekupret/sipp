Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Text
Imports System.Data.Sql
Imports FusionCharts
Imports FusionCharts.Charts
Imports FusionCharts.DataEngine
Imports FusionCharts.Visualization
Imports System.Data.SqlClient

Partial Class Dashboard
    Inherits System.Web.UI.Page

    Private constr As String = ConfigurationManager.ConnectionStrings("conPersediaan").ToString()
    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            If Session("ROLEID") = 1 Then
                panelunitpermohonan.Visible = True
                Dim ChartData As DataTable = New DataTable()
                ChartData = CType(sqlPermohonanTerbanyak.Select(DataSourceSelectArguments.Empty), DataView).Table
                sqlPermohonanTerbanyak.Dispose()
                Dim source As StaticSource = New StaticSource(ChartData)
                Dim model As DataModel = New DataModel()
                Try

                    model.DataSources.Add(source)
                    Dim bar As Charts.BarChart = New Charts.BarChart("bar_chart")
                    bar.Width.Percentage(100)
                    ''bar.Height.Percentage(50)
                    bar.Data.Source = model
                    bar.Caption.Text = "10 Permohonan Barang Terbanyak sampai dengan 2020"
                    bar.Caption.Alignment = CaptionObject.CaptionAlignment.CENTER
                    bar.ThemeName = FusionChartsTheme.ThemeName.FUSION
                    liPermohonan.Text = bar.Render()
                Catch ex As Exception

                Finally
                    model.Dispose()
                End Try
                TampilkanBarUnit1()
            ElseIf Session("ROLEID") = 10 Or Session("ROLEID") = 0 Or Session("ROLEID") = 2 Then
                TampilkanBarKomparasi()
                TampilkanBarAdmin1()
            ElseIf Session("ROLEID") = 0 Then
                TampilkanBarKomparasi()
                TampilkanBarAdmin1()


            End If



        End If
    End Sub

    Private Sub TampilkanBarKomparasi()
        Dim ChartData As DataTable = New DataTable()
        ChartData = CType(sqlKompareMohonPenuhi.Select(DataSourceSelectArguments.Empty), DataView).Table
        sqlKompareMohonPenuhi.Dispose()
        Dim source As StaticSource = New StaticSource(ChartData)
        Dim model As DataModel = New DataModel()
        Try

            model.DataSources.Add(source)
            Dim bar As Charts.ColumnChart = New Charts.ColumnChart("bar_chart2")
            bar.Width.Percentage(100)
            ''bar.Height.Percentage(50)
            bar.Data.Source = model
            ''bar.Caption.Text = "PERBANDING PERMOHONAN DAN PEMENUHAN"
            bar.ThreeD = True
            bar.Labels.Show = True
            bar.Values.Show = True
            bar.Values.ShowLimits = False
            bar.Values.ShowDivLineValues = True
            bar.Labels.Display = LabelFormat.DisplayType.ROTATE
            bar.Labels.Slant = True
            bar.Caption.Alignment = CaptionObject.CaptionAlignment.CENTER
            bar.ThemeName = FusionChartsTheme.ThemeName.FUSION

            bar.Caption.Text = "PERBANDING PERMOHONAN DAN PEMENUHAN"
            bar.SubCaption.Text = "2020"
            bar.XAxis.Text = "PERMOHONAN"
            bar.YAxis.Text = "DIPENUHI"
            liPerbanding.Text = bar.Render()
        Catch ex As Exception

        Finally
            model.Dispose()
        End Try
    End Sub

    Private Sub TampilkanBarUnit1()
        Dim ChartData As DataTable = New DataTable()
        ChartData = CType(sqlDasborUnit1.Select(DataSourceSelectArguments.Empty), DataView).Table
        sqlDasborUnit1.Dispose()
        Dim source As StaticSource = New StaticSource(ChartData)
        Dim model As DataModel = New DataModel()
        Try

            model.DataSources.Add(source)
            Dim OverlappedColumn As Charts.ColumnChart = New Charts.ColumnChart("bar_unit1")
            OverlappedColumn.Width.Percentage(100)
            ''bar.Height.Percentage(50)
            OverlappedColumn.Data.Source = model
            ''bar.Caption.Text = "PERBANDING PERMOHONAN DAN PEMENUHAN"
            OverlappedColumn.Overlapped = False

            OverlappedColumn.ThreeD = False
            OverlappedColumn.Data.Source = model
            OverlappedColumn.PaletteColors("#ED5565", "#F8AC59")
            OverlappedColumn.Labels.Show = True
            OverlappedColumn.Values.Show = True
            OverlappedColumn.Values.ShowLimits = False
            OverlappedColumn.Values.ShowDivLineValues = True
            OverlappedColumn.Labels.Display = LabelFormat.DisplayType.ROTATE
            OverlappedColumn.Labels.Slant = True
            OverlappedColumn.Export.Enabled = True
            OverlappedColumn.Export.ExportedFileName = "GRAFIK_KELUAR_MASUK_BARANG_PERSEDIAAN"
            OverlappedColumn.Export.Action = Exporter.ExportAction.DOWNLOAD


            OverlappedColumn.Caption.Text = "GRAFIK KELUAR MASUK BARANG PERSEDIAAN "
            OverlappedColumn.SubCaption.Text = Year(DateTime.Now).ToString()
            OverlappedColumn.XAxis.Text = "Bulan"
            OverlappedColumn.YAxis.Text = "Jumlah"


            OverlappedColumn.Width.Percentage(100)
            OverlappedColumn.Height.Pixel(600)
            OverlappedColumn.ThemeName = FusionChartsTheme.ThemeName.FUSION
            liDasborUnit.Text = OverlappedColumn.Render()
        Catch ex As Exception

        Finally
            model.Dispose()
        End Try
    End Sub



    'Private Sub GetData()
    '    Dim table As DataTable = New DataTable()

    '    Using conn As SqlConnection = New SqlConnection(constr)
    '        Dim sql As String = "SELECT * FROM Students"

    '        Using cmd As SqlCommand = New SqlCommand(sql, conn)

    '            Using ad As SqlDataAdapter = New SqlDataAdapter(cmd)
    '                ad.Fill(table)
    '            End Using
    '        End Using
    '    End Using

    'End Sub

    Private Sub TampilkanBarAdmin1()
        Dim ChartData As DataTable = New DataTable()
        ChartData = CType(sqlBoardAdmin1.Select(DataSourceSelectArguments.Empty), DataView).Table
        sqlBoardAdmin1.Dispose()
        Dim source As StaticSource = New StaticSource(ChartData)
        Dim model As DataModel = New DataModel()
        Try

            model.DataSources.Add(source)
            Dim OverlappedColumn As Charts.ColumnChart = New Charts.ColumnChart("bar_admin1")
            OverlappedColumn.Width.Percentage(100)
            ''bar.Height.Percentage(50)
            OverlappedColumn.Data.Source = model
            ''bar.Caption.Text = "PERBANDING PERMOHONAN DAN PEMENUHAN"
            OverlappedColumn.Overlapped = False

            OverlappedColumn.ThreeD = True
            OverlappedColumn.Data.Source = model
            OverlappedColumn.PaletteColors("#ED5565", "#F8AC59")
            OverlappedColumn.Labels.Show = True
            OverlappedColumn.Values.Show = True
            OverlappedColumn.Values.ShowLimits = False
            OverlappedColumn.Values.ShowDivLineValues = True
            OverlappedColumn.Labels.Display = LabelFormat.DisplayType.ROTATE
            OverlappedColumn.Labels.Slant = True
            OverlappedColumn.Export.Enabled = True
            OverlappedColumn.Export.ExportedFileName = "GRAFIK_KELUAR_MASUK_BARANG_PERSEDIAAN"
            OverlappedColumn.Export.Action = Exporter.ExportAction.DOWNLOAD


            OverlappedColumn.Caption.Text = "GRAFIK KELUAR MASUK BARANG PERSEDIAAN "
            OverlappedColumn.SubCaption.Text = Year(DateTime.Now).ToString()
            OverlappedColumn.XAxis.Text = "Bulan"
            OverlappedColumn.YAxis.Text = "Jumlah"


            OverlappedColumn.Width.Percentage(100)
            OverlappedColumn.Height.Pixel(600)
            OverlappedColumn.ThemeName = FusionChartsTheme.ThemeName.FUSION
            liDaborAdmin1.Text = OverlappedColumn.Render()
        Catch ex As Exception

        Finally
            model.Dispose()
        End Try
    End Sub

End Class
