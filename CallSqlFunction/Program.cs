/*
 * Execution Policies Documentations: https://docs.microsoft.com/tr-tr/dotnet/api/system.data.sqlclient.sqlcommand.executescalar?view=dotnet-plat-ext-3.1
*/
using CallSqlFunction;

using System;
using System.Data;
using System.Data.SqlClient;

using static System.Console;

static string Num2Words(int NumericValue = 1472172, string lang = "en")
{
    string NumericString = string.Empty;
    // DESKTOP-ET2MV41
    // DESKTOP-Q3V23GG
    string connString = @"server=DESKTOP-MHUSDAI;
                  initial catalog=School;
                  integrated security=true";
    string sql =
        "SELECT dbo.NumbersToWords_v2 (@NumericValue, @lang)";
    using SqlConnection conn = new SqlConnection(connString);
    SqlCommand cmd = new SqlCommand(sql, conn);

    cmd.Parameters.Add("@NumericValue", SqlDbType.VarChar);
    cmd.Parameters.Add("@lang", SqlDbType.VarChar);
    cmd.Parameters["@NumericValue"].Value = NumericValue;
    cmd.Parameters["@lang"].Value = lang;

    try
    {
        conn.Open();
        NumericString = cmd.ExecuteScalar().ToString();
        WriteLine($"NumericValue: {NumericValue}\n" +
            $"Result: {NumericString}");
    }
    catch (Exception ex)
    {
        WriteLine(ex.Message);
    }
    return NumericString;
}

// *-*-*-*-*-*-*-*-*-*-*-*-*  Number To Words *-*-*-*-*-*-*-*-*-*-*-*-*
string NumericString = Num2Words(NumericValue: 1472172,
                                         lang: "en");
// *-*-*-*-*-*-*-*-*-*-*-*-*  Text To Speech(TTS) *-*-*-*-*-*-*-*-*-*-*-*-*
WriteLine("Say something...");
Speech.OldOnlyWindowsSpeak(NumericString);

// I can't take subscription key for now.
//await Speech.RecognizeSpeechAsync();
ReadLine();