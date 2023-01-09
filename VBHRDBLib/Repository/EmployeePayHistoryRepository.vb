Imports System.Data.SqlClient
Imports VBHRDBLib.Context
Imports VBHRDBLib.Model

Namespace Repository
    Public Class EmployeePayHistoryRepository
        Implements IEmployeePayHistoryRepository

        Private ReadOnly _context As IRepositoryContext

        Public Sub New(context As IRepositoryContext)
            _context = context
        End Sub

        Public Function CreateEmployeePayHistory(ephi As EmployeePayHistory) As EmployeePayHistory Implements IEmployeePayHistoryRepository.CreateEmployeePayHistory
            Dim newEmployeePayHistory As New EmployeePayHistory()

            'declare stnt
            Dim stmt As String = "SET IDENTITY_INSERT hr.employee_pay_history ON; " &
                                 "Insert into hr.employee_pay_history(ephi_emp_id, ephi_rate_change_date, ephi_rate_salary, ephi_pay_frequence, ephi_modified_date) " &
                                 "values (@id, @rateChangeDate. @rateSalary, @payFrequence, @modDate); " &
                                 "select cast (scope_identity() as int); " &
                                 "SET IDENTITY_INSERT hr.employee_pay_history OFF; "

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    cmd.Parameters.AddWithValue("@id", ephi.EmphiEmpId)
                    cmd.Parameters.AddWithValue("@rateChangeDate", ephi.EphiRateChangeDate)
                    cmd.Parameters.AddWithValue("@rateSalary", ephi.EphiRateSalary)
                    cmd.Parameters.AddWithValue("@payFrequence", ephi.EphiPayFrequence)
                    cmd.Parameters.AddWithValue("@ephi_modified_date", ephi.EphiModifiedDate)

                    Try
                        conn.Open()
                        'ExecuteScalar return 1 row and get first column
                        Dim regionId As Int32 = Convert.ToInt32(cmd.ExecuteScalar())
                        newEmployeePayHistory = FindEmployeePayHistoryById(ephi.EmphiEmpId)

                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using
            Return newEmployeePayHistory
        End Function

        Public Function DeleteEmployeePayHistory(id As Integer) As Integer Implements IEmployeePayHistoryRepository.DeleteEmployeePayHistory
            Dim rowEffect As Int32 = 0

            'declare stnt
            Dim stmt As String = "delete from hr.employee_pay_history where ephi_emp_id = @id;"

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

        Public Function FindAllEmployeePayHistory() As List(Of EmployeePayHistory) Implements IEmployeePayHistoryRepository.FindAllEmployeePayHistory
            Dim employeePayHistoryList As New List(Of EmployeePayHistory)

            'declare statement
            Dim stmt As String = "select ephi_emp_id, ephi_rate_change_date, ephi_rate_salary, ephi_pay_frequence, ephi_modified_date from hr.employee_pay_history " &
                                "order by ephi_emp_id asc;"

            'try to connect 
            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    Try
                        conn.Open()
                        Dim reader = cmd.ExecuteReader()

                        While reader.Read()
                            employeePayHistoryList.Add(New EmployeePayHistory() With {
                                .EmphiEmpId = reader.GetInt32(0),
                                .EphiRateChangeDate = reader.GetDateTime(1),
                                .EphiRateSalary = reader.GetInt32(2),
                                .EphiPayFrequence = reader.GetInt32(3),
                                .EphiModifiedDate = reader.GetDateTime(4)
                            })
                        End While

                        reader.Close()
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using
            Return employeePayHistoryList
        End Function

        Public Function FindEmployeePayHistoryById(id As Integer) As EmployeePayHistory Implements IEmployeePayHistoryRepository.FindEmployeePayHistoryById
            Dim employeePayHistory As New EmployeePayHistory With {.EmphiEmpId = id}

            'sql statement
            Dim stmt As String = "Select ephi_emp_id, ephi_rate_change_date, ephi_rate_salary, ephi_pay_frequence, ephi_modified_date from hr.employee_pay_history " &
                                 "where ephi_emp_id = @id;"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    cmd.Parameters.AddWithValue("@id", id)

                    Try
                        conn.Open()
                        Dim reader = cmd.ExecuteReader()
                        If reader.HasRows Then
                            reader.Read()
                            employeePayHistory.EphiRateChangeDate = reader.GetDateTime(1)
                            employeePayHistory.EphiRateSalary = reader.GetInt32(2)
                            employeePayHistory.EphiPayFrequence = reader.GetInt32(3)
                            employeePayHistory.EphiModifiedDate = reader.GetDateTime(4)
                        End If

                        reader.Close()
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using
            Return employeePayHistory
        End Function

        Public Function UpdateEmployeePayHistoryBySp(id As Integer, rateChangeDate As Date, Optional rateSalary As Integer = Nothing, Optional payFrequence As Integer = Nothing, Optional modDate As Date = #1/1/0001 12:00:00 AM#, Optional showCommand As Boolean = False) As Boolean Implements IEmployeePayHistoryRepository.UpdateEmployeePayHistoryBySp
            Dim stmt As String = "hr.spUpdateEmployeePayHistory @id, @rateChangeDate, @rateSalary, @payFrequence, @modDate"


            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = stmt}
                    cmd.Parameters.AddWithValue("@id", id)
                    cmd.Parameters.AddWithValue("@rateChangeDate", rateChangeDate)
                    cmd.Parameters.AddWithValue("@rateSalary", rateSalary)
                    cmd.Parameters.AddWithValue("@payFrequence", payFrequence)
                    cmd.Parameters.AddWithValue("@modDate", modDate)

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