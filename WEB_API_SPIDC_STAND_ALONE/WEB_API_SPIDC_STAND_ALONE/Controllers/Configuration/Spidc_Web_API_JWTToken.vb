Imports System.IdentityModel.Tokens.Jwt
Imports System.Security.Claims
Imports System.Text
Imports Microsoft.IdentityModel.Tokens
Imports System.IdentityModel.Tokens
Public Class Spidc_Web_API_JWTToken
    'SPIDC Config
    Private Shared Spidc_Web_API_Config As New Spidc_Web_API_Config

    'Genrate JWT TOKEN
    Public Shared Function GenerateJwtToken(username As String, appName As String, accountNo As String, urlOrigin As String) As String
        ' Call THE SPIDC WEB API CONFIG
        Spidc_Web_API_Config.WebApiConfig()

        ' Define the key that will be used to sign the token (this is a secret key)
        Dim securityKey As New SymmetricSecurityKey(Text.Encoding.UTF8.GetBytes(Spidc_Web_API_Config._mAppJWTTokenSecurityKey))

        ' Create the signing credentials using an HMACSHA256 algorithm
        Dim signingCredentials As New SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)

        ' Set the token expiration to a suitable time from the current time (e.g., 10 minutes)
        Dim expirationTime As DateTime = DateTime.UtcNow.AddMinutes(10)

        ' Set the issued-at (iat) claim to the current time
        Dim issuedAt As DateTime = DateTime.UtcNow

        ' Create a list of claims for the payload
        Dim claims As New List(Of Claim)()
        claims.Add(New Claim(ClaimTypes.Email, username))
        claims.Add(New Claim("appName", appName))
        claims.Add(New Claim("accountNo", accountNo))
        claims.Add(New Claim("iat", issuedAt))

        ' Create a token with the claims, signing credentials, and the specified expiration time
        Dim token As New JwtSecurityToken(
            issuer:=Spidc_Web_API_Config._mAppJWTTokenISSUER,
            audience:=urlOrigin,
            notBefore:=issuedAt,
            expires:=expirationTime,
            claims:=claims,
            signingCredentials:=signingCredentials
        )

        ' Encode the token as a string
        Dim tokenHandler As New JwtSecurityTokenHandler()
        Dim tokenString As String = tokenHandler.WriteToken(token)
        Return tokenString
    End Function


    'Validate JWT TOKEN
    Public Shared Function ValidateJwt(jwtToken As String) As Boolean
        ' Call THE SPIDC WEB API CONFIG  Spidc_Web_API_Config._mAppJWTTokenISSUER .ValidAudience = "https://online.spidc.com.ph/caloocan/WebPortal/Register.aspx",
        Spidc_Web_API_Config.WebApiConfig()
        Dim tokenHandler As New JwtSecurityTokenHandler()
        Dim validationParameters As New TokenValidationParameters() With {
            .ValidateIssuer = True,
            .ValidIssuer = Spidc_Web_API_Config._mAppJWTTokenISSUER,
            .ValidateAudience = False,
            .ValidateLifetime = True,
            .ValidateIssuerSigningKey = True,
            .IssuerSigningKey = New SymmetricSecurityKey(Text.Encoding.UTF8.GetBytes(Spidc_Web_API_Config._mAppJWTTokenSecurityKey))
        }

        Try
            Dim claimsPrincipal As ClaimsPrincipal = tokenHandler.ValidateToken(jwtToken, validationParameters, Nothing)
            ' Check if the token has expired
            Dim expirationClaim As Claim = claimsPrincipal.FindFirst(ClaimTypes.Expiration)
            If expirationClaim IsNot Nothing Then
                Dim expirationTime As DateTime = DateTime.Parse(expirationClaim.Value)
                If expirationTime <= DateTime.UtcNow Then
                    ' Token has expired
                    Return False
                End If
            End If
            ' Token is valid
            Return True
        Catch ex As SecurityTokenException
            ' Token validation failed
            Return False
        End Try
    End Function




End Class
