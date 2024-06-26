﻿Imports System.Collections.Specialized.BitVector32
Imports Entidades
Imports GestionKorrika

Public Class FrmPrincipal
    Private Sub btnCrearKorrika_Click(sender As Object, e As EventArgs) Handles btnCrearKorrika.Click
        If String.IsNullOrWhiteSpace(txtNumKorrika.Text) OrElse String.IsNullOrWhiteSpace(txtAnyo.Text) OrElse String.IsNullOrWhiteSpace(txtEslogan.Text) OrElse String.IsNullOrWhiteSpace(txtFechaInicio.Text) OrElse String.IsNullOrWhiteSpace(txtFechaInicio.Text) OrElse String.IsNullOrWhiteSpace(txtCantKms.Text) Then
            MessageBox.Show("No pueden haber campos vacíos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If korrika Is Nothing Then
            korrika = New Korrika(txtNumKorrika.Text, txtAnyo.Text, txtEslogan.Text, txtFechaInicio.Text, txtFechaFin.Text, txtCantKms.Text)
            Dim msg = korrika.LeerKorrika
            If Not msg = "" Then
                korrika.GrabarFichero()
                MessageBox.Show(msg)
                Exit Sub
            End If
        End If




    End Sub

    Private Sub FinalizarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FinalizarToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub DefinirKmToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DefinirKmToolStripMenuItem.Click
        If korrika Is Nothing Then
            MessageBox.Show("No has creado aún la korrika", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Dim frmDefinirKms As New FrmDefinirKms
        frmDefinirKms.ShowDialog()
    End Sub

    Private Sub FinanciarKmToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FinanciarKmToolStripMenuItem.Click
        If korrika Is Nothing Then
            MessageBox.Show("No has creado aún la korrika", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Dim frmFinanciacion As New FrmFinanciacion
        frmFinanciacion.ShowDialog()
    End Sub

    Private Sub btnAccederKorrika_Click(sender As Object, e As EventArgs) Handles btnAccederKorrika.Click
        If korrika Is Nothing Then
            korrika = New Korrika(txtNumKorrika.Text, txtAnyo.Text, txtEslogan.Text, txtFechaInicio.Text, txtFechaFin.Text, txtCantKms.Text)
            Dim msg As String = korrika.LeerKorrika ' todo Todo era problema del constructor, no de esto
            If Not msg = "" Then
                korrika.GrabarFichero()
                MessageBox.Show(msg)
                Exit Sub
            End If
        Else
            Dim respuestaSN As DialogResult = MessageBox.Show($"Ya has creado una korrika {korrika.toString(True)}. ¿Deseas crear otra nueva?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If respuestaSN = DialogResult.Yes Then
                korrika = Nothing
                btnCrearKorrika_Click(Nothing, Nothing)
                korrika = New Korrika(txtNumKorrika.Text, txtNumKorrika.Text, txtAnyo.Text, txtEslogan.Text, txtFechaInicio.Text, txtFechaFin.Text, txtCantKms.Text)
                Dim msg As String = korrika.LeerKorrika
                If Not msg = "" Then
                    korrika.GrabarFichero()
                    MessageBox.Show(msg)
                    Exit Sub
                End If
            End If
        End If
    End Sub
End Class