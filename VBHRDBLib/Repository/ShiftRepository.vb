Imports System.Data.SqlClient
Imports VBHRDBLib.Context
Imports VBHRDBLib.Model

Namespace Repository
    Public Class ShiftRepository
        Implements IShiftRepository

        Private ReadOnly _context As IRepositoryContext

        Public Sub New(context As IRepositoryContext)
            _context = context
        End Sub

        Public Function CreateShift(shift As Shift) As Shift Implements IShiftRepository.CreateShift
            Dim newSHift As New Shift()

            Dim stmt As String = "SET IDENTITY_INSERT hr.shift ON; " &
                                 "Insert into hr.shift(shift_id, shift_name, shift_start_time, shift_end_time) values (@id, @name, @startTime, @endTime); " &
                                 "select cast (scope_identity() as int); " &
                                 "SET IDENTITY_INSERT hr.shift OFF; "

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    cmd.Parameters.AddWithValue("@id", shift.ShiftId)
                    cmd.Parameters.AddWithValue("@name", shift.ShiftName)
                    cmd.Parameters.AddWithValue("@startTime", shift.ShiftStartTime)
                    cmd.Parameters.AddWithValue("@endTime", shift.ShiftEndTime)
                    Try
                        conn.Open()
                        'ExecuteScalar return 1 row and get first column
                        Dim regionId As Int32 = Convert.ToInt32(cmd.ExecuteScalar())
                        newSHift = FindShiftById(shift.ShiftId)

                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using
            Return newSHift
        End Function

        Public Function DeleteShift(id As Integer) As Integer Implements IShiftRepository.DeleteShift
            Dim rowEffect As Int32 = 0

            'declare stnt
            Dim stmt As String = "delete from hr.shift where shift_id = @id;"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    cmd.Parameters.AddWithValue("@id", id)

                    Try
                        conn.Open()
                        rowEffect = cmd.ExecuteNonQuery()

                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using
            Return rowEffect
        End Function

        Public Function FindAllShift() As List(Of Shift) Implements IShiftRepository.FindAllShift
            Dim shiftList As New List(Of Shift)

            'declare statement
            Dim stmt As String = "select shift_id, shift_name, shift_start_time, shift_end_time from hr.shift " &
                                "order by shift_id asc;"

            'try to connect
            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    Try
                        conn.Open()
                        Dim reader = cmd.ExecuteReader()

                        While reader.Read()
                            shiftList.Add(New Shift() With {
                                .ShiftId = reader.GetInt32(0),
                                .ShiftName = reader.GetString(1),
                                .ShiftStartTime = reader.GetDateTime(2),
                                .ShiftEndTime = reader.GetDateTime(3)
                            })
                        End While

                        reader.Close()
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using
            Return shiftList
        End Function

        Public Function FindShiftById(id As Integer) As Shift Implements IShiftRepository.FindShiftById
            Dim shift As New Shift With {.ShiftId = id}

            'sql statement
            Dim stmt As String = "Select shift_id, shift_name, shift_start_time, shift_end_time from hr.shift " &
                                 "where shift_id = @id;"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    cmd.Parameters.AddWithValue("@id", id)

                    Try
                        conn.Open()
                        Dim reader = cmd.ExecuteReader()
                        If reader.HasRows Then
                            reader.Read()
                            shift.ShiftName = reader.GetString(1)
                            shift.ShiftStartTime = reader.GetDateTime(2)
                            shift.ShiftEndTime = reader.GetDateTime(3)
                        End If

                        reader.Close()
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using
            Return shift
        End Function

        Public Function UpdateShiftBySp(id As Integer, name As String, startTime As DateTime, endTime As DateTime, Optional showCommand As Boolean = False) As Boolean Implements IShiftRepository.UpdateShiftBySp
            'declare stnt
            Dim stmt As String = "hr.spUpdateShift @id, @name, @startTime, @endTime"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    cmd.Parameters.AddWithValue("@id", id)
                    cmd.Parameters.AddWithValue("@name", name)
                    cmd.Parameters.AddWithValue("@startTime", startTime)
                    cmd.Parameters.AddWithValue("@endTime", endTime)

                    'show command
                    If showCommand Then
                        Console.WriteLine(cmd.CommandText)
                    End If

                    Try
                        conn.Open()
                        cmd.ExecuteNonQuery()

                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using
            Return True
        End Function
    End Class
End Namespace