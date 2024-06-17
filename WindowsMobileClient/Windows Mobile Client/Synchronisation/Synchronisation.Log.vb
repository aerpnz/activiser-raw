Imports System
Imports System.Text
Imports System.Collections.Generic

Public Class SyncLog
    ReadOnly MessageFormat As String = "{0:HH:mm:ss}: {1}" & vbNewLine

    Private _FullLog As New StringBuilder(8192)
    Private _lastEntry As String

    Public Event EntryAdded As EventHandler
    Public Event Cleared As EventHandler

    Private _descendingOrder As Boolean = True
    Public Property DescendingOrder() As Boolean
        Get
            Return _descendingOrder
        End Get
        Set(ByVal value As Boolean)
            _descendingOrder = value
        End Set
    End Property


    Public Sub AddEntry(ByVal message As String)
        _lastEntry = message
        Dim eventTime As DateTime = DateTime.Now
        Dim eventData As SyncLogEntryAddedEventArgs = New SyncLogEntryAddedEventArgs(eventTime, message)
        '_SyncLog.Add(New LogEntry(eventTime, message))
        If DescendingOrder Then
            _FullLog.Insert(0, String.Format(WithCulture, MessageFormat, eventTime, message))
        Else
            _FullLog.AppendFormat(WithCulture, MessageFormat, eventTime, message)
        End If

        RaiseEvent EntryAdded(Me, eventData)
    End Sub

    Public Sub AddEntry(ByVal messageFormat As String, ByVal ParamArray args() As String)
        Dim message As String = String.Format(WithCulture, messageFormat, args)
        _lastEntry = message
        Dim eventTime As DateTime = DateTime.Now
        Dim eventData As SyncLogEntryAddedEventArgs = New SyncLogEntryAddedEventArgs(eventTime, message)
        '_SyncLog.Add(New LogEntry(eventTime, message))
        If DescendingOrder Then
            _FullLog.Insert(0, String.Format(WithCulture, messageFormat, eventTime, message))
        Else
            _FullLog.AppendFormat(WithCulture, messageFormat, eventTime, message)
        End If

        RaiseEvent EntryAdded(Me, eventData)
    End Sub

    Shared ReadOnly HR As String = New String("-"c, 40) & vbNewLine
    Public Sub AddBreak()
        If DescendingOrder Then
            _FullLog.Insert(0, HR)
        Else
            _FullLog.Append(HR)
        End If
    End Sub

    Public Sub Clear()
        '_SyncLog.Clear()
        _FullLog.Length = 0
        _lastEntry = Nothing
        RaiseEvent Cleared(Me, New System.EventArgs())
    End Sub

    Public ReadOnly Property Text() As String
        Get
            Return _FullLog.ToString()
        End Get
    End Property

    Public ReadOnly Property LastEntry() As String
        Get
            Return _lastEntry
        End Get
    End Property


End Class
