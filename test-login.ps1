# Test script for login functionality
$baseUrl = "https://localhost:7000"  # Adjust port if different
$registerUrl = "$baseUrl/api/auth/register"
$loginUrl = "$baseUrl/api/auth/login"

# Test user data
$testUser = @{
    Username = "testuser"
    Email = "test@example.com"
    Password = "TestPassword123!"
    Role = "User"
} | ConvertTo-Json

$loginData = @{
    Username = "testuser"
    Password = "TestPassword123!"
} | ConvertTo-Json

Write-Host "Testing registration..."
try {
    $registerResponse = Invoke-RestMethod -Uri $registerUrl -Method POST -Body $testUser -ContentType "application/json"
    Write-Host "Registration successful: $registerResponse"
} catch {
    Write-Host "Registration failed: $($_.Exception.Message)"
}

Write-Host "`nTesting login..."
try {
    $loginResponse = Invoke-RestMethod -Uri $loginUrl -Method POST -Body $loginData -ContentType "application/json"
    Write-Host "Login successful!"
    Write-Host "Token: $($loginResponse.Token)"
    Write-Host "Role: $($loginResponse.Role)"
} catch {
    Write-Host "Login failed: $($_.Exception.Message)"
    Write-Host "Response: $($_.Exception.Response)"
}
