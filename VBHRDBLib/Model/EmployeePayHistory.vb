Imports System.Data.SqlTypes

Namespace Model
    Public Class EmployeePayHistory
        Private _emphiEmpId As Integer
        Private _ephiRateChangeDate As Date
        Private _ephiRateSalary As Integer
        Private _ephiPayFrequence As Integer
        Private _ephiModifiedDate As Date

        Public Sub New()
        End Sub

        Public Sub New(emphiEmpId As Integer, ephiRateChangeDate As Date, Optional ephiRateSalary As Integer = Nothing, Optional ephiPayFrequence As Integer = Nothing,
                       Optional ephiModifiedDate As Date = Nothing)
            _emphiEmpId = emphiEmpId
            _ephiRateChangeDate = ephiRateChangeDate
            _ephiRateSalary = ephiRateSalary
            _ephiPayFrequence = ephiPayFrequence
            _ephiModifiedDate = ephiModifiedDate
        End Sub

        Public Property EmphiEmpId As Integer
            Get
                Return _emphiEmpId
            End Get
            Set(value As Integer)
                _emphiEmpId = value
            End Set
        End Property

        Public Property EphiRateChangeDate As Date
            Get
                Return _ephiRateChangeDate
            End Get
            Set(value As Date)
                _ephiRateChangeDate = value
            End Set
        End Property

        Public Property EphiRateSalary As Integer
            Get
                Return _ephiRateSalary
            End Get
            Set(value As Integer)
                _ephiRateSalary = value
            End Set
        End Property

        Public Property EphiPayFrequence As Integer
            Get
                Return _ephiPayFrequence
            End Get
            Set(value As Integer)
                _ephiPayFrequence = value
            End Set
        End Property

        Public Property EphiModifiedDate As Date
            Get
                Return _ephiModifiedDate
            End Get
            Set(value As Date)
                _ephiModifiedDate = value
            End Set
        End Property
    End Class
End Namespace