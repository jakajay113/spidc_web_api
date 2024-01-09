Imports System.IO
Imports DotEnv
Imports Newtonsoft.Json
Imports System.Data.SqlClient
Imports System.Web.Script.Serialization
Imports System.Threading.Tasks
Imports System.Net.Http
Imports System.Web.Http
Imports System.Web.Http.Filters
Imports System.Net
Imports System
Imports System.Collections.Generic
Public Class Spidc_Web_API_Param_Checker_Config
    'SPIDC Config
    Private Shared Spidc_Web_API_Config As New Spidc_Web_API_Config
    Public Shared _param As String
    Public Shared _paramType As String()
    Public Shared _paramTypeFound As Boolean = False
    Public Shared _paramFoundVar As String

    Public Shared _paramCheckFound As String
    Public Shared _appSystemList As String()
    Public Shared _dbConnNane As String


    '--------------------------------------------------------------------------------------CHECKER PARAM FAMS--------------------------------------------------------------------------------------------------
    Public Shared Function _mCheckParamFams(ByVal param As String)
        'SPIDC Config
        Spidc_Web_API_Config.WebApiConfig()
        'List Of Method Param In SPIDC Config  GET/POST/PUT/DELETE
        _paramType = {
            Spidc_Web_API_Config._mAppGetParam1Fams,
            Spidc_Web_API_Config._mAppGetParam2Fams,
            Spidc_Web_API_Config._mAppGetParam3Fams,
            Spidc_Web_API_Config._mAppGetParam4Fams,
            Spidc_Web_API_Config._mAppGetParam5Fams,
            Spidc_Web_API_Config._mAppGetParam6Fams,
            Spidc_Web_API_Config._mAppGetParam7Fams,
            Spidc_Web_API_Config._mAppGetParam8Fams,
            Spidc_Web_API_Config._mAppGetParam9Fams,
            Spidc_Web_API_Config._mAppGetParam10Fams,
            Spidc_Web_API_Config._mAppGetParam11Fams,
            Spidc_Web_API_Config._mAppGetParam12Fams,
            Spidc_Web_API_Config._mAppGetParam13Fams,
            Spidc_Web_API_Config._mAppGetParam14Fams,
            Spidc_Web_API_Config._mAppPostParam1Fams,
            Spidc_Web_API_Config._mAppPostParam2Fams,
            Spidc_Web_API_Config._mAppPostParam3Fams,
            Spidc_Web_API_Config._mAppPostParam4Fams,
            Spidc_Web_API_Config._mAppPostParam5Fams,
            Spidc_Web_API_Config._mAppPostParam6Fams,
            Spidc_Web_API_Config._mAppPostParam7Fams,
            Spidc_Web_API_Config._mAppPostParam8Fams,
            Spidc_Web_API_Config._mAppPostParam9Fams,
            Spidc_Web_API_Config._mAppPostParam10Fams,
            Spidc_Web_API_Config._mAppPostParam11Fams,
            Spidc_Web_API_Config._mAppPostParam12Fams,
            Spidc_Web_API_Config._mAppPostParam13Fams,
            Spidc_Web_API_Config._mAppPostParam14Fams,
            Spidc_Web_API_Config._mAppPutParam1Fams,
            Spidc_Web_API_Config._mAppPutParam2Fams,
            Spidc_Web_API_Config._mAppPutParam3Fams,
            Spidc_Web_API_Config._mAppPutParam4Fams,
            Spidc_Web_API_Config._mAppPutParam5Fams,
            Spidc_Web_API_Config._mAppPutParam6Fams,
            Spidc_Web_API_Config._mAppPutParam7Fams,
            Spidc_Web_API_Config._mAppPutParam8Fams,
            Spidc_Web_API_Config._mAppPutParam9Fams,
            Spidc_Web_API_Config._mAppPutParam10Fams,
            Spidc_Web_API_Config._mAppPutParam11Fams,
            Spidc_Web_API_Config._mAppPutParam12Fams,
            Spidc_Web_API_Config._mAppPutParam13Fams,
            Spidc_Web_API_Config._mAppPutParam14Fams,
            Spidc_Web_API_Config._mAppDeleteParam1Fams,
            Spidc_Web_API_Config._mAppDeleteParam2Fams,
            Spidc_Web_API_Config._mAppDeleteParam3Fams,
            Spidc_Web_API_Config._mAppDeleteParam4Fams,
            Spidc_Web_API_Config._mAppDeleteParam5Fams,
            Spidc_Web_API_Config._mAppDeleteParam6Fams,
            Spidc_Web_API_Config._mAppDeleteParam7Fams,
            Spidc_Web_API_Config._mAppDeleteParam8Fams,
            Spidc_Web_API_Config._mAppDeleteParam9Fams,
            Spidc_Web_API_Config._mAppDeleteParam10Fams,
            Spidc_Web_API_Config._mAppDeleteParam11Fams,
            Spidc_Web_API_Config._mAppDeleteParam12Fams,
            Spidc_Web_API_Config._mAppDeleteParam13Fams,
            Spidc_Web_API_Config._mAppDeleteParam14Fams
            }
        Return ""
    End Function


    '--------------------------------------------------------------------------------------CHECKER PARAM EIS--------------------------------------------------------------------------------------------------
    Public Shared Function _mCheckParamEis(ByVal param As String) As String
        'SPIDC Config
        Spidc_Web_API_Config.WebApiConfig()
        'List Of Method Param In SPIDC Config GET/POST/PUT/DELETE
        _param = param
        ' YOU CAN ADD MORE METHOD HERE IN ARRAY BASE ON CONFIG
        _paramType = {
             Spidc_Web_API_Config._mAppGetParam2Eis,
             Spidc_Web_API_Config._mAppGetParam1Eis
         }
        For Each _foundParam As String In _paramType
            If _param.Contains(_foundParam) Then
                _paramFoundVar = _foundParam
                _paramTypeFound = True
                Exit For
            Else
                _paramTypeFound = False
            End If
        Next
        'Check the param found if the system  name is in the param to Return the Connection  Name
        _paramCheckFound = _paramFoundVar.ToUpper()
        'List Of APP/SYSTEM YOU CAN ADD MORE APP/SYSTEM HERE IN ARRAY 
        _appSystemList = {
                  Spidc_Web_API_Config._mAppName,
                  Spidc_Web_API_Config._mAppNameFAMS,
                  Spidc_Web_API_Config._mAppNameGAAMS,
                  Spidc_Web_API_Config._mAppNameTOIMS,
                  Spidc_Web_API_Config._mAppNameTIMS,
                  Spidc_Web_API_Config._mAppNameCedula
        }
        Dim matchingAppSystem = _appSystemList.Where(Function(connectionName) _paramCheckFound.Contains(connectionName))
        For Each connectionName As String In matchingAppSystem
            _dbConnNane = connectionName
        Next
        Return _dbConnNane
    End Function


    '--------------------------------------------------------------------------------------CHECKER PARAM WSP CEDULA--------------------------------------------------------------------------------------------------
    Public Shared Function _mCheckParamCedula(ByVal param As String) As String
         'SPIDC Config
        Spidc_Web_API_Config.WebApiConfig()
        'List Of Method Param In SPIDC Config GET/POST/PUT/DELETE
        _param = param
        ' YOU CAN ADD MORE METHOD HERE IN ARRAY BASE ON CONFIG
        _paramType = {
            Spidc_Web_API_Config._mAppGetParam1Tims,
            Spidc_Web_API_Config._mAppGetParam2Tims,
            Spidc_Web_API_Config._mAppGetParam3Toims,
            Spidc_Web_API_Config._mAppGetParam4Toims,
            Spidc_Web_API_Config._mAppGetParam5Toims,
            Spidc_Web_API_Config._mAppGetParam6Toims,
            Spidc_Web_API_Config._mAppGetParam7Toims,
            Spidc_Web_API_Config._mAppGetParam10Toims,
             Spidc_Web_API_Config._mAppGetParam11Toims,
            Spidc_Web_API_Config._mAppGetParam8Tims,
            Spidc_Web_API_Config._mAppGetParam9Tims,
            Spidc_Web_API_Config._mAppPostParam,
            Spidc_Web_API_Config._mAppPostParam1Tims,
            Spidc_Web_API_Config._mAppPostParam2Tims,
            Spidc_Web_API_Config._mAppPutParam1Tims
        }
        For Each _foundParam As String In _paramType
            If _param.Contains(_foundParam) Then
                _paramFoundVar = _foundParam
                _paramTypeFound = True
                Exit For
            Else
                _paramTypeFound = False
            End If
        Next
        'Check the param found if the system  name is in the param to Return the Connection  Name
        _paramCheckFound = _paramFoundVar.ToUpper()
        'List Of APP/SYSTEM YOU CAN ADD MORE APP/SYSTEM HERE IN ARRAY 
        _appSystemList = {
                          Spidc_Web_API_Config._mAppName,
                          Spidc_Web_API_Config._mAppNameOAIMS,
                          Spidc_Web_API_Config._mAppNameFAMS,
                          Spidc_Web_API_Config._mAppNameGAAMS,
                          Spidc_Web_API_Config._mAppNameTOIMS,
                          Spidc_Web_API_Config._mAppNameTIMS
        }
        Dim matchingAppSystem = _appSystemList.Where(Function(connectionName) _paramCheckFound.Contains(connectionName))
        For Each connectionName As String In matchingAppSystem
            _dbConnNane = connectionName
        Next
        Return _dbConnNane
    End Function



    '--------------------------------------------------------------------------------------CHECKER PARAM WSP UNIVERSAL CHECKOUT--------------------------------------------------------------------------------------------------
    Public Shared Function _mCheckParamUniversalCheckout(ByVal param As String) As String
        'SPIDC Config
        Spidc_Web_API_Config.WebApiConfig()
        'List Of Method Param In SPIDC Config GET/POST/PUT/DELETE
        _param = param
        ' YOU CAN ADD MORE METHOD HERE IN ARRAY BASE ON CONFIG
        _paramType = {
            Spidc_Web_API_Config._mAppPostParamUniversalCheckOut,
            Spidc_Web_API_Config._mAppPostParamUniversalCheckOutProceed,
            Spidc_Web_API_Config._mAppGetParamUniversalCheckOut,
            Spidc_Web_API_Config._mAppGetParamUniversalCheckOutPaymentMethod,
             Spidc_Web_API_Config._mAppDeleteParamUniversalCheckOut
        }
        For Each _foundParam As String In _paramType
            If _param.Contains(_foundParam) Then
                _paramFoundVar = _foundParam
                _paramTypeFound = True
                Exit For
            Else
                _paramTypeFound = False
            End If
        Next
        'Check the param found if the system  name is in the param to Return the Connection  Name
        _paramCheckFound = _paramFoundVar.ToUpper()
        'List Of APP/SYSTEM YOU CAN ADD MORE APP/SYSTEM HERE IN ARRAY 
        _appSystemList = {Spidc_Web_API_Config._mAppNameUniversalCheckout
                          }
        Dim matchingAppSystem = _appSystemList.Where(Function(connectionName) _paramCheckFound.Contains(connectionName))
        For Each connectionName As String In matchingAppSystem
            _dbConnNane = connectionName
        Next
        Return _dbConnNane
    End Function

End Class
