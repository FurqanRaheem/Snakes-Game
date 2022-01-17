Module GlobalStuff
    'Player snake
    Public snake(1000) As PictureBox
    Public body As Integer = 0
    Public eaten As Integer
    Public UD As Integer ' Up Down
    Public LR As Integer ' Left Right
    'AI snake
    Public AIsnake(9) As PictureBox
    Public AIUD As Integer 'AI Up Down
    Public AILR As Integer ' AI Left Right 
    'Food
    Public food As PictureBox
    'Boundaries
    Public LRBoundary(90, 1) As PictureBox ' Left Right Boundary
    Public UDBoundary(90, 1) As PictureBox ' Up Down Boundary
    'Merge sort
    Public DataStore() As String
    Public TempStore() As String
    'Status strip
    Public ss As StatusStrip = New StatusStrip
    Public snake_location As ToolStripStatusLabel = New ToolStripStatusLabel
    Public Player2_location As ToolStripStatusLabel = New ToolStripStatusLabel
    Public AI_location As ToolStripStatusLabel = New ToolStripStatusLabel
    Public foodLocation As ToolStripStatusLabel = New ToolStripStatusLabel
    'Leaderboard
    Public gameend As Boolean
    Public list(10) As String
    'Obstacle
    Public Obstacle(20, 3) As PictureBox
    Public obstaclecount As Integer = 0
#Region "Snake"
    Public Sub CreateSnake(frm As Form, Snakes As PictureBox(), length As Integer, colour As Color)
        length = 0
        Snakes(0) = New PictureBox

        'Random X and Y coordinates
        Dim x As Integer = Int(Rnd() * frm.ClientRectangle.Width - 10)
        Dim y As Integer = Int(Rnd() * frm.ClientRectangle.Height - 10)

        With Snakes(0)
            .Width = 12
            .Height = 12
            .BackColor = colour
            .TabIndex = 0
            .Location = New Point(x, y)
        End With
        frm.Controls.Add(Snakes(0))
    End Sub
    Public Sub PlayerSnakeBodyWithColour(frm As Form)
        Dim body_colour As Integer
        body += 1
        snake(body) = New PictureBox

        With snake(body)
            .Width = 12
            .Height = 12
            .Top = frm.Height + 100
            .Left = frm.Width + 100
        End With

        body_colour = Int(Rnd() * 8 + 1)
        Select Case body_colour
            Case 1
                snake(body).Tag = 1
            Case 2
                snake(body).Tag = 2
            Case 3
                snake(body).Tag = 3
            Case 4
                snake(body).Tag = 4
            Case 5
                snake(body).Tag = 5
            Case 6
                snake(body).Tag = 6
            Case 7
                snake(body).Tag = 7
            Case 8
                snake(body).Tag = 8
            Case 9
                snake(body).Tag = 9
        End Select

        'Merge Sort
        ReDim DataStore(body)
        For counter = 0 To body
            DataStore(counter) = snake(counter).Tag
        Next
        Sort(1, body, body)

        Dim ColorVals(9) As Color

        ColorVals(0) = Color.Green
        ColorVals(1) = Color.Red
        ColorVals(2) = Color.Orange
        ColorVals(3) = Color.Yellow
        ColorVals(4) = Color.Green
        ColorVals(5) = Color.Blue
        ColorVals(6) = Color.Indigo
        ColorVals(7) = Color.Violet
        ColorVals(8) = Color.White
        ColorVals(9) = Color.Gold

        'assign colour to snake from the ordered list
        For counter = 1 To body
            snake(counter).BackColor = ColorVals(DataStore(counter))
        Next
        frm.Controls.Add(snake(body))
    End Sub
    Public Sub CreateAISnake(frm As Form)
        Randomize()
        Dim x As Integer = Int(Rnd() * frm.ClientRectangle.Width - 10)
        Dim y As Integer = Int(Rnd() * frm.ClientRectangle.Height - 10)

        AIsnake(0) = New PictureBox
        With AIsnake(0)
            .Width = 13
            .Height = 13
            .BackColor = Color.White
            .Location = New Point(x, y)
        End With

        frm.Controls.Add(AIsnake(0))

        For counter = 1 To 9
            AIsnake(counter) = New PictureBox
            With AIsnake(counter)
                .Width = 13
                .Height = 13
                .BackColor = Color.White
                .Top = AIsnake(counter - 1).Top - 11
                .Left = AIsnake(counter - 1).Left
            End With
            frm.Controls.Add(AIsnake(counter))
            AIsnake(counter).BringToFront()
        Next
    End Sub
#End Region
#Region "Food"
    Public Sub CreateFood(frm As Form, boundaryModeSelected As Boolean)
        Randomize()
        Dim x As Integer = Int(Rnd() * frm.ClientRectangle.Width - 20)
        Dim y As Integer = Int(Rnd() * frm.ClientRectangle.Height - 20)

        food = New PictureBox

        With food
            .Width = 10
            .Height = 10
            .BackColor = Color.Red
            .Location = New Point(x, y)
        End With
        frm.Controls.Add(food)
        'check if food outside the bounds of the form
        If food.Left > frm.ClientRectangle.Width Or food.Left < 0 Then
            food.Hide()
            CreateFood(frm, boundaryModeSelected)
        ElseIf food.Top > frm.ClientRectangle.Height Or food.Top < 0 Then
            food.Hide()
            CreateFood(frm, boundaryModeSelected)
        End If

        ' check if food intersects with the boundary
        If boundaryModeSelected = True Then
            For X = 0 To 90
                For Y = 0 To 1
                    If food.Bounds.IntersectsWith(UDBoundary(X, Y).Bounds) Then
                        food.Hide()
                        CreateFood(frm, boundaryModeSelected)
                    End If
                    If food.Bounds.IntersectsWith(LRBoundary(X, Y).Bounds) Then
                        food.Hide()
                        CreateFood(frm, boundaryModeSelected)
                    End If
                Next
            Next
        End If

        'check if food intersects with the status strip using the AABB(Axis-Aligned Bounding Box) method
        If (food.Left < ss.Left + ss.Width) And (food.Left + food.Width > ss.Left) And (food.Top < ss.Top + ss.Height) And (food.Top + food.Height > ss.Top) Then
            food.Hide()
            CreateFood(frm, boundaryModeSelected)
        End If
        'Update Status strip
        foodLocation.ForeColor = System.Drawing.Color.White
        foodLocation.Text = "Food Location:" & food.Left & "," & food.Top
        foodLocation.BackColor = Color.Gray
    End Sub
#End Region
#Region "Boundary"
    Public Sub CreateBoundary(frm As Form)
        LRBoundary(0, 0) = New PictureBox
        With LRBoundary(0, 0)
            .Height = 12
            .Width = 12
            .BackColor = Color.Silver
            .Top = 0
            .Left = 0
        End With
        frm.Controls.Add(LRBoundary(0, 0))

        LRBoundary(0, 1) = New PictureBox
        With LRBoundary(0, 1)
            .Height = 10
            .Width = 10
            .BackColor = Color.Silver
            .Top = 0
            .Left = frm.ClientRectangle.Right - 10
        End With
        frm.Controls.Add(LRBoundary(0, 1))

        For x = 1 To 90
            For y = 0 To 1
                LRBoundary(x, y) = New PictureBox
                With LRBoundary(x, y)
                    .Height = 10
                    .Width = 10
                    .BackColor = Color.Silver
                    .Top = LRBoundary(x - 1, y).Top + 11
                    .Left = LRBoundary(x - 1, y).Left
                End With
                frm.Controls.Add(LRBoundary(x, y))
            Next
        Next

        UDBoundary(0, 0) = New PictureBox
        With UDBoundary(0, 0)
            .Height = 10
            .Width = 10
            .BackColor = Color.Silver
            .Top = 0
            .Left = 0
        End With
        frm.Controls.Add(UDBoundary(0, 0))

        UDBoundary(0, 1) = New PictureBox
        With UDBoundary(0, 1)
            .Height = 10
            .Width = 10
            .BackColor = Color.Silver
            .Top = frm.ClientRectangle.Bottom - 32
            .Left = 0
        End With

        For x = 1 To 90
            For y = 0 To 1
                UDBoundary(x, y) = New PictureBox
                With UDBoundary(x, y)
                    .Height = 10
                    .Width = 10
                    .BackColor = Color.Silver
                    .Top = UDBoundary(x - 1, y).Top
                    .Left = UDBoundary(x - 1, y).Left + 11
                End With
                frm.Controls.Add(UDBoundary(x, y))
            Next
        Next
    End Sub
#End Region
#Region "Collision"
    Public Sub FoodCollision(frm As Form, boundaryModeselected As Boolean, length As Integer, snakes As PictureBox(), colours As Color)
        If snakes(0).Bounds.IntersectsWith(food.Bounds) Then
            eaten += 1
            food.Hide()
            PlayerSnakeBodyWithColour(frm)
            CreateFood(frm, boundaryModeselected)
        End If
    End Sub
    Public Sub SelfCollision(Time As Timer, snakes() As PictureBox, length As Integer)
        For counter = 1 To length
            If snakes(0).Bounds.IntersectsWith(snakes(counter).Bounds) Then
                Time.Stop()
                MsgBox("You've Lost!")
                gameend = True
            End If
        Next
    End Sub
    Public Sub WallCollision(frm As Form, length As Integer, boundaryModeSelected As Boolean, time As Timer)
        'if collided with boundary then end game
        If boundaryModeSelected = True Then
            For x = 0 To 90
                For y = 0 To 1
                    If snake(0).Bounds.IntersectsWith(UDBoundary(x, y).Bounds) Or snake(0).Bounds.IntersectsWith(LRBoundary(x, y).Bounds) Then
                        time.Stop()
                        MsgBox("You've Lost!")
                        gameend = True
                        Exit Sub
                    End If
                Next
            Next
        Else
            'appear on the opposite side of the form
            For counter = 0 To length
                If snake(counter).Left + snake(counter).Width > frm.ClientRectangle.Right Then
                    snake(counter).Left = 0
                ElseIf Snake(counter).Left < 0 Then
                    snake(counter).Left = frm.ClientRectangle.Right
                ElseIf Snake(counter).Top + Snake(counter).Height > frm.ClientRectangle.Bottom Then
                    snake(counter).Top = 0
                ElseIf Snake(counter).Top < 0 Then
                    snake(counter).Top = frm.ClientRectangle.Bottom
                End If
            Next
        End If
    End Sub
    Public Sub AICollision(frm As Form, time As Timer, time2 As Timer)
        If AIsnake(0).Bounds.IntersectsWith(snake(0).Bounds) Then
            time.Stop()
            time2.Stop()
            MsgBox("You lose")
            gameend = True
        End If
        'Removes the rest of body from point of contact
        For x = 1 To body
            If AIsnake(0).Bounds.IntersectsWith(snake(x).Bounds) Then
                For y = x To body
                    snake(y).Hide()
                    snake(y) = Nothing
                Next
                body = x - 1
                PlayerSnakeBodyWithColour(frm)
                Exit For
            End If
        Next
    End Sub
    Public Sub ObstacleCollision(time As Timer)
        For x = 0 To obstaclecount
            For y = 0 To 3
                If snake(0).Bounds.IntersectsWith(Obstacle(x, y).Bounds) Then
                    time.Stop()
                    MsgBox("You've Lost!")
                    gameend = True
                End If
            Next
        Next
    End Sub
#End Region
#Region "Merge Sort"
    Public Sub Sort(ByVal First As Integer, ByVal Last As Integer, length As Integer)
        Dim Middle As Integer

        If (Last > First) Then
            Middle = CInt((First + Last) \ 2)
            Sort(First, Middle, length)
            Sort(Middle + 1, Last, length)
            Merge(First, Middle, Last, length)
        End If

    End Sub
    Public Sub Merge(ByVal Start As Integer, ByVal Middle As Integer, ByVal ending As Integer, length As Integer)
        ReDim TempStore(length)
        Dim Left As Integer
        Dim Right As Integer
        Dim counterMain As Integer

        For counter = 0 To length
            TempStore(counter) = DataStore(counter)
        Next
        Left = Start
        Right = Middle + 1
        counterMain = Start

        While (Left <= Middle) And (Right <= ending)
            If (TempStore(Left) <= TempStore(Right)) Then
                DataStore(counterMain) = TempStore(Left)
                Left = Left + 1
            Else
                DataStore(counterMain) = TempStore(Right)
                Right = Right + 1
            End If
            counterMain = counterMain + 1
        End While
        If Left <= Middle Then
            For counter = 1 To Middle - Left + 1
                DataStore(counterMain + counter - 1) = TempStore(Left + counter - 1)
            Next
        Else
            For counter = 1 To ending - Right + 1
                DataStore(counterMain + counter - 1) = TempStore(Right + counter - 1)
            Next
        End If
    End Sub
#End Region
#Region "Status Strip"
    Public Sub CreateSS(frm As Form, AImodeSelected As Boolean, MultiplayerModeSelected As Boolean, aisnakes() As PictureBox)
        ss.Items.AddRange(New System.Windows.Forms.ToolStripItem() {snake_location})
        ss.Items.AddRange(New System.Windows.Forms.ToolStripItem() {AI_location})
        ss.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Player2_location})
        ss.Items.AddRange(New System.Windows.Forms.ToolStripItem() {foodLocation})

        ss.Location = New System.Drawing.Point(0, frm.Height)
        ss.Size = New System.Drawing.Size(frm.Width + 10, 22)

        'Player Snake
        snake_location.ForeColor = System.Drawing.Color.White
        snake_location.BackColor = Color.Gray
        snake_location.Text = "Snake Location:(" & snake(0).Top & "," & snake(0).Left & ")"
        'Food
        foodLocation.ForeColor = System.Drawing.Color.White
        foodLocation.Text = "Food Location:" & food.Left & "," & food.Top
        foodLocation.BackColor = Color.Gray
        ' Second Player Snake
        If MultiplayerModeSelected = True Then
            Player2_location.ForeColor = System.Drawing.Color.White
            Player2_location.BackColor = Color.Gray
        End If
        'AI snake
        If AImodeSelected = True Then
            AI_location.ForeColor = System.Drawing.Color.White
            AI_location.Text = "AI snake Location:" & aisnakes(0).Left & "," & aisnakes(0).Top
            AI_location.BackColor = Color.Gray
        End If
        frm.Controls.Add(ss)
        ss.BringToFront()
    End Sub
#End Region
#Region "LeaderBoard"
    Public Sub SortScore(current_mode As String)
        Dim name As String
        Dim score As String

        name = InputBox("Enter your name")
        score = Format(body, "000000") & " " & name

        FileOpen(1, current_mode, OpenMode.Input)
        'read in file
        For counter = 0 To 9
            list(counter) = LineInput(1)
        Next
        For counter = 0 To 9
            'carry out merge sort only if score is higher than the items in the file
            If score > Mid(list(counter), 1, 6) Then
                list(10) = score
                ReDim DataStore(10)
                'use merge sort to sort list
                For x = 0 To 10
                    DataStore(x) = list(x)
                Next
                Sort(0, 10, 10)
                For x = 0 To 9
                    list(x) = DataStore(10 - x)
                Next
                Exit For
            End If
        Next
        FileClose(1)
        FileOpen(1, current_mode, OpenMode.Output)
        For counter = 0 To 9
            PrintLine(1, list(counter))
        Next
        FileClose(1)
        gameend = False
    End Sub
#End Region
End Module
