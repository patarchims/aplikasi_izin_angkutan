Imports System.Data.OleDb
Imports MySql.Data.MySqlClient
Imports JS_FlatMaterialUI.CustomMessageBox
Public Class Pelanggan

    Private Sub Pelanggan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Agama()
        Kelamin()
        TidakAktif()
        TampilGrid()
        BtnEdit.Enabled = False

    End Sub
    Private Sub BtnTambah_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnTambah.MouseEnter
        BtnTambah.BackColor = Color.FromArgb(217, 245, 247)
        BtnTambah.ForeColor = Color.Black
    End Sub

    Private Sub BtnTambah_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnTambah.MouseLeave
        BtnTambah.BackColor = Color.FromArgb(58, 87, 89)
        BtnTambah.ForeColor = Color.White
    End Sub

    Private Sub BtnSimpan_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSimpan.MouseEnter
        BtnSimpan.BackColor = Color.FromArgb(217, 245, 247)
        BtnSimpan.ForeColor = Color.Black
    End Sub

    Private Sub BtnSimpan_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSimpan.MouseLeave
        BtnSimpan.BackColor = Color.FromArgb(58, 87, 89)
        BtnSimpan.ForeColor = Color.White
    End Sub

    Private Sub BtnBatal_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBatal.MouseEnter
        BtnBatal.BackColor = Color.FromArgb(217, 245, 247)
        BtnBatal.ForeColor = Color.Black
    End Sub

    Private Sub BtnBatal_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBatal.MouseLeave
        BtnBatal.BackColor = Color.FromArgb(58, 87, 89)
        BtnBatal.ForeColor = Color.White
    End Sub

    Private Sub BtnHapus_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnHapus.MouseEnter
        BtnHapus.BackColor = Color.FromArgb(217, 245, 247)
        BtnHapus.ForeColor = Color.Black
    End Sub

    Private Sub BtnHapus_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnHapus.MouseLeave
        BtnHapus.BackColor = Color.FromArgb(58, 87, 89)
        BtnHapus.ForeColor = Color.White
    End Sub
    Sub Kelamin()
        Try
            With cbokelamin
                .Items.Clear()
                .Items.Add("laki-Laki")
                .Items.Add("Perempuan")
            End With
        Catch ex As Exception
            MsgBox("Terjadi kesalahan! " & ex.Message)
        End Try
    End Sub
    Sub Agama()
        Try
            With cboagama
                .Items.Clear()
                .Items.Add("Kristen Protestan")
                .Items.Add("Katolik")
                .Items.Add("Islam")
                .Items.Add("Konh Hu Cu")
                .Items.Add("Buddha")
                .Items.Add("Hindu")
            End With

        Catch ex As Exception
            MsgBox("Terjadi kesalahan! " & ex.Message)
        End Try
    End Sub

    Private Sub BtnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBatal.Click
        Bersih()
        txtnama.Enabled = False
        TidakAktif()
        BtnTambah.Enabled = True
    End Sub
    Sub Bersih()
        lblkode.Text = ""
        txtnama.Text = ""
        txtalamat.Text = ""
        Agama()
        Kelamin()
        txttelpone.Text = ""
        txtpemilik.Text = ""
    End Sub
    Sub Aktif()
        BtnBatal.Enabled = True
        BtnSimpan.Enabled = True
        BtnHapus.Enabled = True
        txtnama.Enabled = True
        txtpemilik.Enabled = True
        cbokelamin.Enabled = True
        cboagama.Enabled = True
        txtalamat.Enabled = True
        txttelpone.Enabled = True
    End Sub
    Sub TidakAktif()
        txtnama.Enabled = False
        txtpemilik.Enabled = False
        cbokelamin.Enabled = False
        cboagama.Enabled = False
        txttelpone.Enabled = False
        txtalamat.Enabled = False
        BtnBatal.Enabled = False
        BtnHapus.Enabled = False
        BtnSimpan.Enabled = False
    End Sub
    Sub TampilGrid()
        Try
            tutupkoneksi()
            cekkoneksi()
            Dim table As New DataTable()
            Dim Adapter As New MySqlDataAdapter(" Select * FROM pelanggan  where " & _
            "nm_po like '" & Trim(txtcari.Text) & "%' order by kd_po", conn)
            Adapter.Fill(table)
            GrdView.DataSource = table
            GrdView.Refresh()
            tutupkoneksi()
            AturGrid()
            With GrdView
                .Columns(0).HeaderText = "Kode PO"
                .Columns(1).HeaderText = "Nama PO"
                .Columns(2).HeaderText = "Pemilik"
                .Columns(3).HeaderText = "Alamat"
                .Columns(4).HeaderText = "Kelamin"
                .Columns(5).HeaderText = "Agama"
                .Columns(6).HeaderText = "Telephone / Hp"
            End With
        Catch ex As Exception
            MsgBox("Terjadi kesalahan! " & ex.Message)
        End Try
    End Sub
    Sub AturGrid()
        With GrdView
            .Columns(0).Width = 100
            .Columns(1).Width = 150
            .Columns(2).Width = 170
            .Columns(3).Width = 200
            .Columns(4).Width = 100
            .Columns(5).Width = 100
            .Columns(6).Width = 100
        End With
    End Sub
    Sub KodeAuto()
        tutupkoneksi()
        lblkode.Show()
        lblkode.Enabled = True
        cekkoneksi()
        Dim strSementara As String = ""
        Dim strIsi As String = ""
        sqlcommand = New MySqlCommand("select * from pelanggan order by kd_po desc", conn)
        Dr = sqlcommand.ExecuteReader
        If Dr.Read Then
            strSementara = Mid(Dr.Item("kd_po"), 4, 5)
            strIsi = Val(strSementara) + 1
            lblkode.Text = "PO-" + Mid("00000", 1, 5 - strIsi.Length) & strIsi
        Else
            lblkode.Text = "PO-00001"
        End If
        txtnama.Focus()
    End Sub

    Private Sub BtnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSimpan.Click
        Try
            If txtnama.Text = "" Or txtalamat.Text = "" Or txtpemilik.Text = "" Or txttelpone.Text = "" Or cboagama.Text = "" Or cbokelamin.Text = "" Then
                ShowMessageBox("Data Isian Harus Lengkap", "Critical", MessageBoxType.Critical)
            Else
                tutupkoneksi()
                cekkoneksi()
                sql = "SELECT * FROM pelanggan  WHERE kd_po='" & Trim(lblkode.Text) & "'"
                sqlcommand = New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
                Dr.Close()
                Dr = sqlcommand.ExecuteReader()
                Dr.Read()
                If Not Dr.HasRows Then
                    sql = "INSERT INTO pelanggan values ('" & lblkode.Text & "','" & _
                         txtnama.Text & "','" & txtpemilik.Text & "','" & txtalamat.Text & "','" & cbokelamin.Text & _
                         "','" & cboagama.Text & "','" & txttelpone.Text & "')"
                Else
                    sql = "UPDATE pelanggan set nm_po='" & txtnama.Text & "',pemilik='" & txtpemilik.Text & "',alamat='" & txtalamat.Text & "', kelamin='" & cbokelamin.Text & "',agama='" & cboagama.Text & "',telp='" & txttelpone.Text & "' WHERE kd_po='" & Trim(lblkode.Text) & "'"
                End If
                sqlcommand = New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
                tutupkoneksi()
                cekkoneksi()
                sqlcommand.ExecuteNonQuery()
                Bersih()
                tutupkoneksi()
                TampilGrid()
                ShowMessageBox("Data Berhasil Disimpan.", "Infomasi!", MessageBoxType.Information)
                TidakAktif()
                BtnTambah.Enabled = True
            End If
        Catch ex As Exception
            MsgBox("Terjadi kesalahan! " & ex.Message)
        End Try

    End Sub

    Private Sub BtnHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnHapus.Click
        Try
            Dim Message As DialogResult
            tutupkoneksi()
            cekkoneksi()
            sql = "SELECT * FROM pelanggan where kd_po='" & lblkode.Text & "'"
            sqlcommand = New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
            Dr.Close()
            Dr = sqlcommand.ExecuteReader
            Dr.Read()
            If Not Dr.HasRows Then tutupkoneksi() : Exit Sub
            Message = ShowMessageBox("Apakah Anda Yakin Untuk Menghapus Data ini?", "Konfirmasi", MessageBoxType.Question)
            If Message = Windows.Forms.DialogResult.Yes Then
                tutupkoneksi()
                sql = "Delete From pelanggan where kd_po ='" & lblkode.Text & "'"
                cekkoneksi()
                sqlcommand = New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
                sqlcommand.ExecuteNonQuery()
                sqlcommand = New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
                tutupkoneksi()
                cekkoneksi()
                sqlcommand.ExecuteNonQuery()
                TampilGrid()
                Bersih()
                tutupkoneksi()
                ShowMessageBox("Data Berhasil Dihapus.", "Infomasi!", MessageBoxType.Information)
                TidakAktif()
                BtnTambah.Enabled = True
            Else
                Exit Sub
                TampilGrid()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GrdView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GrdView.CellContentClick
        KlikGrid()
    End Sub
    Sub KlikGrid()
        Try
            Aktif()
            txtnama.Enabled = True
            lblkode.Enabled = False
            lblkode.Text = ""
            BtnSimpan.Enabled = False
            BtnEdit.Enabled = True
            lblkode.Text = GrdView.CurrentRow.Cells(0).Value
            txtnama.Text = GrdView.CurrentRow.Cells(1).Value
            txtpemilik.Text = GrdView.CurrentRow.Cells(2).Value
            txtalamat.Text = GrdView.CurrentRow.Cells(3).Value
            cbokelamin.Text = GrdView.CurrentRow.Cells(4).Value
            cboagama.Text = GrdView.CurrentRow.Cells(5).Value
            txttelpone.Text = GrdView.CurrentRow.Cells(6).Value
        Catch ex As Exception
            MsgBox("Terjadi kesalahan! " & ex.Message)
        End Try
    End Sub

    Private Sub GrdView_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GrdView.CellContentDoubleClick
        KlikGrid()
    End Sub

    Private Sub BtnTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTambah.Click
        lblkode.Enabled = True
        lblkode.Text = ""
        KodeAuto()

        txtnama.Focus()
        Aktif()
        txtnama.Focus()
    End Sub

    Private Sub txtcari_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcari.TextChanged
        TampilGrid()

    End Sub

   
    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Try
            If txtnama.Text = "" Or txtalamat.Text = "" Or txtpemilik.Text = "" Or txttelpone.Text = "" Or cboagama.Text = "" Or cbokelamin.Text = "" Then
                ShowMessageBox("Data Isian Harus Lengkap", "Critical", MessageBoxType.Critical)
            Else
                tutupkoneksi()
                cekkoneksi()
                sql = "SELECT * FROM pelanggan  WHERE kd_po='" & Trim(lblkode.Text) & "'"
                sqlcommand = New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
                Dr.Close()
                Dr = sqlcommand.ExecuteReader()
                Dr.Read()
                If Not Dr.HasRows Then
                    sql = "INSERT INTO pelanggan values ('" & lblkode.Text & "','" & _
                         txtnama.Text & "','" & txtpemilik.Text & "','" & txtalamat.Text & "','" & cbokelamin.Text & _
                         "','" & cboagama.Text & "','" & txttelpone.Text & "')"
                Else
                    sql = "UPDATE pelanggan set nm_po='" & txtnama.Text & "',pemilik='" & txtpemilik.Text & "',alamat='" & txtalamat.Text & "', kelamin='" & cbokelamin.Text & "',agama='" & cboagama.Text & "',telp='" & txttelpone.Text & "' WHERE kd_po='" & Trim(lblkode.Text) & "'"
                End If
                sqlcommand = New MySql.Data.MySqlClient.MySqlCommand(sql, conn)
                tutupkoneksi()
                cekkoneksi()
                sqlcommand.ExecuteNonQuery()
                Bersih()
                tutupkoneksi()
                TampilGrid()
                ShowMessageBox("Data Berhasil Disimpan.", "Infomasi!", MessageBoxType.Information)
                TidakAktif()
                BtnTambah.Enabled = True
                BtnEdit.Enabled = False
                BtnSimpan.Enabled = False
            End If
        Catch ex As Exception
            MsgBox("Terjadi kesalahan! " & ex.Message)
        End Try

    End Sub
End Class