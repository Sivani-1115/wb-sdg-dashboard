# Simple test for login functionality
$baseUrl = "https://localhost:7000"  # Adjust port if different
$loginUrl = "$baseUrl/api/auth/login"

# Test login data
$loginData = @{
    Username = "unas"
    Password = "unas"
} | ConvertTo-Json

Write-Host "Testing login with user 'unas'..."
Write-Host "Request data: $loginData"

try {
    $loginResponse = Invoke-RestMethod -Uri $loginUrl -Method POST -Body $loginData -ContentType "application/json"
    Write-Host "Login successful!"
    Write-Host "Token: $($loginResponse.Token)"
    Write-Host "Role: $($loginResponse.Role)"
} catch {
    Write-Host "Login failed: $($_.Exception.Message)"
    if ($_.Exception.Response) {
        $reader = New-Object System.IO.StreamReader($_.Exception.Response.GetResponseStream())
        $responseBody = $reader.ReadToEnd()
        Write-Host "Response body: $responseBody"
    }
}
