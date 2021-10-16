# Testing Multi Env With DevAttic's ConfigCrypter

I have encountered a seeming issue where using [DevAttic's ConfigCrypter](https://github.com/devattic/ConfigCrypter) nullifies the loading of {env}-based appsettings files. I've opened [an issue on DevAttic's GitHub](https://github.com/devattic/ConfigCrypter/issues/2), so feel free to check there in case someone points out my error and I haven't updated here yet. :0)

## History

This was encountered first in a production application where I had NOT been using {env}-based appsettings files when I added ConfigCrypter to that project. I later decided to make use of the feature to make my life easier vs. the code gyrations needed to check environments and pull a different value in the sole appsettings file based on that value. I found it was not using the appsettings {env}-specific files and assumed (still do) it was something I'd misconfigured so created a fresh project for testing.
 
## Steps which Reproduce the Symptoms

  1. Using Visual Studio 2019, create a C# ASPNET Core 5.0 project.
  2. Add non-encrypted keys to appsettings.{env}.json for both parent (production) and development json files.
  3. Add a "fake prod" option to lauchSettings.json
  4. Test pulling of appropriate values using both "fake prod" and "development" launches. (__*for me, everything displayed as expected*__)
  5. Install DevAttic's ConfigCrypter via NuGet and:
    * Create a pfx file.
    * Encrypt a test key in both appsettings.{env}.json files.
    * Add configuration to Program.cs.
  6. Test pulling of appropriate values using both "fake prod" and "development" launches. (__*for me, everything displayed as expected for "fake prod" launch, however "development" launch continues to display production values for all values, encrypted or not*__)

## If you want to use this project to test:
   * un/comment the Program.cs configuration portion, 
   * de/encrypt keys accordingly, and 
   * change the Index.cshtml encryptionStateText variable to ON or OFF as appropriate (this last is just a visual indicator, does not impact functionality in the least).

### Commands

 Provided you're working in the same folder as both the .pfx and appsettings.{env}.json files ... 
 
#### Encrypt

config-crypter encrypt -p forConfig.pfx -f appsettings.Development.json -k "EncryptedKeys.TestPhrase2" -r

#### Decrypt

config-crypter decrypt -p forConfig.pfx -f appsettings.Development.json -k "EncryptedKeys.TestPhrase2" -r

## My Test Results
![Test Results](/VS_C%23_CORE5/wwwroot/assets/images/example.png?raw=true)

~~~~~
Also, might or might not be related, but I have found that if I'm doing entity framework-based database updates, I have to decrypt the connection string (and comment out that associated line in program.cs) in order for the database update to process through. Otherwise it errors about "Format of the initialization string does not conform to specification".  The update the can be successfully processed, then I uncomment and re-encrypt aftterward.
