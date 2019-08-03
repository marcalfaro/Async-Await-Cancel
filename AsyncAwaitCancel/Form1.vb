Option Explicit On
Imports System.Threading

Public Class Form1

    Private myToken As CancellationTokenSource = Nothing

    Private Async Sub btnToggle_Click(sender As Object, e As EventArgs) Handles btnToggle.Click

        Select Case btnToggle.Text
            Case "Start"
                btnToggle.Text = "Stop"

                If myToken IsNot Nothing Then myToken = Nothing
                myToken = New CancellationTokenSource

                Dim result As String = Await WaitAsync(myToken)
                MsgBox(result)

                btnToggle.Text = "Start"

            Case "Stop"
                If myToken IsNot Nothing Then myToken.Cancel()
        End Select

        ' Display the result.

    End Sub

    Private Async Function WaitAsync(ByVal ct As CancellationTokenSource) As Task(Of String)

        For i As Integer = 1 To 20
            If ct.IsCancellationRequested = True Then
                Exit For
            End If
            BeginInvoke(Sub(ByVal n As Integer)
                            Label1.Text = n
                        End Sub, i)
            Await Task.Delay(1000)
        Next

        Return "Finished - async"
    End Function

End Class
