﻿Option Explicit On ''Added 1/28/2019 td
Option Strict On ''Added 1/28/2019 td

''Added 1/28/2019 td

Imports System.ComponentModel ''Added 1/28/2019 td

Public Class BinaryDataControl
    ''
    ''Added 1/28/2019 td   
    ''
    Public ErrorMessageBuilder As System.Text.StringBuilder ''Added 1/29/2019 thomas downes
    Public CurrentErrorMessage As System.Text.StringBuilder ''String ''Added 1/29/2019 thomas downes

    Private _classCardNumber As ClassCardNumber ''Added 1/29/2019 td  
    Private Const mc_bUseCardNumberClass As Boolean = True ''1/29 td''False ''Added 1/29/2019 td 

    Private _intFacilityCode As Integer ''Added 1/28/2019 td
    Private _longCardNumber As Long ''Added 1/28/2019 td

    Public WriteOnly Property Verbose() As Boolean
        ''Added 1/29/2019 td
        Set(value As Boolean)
            ''Added 1/29/2019 td
            UserControlC4_3.Verbose = value ''Boolean
            UserControlC4_2.Verbose = value ''Boolean
            UserControlC4_1.Verbose = value ''Boolean
            UserControlC4_0.Verbose = value ''Boolean
        End Set
    End Property

    Public Overrides Property Text() As String
        Get
            Return Me.ToString() ''Added 1/29/2019 td 
        End Get
        Set(value As String)
            ''Do nothing
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
    Description("Facility Code"), Browsable(True)>
    Public Property FacilityCode() As String
        Get
            Return _intFacilityCode.ToString()
        End Get
        Set(value As String)
            ''Dim int_Result As Integer
            Integer.TryParse(value, _intFacilityCode)

            ''Power of 16.  (hard-coded)
            UserControlF4_0.PowerOf16 = "0"
            UserControlF4_1.PowerOf16 = "1"

            ''Propagate to the sub-controls.  
            UserControlF4_0.FacilityCode = _intFacilityCode.ToString
            UserControlF4_1.FacilityCode = _intFacilityCode.ToString

            ''Added 1/29/2019 td
            UserControlC4_3.ErrorMessageBuilder = Me.ErrorMessageBuilder
            UserControlC4_2.ErrorMessageBuilder = Me.ErrorMessageBuilder
            UserControlC4_1.ErrorMessageBuilder = Me.ErrorMessageBuilder
            UserControlC4_0.ErrorMessageBuilder = Me.ErrorMessageBuilder

            ''Added 1/29/2019 td
            UserControlC4_3.CurrentErrorMessage = Me.CurrentErrorMessage
            UserControlC4_2.CurrentErrorMessage = Me.CurrentErrorMessage
            UserControlC4_1.CurrentErrorMessage = Me.CurrentErrorMessage
            UserControlC4_0.CurrentErrorMessage = Me.CurrentErrorMessage

            ''Added 1/29/2019 td
            UserControlC4_3.NextOneToTheRight = UserControlC4_2
            UserControlC4_2.NextOneToTheRight = UserControlC4_1
            UserControlC4_1.NextOneToTheRight = UserControlC4_0
            UserControlC4_0.NextOneToTheRight = Nothing

            ''Added 1/29/2019 td
            BuildClassNumberIfNeeded()

            ''Added 1/28/2019 td
            UpdateParityControls()

        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
    Description("Card Number"),
    Browsable(True)>
    Public Property CardNumber() As String
        Get
            Return _longCardNumber.ToString()
        End Get
        Set(value As String)
            ''Dim int_Result As Integer
            Long.TryParse(value, _longCardNumber)

            ''Power of 16.  (hard-coded)
            UserControlC4_0.PowerOf16 = "0"
            UserControlC4_1.PowerOf16 = "1"
            UserControlC4_2.PowerOf16 = "2"
            UserControlC4_3.PowerOf16 = "3"

            ''Added 1/29/2019 td
            If (mc_bUseCardNumberClass) Then

                ''Added 1/29/2019 td
                BuildClassNumberIfNeeded()

                ''Propagate to the sub-controls.  
                ''  Doesn't work well. ---1/29 td''UserControlC4_0.CardNumber = _longCardNumber.ToString
                ''  Doesn't work well. ---1/29 td''UserControlC4_1.CardNumber = _longCardNumber.ToString
                ''  Doesn't work well. ---1/29 td''UserControlC4_2.CardNumber = _longCardNumber.ToString
                ''  Doesn't work well. ---1/29 td''UserControlC4_3.CardNumber = _longCardNumber.ToString
                _classCardNumber.CardNumber = value ''_longCardNumber

            Else
                UserControlC4_0.CardNumber_Deprecated = _longCardNumber.ToString
                UserControlC4_1.CardNumber_Deprecated = _longCardNumber.ToString
                UserControlC4_2.CardNumber_Deprecated = _longCardNumber.ToString
                UserControlC4_3.CardNumber_Deprecated = _longCardNumber.ToString

            End If ''eNd of "If (mc_boolCardNumberClass) Then .... Else ...."

            ''Added 1/28/2019 td
            UpdateParityControls()

        End Set
    End Property

    Public Function GetDecimalValue() As Long
        ''
        ''Added 1/28/2019 thomas downes  
        ''
        Dim strLongBinaryString As String

        strLongBinaryString =
            UserControlParityEven.ToString() &
            UserControlF4_1.ToString() & UserControlF4_0.ToString() &
            UserControlC4_3.ToString() & UserControlC4_2.ToString() &
            UserControlC4_1.ToString() & UserControlC4_0.ToString() &
            UserControlParityOdd.ToString()

        Return WeigandCalculator_CS.ClassStaticBinary.ConvertBinaryString_ToLong(strLongBinaryString)

    End Function ''End of "Public Function GetDecimalValue() As Long"

    Public Overrides Function ToString() As String
        ''Return MyBase.ToString()

        ''Dim strOutput As String = ""

        ''strOutput = UserControlP1.ToString()

        ''strOutput &= ("-" & UserControlF_1.ToString())
        ''strOutput &= ("-" & UserControlF_0.ToString())

        ''strOutput &= ("-" & UserControlC_3.ToString())
        ''strOutput &= ("-" & UserControlC_2.ToString())
        ''strOutput &= ("-" & UserControlC_1.ToString())
        ''strOutput &= ("-" & UserControlC_0.ToString())

        ''strOutput &= ("-" & UserControlP2.ToString())

        ''Return strOutput

        Return ToString_WithSeparator("-")

    End Function ''End of "Public Overrides Function ToString() As String"

    Public Function ToString_WithSeparator(pstrDash As String) As String
        ''Return MyBase.ToString()

        Dim strOutput As String = ""

        strOutput = UserControlParityEven.ToString()

        strOutput &= (pstrDash & UserControlF4_1.ToString())
        strOutput &= (pstrDash & UserControlF4_0.ToString())

        strOutput &= (pstrDash & UserControlC4_3.ToString())
        strOutput &= (pstrDash & UserControlC4_2.ToString())
        strOutput &= (pstrDash & UserControlC4_1.ToString())
        strOutput &= (pstrDash & UserControlC4_0.ToString())

        strOutput &= (pstrDash & UserControlParityOdd.ToString())

        Return strOutput

    End Function ''End of Public Function ToString_WithSeparator(pstrDash As String) As String

    Public Sub BuildClassNumberIfNeeded()

        ''Added 1/29/2019 td
        If (_classCardNumber Is Nothing) Then
            ''Added 1/29/2019 td
            _classCardNumber = New ClassCardNumber(UserControlC4_3, UserControlC4_2, UserControlC4_1, UserControlC4_0)
        End If ''End of "If (_classCardNumber Is Nothing) Then"

    End Sub ''End of "Public Sub BuildClassNumberIfNeeded()"

    Public Sub UpdateParityControls()

        ''Even Parity Bit
        ''   --Added 1/28/2019 td
        UserControlParityEven.LongBinaryString =
                UserControlF4_1.ToString() & UserControlF4_0.ToString() &
                UserControlC4_3.ToString()

        ''Odd Parity Bit
        ''  --Added 1/28/2019 td
        UserControlParityOdd.LongBinaryString =
                UserControlC4_2.ToString() & UserControlC4_1.ToString() &
                UserControlC4_0.ToString()

    End Sub

End Class
