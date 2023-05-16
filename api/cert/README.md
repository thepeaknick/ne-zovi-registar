
links:
https://www.c-sharpcorner.com/article/using-certificates-for-api-authentication-in-net-5/
https://damienbod.com/2021/11/22/implement-certificate-authentication-in-asp-net-core-for-an-azure-b2c-api-connector/
https://learn.microsoft.com/en-us/powershell/module/pki/new-selfsignedcertificate?view=windowsserver2022-ps

Open PowerShell as admin
1. 
CertAuth:
New-SelfSignedCertificate -DnsName "localhost", "localhost" -CertStoreLocation "cert:\LocalMachine\My" -NotAfter (Get-Date).AddYears(10) -FriendlyName "CAlocalhost" -KeyUsageProperty All -KeyUsage CertSign, CRLSign, DigitalSignature
2. create pswd
$mypwd = ConvertTo-SecureString -String "test123" -Force -AsPlainText
3. create .pfx file
 Get-ChildItem -Path cert:\localMachine\my\58CA034B3F2F34D6366CA58344D44CF027A88F30 | Export-PfxCertificate -FilePath "d:\Projects\nezovireg.git\cert\cacert.pfx" -Password $mypwd
4. create roocert variable 
 $rootcert = ( Get-ChildItem -Path cert:\LocalMachine\My\58CA034B3F2F34D6366CA58344D44CF027A88F30 )
5. create client cert
 New-SelfSignedCertificate -certstorelocation cert:\localmachine\my -dnsname "localhost" -Signer $rootcert -NotAfter (Get-Date).AddYears(10) -FriendlyName "Clientlocalhost"      
6. create .pfx file
 Get-ChildItem -Path cert:\localMachine\my\FC2A6F7D627E08FDAB50F194FEC535C7E21824C3 | Export-PfxCertificate -FilePath "d:\Projects\nezovireg.git\cert\clientcert.pfx" -Password $mypwd
 
7. add CertAuth to the cert store

Trusted Root Certification Authorities- Certificates – All Tasks- Import” and add the “cacert.pfx”