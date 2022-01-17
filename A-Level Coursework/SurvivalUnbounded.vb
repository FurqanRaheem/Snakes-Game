Imports System.ComponentModel
Public Class SurvivalUnbounded
    Dim SnakeMover As Timer
    Dim keypressed As String
    Dim previous As String
    Private Sub SurvivalUnbounded_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GlobalStuff.CreateSnake(Me, snake, body, Color.Green)
        GlobalStuff.CreateFood(Me, False)
        GlobalStuff.CreateSS(Me, False, False, Nothing)
        'Timer
        SnakeMover = New Timer
        SnakeMover.Interval = 50
        AddHandler SnakeMover.Tick, AddressOf SnakeMover_Tick
    End Sub
    Private Sub SnakeMover_Tick(sender As Object, e As EventArgs)
        'Sets direction and prevent snake from running back on itself 
        If keypressed = "up" Then
            previous = "down"
            UD = -13
            LR = 0
        ElseIf keypressed = "down" Then
            previous = "up"
            UD = 13
            LR = 0
        ElseIf keypressed = "left" Then
            previous = "right"
            LR = -13
            UD = 0
        ElseIf keypressed = "right" Then
            previous = "left"
            LR = 13
            UD = 0
        End If
        'Replaces the location of picturebox with the previous one
        For counter = body To 1 Step -1
            snake(counter).Top = snake(counter - 1).Top
            snake(counter).Left = snake(counter - 1).Left
        Next
        'Moves the 1st picturebox
        snake(0).Top += UD
        snake(0).Left += LR

        snake_location.Text = "Snake Location:(" & snake(0).Top & "," & snake(0).Left & ")"

        GlobalStuff.SelfCollision(SnakeMover, snake, body)
        GlobalStuff.FoodCollision(Me, False, Nothing, snake, Nothing)
        GlobalStuff.WallCollision(Me, body, False, SnakeMover)
        If gameend = True Then
            GlobalStuff.SortScore("SurvivalUnbounded.txt")
            Me.Close()
        End If
    End Sub
    Private Sub SurvivalUnbounded_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        SnakeMover.Start()

        If e.KeyCode = Keys.Up And (previous = "" Or previous = "down" Or previous = "left" Or previous = "right") Then
            keypressed = "up"
        End If
        'Down
        If e.KeyCode = Keys.Down And (previous = "up" Or previous = "" Or previous = "left" Or previous = "right") Then
            keypressed = "down"
        End If
        'Left
        If e.KeyCode = Keys.Left And (previous = "up" Or previous = "down" Or previous = "" Or previous = "right") Then
            keypressed = "left"
        End If
        'Right
        If e.KeyCode = Keys.Right And (previous = "up" Or previous = "down" Or previous = "left" Or previous = "") Then
            keypressed = "right"
        End If
    End Sub
    Private Sub SurvivalUnbounded_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        SnakeMover.Stop()
        eaten = Nothing
        body = Nothing
        UD = Nothing
        LR = Nothing
        gameend = Nothing
        Me.Controls.Remove(ss)
        MainMenu.Show()
    End Sub
End Class