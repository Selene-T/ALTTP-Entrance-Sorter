<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmES
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.map = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'map
        '
        Me.map.BackgroundImage = Global.ALTTP_Entrance_Sorter.My.Resources.Resources.mapLQ
        Me.map.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.map.Location = New System.Drawing.Point(0, 0)
        Me.map.Margin = New System.Windows.Forms.Padding(0)
        Me.map.Name = "map"
        Me.map.Size = New System.Drawing.Size(512, 320)
        Me.map.TabIndex = 0
        '
        'frmES
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(528, 359)
        Me.Controls.Add(Me.map)
        Me.MinimumSize = New System.Drawing.Size(528, 359)
        Me.Name = "frmES"
        Me.Text = "ALttP: Entrance Sorter"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents map As Panel
End Class
