Imports System.ComponentModel
Public Class Leaderboard
    Private Sub Leaderboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim UnboundedList(9) As String
        Dim BoundedList(9) As String
        Dim AIEasyList(9) As String
        Dim AIMediumList(9) As String
        Dim AIHardList(9) As String

        FileOpen(1, "SurvivalBounded.txt", OpenMode.Input)
        FileOpen(2, "SurvivalUnbounded.txt", OpenMode.Input)
        FileOpen(3, "AIEasy.txt", OpenMode.Input)
        FileOpen(4, "AIMedium.txt", OpenMode.Input)
        FileOpen(5, "AIHard.txt", OpenMode.Input)
        For counter = 0 To 9
            'Read in file
            BoundedList(counter) = LineInput(1)
            UnboundedList(counter) = LineInput(2)
            AIEasyList(counter) = LineInput(3)
            AIMediumList(counter) = LineInput(4)
            AIHardList(counter) = LineInput(5)
            'Display file contents
            Label1.Text += vbCrLf & counter + 1 & ". " & BoundedList(counter)
            Label2.Text += vbCrLf & counter + 1 & ". " & UnboundedList(counter)
            Label3.Text += vbCrLf & counter + 1 & ". " & AIEasyList(counter)
            Label4.Text += vbCrLf & counter + 1 & ". " & AIMediumList(counter)
            Label5.Text += vbCrLf & counter + 1 & ". " & AIHardList(counter)
        Next
        For counter = 1 To 5
            FileClose(counter)
        Next
    End Sub
    Private Sub Leaderboard_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        MainMenu.Show()
    End Sub
End Class