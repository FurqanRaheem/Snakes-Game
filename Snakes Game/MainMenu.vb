Public Class MainMenu
    'Panel Main
    Dim btnP As Button
    Dim btnS As Button
    Dim btnL As Button
    Dim btnE As Button
    Dim lblSnake As Label
    Dim back As PictureBox
    'Panel Play
    Dim btnAI As Button
    Dim btnSurvival As Button
    Dim btnMulti As Button
    'Panel AI
    Dim BtnEasy As Button
    Dim BtnMedium As Button
    Dim BtnHard As Button
    'Panel Survival
    Dim btnB As Button
    Dim btnU As Button
    'Panels
    Dim PnlMain As Panel
    Dim pnlPlay As Panel
    Dim PnlAI As Panel
    Dim pnlSurvival As Panel
    Dim current As String
    Private Sub MainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CreatePanelMain()
        ' Snake Game label
        lblSnake = New Label
        With lblSnake
            .Size = New Size(590, 117)
            .Location = New Point(39, 36)
            .Text = "Snakes Game"
            .Font = New System.Drawing.Font("Impact", 72, style:=FontStyle.Bold)
            .BackColor = Color.Black
            .ForeColor = Color.White
        End With
        Me.Controls.Add(lblSnake)
        lblSnake.BringToFront()

        'Back picturebox
        back = New PictureBox
        With back
            .Height = 90
            .Width = 90
            .Location = New Point(20, 490)
            .BackColor = Color.Black
            .BackgroundImage = My.Resources.BackIcon1
            .BackgroundImageLayout = ImageLayout.Stretch
        End With
        AddHandler back.Click, AddressOf back_Click
        Me.Controls.Add(back)
        back.Hide()
    End Sub
#Region "Panels"
    Private Sub CreatePanelMain()
        'Play Button 
        btnP = New Button
        With btnP
            .Name = "btnPlay"
            .Size = New Size(270, 81)
            .Location = New Point(195, 211)
            .Text = "Play"
            .Font = New System.Drawing.Font("Impact", 40, FontStyle.Bold)
            .BackColor = Color.Black
            .ForeColor = Color.White
        End With
        AddHandler btnP.Click, AddressOf btnPlay_Click
        Me.Controls.Add(btnP)
        btnP.BringToFront()

        'Settings Button
        btnS = New Button
        With btnS
            .Name = "btnSettings"
            .Size = New Size(270, 81)
            .Location = New Point(195, 298)
            .Text = "Settings"
            .Font = New System.Drawing.Font("Impact", 40, FontStyle.Bold)
            .BackColor = Color.Black
            .ForeColor = Color.White
        End With
        AddHandler btnS.Click, AddressOf btnSettings_Click
        Me.Controls.Add(btnS)
        btnS.BringToFront()

        'Leaderboard Button
        btnL = New Button
        With btnL
            .Name = "btnLeaderboard"
            .Size = New Size(270, 81)
            .Location = New Point(195, 385)
            .Text = "Leaderboard"
            .Font = New System.Drawing.Font("Impact", 32, FontStyle.Bold)
            .BackColor = Color.Black
            .ForeColor = Color.White
        End With
        AddHandler btnL.Click, AddressOf btnLeaderboard_Click
        Me.Controls.Add(btnL)
        btnL.BringToFront()

        'Exit Button
        btnE = New Button
        With btnE
            .Name = "btnExit"
            .Size = New Size(270, 81)
            .Location = New Point(195, 472)
            .Text = "Exit"
            .Font = New System.Drawing.Font("Impact", 40, FontStyle.Bold)
            .BackColor = Color.Black
            .ForeColor = Color.White
        End With
        AddHandler btnE.Click, AddressOf btnExit_Click
        Me.Controls.Add(btnE)
        btnE.BringToFront()

        'Panel Main
        PnlMain = New Panel
        With PnlMain
            .Size = New Size(Me.Width, Me.Height)
            .Location = New Point(0, 0)
            .BackColor = Color.Black
            .Controls.Add(btnP)
            .Controls.Add(btnS)
            .Controls.Add(btnL)
            .Controls.Add(btnE)
        End With
        Me.Controls.Add(PnlMain)
    End Sub
    Private Sub CreatePanelPlay()
        'AI
        btnAI = New Button
        With btnAI
            .Name = "btnAI"
            .Size = New Size(270, 81)
            .Location = New Point(195, 200)
            .Text = "AI Mode"
            .Font = New System.Drawing.Font("Impact", 40, FontStyle.Bold)
            .BackColor = Color.Black
            .ForeColor = Color.White
        End With
        AddHandler btnAI.Click, AddressOf btnAI_Click
        Me.Controls.Add(btnAI)

        'Survival
        btnSurvival = New Button
        With btnSurvival
            .Name = "btnSurvival"
            .Size = New Size(270, 81)
            .Location = New Point(195, 300)
            .Text = "Survival mode"
            .Font = New System.Drawing.Font("Impact", 40, FontStyle.Bold)
            .BackColor = Color.Black
            .ForeColor = Color.White
        End With
        AddHandler btnSurvival.Click, AddressOf btnSurvival_Click
        Me.Controls.Add(btnSurvival)

        'Multiplayer
        btnMulti = New Button
        With btnMulti
            .Name = "btnMulti"
            .Size = New Size(270, 81)
            .Location = New Point(195, 400)
            .Text = "Multiplayer"
            .Font = New System.Drawing.Font("Impact", 36, FontStyle.Bold)
            .BackColor = Color.Black
            .ForeColor = Color.White
        End With
        AddHandler btnMulti.Click, AddressOf btnMulti_Click
        Me.Controls.Add(btnMulti)

        'Panel Play
        pnlPlay = New Panel
        With pnlPlay
            .Size = New Size(Me.Width, Me.Height)
            .Location = New Point(0, 0)
            .BackColor = Color.Black
            .Controls.Add(btnAI)
            .Controls.Add(btnMulti)
            .Controls.Add(btnSurvival)
        End With
        Me.Controls.Add(pnlPlay)
        back.Show()
    End Sub
    Private Sub CreatePanelAI()
        'Easy
        BtnEasy = New Button
        With BtnEasy
            .Size = New Size(270, 81)
            .Location = New Point(195, 200)
            .Text = "Easy"
            .Font = New System.Drawing.Font("Impact", 40, FontStyle.Bold)
            .BackColor = Color.Black
            .ForeColor = Color.White
        End With
        AddHandler BtnEasy.Click, AddressOf btnEasy_Click
        Me.Controls.Add(BtnEasy)

        'Medium
        BtnMedium = New Button
        With BtnMedium
            .Size = New Size(270, 81)
            .Location = New Point(195, 300)
            .Text = "Medium"
            .Font = New System.Drawing.Font("Impact", 40, FontStyle.Bold)
            .BackColor = Color.Black
            .ForeColor = Color.White
        End With
        AddHandler BtnMedium.Click, AddressOf btnMedium_Click
        Me.Controls.Add(BtnMedium)

        'Hard
        BtnHard = New Button
        With BtnHard
            .Size = New Size(270, 81)
            .Location = New Point(195, 400)
            .Text = "Hard"
            .Font = New System.Drawing.Font("Impact", 40, FontStyle.Bold)
            .BackColor = Color.Black
            .ForeColor = Color.White
        End With
        AddHandler BtnHard.Click, AddressOf btnHard_Click
        Me.Controls.Add(BtnHard)

        'Panel AI
        PnlAI = New Panel
        With PnlAI
            .Size = New Size(Me.Width, Me.Height)
            .Location = New Point(0, 0)
            .BackColor = Color.Black
            .Controls.Add(BtnEasy)
            .Controls.Add(BtnMedium)
            .Controls.Add(BtnHard)
        End With
        Me.Controls.Add(PnlAI)
    End Sub
    Private Sub CreatePanelSurvival()
        'Bounded
        btnB = New Button
        With btnB
            .Size = New Size(270, 81)
            .Location = New Point(195, 200)
            .Text = "Bounded"
            .Font = New System.Drawing.Font("Impact", 40, FontStyle.Bold)
            .BackColor = Color.Black
            .ForeColor = Color.White
        End With
        AddHandler btnB.Click, AddressOf btnB_Click
        Me.Controls.Add(btnB)

        'Unbounded
        btnU = New Button
        With btnU
            .Size = New Size(270, 81)
            .Location = New Point(195, 300)
            .Text = "Unbounded"
            .Font = New System.Drawing.Font("Impact", 36, FontStyle.Bold)
            .BackColor = Color.Black
            .ForeColor = Color.White
        End With
        AddHandler btnU.Click, AddressOf btnU_Click
        Me.Controls.Add(btnU)

        'Panel Survival
        pnlSurvival = New Panel
        With pnlSurvival
            .Size = New Size(Me.Width, Me.Height)
            .Location = New Point(0, 0)
            .BackColor = Color.Black
            .Controls.Add(btnB)
            .Controls.Add(btnU)
        End With
        Me.Controls.Add(pnlSurvival)
    End Sub
#End Region
#Region "Click Subroutines"
#Region "Panel Main"
    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As EventArgs)
        If MsgBox("Are you sure you want to quit", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub
    Private Sub btnPlay_Click(ByVal sender As Object, ByVal e As EventArgs)
        PnlMain.Hide()
        CreatePanelPlay()
        current = "Play"
    End Sub
    Private Sub btnLeaderboard_Click(ByVal sender As Object, ByVal e As EventArgs)
        Leaderboard.Show()
        Me.Hide()
    End Sub
    Private Sub btnSettings_Click(ByVal sender As Object, ByVal e As EventArgs)

    End Sub
#End Region
#Region "Panel Play"
    Private Sub btnAI_Click(ByVal sender As Object, ByVal e As EventArgs)
        pnlPlay.Hide()
        CreatePanelAI()
        current = "AI"
    End Sub
    Private Sub btnSurvival_Click(ByVal sender As Object, ByVal e As EventArgs)
        pnlPlay.Hide()
        CreatePanelSurvival()
        current = "Survival"
    End Sub
    Private Sub btnMulti_Click(ByVal sender As Object, ByVal e As EventArgs)
        Multiplayer.Show()
        Me.Hide()
    End Sub
#End Region
#Region "Panel AI"
    Private Sub btnEasy_Click(ByVal sender As Object, ByVal e As EventArgs)
        AIEasy.Show()
        Me.Hide()
    End Sub
    Private Sub btnMedium_Click(ByVal sender As Object, ByVal e As EventArgs)
        AIMedium.Show()
        Me.Hide()
    End Sub
    Private Sub btnHard_Click(ByVal sender As Object, ByVal e As EventArgs)
        AIHard.Show()
        Me.Hide()
    End Sub
#End Region
#Region "Panel Survival"
    Private Sub btnB_Click(ByVal sender As Object, ByVal e As EventArgs)
        SurvivalBounded.Show()
        Me.Hide()
    End Sub
    Private Sub btnU_Click(ByVal sender As Object, ByVal e As EventArgs)
        SurvivalUnbounded.Show()
        Me.Hide()
    End Sub
#End Region
    Private Sub back_Click(sender As Object, e As EventArgs)
        If current = "Play" Then
            pnlPlay.Hide()
            PnlMain.Show()
            back.Hide()
            current = "Main"
        ElseIf current = "AI" Then
            PnlAI.Hide()
            pnlPlay.Show()
            back.Show()
            current = "Play"
        ElseIf current = "Survival" Then
            pnlSurvival.Hide()
            pnlPlay.Show()
            current = "Play"
        End If
    End Sub
#End Region
End Class
