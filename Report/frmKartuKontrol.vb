Imports System.Data.OleDb
Imports MySql.Data.MySqlClient
Imports JS_FlatMaterialUI.CustomMessageBox
Public Class frmKartuKontrol

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub frmKartuKontrol_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GrdKartu.Visible = False
        CRV.ReportSource = Nothing
    End Sub

    Private Sub txtnoKartu_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtnoKartu.TextChanged
        If txtnoKartu.Text = "" Then
            GrdKartu.Visible = False
        Else
            GrdKartu.Visible = True
            TampilPemilik()
        End If
    End Sub
    Sub TampilPemilik()
        Try
            tutupkoneksi()
            cekkoneksi()
            Dim table As New DataTable()
            Dim Adapter As New MySqlDataAdapter(" SELECT kartu.no_kartu, pelanggan.nm_po, kartu.dari_tanggal, kartu.sampai_tanggal FROM kartu INNER JOIN pelanggan on pelanggan.kd_po=kartu.kd_po  where " & _
            "kartu.no_kartu like '" & Trim(txtnoKartu.Text) & "%' order by no_kartu ASC", conn)
            Adapter.Fill(table)
            GrdKartu.DataSource = table
            GrdKartu.Refresh()
            tutupkoneksi()
            With Me.GrdKartu
                .Columns(0).HeaderText = "Nomor Kartu"
                .Columns(1).HeaderText = "Nama PO"
                .Columns(2).HeaderText = "Dari Tanggal"
                .Columns(3).HeaderText = "Sampai Tanggal"

                .Columns(0).Width = 75
                .Columns(1).Width = 100
                .Columns(2).Width = 115
                .Columns(3).Width = 100

            End With
        Catch ex As Exception
            MsgBox("Terjadi kesalahan! " & ex.Message)
        End Try
    End Sub

    Private Sub GrdKartu_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GrdKartu.CellContentClick
        Try
            txtnoKartu.Text = GrdKartu.CurrentRow.Cells(0).Value
            GrdKartu.Visible = False
        Catch ex As Exception
            MsgBox("Terjadi kesalahan! " & ex.Message)
        End Try
    End Sub

    Private Sub GrdKartu_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GrdKartu.CellContentDoubleClick
        Try
            txtnoKartu.Text = GrdKartu.CurrentRow.Cells(0).Value
            GrdKartu.Visible = False
        Catch ex As Exception
            MsgBox("Terjadi kesalahan! " & ex.Message)
        End Try
    End Sub

    Private Sub BtnCetak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCetak.Click
        If txtnoKartu.Text = "" Then
            MsgBox("Silahkan cari telebih dahulu data yang akan di cetak... ")
            txtnoKartu.Focus()
        Else
            CRV.SelectionFormula = "({command.no_kartu}) like'" & txtnoKartu.Text & "'"
            RptKartu1.Load("RptKartu.rpt")
            CRV.ReportSource = RptKartu1
            CRV.RefreshReport()
        End If

    End Sub
End Class