Imports System.ComponentModel
Public Class Multiplayer
    'Second Snake
    Dim Ssnake(999) As PictureBox
    Dim Sbody As Integer = 0
    Dim SUD As Integer 'Second Snake Up Down
    Dim SLR As Integer 'Second Snake Left Right
    Dim score1 As Integer
    Dim score2 As Integer
    'Timer
    Dim Snakemover As Timer
    Dim Ssnakemover As Timer
    'Name
    Dim name1 As String
    Dim name2 As String
    Private Sub Multiplayer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GlobalStuff.CreateSnake(Me, snake, body, Color.Green)
        GlobalStuff.CreateSnake(Me, Ssnake, Sbody, Color.Yellow)
        GlobalStuff.CreateFood(Me, False)
        GlobalStuff.CreateSS(Me, False, True, Nothing)

        Snakemover = New Timer
        Snakemover.Interval = 50
        AddHandler Snakemover.Tick, AddressOf SnakeMover_Tick

        Ssnakemover = New Timer
        Ssnakemover.Interval = 50
        AddHandler Ssnakemover.Tick, AddressOf SsnakeMover_Tick
        body = 0
        name1 = InputBox("Please input Player 1 name")
        name2 = InputBox("Please input Player 2 name")
    End Sub
#Region "Snake 1"
    Private Sub SnakeMover_Tick(sender As Object, e As EventArgs)

        'Replaces the location of picturebox with the previous one
        For counter = body To 1 Step -1
            snake(counter).Top = snake(counter - 1).Top
            snake(counter).Left = snake(counter - 1).Left
        Next

        'Moves the 1st picturebox
        snake(0).Top += UD
        snake(0).Left += LR

        snake_location.Text = name & "'s Snake Location:(" & snake(0).Top & "," & snake(0).Left & ")"

        GlobalStuff.SelfCollision(Snakemover, snake, body)
        If snake(0).Bounds.IntersectsWith(food.Bounds) Then
            score1 += 1
            PlayerSnakeBody1()
            food.Hide()
            CreateFood(Me, False)
        End If
        GlobalStuff.WallCollision(Me, body, False, Snakemover)
        For counter = 0 To body
            If Ssnake(0).Bounds.IntersectsWith(snake(counter).Bounds) Then
                Snakemover.Stop()
                Ssnakemover.Stop()
                CheckWinner()
                Me.Close()
            End If
        Next
    End Sub
    Public Sub PlayerSnakeBody1()
        body += 1
        snake(body) = New PictureBox

        With snake(body)
            .Width = 12
            .Height = 12
            .BackColor = Color.Green
            .Top = Me.ClientRectangle.Height + 100
            .Left = Me.ClientRectangle.Width + 100
        End With

        Me.Controls.Add(snake(body))
    End Sub
#End Region
#Region "Snake 2"
    Private Sub SsnakeMover_Tick(sender As Object, e As EventArgs)
        'Replaces the location of picturebox with the previous one
        For counter = Sbody To 1 Step -1
            Ssnake(counter).Top = Ssnake(counter - 1).Top
            Ssnake(counter).Left = Ssnake(counter - 1).Left
        Next

        'Moves the 1st picturebox
        Ssnake(0).Top += SUD
        Ssnake(0).Left += SLR

        GlobalStuff.SelfCollision(Ssnakemover, Ssnake, Sbody)
        If Ssnake(0).Bounds.IntersectsWith(food.Bounds) Then
            score2 += 1
            PlayerSnakeBody2()
            food.Hide()
            CreateFood(Me, False)
        End If
        For counter = 0 To Sbody
            If Ssnake(counter).Left + Ssnake(counter).Width > Me.ClientRectangle.Right Then
                Ssnake(counter).Left = 0
            ElseIf ssnake(counter).Left < 0 Then
                Ssnake(counter).Left = Me.ClientRectangle.Right
            ElseIf ssnake(counter).Top + ssnake(counter).Height > Me.ClientRectangle.Bottom Then
                Ssnake(counter).Top = 0
            ElseIf ssnake(counter).Top < 0 Then
                Ssnake(counter).Top = Me.ClientRectangle.Bottom
            End If
        Next

        For counter = 0 To Sbody
            If snake(0).Bounds.IntersectsWith(Ssnake(counter).Bounds) Then
                Snakemover.Stop()
                Ssnakemover.Stop()
                CheckWinner()
                Me.Close()
            End If
        Next
        Player2_location.Text = name2 & "'s Snake Location:(" & Ssnake(0).Top & "," & Ssnake(0).Left & ")"

    End Sub
    Public Sub PlayerSnakeBody2()
        Sbody += 1
        Ssnake(Sbody) = New PictureBox

        With Ssnake(Sbody)
            .Width = 12
            .Height = 12
            .BackColor = Color.Yellow
            .Top = Ssnake(Sbody - 1).Top
            .Left = Ssnake(Sbody - 1).Left
        End With

        Me.Controls.Add(Ssnake(Sbody))
    End Sub
#End Region
    Private Sub Multiplayer_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Snakemover.Start()
        Ssnakemover.Start()
        'Snake 1
        Select Case e.KeyCode
            Case Keys.Up
                UD = -13
                LR = 0
            Case Keys.Down
                UD = 13
                LR = 0
            Case Keys.Left
                LR = -13
                UD = 0
            Case Keys.Right
                LR = 13
                UD = 0
        End Select

        'Snake 2
        Select Case e.KeyCode
            Case Keys.W
                SUD = -13
                SLR = 0
            Case Keys.S
                SUD = 13
                SLR = 0
            Case Keys.A
                SUD = 0
                SLR = -13
            Case Keys.D
                SUD = 0
                SLR = 13
        End Select
    End Sub
    Private Sub CheckWinner()
        'Draw
        If score1 = score2 Then
            MsgBox("Its a draw")
            Exit Sub
        End If
        'Win or lose
        If score1 > score2 Then
            MsgBox(name & "wins")
        Else
            MsgBox(name2 & "wins")
        End If
    End Sub
    Private Sub Multiplayer_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Snakemover.Stop()
        Ssnakemover.Stop()
        body = Nothing
        UD = Nothing
        LR = Nothing
        Me.Controls.Remove(ss)
        MainMenu.Show()
    End Sub
End Class