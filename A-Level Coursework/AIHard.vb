Imports System.ComponentModel
Public Class AIHard
    Dim keypressed As String
    Dim previous As String
    'Timer
    Dim AISnakeMover As Timer
    Dim SnakeMover As Timer
    'Chasing snake
    Dim angle
    Dim difference As Point
    Dim hypotenuse As Integer
    Dim ahead As Point
    Dim ahead2 As Point
    Dim inProximity As Boolean
    Dim CentrePoint As Point
    Dim PerimeterPoint As Point
    Dim distance(2) As Integer
    Const radius = 40
    Private Sub AIHard_Load(sender As Object, e As EventArgs) Handles Me.Load
        GlobalStuff.CreateSnake(Me, snake, body, Color.Green)
        GlobalStuff.CreateAISnake(Me)
        GlobalStuff.CreateBoundary(Me)
        GlobalStuff.CreateFood(Me, True)
        GlobalStuff.CreateSS(Me, True, False, AIsnake)
        CreateObstacle()

        'Timer
        AISnakeMover = New Timer
        AISnakeMover.Interval = 100
        AddHandler AISnakeMover.Tick, AddressOf AISnakeMover_Tick

        SnakeMover = New Timer
        SnakeMover.Interval = 50
        AddHandler SnakeMover.Tick, AddressOf SnakeMover_Tick
    End Sub
#Region "Player Snake"
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

        GlobalStuff.SelfCollision(SnakeMover, snake, body)
        GlobalStuff.FoodCollision(Me, True, Nothing, snake, Nothing)
        GlobalStuff.WallCollision(Me, body, True, SnakeMover)
        GlobalStuff.AICollision(Me, SnakeMover, AISnakeMover)

        ' Creates another obstacle after 7 items of food has been eaten
        'If eaten = 5 Then
        '    eaten = 0
        '    CreateObstacle()
        'End If

        'Obstacle Collision
        For x = 1 To obstaclecount
            For y = 0 To 3
                If snake(0).Bounds.IntersectsWith(Obstacle(x, y).Bounds) Then
                    SnakeMover.Stop()
                    AISnakeMover.Stop()
                    MsgBox("You've Lost!")
                    gameend = True
                End If
            Next
        Next

        If gameend = True Then
            GlobalStuff.SortScore("AIHard.txt")
            Me.Close()
        End If
    End Sub
    Private Sub AIHard_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        SnakeMover.Start()
        AISnakeMover.Start()
        'Checks for previous key down and sets direction by checking if the same key wasn't pressed previously
        'Up
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
#End Region
#Region "AI snake"
    Private Sub FollowSnake()
        'calculate length
        difference = snake(0).Location - AIsnake(0).Location
        hypotenuse = magnitude(snake(0).Location, AIsnake(0).Location)
        angle = Math.Asin(difference.X / hypotenuse)
        'convert units (Radian to degree)
        angle = angle * 180 / Math.PI
        If AIsnake(0).Top > snake(0).Top Then
            If angle = 0 Then
                AIUD = -8
                AILR = 0
            ElseIf angle > 0 And angle < 22.5 Then
                AIUD = -8
                AILR = 4
            ElseIf angle > 22.5 And angle < 67.5 Then
                AIUD = -8
                AILR = 8
            ElseIf angle > 67.5 And angle < 90 Then
                AIUD = -4
                AILR = 8
            ElseIf angle = 90 Then
                AIUD = 0
                AILR = 8
            End If
            If angle < 0 And angle > -22.5 Then
                AIUD = -8
                AILR = -4
            ElseIf angle < -22.5 And angle > -67.5 Then
                AIUD = -8
                AILR = -8
            ElseIf angle < -67.5 And angle > -90 Then
                AIUD = -4
                AILR = -8
            ElseIf angle = -90 Then
                AIUD = 0
                AILR = -8
            End If
        ElseIf AIsnake(0).Top < snake(0).Top Then
            If angle = 90 Then
                AIUD = 0
                AILR = 8
            ElseIf angle < 90 And angle > 67.5 Then
                AIUD = 4
                AILR = 8
            ElseIf angle < 67.5 And angle > 22.5 Then
                AIUD = 8
                AILR = 8
            ElseIf angle < 22.5 And angle > 0 Then
                AIUD = 8
                AILR = 4
            ElseIf angle = 0 Then
                AIUD = 8
                AILR = 0
            End If
            If angle = -90 Then
                AIUD = 0
                AILR = -8
            ElseIf angle > -90 And angle < -67.5 Then
                AIUD = 4
                AILR = -8
            ElseIf angle > -67.5 And angle < -22.5 Then
                AIUD = 8
                AILR = -8
            ElseIf angle > -22.5 And angle < 0 Then
                AIUD = 8
                AILR = -4
            ElseIf angle = 0 Then
                AIUD = 8
                AILR = 0
            End If
        End If
        'copy of the velocity vector but with different magnitude
        ahead = New Point(AIsnake(0).Left + AILR * 8, AIsnake(0).Top + AIUD * 8)
        ahead2 = New Point(AIsnake(0).Left + AILR * 4, AIsnake(0).Top + AIUD * 4)
        For counter = 1 To obstaclecount
            'Radius
            CentrePoint.X = Obstacle(counter, 0).Left + 14
            CentrePoint.Y = Obstacle(counter, 0).Top + 14
            PerimeterPoint = New Point(CentrePoint.X, CentrePoint.Y + radius)
            'Calculates distance between all vectors
            distance(0) = magnitude(ahead, CentrePoint)
            distance(1) = magnitude(ahead2, CentrePoint)
            distance(2) = magnitude(AIsnake(0).Location, CentrePoint)
            'check if the vectors are shorter than the radius
            If distance(0) <= radius Or distance(1) <= radius Or distance(2) <= radius Then
                inProximity = True
            Else
                inProximity = False
            End If
        Next
    End Sub
    Private Sub AISnakeMover_Tick(sender As Object, e As EventArgs)
        For counter = 9 To 1 Step -1
            AIsnake(counter).Top = AIsnake(counter - 1).Top
            AIsnake(counter).Left = AIsnake(counter - 1).Left
        Next

        FollowSnake()
        If inProximity = True Then
            Dim origin As Point = CentrePoint
            Dim currentPoint As Point = AIsnake(0).Location - origin
            'x′=xcosθ−ysinθ
            'y′=ycosθ+xsinθ
            AIsnake(0).Left = origin.X + ((Math.Cos(0.174533) * currentPoint.X - Math.Sin(0.174533) * currentPoint.Y))
            AIsnake(0).Top = origin.Y + ((Math.Sin(0.174533) * currentPoint.X + Math.Cos(0.174533) * currentPoint.Y))
        Else
            AIsnake(0).Top += AIUD
            AIsnake(0).Left += AILR
        End If
        'Update status strip
        AI_location.Text = "AI Snake Location:(" & AIsnake(0).Top & "," & AIsnake(0).Left & ")"
    End Sub
#End Region
#Region "Obstacle"
    Private Sub CreateObstacle()
        obstaclecount += 1
        Randomize()
        Dim x As Integer = CInt(Rnd() * (Me.ClientRectangle.Width - 50)) + 50
        Dim y As Integer = CInt(Rnd() * (Me.ClientRectangle.Height - 50)) + 50
        'Dim x As Integer = Me.Width / 2
        'Dim y As Integer = Me.Height / 2
        For counter = 0 To 3
            Obstacle(obstaclecount, counter) = New PictureBox
            With Obstacle(obstaclecount, counter)
                .Height = 13
                .Width = 13
                .BackColor = Color.Gray
            End With
            Me.Controls.Add(Obstacle(obstaclecount, counter))
        Next

        Obstacle(obstaclecount, 0).Location = New Point(x, y)
        Obstacle(obstaclecount, 1).Location = New Point(x + 15, y)
        Obstacle(obstaclecount, 2).Location = New Point(x + 15, y + 15)
        Obstacle(obstaclecount, 3).Location = New Point(x, y + 15)
    End Sub
#End Region
    Private Function magnitude(targetpoint As Point, sourcepoint As Point)
        Return Math.Sqrt((targetpoint.X - sourcepoint.X) ^ 2 + (targetpoint.Y - sourcepoint.Y) ^ 2)
    End Function
    Private Sub AIHard_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
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