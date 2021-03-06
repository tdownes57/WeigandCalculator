﻿Option Explicit On ''Added 1/28/2019 td 
Option Strict On ''Added 1/28/2019 td 

Imports System.ComponentModel ''Added 1/28/2019 td

Public Class UserControlP_Even

    ''Private _intPowerOf8 As Integer

    ''''[Description("Test text displayed in the textbox"),Category("Data")]
    ''Public Property PowerOf8() As String
    ''    Get
    ''        Return _intPowerOf8.ToString()
    ''    End Get
    ''    Set(value As String)
    ''        ''Dim int_Result As Integer
    ''        Integer.TryParse(value, _intPowerOf8)
    ''    End Set
    ''End Property

    Private _intBinaryValue As Integer ''ADded 1/28/2019 td 
    Private _strLongBinaryString As String = "" ''Added 1/28/2019 td 

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
  Description("Sequential position (0-based) counting from the right."),
  Browsable(True)>
    Public Property BinaryValue() As String
        Get
            Return _intBinaryValue.ToString()
        End Get
        Set(value As String)
            ''Dim int_Result As Integer
            Integer.TryParse(value, _intBinaryValue)
            TextBox1.Text = _intBinaryValue.ToString
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
  Description("Binary string, for example 100101110000."),
  Browsable(True)>
    Public Property LongBinaryString() As String
        Get
            Return _strLongBinaryString.ToString()
        End Get
        Set(value As String)
            ''Dim int_Result As Integer
            Dim intNumOnes As Integer = 0 ''Added 1/29/2019 thomas downes  
            Dim str_out As String = "out" ''Added 1/29/2019 thomas downes  

            _strLongBinaryString = value
            ''1/29/2019 td''TextBox1.Text = WeigandCalculator_CS.ClassStatic.GetParityBit_Even(_strLongBinaryString)
            TextBox1.Text = WeigandCalculator_CS.ClassStaticBinary.GetParityBit_Even(_strLongBinaryString, intNumOnes)
            _intBinaryValue = Integer.Parse(TextBox1.Text) ''Added 1/29/2019 td

            ''Add "with" or "without" to  the label caption.   --- 1/29/2019 td
            ''str_out = IIf("1" = TextBox1.Text, "with", "without").ToString() ''Added 1/29/2019 td
            str_out = IIf("1" = TextBox1.Text, "with P (= 1)", "without P (= 0)").ToString() ''Added 1/29/2019 td
            LabelEvenParityField.Text = String.Format(LabelEvenParityField.Tag.ToString(), str_out, intNumOnes)

        End Set
    End Property

    Public Overrides Function ToString() As String
        ''Return MyBase.ToString()

        Return _intBinaryValue.ToString

    End Function ''End of "Public Overrides Function ToString() As String"

End Class
