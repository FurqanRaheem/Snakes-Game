Imports System.ComponentModel
Public Class AIEasy
    Dim keypressed As String
    Dim prev As String
    'Timer
    Dim AISnakeMover As Timer
    Dim SnakeMover As Timer
    Private Sub AIEasy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GlobalStuff.CreateSnake(Me, snake, body, Color.Green)
        GlobalStuff.CreateAISnake(Me)
        GlobalStuff.CreateBoundary(Me)
        GlobalStuff.CreateFood(Me, True)
        GlobalStuff.CreateSS(Me, True, False, AIsnake)

        'Timer
        AISnakeMover = New Timer
        AISnakeMover.Interval = 50
        AddHandler AISnakeMover.Tick, AddressOf AISnakeMover_Tick

        SnakeMover = New Timer
        SnakeMover.Interval = 50
        AddHandler SnakeMover.Tick, AddressOf SnakeMover_Tick
    End Sub
#Region "Player Snake"
    Private Sub SnakeMover_Tick(sender As Object, e As EventArgs)
        'Sets direction and prevent snake from running back in itself 
        If keypressed = "up" Then
            prev = "down"
            UD = -13
            LR = 0
        ElseIf keypressed = "down" Then
            prev = "up"
            UD = 13
            LR = 0
        ElseIf keypressed = "left" Then
            prev = "right"
            LR = -13
            UD = 0
        ElseIf keypressed = "right" Then
            prev = "left"
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
        GlobalStuff.FoodCollision(Me, True, Nothing, snake, Nothing)
        GlobalStuff.WallCollision(Me, body, True, SnakeMover)
        GlobalStuff.AICollision(Me, SnakeMover, AISnakeMover)
        If gameend = True Then
            GlobalStuff.SortScore("AIEasy.txt")
            Me.Close()
        End If
    End Sub
    Private Sub AIEasy_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        SnakeMover.Start()
        AISnakeMover.Start()

        'Checks for previous key down and sets direction by checking if the same key wasn't pressed previously
        'Up
        If e.KeyCode = Keys.Up And (prev = "" Or prev = "down" Or prev = "left" Or prev = "right") Then
            keypressed = "up"
        End If
        'Down
        If e.KeyCode = Keys.Down And (prev = "up" Or prev = "" Or prev = "left" Or prev = "right") Then
            keypressed = "down"
        End If
        'Left
        If e.KeyCode = Keys.Left And (prev = "up" Or prev = "down" Or prev = "" Or prev = "right") Then
            keypressed = "left"
        End If
        'Right
        If e.KeyCode = Keys.Right And (prev = "up" Or prev = "down" Or prev = "left" Or prev = "") Then
            keypressed = "right"
        End If

    End Sub
#End Region
#Region "AI Snake"
    Private Sub FollowSnake()
        'Assign direction to AI snake according to where the player snake is
        'Up
        If AIsnake(0).Top < snake(0).Top Then
            AIUD = 5
        End If
        'Down
        If AIsnake(0).Top > snake(0).Top Then
            AIUD = -5
        End If
        'Left
        If AIsnake(0).Left > snake(0).Left Then
            AILR = -5
        End If
        'Right
        If AIsnake(0).Left < snake(0).Left Then
            AILR = 5
        End If
    End Sub
    Private Sub AISnakeMover_Tick(sender As Object, e As EventArgs)
        For counter = 9 To 1 Step -1
            AIsnake(counter).Top = AIsnake(counter - 1).Top
            AIsnake(counter).Left = AIsnake(counter - 1).Left
        Next

        FollowSnake()
        AIsnake(0).Top += AIUD
        AIsnake(0).Left += AILR

        'Update status strip
        AI_location.Text = "AI Snake Location:(" & AIsnake(0).Top & "," & AIsnake(0).Left & ")"
    End Sub
#End Region
    Private Sub AIEasy_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        AISnakeMover.Stop()
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